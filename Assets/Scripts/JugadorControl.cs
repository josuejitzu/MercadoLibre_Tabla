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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Limite")
        {
            Debug.Log("jugador salio del limite");
            TirarJugador();
        }
    }


    public void TirarJugador()
    {
        rigidCentro.isKinematic = false;
    }

}
