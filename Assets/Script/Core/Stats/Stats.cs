using System;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    public bool IsDead { get => isDead; private set => isDead = value; }
    public float CurrentHealth { get => currentHealth; private set => currentHealth = value; }

    [SerializeField]
    private float maxHealth = 100;

    private bool isDead;
    private float currentHealth;
    protected override void Awake()
    {
        base.Awake();

        currentHealth = maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        currentHealth = currentHealth - amount;
        if (currentHealth <= 0)
        {
            isDead = true;
            OnHealthZero?.Invoke();
        }
    }
}
