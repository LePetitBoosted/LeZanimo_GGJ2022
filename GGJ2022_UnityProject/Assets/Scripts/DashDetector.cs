using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDetector : MonoBehaviour
{
    DataManager dataManager;
    float ejectForce;

    GameObject otherPlayer;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
    }

    private void OnEnable()
    {
        ejectForce = dataManager.maxEjectForce;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }

    private void Update()
    {
        float alpha = Time.deltaTime / dataManager.dashDuration;
        ejectForce -= alpha * (dataManager.maxEjectForce - dataManager.minEjectForce);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3) 
        {
            otherPlayer = collision.gameObject;

            otherPlayer.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            otherPlayer.GetComponent<PlayerControls>().hasInput = false;

            Vector2 ejectDirection = (otherPlayer.transform.position - transform.position) + new Vector3(0, 0.5f, 0);
            otherPlayer.GetComponent<Rigidbody2D>().AddForce((ejectDirection * ejectForce), ForceMode2D.Impulse);

            Time.timeScale = dataManager.slowMotionStrenght;
            StartCoroutine(StopSlowMotion());

            otherPlayer.GetComponent<PlayerControls>().RetrieveInputs(.5f);
        }
    }

    IEnumerator StopSlowMotion() 
    {
        yield return new WaitForSeconds(dataManager.slowMotionDuration * Time.timeScale);
        Time.timeScale = 1f;
    }
}
