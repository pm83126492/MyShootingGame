﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyParent , IPoolObject
{
    protected override void Start()
    {
        base.Start();
        //InvokeRepeating("EnemyFire", 1f, 2f);//EnemyShooting
    }

    public void OnObjectSpawn()
    {
        InvokeRepeating("EnemyFire", 1f, 2f);//EnemyShooting
    }

    private void Update()
    {
        Move(MoveSpeed);
    }

    //CircleShooting
    void EnemyFire()
    {
        Vector3 fireDirection = transform.up;
        float rotationAngle=0;
        for (int j = 0; j < 30; j++)
        {
            Shoot("CircleBullet", FirePoint[0].position, Quaternion.AngleAxis(rotationAngle, Vector3.forward));
            rotationAngle += 12;
        }

    }
}
