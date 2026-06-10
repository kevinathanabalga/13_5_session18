using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [Header("Scene Index")]
    [SerializeField] private int sceneIndex = 0;

    [Header("Gameplay Buttons")]
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonMenu;

    private void Start()
    {
        if (buttonMenu != null)
        {
            buttonMenu.onClick.AddListener(() =>
            {
                GameManager.Instance.ChangeScene(sceneIndex);
            });
        }

        if (buttonPause != null)
        {
            buttonPause.onClick.AddListener(HandlePauseResume);
        }

        if (buttonResume != null)
        {
            buttonResume.onClick.AddListener(HandlePauseResume);
        }

        UpdatePauseButtons();
    }

    private void HandlePauseResume()
    {
        if (GameManager.Instance.isPaused)
        {
            GameManager.Instance.Resume();
        }
        else
        {
            GameManager.Instance.Pause();
        }

        UpdatePauseButtons();
    }

    private void UpdatePauseButtons()
    {
        if (buttonPause != null)
        {
            buttonPause.gameObject.SetActive(!GameManager.Instance.isPaused);
        }

        if (buttonResume != null)
        {
            buttonResume.gameObject.SetActive(GameManager.Instance.isPaused);
        }
    }

    private void OnDestroy()
    {
        if (buttonMenu != null)
        {
            buttonMenu.onClick.RemoveAllListeners();
        }

        if (buttonPause != null)
        {
            buttonPause.onClick.RemoveAllListeners();
        }

        if (buttonResume != null)
        {
            buttonResume.onClick.RemoveAllListeners();
        }
    }
}