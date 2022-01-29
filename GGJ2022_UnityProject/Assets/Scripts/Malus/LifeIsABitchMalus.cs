using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeIsABitchMalus : MonoBehaviour
{
    GameObject target;
    private void OnEnable()
    {
        StartCoroutine(SetLifeBitch());
    }

    IEnumerator SetLifeBitch()
    {
        yield return new WaitForEndOfFrame();
        target = transform.parent.GetComponent<MalusManager>().targetPlayer;
        target.GetComponentInChildren<PlayerBall>().hasLifeBitchMalus = true;
    }
}
