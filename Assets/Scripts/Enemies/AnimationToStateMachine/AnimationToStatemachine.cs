using UnityEngine;

namespace Tuleeeeee.Enemies
{
    public class AnimationToStatemachine : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private Collider2D _boxCollider;
        private Collider2D _boxColliderMain;
        private MonoBehaviour[] _scripts;

        private void Start()
        {
            _scripts = gameObject.GetComponents<MonoBehaviour>();
        }

        private void DisableObject()
        {
            DisableAllScripts();

            Transform objectCTransform = transform.Find("Core");
            if (objectCTransform != null)
            {
                _boxCollider = objectCTransform.GetComponentInChildren<Collider2D>();
                _boxColliderMain = transform.GetComponent<Collider2D>();
                if (_boxCollider != null)
                {
                    _boxCollider.enabled = false;
                }  
                if (_boxColliderMain != null)
                {
                    _boxColliderMain.enabled = false;
                }
            }

            _rb = GetComponent<Rigidbody2D>();
            if (_rb != null)
            {
                _rb.velocity = new Vector2(2f, 20f);
                _rb.constraints = RigidbodyConstraints2D.None;
                _rb.gravityScale = 5f;
                _rb.AddTorque(-20f);
            }
        }

        private void OnDead()
        {
            Invoke(nameof(Deactive), 5f);
        }

        private void Deactive()
        {
            gameObject.SetActive(false);
        }

        private void DisableAllScripts()
        {
            foreach (var script in _scripts)
            {
                script.enabled = false;
            }
        }
    }
}