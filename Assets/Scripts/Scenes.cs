using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes
{
    public void ReastartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
}