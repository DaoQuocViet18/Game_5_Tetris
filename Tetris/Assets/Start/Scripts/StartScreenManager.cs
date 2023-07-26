using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Chuyển đến Scene chơi game (SampleScene)
    }

    public void QuitGame()
    {
            // Kiểm tra xem ứng dụng đang chạy trong trình duyệt hay không.
    #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
    #else
                                    Application.Quit();
    #endif
    }
}
