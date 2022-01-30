using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class DashDetector : MonoBehaviour
{
    DataManager dataManager;
    float ejectForce;

    GameObject otherPlayer;

    Vector3 cameraOriginPosition;

    private void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
        cameraOriginPosition = Camera.main.transform.position;
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
            
            if (otherPlayer.GetComponentInChildren<PlayerBall>() != null)
            {
                otherPlayer.GetComponentInChildren<PlayerBall>().LooseBall();
            }

            Vector2 ejectDirection = (otherPlayer.transform.position - transform.position) + new Vector3(0, 0.5f, 0);
            otherPlayer.GetComponent<Rigidbody2D>().AddForce((ejectDirection * ejectForce), ForceMode2D.Impulse);


            if (otherPlayer.GetComponentInChildren<DashDetector>() == null)
            {
                SetVibrationOnSelf(0.4f);
                Time.timeScale = dataManager.slowMotionStrenght;
                StartCoroutine(StopSlowMotion());
            }
            else 
            {
                SetVibrationOnSelf(0.6f);
                Time.timeScale = dataManager.bigSlowMotionStrenght;
                StartCoroutine(BigSlowMotionWithZoom());
            }

            otherPlayer.GetComponent<PlayerControls>().RetrieveInputs(.5f);
        }
    }

    IEnumerator StopSlowMotion() 
    {
        yield return new WaitForSeconds(dataManager.slowMotionDuration * Time.timeScale);
        SetVibrationOnSelf(0f);
        Time.timeScale = 1f;
    }

    IEnumerator BigSlowMotionWithZoom()
    {
        Vector3 focusPoint = otherPlayer.transform.position;

        ///LE CLAMP MARCHE PAS DONC JE FAIS CA POUR L'INSTANT\\\
        if (focusPoint.x > 18 * (dataManager.shockZoomStrenght/10f))
        {
            focusPoint.x = 18 * (dataManager.shockZoomStrenght / 10f);
        }
        else if (focusPoint.x < -18 * (dataManager.shockZoomStrenght / 10f))
        {
            focusPoint.x = -18 * (dataManager.shockZoomStrenght / 10f);
        }
        
        if (focusPoint.y > 10 * (dataManager.shockZoomStrenght / 10f))
        {
            focusPoint.y = 10 * (dataManager.shockZoomStrenght / 10f);
        }
        else if (focusPoint.y < -10 * (dataManager.shockZoomStrenght / 10f))
        {
            focusPoint.y = -10 * (dataManager.shockZoomStrenght / 10f);
        }


        //Mathf.Clamp(focusPoint.x, -18 * (dataManager.shockZoomStrenght / 10f), 18 * (dataManager.shockZoomStrenght / 10f));
        //Mathf.Clamp(focusPoint.y, -10 * (dataManager.shockZoomStrenght / 10f), 10 * (dataManager.shockZoomStrenght / 10f));

        //Camera.main.orthographicSize = 10f - dataManager.shockZoomStrenght;
        //Camera.main.transform.position = new Vector3(focusPoint.x, focusPoint.y, -10);

        for (int i=0; i<=10; i++)
        {
            Camera.main.orthographicSize = Mathf.Lerp(10, 10f - dataManager.shockZoomStrenght, i/10f);
            Camera.main.transform.position = new Vector3(Mathf.Lerp(0f,focusPoint.x,i/10f), Mathf.Lerp(0f, focusPoint.y, i / 10f), -10);

            yield return new WaitForSeconds(0.01f / 10f);
        }

        yield return new WaitForSeconds(dataManager.slowMotionDuration * Time.timeScale);
        SetVibrationOnSelf(0f);


        for (int i = 0; i <= 10; i++)
        {
            Camera.main.orthographicSize = Mathf.Lerp(10f - dataManager.shockZoomStrenght, 10f, i / 10f);
            Camera.main.transform.position = new Vector3(Mathf.Lerp(focusPoint.x, 0f, i / 10f), Mathf.Lerp(focusPoint.y, 0f, i / 10f), -10);

            yield return new WaitForSeconds(0.01f / 10f);
        }

        

        Camera.main.orthographicSize = 10f;
        Camera.main.transform.position = cameraOriginPosition;
        Time.timeScale = 1f;

    }

    void SetVibrationOnSelf(float intensity)
    {
        intensity = Mathf.Clamp01(intensity);
        
        if (GetComponentInParent<PlayerControls>().playerNumber == PlayerNumber.PlayerOne)
        {
            GamePad.SetVibration(PlayerIndex.One, intensity, intensity);
        }
        else
        {
            GamePad.SetVibration(PlayerIndex.Two, intensity, intensity);
        }
    }
}
