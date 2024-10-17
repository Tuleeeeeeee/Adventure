

using UnityEngine;
public class AnimationToStatemachine : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private MonoBehaviour[] scripts;
    private void Start()
    {
        scripts = gameObject.GetComponents<MonoBehaviour>();
    }
    private void DisableObject()
    {
        DisableAllScripts();

        Transform objectCTransform = transform.Find("Core");
        if (objectCTransform != null)
        {
            boxCollider = objectCTransform.GetComponentInChildren<BoxCollider2D>();
            if (boxCollider != null)
            {
                boxCollider.enabled = false;
            }
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(2f, 20f);
            rb.constraints = RigidbodyConstraints2D.None;
            rb.gravityScale = 5f;
            rb.AddTorque(-20f);
        }

        Invoke(nameof(OnDead), 2f);
    }
    private void OnDead()
    {
        gameObject.SetActive(false);
    }
    private void DisableAllScripts()
    {
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
    }
}

