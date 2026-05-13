using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public Transform player;

    public int coins = 0;

    void Start()
    {
        LoadGame();
    }

    void Update()
    {
        // SAVE
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        // LOAD
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        // Save Player Position
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);

        // Save Coins
        PlayerPrefs.SetInt("Coins", coins);

        // Save Current Scene
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);

        // Write Save Data
        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        // Load Scene Name
        string sceneName = PlayerPrefs.GetString("SceneName", "");

        // Load Scene if different
        if (sceneName != "" && sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        // Load Position
        float x = PlayerPrefs.GetFloat("PlayerX", player.position.x);
        float y = PlayerPrefs.GetFloat("PlayerY", player.position.y);

        // Move Player
        player.position = new Vector3(x, y, 0);

        // Load Coins
        coins = PlayerPrefs.GetInt("Coins", 0);

        Debug.Log("Game Loaded");
    }
}