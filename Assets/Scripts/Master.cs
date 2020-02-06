﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using EasyButtons;
using System.Threading.Tasks;
using System;

public class Master : MonoBehaviour
{
    public static Master _master;

    [Header("Estado Juego")]
    public bool jugando;
    public bool enTutorial;
    public bool tutorialTerminado;

    public GameObject instruccionesInicio;
    public GameObject contadorInicio;
    public GameObject helicopteroAnim;
    public GameObject zonaMeta;
    public GameObject panelFinal_gano;
    public GameObject panelFinal_perdio;
    public GameObject panelInicio;
    public GameObject limites;
    [Header("CullingMask")]
    public Camera camaraJugador;
    public LayerMask cullingInicio;
    public LayerMask cullingJuego;
    public Animator fadeEsfera;
    [Header("Tiempos")]
    public float tiempoLimite;
    public float tiempo;
    public bool empezarConteo;
    public TMP_Text cronometro_jugador_text;
    [Space(10)]
    [Header("UIX")]
    public TMP_Text tiempo_texto;
    public GameObject panelCamaraB;
    [Space(10)]
    [Header("SFX")]
    public AudioSource afirmacion_sfx;
    public AudioSource trafico_sfx;
    public AudioSource viento_sfx;
    public AudioSource suspenso_sfx;
    public AudioSource heartbeat_sfx;
    public AudioSource musicaLobby_sfx;
    public AudioSource celebracion_sfx;
    public AudioSource beepReloj_sfx;

    [Header("VFX")]
    public ParticleSystem confetti_vfx;
    [Header("PersonasUpdate")]
    public List<PersonasControl> personas = new List<PersonasControl>();

    bool manoDerechaLista, manoizquierdaLista;
    void Start()
    {
        _master = this;

        camaraJugador.cullingMask = cullingInicio;

        tiempo = tiempoLimite;
        tiempo_texto.text = (((Mathf.Floor(tiempo / 60f)) % 60).ToString("00")) + ":" + (Mathf.Floor(tiempo % 60f).ToString("00") + "." + ((tiempo * 100) % 100).ToString("00"));
        cronometro_jugador_text.text = tiempo_texto.text;
        zonaMeta.SetActive(false);
        instruccionesInicio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if(empezarConteo)
        {
            ConteoTiempo();
        }

        if(personas.Count >0)
        {
            foreach(PersonasControl p in personas)
            {
                p.MiUpdate();
            }
        }
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            IniciarTutorial();
        }
    }

    /// <summary>
    /// Este metodo solo puede ser llamado por el operador para empezar desde el tutorial
    /// </summary>
    [Button]
    public async void IniciarTutorial()
    {
        if (jugando || enTutorial || tutorialTerminado)
            return;

        panelInicio.SetActive(false);
        enTutorial = true;
        instruccionesInicio.SetActive(true);
        afirmacion_sfx.Play();

        await Task.Delay(TimeSpan.FromSeconds(11.0f));
        tutorialTerminado = true;
        enTutorial = false;
    }




    [Button]
    public async void IniciarJuego()
    {
        
        if (jugando || enTutorial)
            return;
        else
            jugando = true;

        panelInicio.SetActive(false);
        limites.SetActive(true);

        afirmacion_sfx.Play();
        musicaLobby_sfx.Stop();
        suspenso_sfx.Play();
        Ambiente_sfx(true);

        Debug.Log("Se Inicio el Juego");
        instruccionesInicio.GetComponent<Animator>().SetTrigger("desaparecer");

        await Task.Delay(TimeSpan.FromSeconds(0.5f));//le damos tiempo a las instrucciones a desaparecer

        trafico_sfx.Play();
        viento_sfx.Play();
        camaraJugador.cullingMask = cullingJuego;
        fadeEsfera.SetTrigger("fadeOut");
        await Task.Delay(TimeSpan.FromSeconds(1.0f));//tiempo para que desaparezca la esfera

        contadorInicio.SetActive(true);

        await Task.Delay(TimeSpan.FromSeconds(4.2f));//tiempo para que aparezca el contador 3,2,1

        contadorInicio.SetActive(false);
        empezarConteo = true;

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
        cronometro_jugador_text.text = tiempo_texto.text;





        if (tiempo <= 0.0)
        {
            Debug.Log("Se termino el tiempo");

            empezarConteo = false;
            FinJuego();
        }
    }
    async void BeepReloj()
    {
        beepReloj_sfx.Play();
        await Task.Delay(TimeSpan.FromSeconds(30.0f));
        if(empezarConteo)
        {
            BeepReloj();
        }

    }
    [Button]
    public void GanoJuego()
    {
        limites.SetActive(false);
        empezarConteo = false;

        fadeEsfera.SetTrigger("fadeIn");

        Ambiente_sfx(false);

        suspenso_sfx.Stop();

        confetti_vfx.Play();
        celebracion_sfx.Play();
        camaraJugador.cullingMask = cullingInicio;
        panelFinal_gano.SetActive(true);
    }

    [Button]
    public async void JugadorCayo()
    {
        limites.SetActive(false);
        empezarConteo = false;

        Debug.Log("Jugador Cayo");
        //TODO: sfx de perdio
        //JugadorControl._jugador.fadeBlanco.SetTrigger("fadeIn");
        fadeEsfera.SetTrigger("fadeIn");
        suspenso_sfx.Stop();
        camaraJugador.cullingMask = cullingInicio;
        Ambiente_sfx(false);

        await Task.Delay(TimeSpan.FromSeconds(1.5f));
       
        panelFinal_perdio.SetActive(true);
    }

    [Button]
    public async void CajaCayo()
    {
        limites.SetActive(false);
        empezarConteo = false;


        Debug.Log("Caja Cayo");
        Ambiente_sfx(false);
        camaraJugador.cullingMask = cullingInicio;

        fadeEsfera.SetTrigger("fadeIn");
        suspenso_sfx.Stop();
        await Task.Delay(TimeSpan.FromSeconds(1.5f));
        panelFinal_perdio.SetActive(true);


    }

    public void FinJuego()
    {
        Debug.Log("Se termino el juego");
    }

    /// <summary>
    /// Recibe el input de los triggers de los controles para empezar el juego
    /// </summary>
    /// <param name="mano"></param>
    public void InicioTrigger(ManoControl.TipoMano mano)
    {
        if (!tutorialTerminado)//Si el tutorial no ha sido pasado no puede jugar
            return;
        if (jugando)//Si ya esta jugando lo descarta
            return;
        if (mano == ManoControl.TipoMano.derecha)
        {
            manoDerechaLista = true;
        }
        if (mano == ManoControl.TipoMano.izquierda)
        {
            manoizquierdaLista = true;
        }

        if(manoizquierdaLista && manoDerechaLista)
        {
            IniciarJuego();
        }
    }

    [Button]
    public void ActivarHelicoptero()
    {
        helicopteroAnim.SetActive(true);
    }

    public void SuscrbirPersona(PersonasControl persona)
    {
        personas.Add(persona);
    }

    void Ambiente_sfx(bool reproducir)
    {
        if (reproducir)
        {
            trafico_sfx.Play();
            viento_sfx.Play();
            heartbeat_sfx.Play();
        }
        else if(!reproducir)
        {
            trafico_sfx.Stop();
            viento_sfx.Stop();
            heartbeat_sfx.Stop();
        }

    }
}
