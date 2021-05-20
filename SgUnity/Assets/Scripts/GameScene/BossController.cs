﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BossController : MonoBehaviour
{
    public Slider BossHPSlider;
    public ObjectsPool objectsPool;
    public static float BossHP;
    public int MoveSpeed;
    float DistanceBossToNextPoint;//Boss to NextPoint Distance
    int RandomNextPoint;//NextMovePoint
    public int RandomAttack;//Radom Attack Number
    public float BossTime;//Boss Game Time

    public bool isBeginBoss;//Boss Is Begin?
    bool isAttack;//Boss Can Attack

    public Transform[] MovePoint;
    public Transform[] FirePoint;
    public Transform[] TicPoint;
    public GameObject target;
    public enum AttackState
    {
        NONE,
        CIRCLEATTACK,
        SPIRALATTACK,
        SECTORATTACK,
        TICTACTOEATTACK,
    }

    public AttackState attackState;

    void Start()
    {
        BossHP = 1000;
        FirePoint = GetComponentsInChildren<Transform>();
        FirePoint = GetComponentsInChildren<Transform>().Where(item => item != this.transform).ToArray();
    }

    void Update()
    {
        BossHPSlider.value = BossHP;

        BossMove();//BossMoveEvent

        AttackLevel();//Change Attack

        FirePointRotateToPlayer();//Fire Point Rotate to Player

        AttackChangeEvent();//Change Attack Event
    }


    void AttackChangeEvent()
    {
        if (isBeginBoss)
        {
            BossTime += Time.deltaTime;
        }

        //Change Attack 
        if (Mathf.Floor(BossTime)%8==0&& Mathf.Floor(BossTime) != 0)
        {
            RandomAttack = Random.Range(1, 5);
            StopAllCoroutines();
        }
        else if (Mathf.Floor(BossTime) % 8 == 1)
        {
            isAttack = false;
        }

        //Change Attack every 8 Seconds
        if (!isAttack)
        {
            if (RandomAttack == 1)
            {
                attackState = AttackState.SECTORATTACK;
            }
            else if (RandomAttack == 2)
            {
                attackState = AttackState.CIRCLEATTACK;
            }
            else if (RandomAttack == 3)
            {
                attackState = AttackState.SPIRALATTACK;
            }
            else if (RandomAttack == 4)
            {
                attackState = AttackState.TICTACTOEATTACK;
            }
            isAttack = true;
        }
    }

    void BossMove()
    {
        //Boss Change Next Point And Move to Point
        if (isBeginBoss)
        {
            if (DistanceBossToNextPoint == 0)
            {
                RandomNextPoint = Random.Range(0, 6);
            }
            DistanceBossToNextPoint = Vector3.Distance(transform.position, MovePoint[RandomNextPoint].position);
            transform.position = Vector3.MoveTowards(transform.position, MovePoint[RandomNextPoint].position, MoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet") && isBeginBoss)
        {
            BossHP -= 0.5f;
            if (BossHP <= 0)
            {
                BossHP = 0;
            }
        }
    }

    void FirePointRotateToPlayer()
    {
        Vector3 PlayerAndEnemyDifference = target.transform.position - transform.position;
        float rotationZ = Mathf.Atan2(PlayerAndEnemyDifference.y, PlayerAndEnemyDifference.x) * Mathf.Rad2Deg;
        FirePoint[1].transform.rotation = FirePoint[2].transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
    }

    void AttackLevel()
    {
        switch (attackState)
        {
            case AttackState.CIRCLEATTACK:
                StartCoroutine(AddCircleAttack());
                RandomAttack = 0;
                attackState = AttackState.NONE;
                break;
            case AttackState.SPIRALATTACK:
                StartCoroutine(AddSpiralAttack());
                RandomAttack = 0;
                attackState = AttackState.NONE;
                break;
            case AttackState.SECTORATTACK:
                StartCoroutine(AddSectorAttack());
                RandomAttack = 0;
                attackState = AttackState.NONE;
                break;
            case AttackState.TICTACTOEATTACK:
                StartCoroutine(AddTicTacToeAttack());
                RandomAttack = 0;
                attackState = AttackState.NONE;
                break;
        }
    }

    //TwoCircleAttack
    IEnumerator AddCircleAttack()
    {
        float rotationAngle = 0;
        List<GameObject> bullets = new List<GameObject>();
        for (int i = 0; i < 15; i++)
        {
            rotationAngle += 24;
            bullets.Add(objectsPool.SpawnFromPool("BurstBullet", FirePoint[0].position, Quaternion.AngleAxis(rotationAngle, Vector3.forward)));
        }
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < bullets.Count; i++)
        {
            StartCoroutine(CircleAttack(bullets[i].transform.position));
        }
    }

    //CircleAttack
    IEnumerator CircleAttack(Vector3 pos)
    {
        float rotationAngle = 0;
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 24; j++)
            {
                rotationAngle += 15;
                objectsPool.SpawnFromPool("BurstBullet", pos, Quaternion.AngleAxis(rotationAngle, Vector3.forward));
            }
            yield return new WaitForSeconds(2f);
        }
    }

    //SpiralAttack
    IEnumerator AddSpiralAttack()
    {
        Vector3 bulletDir = FirePoint[0].transform.right; //direction
        Quaternion rotate = Quaternion.AngleAxis(20, Vector3.forward); //rotate 20 degrees
        float rotation = 0;
        float radius = 0.8f;//radius 半径
        float distance = 0.2f;//increased distance
        for (int i = 0; i < 18; i++)
        {
            Vector3 firePoint = FirePoint[0].transform.position + bulletDir * radius;//spawn position
            StartCoroutine(CircleAttack(firePoint));
            yield return new WaitForSeconds(0.1f);
            rotation += 20;
            bulletDir = rotate * bulletDir;//change direction
            radius += distance;//increase radius 半径
        }
    }

    //SectorAttack
    IEnumerator AddSectorAttack()
    {
        for (int i = 0; i < 15; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Quaternion zeroQuaternoin = Quaternion.AngleAxis(FirePoint[1].localEulerAngles.z, Vector3.forward);
                Quaternion leftQuaternoin1 = Quaternion.AngleAxis(FirePoint[1].localEulerAngles.z + 15, Vector3.forward);
                Quaternion rightQuaternoin1 = Quaternion.AngleAxis(FirePoint[1].localEulerAngles.z - 15, Vector3.forward);
                Quaternion zeroQuaternoin2 = Quaternion.AngleAxis(FirePoint[2].localEulerAngles.z, Vector3.forward);
                Quaternion leftQuaternoin2 = Quaternion.AngleAxis(FirePoint[2].localEulerAngles.z + 15, Vector3.forward);
                Quaternion rightQuaternoin2 = Quaternion.AngleAxis(FirePoint[2].localEulerAngles.z - 15, Vector3.forward);
                switch (j)
                {
                    case 0:
                        objectsPool.SpawnFromPool("SpinBullet", FirePoint[1].transform.position, leftQuaternoin1);
                        objectsPool.SpawnFromPool("SpinBullet", FirePoint[2].transform.position, leftQuaternoin2);
                        break;
                    case 1:
                        objectsPool.SpawnFromPool("SpinBullet", FirePoint[1].transform.position, rightQuaternoin1);
                        objectsPool.SpawnFromPool("SpinBullet", FirePoint[2].transform.position, rightQuaternoin2);
                        break;
                    case 2:
                        objectsPool.SpawnFromPool("SpinBullet", FirePoint[1].transform.position, zeroQuaternoin);
                        objectsPool.SpawnFromPool("SpinBullet", FirePoint[2].transform.position, zeroQuaternoin2);
                        break;
                }
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    //TicTacToeAttack
    IEnumerator AddTicTacToeAttack()
    {
        int SpawnPoint=0;
        int SpawnDir=0;
        Vector3 fireDir= Vector3.up;
        for (int i = 0; i < 4; i++)
        {

            if (SpawnDir == 0)
            {
                fireDir = Vector3.up;
            }
            else if (SpawnDir == 1)
            {
                fireDir = Vector3.down;
            }
            else if (SpawnDir == 2)
            {
                fireDir = Vector3.left;
            }
            else if (SpawnDir == 3)
            {
                fireDir = Vector3.right;
            }

            Vector3 firePoint = TicPoint[SpawnPoint].transform.position;
            for (int j = 0; j < 10; j++)
            {
                firePoint += fireDir;//spawn position
                objectsPool.SpawnFromPool("TicBullet", firePoint, transform.rotation);
                yield return new WaitForSeconds(0.1f);
            }
            SpawnPoint += 1;
            SpawnDir += 1;
        }
    }
}
