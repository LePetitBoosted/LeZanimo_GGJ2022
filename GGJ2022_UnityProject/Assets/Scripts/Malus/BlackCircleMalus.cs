using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCircleMalus : MonoBehaviour
{
    GameObject target;
    GameObject blackCircle;
    private void OnEnable()
    {
        StartCoroutine(SetBlackCircle());
    }

    private void OnDisable()
    {
        blackCircle.SetActive(false);
    }

    IEnumerator SetBlackCircle()
    {
        yield return new WaitForEndOfFrame();
        target = transform.parent.GetComponent<MalusManager>().targetPlayer;
        blackCircle = target.transform.Find("BlackCircle").gameObject;
        blackCircle.SetActive(true);
    }
}
