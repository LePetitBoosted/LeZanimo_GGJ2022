using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MalusManager : MonoBehaviour
{
    DataSaver dataSaver;

    bool allStar;

    GameManager gameManager;

    [SerializeField] List<GameObject> malusList = new List<GameObject>();
    List<GameObject> possibleMalus = new List<GameObject>();
    int currentMalusIndex;

    public GameObject targetPlayer;

    [Header("UI")]
    [SerializeField] GameObject currentMalusUI;
    [SerializeField] GameObject firstTransformUI;
    [SerializeField] GameObject redTransformUI;
    [SerializeField] GameObject greenTransformUI;
    bool shouldLerp;
    float timeElapsed;
    [SerializeField] float lerpDuration;

    private void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        if(FindObjectOfType<DataSaver>() != null) 
        {
            dataSaver = FindObjectOfType<DataSaver>();
            allStar = dataSaver.allStar;
        }
    }

    public void GiveMalus(GameObject player) 
    {
        targetPlayer = player;
        possibleMalus.Clear();
        ChooseMalus();

        DisplayMalus();
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
        if (allStar == false)
        {
            possibleMalus[currentMalusIndex].SetActive(false);
        }
        else 
        {
            malusList[currentMalusIndex].SetActive(false);
        }

        if (currentMalusUI != null)
        {
            currentMalusUI.SetActive(false);
        }
        shouldLerp = false;
        timeElapsed = 0;
        StopCoroutine(WaitForUI());
    }

    void DisplayMalus() 
    {
        currentMalusUI.GetComponent<Image>().sprite = GetComponentInChildren<MalusBase>().malusIcon;
        currentMalusUI.GetComponent<RectTransform>().anchoredPosition = firstTransformUI.GetComponent<RectTransform>().anchoredPosition;
        currentMalusUI.GetComponent<RectTransform>().localScale = firstTransformUI.GetComponent<RectTransform>().localScale;
        currentMalusUI.SetActive(true);
        StartCoroutine(WaitForUI());
    }

    IEnumerator WaitForUI()
    {
        yield return new WaitForSeconds(1f);
        shouldLerp = true;
    }

    private void Update()
    {
        if (shouldLerp == true)
        {
            if (timeElapsed < lerpDuration)
            {
                if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
                {
                    currentMalusUI.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(firstTransformUI.GetComponent<RectTransform>().anchoredPosition, redTransformUI.GetComponent<RectTransform>().anchoredPosition, timeElapsed / lerpDuration);
                    currentMalusUI.GetComponent<RectTransform>().localScale = Vector2.Lerp(firstTransformUI.GetComponent<RectTransform>().localScale, redTransformUI.GetComponent<RectTransform>().localScale, timeElapsed / lerpDuration);
                }
                else
                {
                    currentMalusUI.GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(firstTransformUI.GetComponent<RectTransform>().anchoredPosition, greenTransformUI.GetComponent<RectTransform>().anchoredPosition, timeElapsed / lerpDuration);
                    currentMalusUI.GetComponent<RectTransform>().localScale = Vector2.Lerp(firstTransformUI.GetComponent<RectTransform>().localScale, greenTransformUI.GetComponent<RectTransform>().localScale, timeElapsed / lerpDuration);
                }
                timeElapsed += Time.deltaTime;
            }
            else
            {
                if (targetPlayer.GetComponent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
                {
                    currentMalusUI.GetComponent<RectTransform>().anchoredPosition = redTransformUI.GetComponent<RectTransform>().anchoredPosition;
                    currentMalusUI.GetComponent<RectTransform>().localScale = redTransformUI.GetComponent<RectTransform>().localScale;
                }
                else
                {
                    currentMalusUI.GetComponent<RectTransform>().anchoredPosition = greenTransformUI.GetComponent<RectTransform>().anchoredPosition;
                    currentMalusUI.GetComponent<RectTransform>().localScale = greenTransformUI.GetComponent<RectTransform>().localScale;
                }
            }
        }
    }
}
