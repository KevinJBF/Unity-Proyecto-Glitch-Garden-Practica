using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacker : MonoBehaviour
{
    float currentSpeed;
    GameObject currentTarget;
    Animator animator;

    private void Awake()
    {
        FindObjectOfType<LevelController>().AttackerSpawnerd();
    }

    private void OnDestroy()
    {
        LevelController levelController = FindObjectOfType<LevelController>();
        
        if (levelController != null)
        {
            levelController.AttackerKilled();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * currentSpeed);
        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        if (!currentTarget)
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void SertMovementSpeed(float speed)
    {
        currentSpeed = speed;
    }

    public void Attack(GameObject target)
    {
        animator.SetBool("isAttacking", true);
        currentTarget = target;
    }

    public void StrikeCurrentTarget(float damage)
    {
        if (!currentTarget) { return; }

        Health health = currentTarget.GetComponent<Health>();

        if (health)
        {
            health.DealDamage(damage);
        }
    }
}
