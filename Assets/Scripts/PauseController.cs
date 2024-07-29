using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    float previousTimeScale = 1;
    public Text pauseText;
    public static bool gameIsPaused;

    [SerializeField]
    GameObject pauseUICanvas;

    [SerializeField]
    GameObject portalCrosshairCanvas;

    [SerializeField]
    GameObject scalingUICanvas;

    private void Start()
    {
        pauseUICanvas.SetActive(false);
        portalCrosshairCanvas.SetActive(true);
        scalingUICanvas.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        if (Time.timeScale > 0)
        {
            //Pause
            previousTimeScale = Time.timeScale;
            Time.timeScale = 0;
            AudioListener.pause = true;
            pauseUICanvas.SetActive(true);
            portalCrosshairCanvas.SetActive(false);
            scalingUICanvas.SetActive(false);
            gameIsPaused = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else if (Time.timeScale == 0)
        {
            //Unpause
            Time.timeScale = previousTimeScale;
            AudioListener.pause = false;
            pauseUICanvas.SetActive(false);
            portalCrosshairCanvas.SetActive(true);
            scalingUICanvas.SetActive(true);
            gameIsPaused = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        //reloads current scene in unity. to help stuck players
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
