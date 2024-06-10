using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject menuObject;

    void Start()
    {
        menuObject.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        menuObject.SetActive(false);
    }

    public void SaveSettings()
    {
        // Add your settings save logic here

        // Close the settings panel and return to the menu
        settingsPanel.SetActive(false);
        menuObject.SetActive(true);
    }
}