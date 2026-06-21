using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeScene(1);
        }
        else
        {
            Debug.LogError(
                "GameManager Instance tidak ditemukan!"
            );
        }
    }

    public void BackToMenu()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeScene(0);
        }
        else
        {
            Debug.LogError(
                "GameManager Instance tidak ditemukan!"
            );
        }
    }

    public void RestartLevel()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartCurrentScene();
        }
        else
        {
            Debug.LogError(
                "GameManager Instance tidak ditemukan!"
            );
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");

        Application.Quit();
    }
}