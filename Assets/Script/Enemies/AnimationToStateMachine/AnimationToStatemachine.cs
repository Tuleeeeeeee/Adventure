using Unity.VisualScripting;
using UnityEngine;

public class AnimationToStatemachine : MonoBehaviour
{
    public E_AttackState attackState;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private void TriggerAttack()
    {
        attackState.TriggerAttack();
    }

    private void FinishAttack()
    {
        attackState.FinishAttack();
    }
    private void DisableObject()
    {
        Transform objectCTransform = transform.Find("Core");
        boxCollider = objectCTransform.GetComponentInChildren<BoxCollider2D>();
        boxCollider.enabled = false;
      
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, 20f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.gravityScale = 5f;
        rb.AddTorque(-20f);
        
        //Invoke(nameof(OnDead), 2f);
    }
    private void OnDead()
    {
        transform.gameObject.SetActive(false);
    }
}
