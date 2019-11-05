using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    private byte nextPosition;
    [SerializeField] float speed = 2f;
    [SerializeField] BoxCollider2D enemyAtackCollider;

    private float changeDistance = 0.2f;


    [SerializeField] Transform target;

    void Start()
    {
        nextPosition = 0;
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, target.position) < 0)
        {
            transform.Rotate(0, 180, 0);
        }
        else
        {
            transform.Rotate(0, 0, 0);
        }
        if ((Vector3.Distance(transform.position, target.position) < 4 && 
            Vector3.Distance(transform.position, target.position) > 1) || 
            (Vector3.Distance(transform.position, target.position) < -4 &&
            Vector3.Distance(transform.position, target.position) > -1))
        {
            transform.position = Vector2.MoveTowards(transform.position,
            target.position, speed * Time.deltaTime);
        }
        else if(Vector3.Distance(transform.position, target.position) > 4.1)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                wayPoints[nextPosition].transform.position,
                    speed * Time.deltaTime);

            if (Vector3.Distance(transform.position,
                wayPoints[nextPosition].transform.position) < changeDistance)
            {
                nextPosition++;
                if (nextPosition >= wayPoints.Count)
                    nextPosition = 0;
            }
        }
        if (Vector3.Distance(transform.position, target.position) <= 1.1 ||
                Vector3.Distance(transform.position, target.position) <= -1.1)
        {
            //enemyAtackCollider.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            if (enemyAtackCollider.enabled)
            {
                FindObjectOfType<Player>().SendMessage("Recolocar");
            }
        }
    }
}
