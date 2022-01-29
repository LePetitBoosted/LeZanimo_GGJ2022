using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollbackMalus : MonoBehaviour
{
    List<PointInTime> pointsInTime = new List<PointInTime>();
    GameObject target;

    private void OnEnable()
    {
        StartCoroutine(GetTarget());
        StartCoroutine(Cooldown());
    }

    private void OnDisable()
    {
        pointsInTime.Clear();
    }

    void Rollback() 
    {
        target.transform.position = pointsInTime[pointsInTime.Count - 1].position;
        target.GetComponent<Rigidbody2D>().velocity = pointsInTime[pointsInTime.Count - 1].velocity;
        target.GetComponent<PlayerControls>().isGrounded = pointsInTime[pointsInTime.Count - 1].isGrounded;
        StartCoroutine(Cooldown());
    }

    IEnumerator Cooldown()
    {
        float cooldown = Random.Range(2f, 4f);
        yield return new WaitForSeconds(cooldown);
        Rollback();
    }

    private void FixedUpdate()
    {
        Record();
    }

    void Record() 
    {
        if(pointsInTime.Count > Mathf.Round(1f / Time.deltaTime)) 
        {
            pointsInTime.RemoveAt(pointsInTime.Count - 1);
        }

        pointsInTime.Insert(0, new PointInTime(target.transform.position, target.GetComponent<Rigidbody2D>().velocity, target.GetComponent<PlayerControls>().isGrounded));
    }

    IEnumerator GetTarget() 
    {
        yield return new WaitForEndOfFrame();
        target = transform.parent.GetComponent<MalusManager>().targetPlayer;
    }
}

public class PointInTime 
{
    public Vector3 position;
    public Vector2 velocity;
    public bool isGrounded;

    public PointInTime(Vector3 _position, Vector2 _velocity, bool _isGrounded) 
    {
        position = _position;
        velocity = _velocity;
        isGrounded = _isGrounded;
    }
}
