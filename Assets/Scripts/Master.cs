﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using EasyButtons;

public class Master : MonoBehaviour
{
    [Header("CullingMask")]
    public Camera camaraJugador;
    public LayerMask cullingInicio;
    public LayerMask cullingJuego;
    public Animator fadeEsfera;
    [Header("Tiempos")]
    public float tiempoLimite;
    public float tiempo;
    public bool empezarConteo;
    [Space(10)]
    [Header("UIX")]
    public TMP_Text tiempo_texto;
    public GameObject panelCamaraB;

    void Start()
    {
        camaraJugador.cullingMask = cullingInicio;

        tiempo = tiempoLimite;
        tiempo_texto.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));
    }

    // Update is called once per frame
    void Update()
    {

        if(empezarConteo)
        {
            ConteoTiempo();
        }


    }

    [Button]
    public void IniciarJuego()
    {
        Debug.Log("Se Inicio el Juego");

        empezarConteo = true;
        camaraJugador.cullingMask = cullingJuego;
        fadeEsfera.SetTrigger("fadeOut");

    }
    [Button]
    public void ReiniciarJuego()
    {
        Debug.Log("Reiniciando Juego");

        SceneManager.LoadScene(0);
    }

    [Button]
    public void CamaraB()
    {
        panelCamaraB.SetActive(!panelCamaraB.activeInHierarchy);
    }

    void ConteoTiempo()
    {
        tiempo -= Time.deltaTime;

        tiempo_texto.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));// % deja el restante y / sale cuanto tuvo que dividires(arriba de la casita)

        if(tiempo <= 0.0)
        {
            Debug.Log("Se termino el tiempo");

            empezarConteo = false;
            FinJuego();
        }
    }

    public void FinJuego()
    {
        Debug.Log("Se termino el juego");
    }
}
