using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform player;

    [SerializeField] private TransformData playerData;

    [Header("Game Data")]
    [SerializeField] private int coins = 0;

    private void Awake()
    {
        FindPlayer();
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
        // Sengaja dikosongkan
        // Jangan auto LoadGame()
        // supaya MainMenu tidak langsung pindah scene
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }

    private void OnSceneLoaded(
        Scene scene,
        LoadSceneMode mode)
    {
        FindPlayer();
    }

    private void FindPlayer()
    {
        GameObject playerObj =
            GameObject.FindGameObjectWithTag(
                "Player"
            );

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    }

    #region Save System

    public void SaveGame()
    {
        if (player == null)
        {
            FindPlayer();

            if (player == null)
            {
                Debug.LogError(
                    "Player reference missing!"
                );

                return;
            }
        }

        if (playerData == null)
        {
            Debug.LogError(
                "TransformData belum diassign!"
            );

            return;
        }

        PlayerPrefs.SetFloat(
            "PlayerX",
            player.position.x
        );

        PlayerPrefs.SetFloat(
            "PlayerY",
            player.position.y
        );

        PlayerPrefs.SetInt(
            "Coins",
            coins
        );

        PlayerPrefs.SetString(
            "SceneName",
            SceneManager
                .GetActiveScene()
                .name
        );

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        if (playerData == null)
        {
            Debug.LogError(
                "TransformData belum diassign!"
            );

            return;
        }

        string sceneName =
            PlayerPrefs.GetString(
                "SceneName",
                ""
            );

        if (sceneName != "" &&
            sceneName !=
            SceneManager
                .GetActiveScene()
                .name)
        {
            SceneManager.LoadScene(
                sceneName
            );

            return;
        }

        if (player == null)
        {
            FindPlayer();

            if (player == null)
            {
                Debug.LogWarning(
                    "Player belum ditemukan."
                );

                return;
            }
        }

        float x =
            PlayerPrefs.GetFloat(
                "PlayerX",
                player.position.x
            );

        float y =
            PlayerPrefs.GetFloat(
                "PlayerY",
                player.position.y
            );

        Vector2 loadedPosition =
            new Vector2(x, y);

        playerData.SetPosition(
            loadedPosition
        );

        player.position =
            loadedPosition;

        coins =
            PlayerPrefs.GetInt(
                "Coins",
                0
            );

        Debug.Log("Game Loaded");
    }

    public void ResetSave()
    {
        PlayerPrefs.DeleteAll();

        PlayerPrefs.Save();

        if (playerData != null)
        {
            playerData.ResetData();
        }

        coins = 0;

        Debug.Log("Save Reset");
    }

    #endregion
}