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
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
    }

    public void SaveGame()
    {
        PlayerPrefs.SetFloat("PlayerX", player.position.x);
        PlayerPrefs.SetFloat("PlayerY", player.position.y);

        PlayerPrefs.SetInt("Coins", coins);

        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);

        PlayerPrefs.Save();

        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        string sceneName = PlayerPrefs.GetString("SceneName", "");

        if (sceneName != "" && sceneName != SceneManager.GetActiveScene().name)
        {
            SceneManager.LoadScene(sceneName);
            return;
        }

        float x = PlayerPrefs.GetFloat("PlayerX", player.position.x);
        float y = PlayerPrefs.GetFloat("PlayerY", player.position.y);

        player.position = new Vector3(x, y, 0);

        coins = PlayerPrefs.GetInt("Coins", 0);

        Debug.Log("Game Loaded");
    }
}