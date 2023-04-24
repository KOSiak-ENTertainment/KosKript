using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButtonScript : MonoBehaviour
{
    public Button startGameButton;

    void Start()
    {
        startGameButton.onClick.AddListener(PlayGame);

    }

    void PlayGame()
    {
        SceneManager.LoadScene("Scenes/MainScene");
    }
}
