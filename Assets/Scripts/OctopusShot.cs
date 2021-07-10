using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopusShot : Weapon
{

    GameObject player;
    Vector2 AreaAcao = new Vector2(10, 10);
    BoxCollider2D boxCollider;
    bool m_FacingRight;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, AreaAcao);
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (othe)
    //    {

    //    }
    //}
    private void Update()
    {
        var origin = new Vector2(transform.position.x, transform.position.y);
        var playerEntrou = Physics2D.BoxCast(origin, AreaAcao, 180f, Vector2.up);

        if (playerEntrou.collider.CompareTag("Player"))
        {
            Debug.LogWarning("em breve coisas");
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
        
    }
}
