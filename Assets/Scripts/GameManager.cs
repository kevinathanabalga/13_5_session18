using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    private int score = 0;
    private int highScore = 0;

    public bool isPaused { get; private set; }

    private void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        LoadHighScore();

        UpdateScoreUI();
    }

    #region Score System

    public void AddScore(int points)
    {
        score += points;

        UpdateScoreUI();

        // Cek apakah score saat ini melebihi high score
        if (score > highScore)
        {
            highScore = score;

            SaveHighScore();

            UpdateHighScoreUI();
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: " + highScore;
        }
    }

    #endregion

    #region Save & Load System

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        UpdateHighScoreUI();
    }

    public void ResetSaveData()
    {
        PlayerPrefs.DeleteKey("HighScore");

        highScore = 0;

        UpdateHighScoreUI();
    }

    #endregion

    #region Pause System

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

    #endregion

    #region Scene Management

    public void ChangeScene(int sceneIndex)
    {
        Time.timeScale = 1f;
        isPaused = false;

        SceneManager.LoadScene(sceneIndex);
    }

    #endregion
}