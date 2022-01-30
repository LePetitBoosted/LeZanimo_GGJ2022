using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTitle : MonoBehaviour
{
    [SerializeField] GameObject startGameCanvas;
    private void Update()
    {
        if (Input.GetButtonDown("Pause")) 
        {
            startGameCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
