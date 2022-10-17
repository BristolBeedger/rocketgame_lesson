using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 moveVector;
    [SerializeField] float period = 2f;

    Vector3 startPos;
    float moveFactor;


    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if(period <= Mathf.Epsilon) return;
        float cycles = Time.time/period;

        const float tau = Mathf.PI * 2;                       // constant value of tau, 2pi, ~6.283
        float rawSinWave = Mathf.Sin(cycles*tau);             // sine function, cycling value from (-1,1)

        moveFactor = (rawSinWave + 1f) / 2;                   // shifting sine wave to (0,1)

        Vector3 offset = moveVector * moveFactor;
        transform.position = startPos + offset;
    }
}
