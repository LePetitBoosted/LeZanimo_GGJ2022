using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpontaneousDashMalus : MonoBehaviour
{
    [SerializeField] MalusManager malusManager;
    [SerializeField] float minInterval = 2f;
    [SerializeField] float maxInterval = 5f;


    private void OnEnable()
    {
        Debug.Log("Dash auto activé");

   

        StartCoroutine(WaitAndDash());
    }

    IEnumerator WaitAndDash ()
    {
        float intervalTime = Random.Range(minInterval, maxInterval);

        yield return new WaitForSeconds(intervalTime);

        //lance le dash                      nécessite de rework un peu le playercontrol
        //malusManager.targetPlayer.Dash()

        Debug.Log("Dash");

        StartCoroutine(WaitAndDash());
    }

    private void OnDisable()
    {
        Debug.Log("Dash Auto fin");
    }
}
