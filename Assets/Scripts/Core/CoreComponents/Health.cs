using System;
using Tuleeeeee.CoreComponet;

namespace Tuleeeeee.CoreComponets
{
    public class Health : CoreComponent
    {
        public int CurrentHealth
        {
            get => currentHealth;
            private set => currentHealth = value;
        }

        private int startingHealth;
        private int currentHealth;

        private HealthEvent healthEvent;

        private void CallHealthEvent(int damageAmount)
        {
            healthEvent.CallHealthChangedEvent((float)currentHealth / (float)startingHealth, currentHealth, damageAmount);
        }
        protected override void Awake()
        {
            base.Awake();
            healthEvent = GetComponent<HealthEvent>();
        }

        private void Start()
        {
            CallHealthEvent(0);
        }

        public void TakeDamage(int damageAmount)
        {
            currentHealth -= damageAmount;
            CallHealthEvent(damageAmount);
        }

        public void SetStartingHealth(int startingHealth)
        {
            this.startingHealth = startingHealth;
            currentHealth = startingHealth;
        }

        public int GetStartingHealth()
        {
            return startingHealth;
        }
    }
}