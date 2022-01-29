using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalusManager : MonoBehaviour
{
    [SerializeField] GameObject PlayerOne;
    [SerializeField] GameObject PlayerTwo;

    [SerializeField] List<GameObject> malusList = new List<GameObject>();
    int currentMalusIndex;

    public GameObject targetPlayer;

    public void GiveMalus(GameObject player) 
    {
        ChooseMalus();
        malusList[currentMalusIndex].SetActive(true);
        targetPlayer = player;
    }

    void ChooseMalus() 
    {
        currentMalusIndex = Random.Range(0, malusList.Count);
    }

    public void EndMalus() 
    {
        malusList[currentMalusIndex].SetActive(false);
    }
}

