using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private float _health;
    public float health
    {
        get => _health;
        set
        {
            _health = value;
            Vector3 scale = transform.GetChild(0).localScale;
            scale.x = _health;
            transform.GetChild(0).localScale = scale;
        }
    }
}
