using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    private AudioSource shot;
    private Animator animator;

    void Start(){
        shot = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!PauseMenu.isPaused || animator.GetCurrentAnimatorStateInfo(0).IsName("hurt"))
        {
            if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Return))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        shot.PlayOneShot(shot.clip);
    }
}
