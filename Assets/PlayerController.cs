using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() {
        if(Input.GetKey(KeyCode.Space)) {
            Debug.Log("Pressed SPACE");
        }
    }
    void ProcessRotation() {
        if(Input.GetKey(KeyCode.D)) {
            Debug.Log("Rotate Clockwise");
        }
        else if(Input.GetKey(KeyCode.A)) {
            Debug.Log("Rotate CounterClockwise");
        }
    }
}
