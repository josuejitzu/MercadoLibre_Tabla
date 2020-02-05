using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ManoControl : MonoBehaviour
{
    public enum TipoMano { derecha,izquierda};
    public TipoMano _tipoMano;

    public float triggerPresion;
    [Range(0.0f,1.0f)]
    public float sensibilidadTrigger = 0.1f;

    public GameObject caja;
    public ManoControl manoContraria;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();



        if (_tipoMano == TipoMano.derecha)
        {
            triggerPresion = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.RTouch);
        }
        else if (_tipoMano == TipoMano.izquierda)
        {
            triggerPresion = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, OVRInput.Controller.LTouch);

        }

  

        if(triggerPresion >= sensibilidadTrigger)
        {
            print("Trigger " + _tipoMano + " presionado");
            Master._master.InicioTrigger(_tipoMano);


            if (caja != null && caja.GetComponent<Caja_Control>().cajaTomada == false)
            {
                if (_tipoMano == TipoMano.derecha && caja.GetComponent<Caja_Control>().manoIzquierda)
                {
                    caja.GetComponent<Caja_Control>().manoDerecha = true;
                    caja.GetComponent<Caja_Control>().cajaTomada = true;
                    TomarCaja();
                }else if(_tipoMano == TipoMano.derecha)
                {
                    caja.GetComponent<Caja_Control>().manoDerecha = true;
                }

                if(_tipoMano == TipoMano.izquierda && caja.GetComponent<Caja_Control>().manoDerecha)
                {
                    caja.GetComponent<Caja_Control>().manoIzquierda = true;
                    TomarCaja();

                }else if(_tipoMano == TipoMano.izquierda)
                {
                    caja.GetComponent<Caja_Control>().manoIzquierda = true;

                }


            }
           // TomarCaja();
        }else if(triggerPresion < sensibilidadTrigger)
        {
           

            SoltarCaja();

          

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "caja")
        {

            if (caja != null)
                return;
            else if(caja == null)
            {
                caja = other.gameObject;

               // TomarCaja();
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "caja")
        {
            if (caja != null)
            {
                if (_tipoMano == TipoMano.derecha)
                {
                    caja.GetComponent<Caja_Control>().manoDerecha = false;

                }
                else if (_tipoMano == TipoMano.izquierda)
                {
                    caja.GetComponent<Caja_Control>().manoIzquierda = false;

                }
                //manoContraria.DestruirJoint();
                //caja.GetComponent<Caja_Control>().cajaTomada = false;
            }
            SoltarCaja();

            caja = null;
           // DestruirJoint();

        }

    }

    void TomarCaja()
    {
        if (caja == null)
            return;

        var joint = AddFixedJoint();
        joint.connectedBody = caja.GetComponent<Rigidbody>();
        Master._master.zonaMeta.SetActive(true);
    }
    void SoltarCaja()
    {
        if (caja == null)
            return;



        if (_tipoMano == TipoMano.derecha)
        {
            caja.GetComponent<Caja_Control>().manoDerecha = false;

        }
        else if (_tipoMano == TipoMano.izquierda)
        {
            caja.GetComponent<Caja_Control>().manoIzquierda = false;

        }

        caja.GetComponent<Caja_Control>().cajaTomada = false;
        //caja = null;
        Master._master.zonaMeta.SetActive(false);

        DestruirJoint();
        manoContraria.DestruirJoint();
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();

        fx.breakForce = 15000;
        fx.breakTorque = 15000;
        return fx;
    }

    private HingeJoint AddHingeJoint()
    {
        HingeJoint hx = gameObject.AddComponent<HingeJoint>();
        hx.breakForce = 5000;
        hx.breakTorque = 5000;
        return hx;
    }

    public void DestruirJoint()
    {
        if (GetComponent<FixedJoint>() != null)
        {
            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());
        }
    }
}

