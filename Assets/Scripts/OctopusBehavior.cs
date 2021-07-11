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
    bool isJumping = false;
    private bool cooldown;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;
        rb.Sleep();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, AreaAcao);
    }

    private void Update()
    {
        var origin = new Vector2(transform.position.x, transform.position.y);
        var playerEntered = Physics2D.BoxCastAll(origin, AreaAcao, 180f, Vector2.up).FirstOrDefault(col => col.collider.CompareTag("Player"));

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
        cooldown = false;
        isJumping = false;
        rb.gravityScale = 0.0f;
        rb.Sleep();
    }

    private void ShootController()
    {
        if (!cooldown
            && transform.position.y >= player.transform.position.y - .5f
            && transform.position.y <= player.transform.position.y + .5f)
        {
            cooldown = true;
            Shoot();
        }
    }

    private void Jump()
    {
        isJumping = true;
        rb.gravityScale = 1.5f;
        rb.WakeUp();
        axisY = transform.position.y;
        rb.AddForce(new Vector2(0, 500f));

    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
