using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSaver : MonoBehaviour
{
    public bool allStar;
    public PlayerNumber winner;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
