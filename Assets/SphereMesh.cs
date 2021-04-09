using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMesh : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private SphereController sphere;

    [Header("Settings")]
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;
    [SerializeField] private AnimationCurve scaleCurve;
    [SerializeField] private float emissionMin;
    [SerializeField] private float emissionMax;
    [SerializeField] private Gradient emissionColor;

    private Material material;
    private float currentCharge = 0f;

    private void OnValidate() {
        if (mesh == null) mesh = GetComponent<MeshRenderer>();
        if (sphere == null) sphere = GetComponentInParent<SphereController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        material = mesh.material;
        sphere.onMomentumChanged += OnMomentumChanged;
        sphere.onChargeChanged += OnChargeChanged;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = Random.insideUnitSphere * 0.05f * currentCharge;
    }

    private void OnChargeChanged(float newCharge) {
        currentCharge = newCharge;
    }

    private void OnMomentumChanged(float newMomentum) {
        newMomentum = Mathf.Clamp01(newMomentum);
        material.SetColor("_EmissionColor", emissionColor.Evaluate(newMomentum));
        transform.localScale = Vector3.Lerp(transform.localScale,  Vector3.one * (newMomentum + 1f), Time.deltaTime * 5);
    }
}
