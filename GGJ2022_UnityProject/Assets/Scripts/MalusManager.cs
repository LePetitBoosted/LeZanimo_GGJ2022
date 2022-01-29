using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MalusManager : MonoBehaviour
{
    bool allStar;

    GameManager gameManager;

    [SerializeField] List<GameObject> malusList = new List<GameObject>();
    int currentMalusIndex;

    public GameObject targetPlayer;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void GiveMalus(GameObject player) 
    {
        targetPlayer = player;
        ChooseMalus();
    }

    void ChooseMalus() 
    {
        if (allStar == false)
        {
            float targetLevel;
            if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
            {
                targetLevel = gameManager.playerOneScore;
            }
            else 
            {
                targetLevel = gameManager.playerTwoScore;
            }

            List<GameObject> possibleMalus = new List<GameObject>();
            foreach(GameObject malus in malusList) 
            {
                if (Enumerable.Range(Mathf.RoundToInt(malus.GetComponent<MalusBase>().malusLevel.x), Mathf.RoundToInt(malus.GetComponent<MalusBase>().malusLevel.y)).Contains(Mathf.RoundToInt(targetLevel))) 
                {
                    possibleMalus.Add(malus);
                }
            }

            currentMalusIndex = Random.Range(0, possibleMalus.Count);
            possibleMalus[currentMalusIndex].SetActive(true);
        }
        else 
        {
            currentMalusIndex = Random.Range(0, malusList.Count);
            malusList[currentMalusIndex].SetActive(true);
        }
    }

    public void EndMalus() 
    {
        malusList[currentMalusIndex].SetActive(false);
    }
}
