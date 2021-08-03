using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectile;
    [SerializeField] GameObject gun;
    AtackerSpawner myLaneSpawner;
    Animator animator;

    GameObject projectileParent;
    const string PROJECTILE_PARENT_GAME = "Projectiles";

    private void Start()
    {
        SetLaneSpawner();
        animator = GetComponent<Animator>();
        CreateProjectileParent();
    }

    private void Update()
    {
        if (IsAttackerInLane())
        {
            animator.SetBool("IsAttacking", true);
        }
        else
        {
            animator.SetBool("IsAttacking", false);
        }
    }

    private void CreateProjectileParent()
    {
        projectileParent = GameObject.Find(PROJECTILE_PARENT_GAME);

        if (!projectileParent)
        {
            projectileParent = new GameObject(PROJECTILE_PARENT_GAME);
        }
    }

    private void SetLaneSpawner()
    {
        AtackerSpawner[] spawners;
        spawners = FindObjectsOfType<AtackerSpawner>();

        foreach (AtackerSpawner spawner in spawners)
        {
            bool IsCloseEnough = (Mathf.Abs(spawner.transform.position.y - transform.position.y) <= Mathf.Epsilon);
            if (IsCloseEnough)
            {
                myLaneSpawner = spawner;
            }
        }
    }

    private bool IsAttackerInLane()
    {
        if (myLaneSpawner.transform.childCount <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Fire(float damage)
    {
        GameObject newProjectile = Instantiate(projectile, gun.transform.position, gun.transform.rotation) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
    }
}
