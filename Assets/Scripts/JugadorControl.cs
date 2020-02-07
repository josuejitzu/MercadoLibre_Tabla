using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using System;

public class JugadorControl : MonoBehaviour
{
    public static JugadorControl _jugador;

    public Rigidbody rigidCentro;
    public Animator fadeBlanco;
    public Transform posInicial;

    void Start()
    {

        _jugador = this;
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
            Master._master.ActivarHelicoptero();
        }
    }

   

    public async void TirarJugador()
    {
        rigidCentro.isKinematic = false;
        //Tiempo para poner el fade blanco
        fadeBlanco.SetTrigger("fadeIn");
        await Task.Delay(TimeSpan.FromSeconds(2.1f));
        //TODO: reiniciar posicion a la plataforma
        this.rigidCentro.isKinematic = true;
        rigidCentro.transform.position = posInicial.position;
        rigidCentro.transform.rotation = posInicial.rotation;
        Master._master.JugadorCayo();

        await Task.Delay(TimeSpan.FromSeconds(0.5f));
        fadeBlanco.SetTrigger("fadeOut");
        //TODO: Mandar el mensaje a Master

        //TODO: FadeOut blancos
    }

}
