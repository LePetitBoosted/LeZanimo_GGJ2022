using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangedDirectionsMalus : MonoBehaviour
{
    GameObject target;
    private void OnEnable()
    {
        StartCoroutine(SetChangedDirections());
    }

    private void OnDisable()
    {
        target.GetComponent<PlayerControls>().horizontalStr = "Horizontal P";
        target.GetComponent<PlayerControls>().verticalStr = "Vertical P";
    }

    IEnumerator SetChangedDirections()
    {
        yield return new WaitForEndOfFrame();
        target = transform.parent.GetComponent<MalusManager>().targetPlayer;
        target.GetComponent<PlayerControls>().horizontalStr = "Vertical P";
        target.GetComponent<PlayerControls>().verticalStr = "Horizontal P";
    }
}
