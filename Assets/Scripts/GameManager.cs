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

    public int CurrentScore => score;
    public int CurrentHighScore => highScore;

    public bool isPaused { get; private set; }

    private void Awake()
    {
        // Singleton + Persistent Across Scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        FindUIReferences();

        LoadHighScore();

        UpdateScoreUI();

        UpdateHighScoreUI();
    }

    private void OnSceneLoaded(
        Scene scene,
        LoadSceneMode mode)
    {
        FindUIReferences();

        UpdateScoreUI();

        UpdateHighScoreUI();
    }

    private void FindUIReferences()
    {
        GameObject scoreObj =
            GameObject.Find("ScoreText");

        if (scoreObj != null)
        {
            scoreText =
                scoreObj.GetComponent<TextMeshProUGUI>();
        }

        GameObject highScoreObj =
            GameObject.Find("HighScoreText");

        if (highScoreObj != null)
        {
            highScoreText =
                highScoreObj.GetComponent<TextMeshProUGUI>();
        }
    }

    #region Score System

    public void AddScore(int points)
    {
        score += points;

        UpdateScoreUI();

        if (score > highScore)
        {
            highScore = score;

            SaveHighScore();

            UpdateHighScoreUI();
        }

        Debug.Log("Current Score: " + score);
    }

    public void ResetScore()
    {
        score = 0;

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text =
                "Score: " + score;
        }
    }

    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
        {
            highScoreText.text =
                "High Score: " + highScore;
        }
    }

    #endregion

    #region Save & Load System

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt(
            "HighScore",
            highScore
        );

        PlayerPrefs.Save();

        Debug.Log(
            "High Score Saved: "
            + highScore
        );
    }

    private void LoadHighScore()
    {
        highScore =
            PlayerPrefs.GetInt(
                "HighScore",
                0
            );

        UpdateHighScoreUI();
    }

    public void ResetSaveData()
    {
        PlayerPrefs.DeleteKey(
            "HighScore"
        );

        PlayerPrefs.Save();

        highScore = 0;

        UpdateHighScoreUI();

        Debug.Log(
            "High Score Reset"
        );
    }

    #endregion

    #region Pause System

    public void Pause()
    {
        if (isPaused)
            return;

        isPaused = true;

        Time.timeScale = 0f;
    }

    public void Resume()
    {
        if (!isPaused)
            return;

        isPaused = false;

        Time.timeScale = 1f;
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    #endregion

    #region Scene Management

    public void ChangeScene(
        int sceneIndex)
    {
        Time.timeScale = 1f;

        isPaused = false;

        SceneManager.LoadScene(
            sceneIndex
        );
    }

    public void RestartCurrentScene()
    {
        Time.timeScale = 1f;

        isPaused = false;

        SceneManager.LoadScene(
            SceneManager
                .GetActiveScene()
                .buildIndex
        );
    }

    #endregion
}