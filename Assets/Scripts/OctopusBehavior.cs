using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OctopusBehavior : Weapon
{
    GameObject player;
    Vector2 AreaAcao = new Vector2(12.5f, 10);
    BoxCollider2D boxCollider;
    bool m_FacingRight;
    Rigidbody2D rb;
    float axisY;
    float axisX;
    bool isJumping = false;
    private bool slowdown;
    private bool cooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.Sleep();
        axisX = transform.position.x;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var boneco = collision.collider.GetComponent<PlayerMovement>();
            if (boneco != null)
                boneco.TakeDamage(10, 0);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, AreaAcao);
    }

    private void Update()
    {
        transform.position = new Vector3(axisX, transform.position.y, transform.position.z);
        var origin = new Vector2(transform.position.x, transform.position.y);
        var playerEntered = Physics2D.BoxCastAll(origin, AreaAcao, 180f, Vector2.up).FirstOrDefault(col => col.collider.CompareTag("Player"));

        Debug.LogWarning($"{rb.velocity}, {rb.gravityScale}, {rb.IsAwake()}");

        if (transform.position.y <= axisY)
        {
            OnLanding();
        }

        if (playerEntered.collider != null && !isJumping)
        {
            Jump();
        }

        if (player.transform.position.x > transform.position.x && !m_FacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && m_FacingRight)
        {
            Flip();
        }

        if (rb.IsAwake() && rb.velocity.y < 0)
        {
            rb.gravityScale = .5f;
        }

        if (isJumping)
            ShootController();
    }

    private void OnLanding()
    {
        if (isJumping)
        {
            transform.position = new Vector3(transform.position.x, axisY, 0.0f);
        }
        else
            axisY = transform.position.y;

        slowdown = false;
        cooldown = false;
        isJumping = false;
        rb.gravityScale = 0.0f;
        rb.Sleep();
    }

    private void ShootController()
    {
        if (!cooldown
            && transform.position.y <= player.transform.position.y + .1f
            && transform.position.y >= player.transform.position.y - .1f
            && rb.velocity.y <= 0)
        {
            cooldown = true;
            Shoot();
        }
    }

    private void Jump()
    {
        slowdown = true;
        isJumping = true;
        rb.gravityScale = 1.5f;
        rb.WakeUp();
        axisY = transform.position.y;
        rb.AddForce(new Vector2(0, 600f));

    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
