using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int velocidad;
    [SerializeField] int velocidadSalto = 20;
    [SerializeField] GameObject enemy;
    [SerializeField] CircleCollider2D atackCollider;

    float xInicial, yInicial, alturaPersonaje;
    bool saltando;
    private Animator anim;
    void Start()
    {
        xInicial = transform.position.x;
        yInicial = transform.position.y;
        saltando = false;
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y;
        anim = gameObject.GetComponent<Animator>();
        atackCollider.enabled = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        transform.Translate(horizontal * velocidad * Time.deltaTime, 0, 0);

        float salto = Input.GetAxis("Jump");
        if (salto > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
            new Vector2(0, -1));
            if (hit.collider != null)
            {
                float distanciaAlSuelo = hit.distance;
                bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;

                if (tocandoElSuelo)
                {
                    Vector3 fuerzaSalto = new Vector3(0, velocidadSalto, 0);
                    GetComponent<Rigidbody2D>().AddForce(fuerzaSalto);
                }
            }
        }

        if(Input.GetButtonDown("Fire1") && !atackCollider.enabled)
        {
            anim.Play("golpear");
        }
    }

    public void Recolocar()
    {
        transform.position = new Vector3(xInicial, yInicial, 0);
    }
    public void ActivarColliderAtaque()
    {
        atackCollider.enabled = true;
    }
    public void TerminarAnimacion()
    {
        atackCollider.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "enemy")
        {
            if (atackCollider.enabled)
            {
                Destroy(enemy);
            }
            else
            {
                Debug.Log("Tocado");
                FindObjectOfType<GameController>().SendMessage("PerderVida");
            }
        }
    }
}
