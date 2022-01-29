using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMalus : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.LogWarning("Malus");
    }

    private void OnDisable()
    {
        Debug.LogWarning("Malus ended");
    }
}
