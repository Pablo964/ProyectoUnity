﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Transform> wayPoints;
    private byte nextPosition;
    [SerializeField] float speed = 1f;
    [SerializeField] BoxCollider2D enemyAtackCollider;
    public static bool mirandoIzq = false;

    private float changeDistance = 0.2f;


    [SerializeField] Transform target;

    void Start()
    {
        nextPosition = 0;
    }

    void Update()
    {
        if ((Vector3.Distance(transform.position, target.position) < 6 && 
            Vector3.Distance(transform.position, target.position) > 0))
        {
            if (transform.position.x > target.position.x)
            {
                transform.eulerAngles = new Vector2(0, 0);
                mirandoIzq = true;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                mirandoIzq = false;
            }
            transform.position = Vector2.MoveTowards(transform.position,
            new Vector2(target.position.x, -1.77f), speed * Time.deltaTime);
        }
        else if(Vector3.Distance(transform.position, target.position) >= 6)
        {
            if (transform.position.x > wayPoints[nextPosition].transform.position.x)
            {
                transform.eulerAngles = new Vector2(0, 0);
                mirandoIzq = false;
            }
            else
            {
                transform.eulerAngles = new Vector2(0, 180);
                mirandoIzq = false;
            }
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
            enemyAtackCollider.enabled = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "player")
        {
            if (enemyAtackCollider.enabled)
            {
                //FindObjectOfType<Player>().SendMessage("Recolocar");
            }
        }
    }
}
