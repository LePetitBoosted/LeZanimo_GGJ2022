using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] windowsSounds;
    public AudioClip[] secretSounds;
    public AudioClip[] currentSounds;

    // Start is called before the first frame update
    void Start()
    {
        currentSounds = new AudioClip[6];

        for (int i = 0; i <= 5; i++)
        {
            currentSounds[i] = windowsSounds[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        Secretironni();
    }

    void Secretironni()
    {
        if (Input.GetKey(KeyCode.P))
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("Secretironni");
                for (int i = 0; i <= 4; i++)
                {
                    currentSounds[i] = secretSounds[i];
                }
            }
        }
    }
}
