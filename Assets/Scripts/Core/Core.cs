using System.Collections.Generic;
using System.Linq;
using Tuleeeeee.CoreComponet;
using UnityEngine;

namespace Tuleeeeee.Cores
{
    public class Core : MonoBehaviour
    {
        private readonly List<CoreComponent> _coreComponents = new List<CoreComponent>();

        public void LogicUpdate()
        {
            foreach (CoreComponent component in _coreComponents)
            {
                component.LogicsUpdate();
            }
        }
        public void AddComponent(CoreComponent component)
        {
            if (!_coreComponents.Contains(component))
            {
                _coreComponents.Add(component);
            }
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var comp = _coreComponents.OfType<T>().FirstOrDefault();

            if (comp)
                return comp;

            comp = GetComponentInChildren<T>();

            if (comp)
                return comp;

            Debug.LogWarning($"{typeof(T)} not found on {transform.parent.name}");
            return null;
        }

        public T GetCoreComponent<T>(ref T value) where T : CoreComponent
        {
            value = GetCoreComponent<T>();
            return value;
        }
    }
}
