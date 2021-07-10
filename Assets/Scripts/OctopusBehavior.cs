using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusBehavior : Weapon
{

    GameObject player;
    Vector2 AreaAcao = new Vector2(10, 10);
    BoxCollider2D boxCollider;
    bool m_FacingRight;
    Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
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
        var playerEntrou = Physics2D.BoxCast(origin, AreaAcao, 180f, Vector2.up);

        if (playerEntrou.collider.CompareTag("Player"))
        {
            Shoot();
        }

        if (player.transform.position.x > transform.position.x && !m_FacingRight)
        {
            Flip();
        }
        else if (player.transform.position.x < transform.position.x && m_FacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
