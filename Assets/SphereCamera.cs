using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCamera : MonoBehaviour
{
    [SerializeField] private SphereController sphere;
    [SerializeField] private float distanceAtFullMomentum;
    [SerializeField] private Vector3 offsetBase = new Vector3(-10f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, offsetBase + sphere.transform.position + Vector3.up * (10f + distanceAtFullMomentum * sphere.GetMomentum()), Time.deltaTime);
    }
}
