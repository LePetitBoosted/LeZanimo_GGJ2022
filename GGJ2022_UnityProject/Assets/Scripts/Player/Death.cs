using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    DataManager dataManager;

    bool isDead;

    [SerializeField] GameObject normalState;
    [SerializeField] GameObject dashState;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 && isDead == false)
        {
            Die();
        }
    }

    void Die() 
    {
        isDead = true;

        GetComponent<PlayerControls>().hasInput = false;
        GetComponent<PlayerControls>().StopAllCoroutines();
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        normalState.GetComponent<BoxCollider2D>().enabled = false;
        if(dashState.activeSelf == true) 
        {
            dashState.SetActive(false);
        }

        if(GetComponentInChildren<PlayerBall>() != null) 
        {
            GetComponentInChildren<PlayerBall>().LooseBallOnDeath();
        }
        GetComponentInChildren<Animator>().SetTrigger("Death");

        StartCoroutine(DelayDepop());
    }

    IEnumerator DelayDepop() 
    {
        yield return new WaitForSeconds(dataManager.delayToDepop);
        normalState.SetActive(false);

        yield return new WaitForSeconds(dataManager.delayToRespawn);
        Respawn();
    }

    void Respawn() 
    {
        int randomIndex = Random.Range(0, dataManager.spawnPoints.Count);
        transform.position = dataManager.spawnPoints[randomIndex].position;
        
        normalState.SetActive(true);
        GetComponentInChildren<Animator>().SetTrigger("Respawn");

        StartCoroutine(RetrieveInputs());
    }

    IEnumerator RetrieveInputs() 
    {
        yield return new WaitForSeconds(dataManager.delayForInputs);
        normalState.GetComponent<BoxCollider2D>().enabled = true;
        GetComponent<Rigidbody2D>().gravityScale = dataManager.gravityScale;
        GetComponentInChildren<Animator>().SetTrigger("Reset");
        GetComponent<PlayerControls>().RetrieveInputs(0f);
        GetComponent<PlayerControls>().dashAvailable = true;

        isDead = false;
    }
}
