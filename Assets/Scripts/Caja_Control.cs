using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja_Control : MonoBehaviour
{
    public bool manoIzquierda;
    public bool manoDerecha;
    public bool cajaTomada;
    public string nombreZonaLimite = "ZonaLimite_Caja";

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "meta")
        {
            Debug.Log("Caja llego a meta");
            //Master._master.FinJuego();
            Master._master.GanoJuego();
        }

        if(other.transform.name == nombreZonaLimite)
        {
            Master._master.CajaCayo();
        }
    }
}
