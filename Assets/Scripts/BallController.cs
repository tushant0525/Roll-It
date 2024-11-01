using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    private Rigidbody rb;
    public Vector3 originalBallPosition;
    public static BallController instance { get; private set; }
    private void Awake()
    {
        instance = this;
        rb = GetComponent<Rigidbody>();
        originalBallPosition = transform.position;
    }
}
