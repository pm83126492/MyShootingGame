using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyParent , IPoolObject
{
    public void OnObjectSpawn()
    {
        InvokeRepeating("EnemyFire", 0f, 0.3f);//EnemyShooting
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();//EnemyMoving

        //EnemyFire();//EnemyShooting
    }

    void EnemyMove()
    {
        Move(2); 
    }

    //SectorShooting
    void EnemyFire()
    {
        Vector3 PlayerAndEnemyDifference = target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(PlayerAndEnemyDifference.y, PlayerAndEnemyDifference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);

        Quaternion zeroQuaternoin = Quaternion.AngleAxis(transform.localEulerAngles.z+180, Vector3.forward);
        Quaternion leftQuaternoin1 = Quaternion.AngleAxis(transform.localEulerAngles.z+187, Vector3.forward);
        Quaternion rightQuaternoin1 = Quaternion.AngleAxis(transform.localEulerAngles.z + 173, Vector3.forward);
        Quaternion leftQuaternoin2 = Quaternion.AngleAxis(transform.localEulerAngles.z+195, Vector3.forward);
        Quaternion rightQuaternoin2 = Quaternion.AngleAxis(transform.localEulerAngles.z + 165, Vector3.forward);

        for (int j = 0; j < 5; j++)
        {
            switch (j)
            {
                case 0:
                    Shoot("SectorBullet", FirePoint[0].position, leftQuaternoin1);
                    break;
                case 1:
                    Shoot("SectorBullet", FirePoint[0].position, rightQuaternoin1);
                    break;
                case 2:
                    Shoot("SectorBullet", FirePoint[0].position, leftQuaternoin2);
                    break;
                case 3:
                    Shoot("SectorBullet", FirePoint[0].position, rightQuaternoin2);
                    break;
                case 4:
                    Shoot("SectorBullet", FirePoint[0].position, zeroQuaternoin);
                    break;
            }
        }
    }
}
