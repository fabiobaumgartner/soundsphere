using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisWorld : MonoBehaviour
{
    [SerializeField] SphereController sphere;
    Vector3 newPos;
    // Update is called once per frame
    void Update()
    {
        newPos.x = sphere.transform.position.x + 50;
        transform.position = newPos;
    }
}
