using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : EnemyParent , IPoolObject
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //InvokeRepeating("EnemyFire", 0f, 0.1f);//EnemyShooting
    }

    public void OnObjectSpawn()
    {
        InvokeShoot("EnemyFire", 0, 0.1f);//EnemyShooting
    }

    // Update is called once per frame
    void Update()
    {
        Move(MoveSpeed);
    }

    //BurstShooting
    void EnemyFire()
    {
        if (!isTurnToPlayer)
        {
            Vector3 PlayerAndEnemyDifference = target.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(PlayerAndEnemyDifference.y, PlayerAndEnemyDifference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
            isTurnToPlayer = true;
        }
        Quaternion RandomQuaternoin = Quaternion.AngleAxis(Random.Range(0,180), Vector3.forward);
        Shoot("StraightBullet", FirePoint[0].position, RandomQuaternoin);
    }
}
