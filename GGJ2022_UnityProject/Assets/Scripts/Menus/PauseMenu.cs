using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject elements;
    [SerializeField] GameObject playerOne;
    [SerializeField] GameObject playerTwo;
    [SerializeField] VibrationManager vibrationManager;

    bool isPaused;

    float previousTimeScale;

    private void Update()
    {
       if (Input.GetButtonDown("Pause") && isPaused == false)
       {
            previousTimeScale = Time.timeScale;
            Pause();
       }
       else if (Input.GetButtonDown("Pause") && isPaused == true)
       {
            Unpause();
       }
    }

    public void Pause() 
    {
        isPaused = true;
        Time.timeScale = 0f;
        elements.SetActive(true);
        playerOne.GetComponent<PlayerControls>().hasInput = false;
        playerTwo.GetComponent<PlayerControls>().hasInput = false;
    }

    public void Unpause() 
    {
        isPaused = false;
        elements.SetActive(false);
        Time.timeScale = previousTimeScale;
        playerOne.GetComponent<PlayerControls>().RetrieveInputs(.05f);
        playerTwo.GetComponent<PlayerControls>().RetrieveInputs(.05f);
    }

    public void Restart() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SN_Main");
    }

    public void MainMenu() 
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("SN_Menu");
    }

    public void rumbleToggle(bool isOn) 
    {
        vibrationManager.invertedControllerRumble = isOn;
    }
}
