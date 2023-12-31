using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadScene(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Resume()
    {
        Time.timeScale = 1;
        Camera.main.GetComponent<CameraMovement>()._panel.SetActive(false);
    }
}
