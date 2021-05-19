using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyParent ,IPoolObject
{
    int FireTime;

    public bool isMoveing;

    protected override void Start()
    {
        base.Start();
        //InvokeRepeating("EnemyFire", 1f, 0.1f);//EnemyShooting
    }

    public void OnObjectSpawn()
    {
        InvokeRepeating("EnemyFire", 1f, 0.3f);//EnemyShooting
        isMoveing = false;
        FireTime = 0;
    }

    void Update()
    {
        EnemyMove();//EnemyMoving

        if (isMoveing)
        {
            Move(MoveSpeed);
        }

        //Stop Shooting
        if (FireTime > 8)
        {
            CancelInvoke("EnemyFire");
            StartCoroutine(ReloadFire());
            FireTime = 0;
            isMoveing = true;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(90, 271));
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
        Quaternion RandomQuaternoin = Quaternion.AngleAxis(transform.localEulerAngles.z+Random.Range(180, 225), Vector3.forward);
        Shoot("StraightBullet", FirePoint[0].position, RandomQuaternoin);
        FireTime += 1;
    }

    //EnemyReloadShooting
    IEnumerator ReloadFire()
    {
        yield return new WaitForSeconds(1f);
        isMoveing = false;
        InvokeRepeating("EnemyFire", 0f, 0.1f);
    }
}
