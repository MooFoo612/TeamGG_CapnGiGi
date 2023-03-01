using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAnimations : PlayerController
{
    Animator animator;
    TouchingDirections td;
    Rigidbody2D parent;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "isOnGround_Particles" && collision.transform.position.y < transform.position.y)
        {
            animator.SetTrigger("isLanding");
        }
    }
}
