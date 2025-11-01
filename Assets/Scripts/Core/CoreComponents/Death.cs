using UnityEngine;
using Tuleeeeee.CoreComponet;
using Tuleeeeee.Manager;

namespace Tuleeeeee.CoreComponets
{
    public class Death : CoreComponent
    {
        private Health Stats
        {
            get => _stats ?? Core.GetCoreComponent(ref _stats);
        }

        private Health _stats;

        private Animator _animator;

        private void Start()
        {
            _animator = Core.transform.parent.GetComponent<Animator>();
        }

        public void Die()
        {
            _animator.SetBool("hit", true);
        }
    }
}