using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D rb;
    public int damage = 20;

    Animator anim;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        Invoke("DestroyThis", 5);
        sr.sortingOrder = 9999;
    }

    void OnTriggerEnter2D(Collider2D hitInfo){
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("shot")
                && hitInfo.tag != "shot")
        {
            Enemy enemy = hitInfo.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            rb.velocity = Vector2.zero;
            anim.SetTrigger("hit");
        }
        
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
