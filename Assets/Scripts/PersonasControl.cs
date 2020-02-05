using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody),typeof(BoxCollider))]
public class PersonasControl : MonoBehaviour
{
    public float velocidad = 1.5f;

    public Transform[] puntosCamino;
    public int enPunto = 0;
    // Start is called before the first frame update
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < puntosCamino.Length; i++)
        {
            if(i == 0)
            Gizmos.DrawLine(this.transform.position, puntosCamino[i].position);
            else
                Gizmos.DrawLine(puntosCamino[i-1].position, puntosCamino[i].position);
            puntosCamino[i].transform.name = this.transform.name +"_"+ i;
        }
    }
    private void OnValidate()
    {
        //GetComponent<Rigidbody>().isKinematic = true;
        //GetComponent<BoxCollider>().isTrigger = true;
    }
    void Start()
    {
    }
    void BuscarMaster()
    {

    }

    public void MiUpdate()
    {
        if (puntosCamino.Length > 0)
            Movimiento();
    }
    public void Movimiento()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, puntosCamino[enPunto].position, (Time.deltaTime * velocidad));
        Vector3 dist = puntosCamino[enPunto].position - this.transform.position;
        if (dist.magnitude <= 0.5f)
        {
            SiguientePunto();
            Vector3 sigPuntoRotacion = puntosCamino[enPunto].position;
            sigPuntoRotacion.z = 0.0f;
            this.transform.LookAt(sigPuntoRotacion);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    // if(other.transform.tag == "retorno")
    //    {
    //        Quaternion rotacion = this.transform.rotation;
    //        rotacion.y += 180;
    //        this.transform.rotation = rotacion;
    //    }
    //}

    public void SiguientePunto()
    {
        if (puntosCamino.Length <= 0)
            return;
        if(enPunto < puntosCamino.Length-1)
        {
            enPunto++;
        }else
        {
            enPunto = 0;
        }
    }
}
