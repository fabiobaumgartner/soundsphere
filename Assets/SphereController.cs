using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnMomentumChanged(float newMomentum);
public delegate void OnChargeChanged(float newCharge);

public class SphereController : MonoBehaviour
{
    [SerializeField] private float charge;
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float speed;
    [SerializeField, Range(0f, 1f)] private float momentum = 0f;
    [SerializeField] private float speedBoostMomentum = 15f;

    private float zPos = 0f;
    private float yPos = 0f;
    private float xPos = 0f;

    public event OnMomentumChanged onMomentumChanged;
    public event OnChargeChanged onChargeChanged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(PressActionRoutine());
        }

        speed = baseSpeed + momentum * speedBoostMomentum;

        xPos = xPos + Time.deltaTime * speed;
        zPos = Mathf.Sin(Time.time * 3) * (0.1f + (1 - charge));
        yPos = Mathf.Cos(Time.time * 3) * (0.1f + (1 - charge));

        transform.position = Vector3.right * xPos + Vector3.forward * zPos + Vector3.up * yPos; 


    }

    private IEnumerator PressActionRoutine() {
        
        
        while (Input.GetKey(KeyCode.Space)) {
            momentum = Mathf.Lerp(momentum, -0.05f, Time.deltaTime);
            onMomentumChanged?.Invoke(momentum);
            charge = Mathf.Clamp01(charge + Time.deltaTime);
            onChargeChanged?.Invoke(charge);
            yield return null;
        }

        TriggerAction();
    }

    private IEnumerator ExplosionRoutine(float explosionCharge) {
        momentum = charge;
        
        while (momentum > 0f) {
            if (charge > 0) charge = Mathf.Clamp01(charge - Time.deltaTime);
            onChargeChanged?.Invoke(charge);
            momentum -= Time.deltaTime * 0.2f;
            onMomentumChanged?.Invoke(momentum);
            yield return null;
        }
        yield return null;
        
    }

    private void TriggerAction() {
        StartCoroutine(ExplosionRoutine(charge));
    }



    public float GetMomentum() { return momentum; }
}
