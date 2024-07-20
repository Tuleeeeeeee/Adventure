using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D Rb;
    private Animator anim;
    private BoxCollider2D boxCollider2d;
    private AudioManager audioManager;
    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponent<Animator>();
        Rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Trap"))
        {
            Die();
         
            boxCollider2d.enabled = false;
            Debug.Log("dead");
        }
    }
    private void Die()
    {
       // Rb.bodyType = RigidbodyType2D.Kinematic;
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
