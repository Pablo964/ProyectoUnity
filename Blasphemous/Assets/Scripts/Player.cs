using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] int velocidad;
    [SerializeField] int velocidadSalto = 20;
    [SerializeField] GameObject enemy;
    [SerializeField] CircleCollider2D atackCollider;
    [SerializeField] Rigidbody2D rigidBodyPlayer;


    float xInicial, yInicial, alturaPersonaje;
    bool saltando;
    private Animator anim;


    bool mirandoIzq;
    void Start()
    {
        xInicial = transform.position.x;
        yInicial = transform.position.y;
        saltando = false;
        alturaPersonaje = GetComponent<Collider2D>().bounds.size.y;
        anim = gameObject.GetComponent<Animator>();
        atackCollider.enabled = false;
        mirandoIzq = false;

    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Debug.Log(saltando);
        if ((horizontal > 0 || horizontal < 0)
           && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("salto")
           && !this.anim.GetCurrentAnimatorStateInfo(0).IsName("golpear"))
        {
            anim.Play("correr");
        }
        if (mirandoIzq)
        {
            transform.Translate(-(horizontal * velocidad * Time.deltaTime), 0, 0);
        }
        else
        transform.Translate((horizontal * velocidad * Time.deltaTime), 0, 0);

        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
            mirandoIzq = true;
        }
        else if(horizontal > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
            mirandoIzq = false;
        }

        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position,
            new Vector2(0, -1));
            if (hit.collider != null)
            {
                float distanciaAlSuelo = hit.distance;
                bool tocandoElSuelo = distanciaAlSuelo < alturaPersonaje;

                if (tocandoElSuelo)
                {
                    saltando = false;
                }
            }
            if (!saltando)
            {
                rigidBodyPlayer.AddForce(new Vector2(0, 4), ForceMode2D.Impulse);
                anim.Play("salto");
                saltando = true;
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
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("Tocado");
                FindObjectOfType<GameController>().SendMessage("PerderVida");
                if (Enemy.mirandoIzq)
                    rigidBodyPlayer.AddForce(new Vector2(-1, 0),
                        ForceMode2D.Impulse);
                else
                rigidBodyPlayer.AddForce(new Vector2(1, 0),
                    ForceMode2D.Impulse);
            }
        }
    }
}
