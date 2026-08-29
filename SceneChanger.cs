using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;
    public Animator fadeAnim;
    public float fadeTime = .5f;
    public Vector2 newPlayerPosition;
    private Transform player;

    // SAKLAR PENANDA: True jika player pindah lewat portal ini
    public static bool isTeleporting = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.transform;

            // Tandai bahwa perpindahan ini berasal dari portal
            isTeleporting = true;

            if (fadeAnim != null)
            {
                fadeAnim.Play("FadeWhite");
            }

            StartCoroutine(DelayFade());
        }
    }

    IEnumerator DelayFade()
    {
        yield return new WaitForSeconds(fadeTime);

        // Memindahkan posisi player ke newPlayerPosition sesuai pengaturan Inspector
        player.position = newPlayerPosition;
        SceneManager.LoadScene(sceneToLoad);
    }

    // Fungsi untuk Tombol Start Game di UI
    public void ChangeSceneNow()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    // --- TAMBAHAN UNTUK TOMBOL EXIT GAME ---
    public void QuitGame()
    {
        Debug.Log("Game ditutup!"); // Memastikan fungsi berjalan di Console

        Application.Quit(); // Menutup game saat sudah di-build (.exe)

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Menghentikan Play Mode saat tes di Unity Editor
#endif
    }
}