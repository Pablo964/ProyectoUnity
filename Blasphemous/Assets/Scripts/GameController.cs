using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private int vida;
    void Start()
    {
        vida = 3;
    }

    void Update()
    {
        
    }

    public void PerderVida()
    {
        vida--;
        Debug.Log("Te queda: " + vida+" de vida");
        if (vida <= 0)
        {
            // TO DO: Volver al menú principal
            Debug.Log("Partida terminada");
            Application.Quit();
        }
    }
}
