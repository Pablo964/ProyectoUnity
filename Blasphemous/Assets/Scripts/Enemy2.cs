using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] CircleCollider2D ataque;
    Animator anim;

    void Start()
    {
        ataque.enabled = false;
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if ((Vector3.Distance(transform.position, target.position) < 2))
        {
            anim.Play("ataqueEnemigo2");
        }
    }

    void ActivarAtaque()
    {
        ataque.enabled = true;
    }
    void DesactivarAtaque()
    {
        ataque.enabled = false;
    }
    
}
