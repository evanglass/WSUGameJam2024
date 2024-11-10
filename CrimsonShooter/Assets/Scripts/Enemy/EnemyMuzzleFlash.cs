using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMuzzleFlash : MonoBehaviour
{

    [SerializeField] private float lifetime;
    [SerializeField] private float decay = 20;
    [SerializeField] private float power = 3;
    private Material mat;
    private Light light;

    private float timer;
    private void OnEnable() {
        timer = 0f;
        mat = GetComponentInChildren<MeshRenderer>().material;
        light = GetComponentInChildren<Light>();
    }

    private void Update() {
        Camera mainCamera = Camera.main;
        transform.rotation = mainCamera.transform.rotation;
        timer += Time.deltaTime;
        float intensity = (1 / (1 + Mathf.Pow(timer * decay, power)));
        light.intensity = intensity;
        mat.color = new Color(1, 1, 1, intensity);

        if (timer > lifetime) {
            Destroy(gameObject);
        }
    }
}
