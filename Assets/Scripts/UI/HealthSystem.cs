using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth = 100f;
    public float decayRate = 5f;
    private float currentHealth;
    public bool IsDead { get; private set; }

    public event Action<float> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
        IsDead = false;
    }

    private void Update()
    {
        if (IsDead) return; 

        currentHealth -= decayRate * Time.deltaTime;
        currentHealth = Mathf.Max(currentHealth, 0f);
        OnHealthChanged?.Invoke(currentHealth/ maxHealth);

        if(currentHealth <= 0f && !IsDead)
        {
            Die();
        }
    }

    private void Die()
    {
        IsDead = true;
        OnDeath?.Invoke();
    }
}
