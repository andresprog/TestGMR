﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotate : MonoBehaviour
{
    public float angularSpeed = 90;
    public Vector3 axis = Vector3.up;

    void Update()
    {
        transform.Rotate(axis * angularSpeed * Time.deltaTime);
    }
}
