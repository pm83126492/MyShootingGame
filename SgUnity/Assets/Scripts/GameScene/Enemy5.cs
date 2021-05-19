using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy5 : EnemyParent ,IPoolObject
{
    public float RSpeed;
    protected override void Start()
    {
        base.Start();
        //InvokeRepeating("EnemyFire", 0f, 0.2f);//EnemyShooting
    }

    public void OnObjectSpawn()
    {
        InvokeRepeating("EnemyFire", 0f, 0.2f);//EnemyShooting
    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, RSpeed));
        transform.position += new Vector3(0f, MoveSpeed * Time.deltaTime, 0f);
        //Move(MoveSpeed);
    }

    //SpinShooting
    void EnemyFire()
    {
        for (int j = 0; j < 5; j++)
        {
            Quaternion zeroQuaternoin = Quaternion.AngleAxis(transform.localEulerAngles.z, Vector3.forward);
            Quaternion zeroQuaternoin2 = Quaternion.AngleAxis(transform.localEulerAngles.z+180, Vector3.forward);
            Quaternion leftQuaternoin1 = Quaternion.AngleAxis(transform.localEulerAngles.z + 30, Vector3.forward);
            Quaternion rightQuaternoin1 = Quaternion.AngleAxis(transform.localEulerAngles.z - 30, Vector3.forward);
            Quaternion leftQuaternoin2 = Quaternion.AngleAxis(transform.localEulerAngles.z + 150, Vector3.forward);
            Quaternion rightQuaternoin2 = Quaternion.AngleAxis(transform.localEulerAngles.z - 150, Vector3.forward);
            switch (j)
            {
                case 0:
                    Shoot("SectorBullet", FirePoint[1].position, leftQuaternoin1);
                    Shoot("SectorBullet",FirePoint[0].position, leftQuaternoin2);
                    break;
                case 1:
                    Shoot("SectorBullet",FirePoint[1].position, rightQuaternoin1);
                    Shoot("SectorBullet",FirePoint[0].position, rightQuaternoin2);
                    break;
                case 2:
                    Shoot("SectorBullet", FirePoint[1].position, zeroQuaternoin);
                    Shoot("SectorBullet",FirePoint[0].position, zeroQuaternoin2);
                    break;
            }
        }
    }
}
