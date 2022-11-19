using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void ShowHelp()
    {
        Debug.Log("Help");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }
}
