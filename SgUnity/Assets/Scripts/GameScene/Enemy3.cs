using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : EnemyParent , IPoolObject
{
    protected override void Start()
    {
        base.Start();
       // InvokeRepeating("EnemyFire", 1f, 1f);//EnemyShooting
    }

    public void OnObjectSpawn()
    {
        InvokeRepeating("EnemyFire", 1f, 0.3f);//EnemyShooting
    }

    private void Update()
    {
        Move(1);
    }

    //CircleShooting
    void EnemyFire()
    {
        Vector3 fireDirection = transform.up;
        float rotationAngle=0;
        for (int j = 0; j < 18; j++)
        {
            Shoot("CircleBullet", FirePoint[0].position, Quaternion.AngleAxis(rotationAngle, Vector3.forward));
            rotationAngle += 20;
        }


    }
}
