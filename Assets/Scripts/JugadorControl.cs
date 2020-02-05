using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JugadorControl : MonoBehaviour
{
    public Rigidbody rigidCentro;

    void Start()
    {
       
        
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Limite")
        {
            Debug.Log("jugador salio del limite");
            TirarJugador();
        }
        if(other.transform.name == "triggerHelicoptero")
        {
            Debug.Log("Jugador entro a trigger helicoptero");
        }
    }


    public void TirarJugador()
    {
        rigidCentro.isKinematic = false;
    }

}
