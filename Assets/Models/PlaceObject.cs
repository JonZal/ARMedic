using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class NewBehaviourScript : MonoBehaviour
{
    private ARSessionOrigin arOrigin;

    // Start is called before the first frame update
    void Start() 
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
