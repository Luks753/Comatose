using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform attack;
    public Transform patrol;
    public Animator animator;
    public LayerMask playerLayer;

    public int damage = 20;

    public float attackRange = 0.7f;
    public float patrolRange = 2f;
    public float deathTime;
    float nextAttack = 2f;
    float attackRate = 2f;

    public int maxHP = 100;
    int actualHP;

    Rigidbody2D body;
    Animator anim;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        actualHP = maxHP;
        getDeathTime();
    }

    void Update()
    {
        //FindPlayer();
        if (body.velocity.x != 0)
        {
            animator.SetBool("speed", true);
        }
        else
        {
            animator.SetBool("speed", false);
        }

        if (Time.time >= nextAttack)
        {
            Attack();
        }
    }

    public void getDeathTime()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == "death")
            {
                deathTime = clip.length;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        actualHP -= damage;
        animator.SetTrigger("hit");
        if (actualHP <= 0)
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            Die();
        }
    }

    void Attack()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attack.position, attackRange, playerLayer);
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("explosion"))
        {
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<PlayerMovement>().TakeDamage(damage, transform.position.x);
                nextAttack = Time.time + 1f / attackRate;
            }
        }

    }

    void FindPlayer()
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(patrol.position, patrolRange, playerLayer);

        foreach (Collider2D player in hitPlayer)
        {
            EnemyIA.target = player.GetComponent<Transform>();
        }
    }

    void Die()
    {
        var colliders = GetComponents<Collider2D>();
        if (colliders != null)
        {
            foreach (var collider in colliders)
            {
                collider.enabled = false;
            }
        }

        animator.SetTrigger("isDead");
    }

    // chamado na anima��o de explos�o de inimigo
    void Death()
    {
        this.enabled = false;
        gameObject.SetActive(false);
        PlayerMovement.specialKilled++;
    }

    void OnDrawGizmosSelected()
    {
        if (attack == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(attack.position, attackRange);
        Gizmos.DrawWireSphere(patrol.position, patrolRange);
    }

}