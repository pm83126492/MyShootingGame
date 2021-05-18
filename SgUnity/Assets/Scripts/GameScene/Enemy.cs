using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyParent
{
    int FireTime;

    protected override void Start()
    {
        base.Start();
        InvokeRepeating("EnemyFire", 1f, 0.1f);//EnemyShooting
    }

    void Update()
    {
        EnemyMove();//EnemyMoving

        //Stop Shooting
        if (FireTime > 10)
        {
            StartCoroutine(ReloadFire());
            CancelInvoke("EnemyFire");
            FireTime = 0;
        }
    }

    void EnemyMove()
    {
        if (transform.position.y >= 3)
        {
            Move(3);
        }
        else
        {
            //Enemy Turn to Player
            if (!isTurnToPlayer)
            {
                Vector3 PlayerAndEnemyDifference = target.transform.position - transform.position;
                float rotationZ = Mathf.Atan2(PlayerAndEnemyDifference.y, PlayerAndEnemyDifference.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
                isTurnToPlayer = true;
            }
        }
    }

    //StraightShooting
    void EnemyFire()
    {
        Quaternion zeroQuaternoin = Quaternion.AngleAxis(0, Vector3.forward);
        Shoot("StraightBullet", FirePoint[0].position, FirePoint[0].rotation);
        FireTime += 1;
    }

    //EnemyReloadShooting
    IEnumerator ReloadFire()
    {
        yield return new WaitForSeconds(1f);
        Vector3 PlayerAndEnemyDifference = target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(PlayerAndEnemyDifference.y, PlayerAndEnemyDifference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
        InvokeRepeating("EnemyFire", 0f, 0.1f);
    }
}
