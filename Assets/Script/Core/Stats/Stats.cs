using System;
using Unity.VisualScripting;
using UnityEngine;

public class Stats : CoreComponent
{
    public event Action OnHealthZero;
    public bool IsDead { get => isDead; set => isDead = value; }

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
