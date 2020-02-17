using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator_control : MonoBehaviour
{
    public Transform helice;
    public float velocidadRotacion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        helice.transform.Rotate(Vector3.up * (Time.deltaTime * velocidadRotacion));

    }
}
