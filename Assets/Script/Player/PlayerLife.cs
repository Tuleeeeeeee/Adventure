using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D boxCollider2d;
    private AudioManager audioManager;
    public float deathFroce;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("DoDamage"))
        {
            Die();
            rb.velocity = new Vector2(rb.velocity.x, deathFroce);
            boxCollider2d.enabled = false;
            Debug.Log("dead");
        }
    }
    private void Die()
    {
      /*  rb.bodyType = RigidbodyType2D.Kinematic;*/
        anim.SetTrigger("hit");
    }

    private void WaitForRestartLevel()
    {
        Invoke(nameof(RestartLevel), 0.5f);
    }
    private void RestartLevel()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
