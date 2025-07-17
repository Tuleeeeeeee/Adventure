using UnityEngine;
using Tuleeeeee.CoreComponet;

namespace Tuleeeeee.CoreComponets
{
    public class Combat : CoreComponent, IDamageable
    {
        private Health Health
        {
            get => health ?? Core.GetCoreComponent(ref health);
        }

        private Health health;

        public void DealDamage(int damageAmount)
        {
            Debug.Log(Core.transform.parent.name + "Take Damage: " + damageAmount);
            Health.TakeDamage(damageAmount);
        }
    }
}