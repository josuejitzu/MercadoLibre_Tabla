using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globo_Control : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(0.1f,10.0f)]
    public float velocidadRotacion = 1.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(Vector3.up * (Time.deltaTime * velocidadRotacion));
    }
}
