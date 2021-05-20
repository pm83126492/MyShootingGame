using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicBullet : Bullet ,IPoolObject
{
    public float dir;
    bool CanMove;

    public void OnObjectSpawn()
    {
        Invoke("StartMove", 1.5f);
        dir = Random.Range(0, 361);
        Quaternion rotation = Quaternion.AngleAxis(transform.localEulerAngles.z+dir, Vector3.forward);
        transform.rotation = rotation;
    }

    void StartMove()
    {
        CanMove = true;
    }

    protected override void Update()
    {
        if (CanMove)
        {
            base.Update();
        }
    }
}
