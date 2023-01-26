using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;
    MoveVelocityS target;

    float fireRate;
    float nextFire;
    float attackDistance = 5f;

    private void Start()
    {
        target = FindObjectOfType<MoveVelocityS>();

        fireRate = 1f;
        nextFire = Time.time;
    }

    private void Update()
    {
        CheckIfTimeToFire();
    }

    private void CheckIfTimeToFire()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < attackDistance) {
            if (Time.time > nextFire)
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                nextFire = Time.time + fireRate;
            }
        }
        else
        {
            Debug.Log("MEHEHEH");
        }
    }
}
