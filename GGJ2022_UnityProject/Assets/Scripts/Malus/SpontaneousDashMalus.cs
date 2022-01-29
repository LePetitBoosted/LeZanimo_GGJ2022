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
        StartCoroutine(WaitAndDash());
    }

    IEnumerator WaitAndDash ()
    {
        float intervalTime = Random.Range(minInterval, maxInterval);

        yield return new WaitForSeconds(intervalTime);

        malusManager.targetPlayer.GetComponent<PlayerControls>().CalculateDash();

        StartCoroutine(WaitAndDash());
    }

    private void OnDisable()
    {
    }
}
