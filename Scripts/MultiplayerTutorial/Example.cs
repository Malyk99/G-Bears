using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public GameObject sphere;
    public Vector3 scaleChange;

    void Awake()
    {

    }

    void Update()
    {
        sphere.transform.localScale += scaleChange;
    
        scaleChange = -scaleChange;
        
    }
}
