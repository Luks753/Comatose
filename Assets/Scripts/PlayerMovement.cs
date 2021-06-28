using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    public Transform attack;
    public Transform special;
    public Transform groundCheck;

    public float horizontalMove = 0f;
    public float speed = 40f;
    public float attackRange = 0.5f;
    public float specialRange = 10f;
    public float deathTime;

    public bool jumping = false;

    public static int specialKilled;
    public static bool canSpecial = false;
    public static bool specialMode = false;
    public float attackRate = 0.5f;
    float nextAttack = 0f;
    public LayerMask enemies;
    public int damage = 20;
    public int maxHP = 100;
    public static bool died;
    int actualHP;

    Rigidbody2D body;

    void Start()
    {
        specialMode = false;
        specialKilled = 0;
        died = false;
        body = GetComponent<Rigidbody2D>();
        actualHP = maxHP;
        getDeathTime();
    }

    void Update()
    {
        if(!PauseMenu.isPaused){
            horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
            animator.SetFloat("speed", Mathf.Abs(horizontalMove));

            if (Input.GetButtonDown("Jump"))
            {
                jumping = true;
                animator.SetBool("isJump", true);
            }
            
            if ((Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Return)) )
            {
                animator.SetBool("isShooting", true);
            }else{
                animator.SetBool("isShooting", false);
            }

    }

    public void OnLanding()
    {
        animator.SetBool("isJump", false);
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, jumping);
        jumping = false;
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attack.position, attackRange, enemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            // enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
    void Special()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(special.position, specialRange, enemies);

        foreach (Collider2D enemy in hitEnemies)
        {
            // enemy.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attack == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(special.position, specialRange);
        Gizmos.DrawWireSphere(attack.position, attackRange);
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

    void Die()
    {
        animator.SetBool("isDead", true);
        this.enabled = false;
        StartCoroutine(WaitDeath());
    }

    IEnumerator WaitDeath()
    {
        yield return new WaitForSeconds(deathTime);
        GetComponent<Collider2D>().enabled = false;
        gameObject.SetActive(false);
        died = true;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}