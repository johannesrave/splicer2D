using System;
using Pixelplacement;
using UnityEngine;

[RequireComponent(typeof(Spline))]
public class PathController : MonoBehaviour
{
    private Spline _spline;

    private void Awake()
    {
        _spline = GetComponent<Spline>();
    }
}