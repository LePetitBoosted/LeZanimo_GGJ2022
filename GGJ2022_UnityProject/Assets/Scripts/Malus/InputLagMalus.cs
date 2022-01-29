using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputLagMalus : MonoBehaviour
{
    GameObject target;

    private void OnEnable()
    {
        StartCoroutine(SetInputLag());
    }

    private void OnDisable()
    {
        target.GetComponent<PlayerControls>().inputLag = 0f;
    }

    IEnumerator SetInputLag()
    {
        yield return new WaitForEndOfFrame();
        target = transform.parent.GetComponent<MalusManager>().targetPlayer;
        target.GetComponent<PlayerControls>().inputLag = 0.4f;
    }
}
