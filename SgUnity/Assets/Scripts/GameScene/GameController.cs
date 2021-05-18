using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform[] EnemyPoint;

    bool isAppearing;

    int RandomLV1Number;
    public enum EnemyState
    {
        NONE,
        LV1_1,
        LV1_2,
        LV2,
        LV3,
        LV4,
        LV5,
    }

    public EnemyState enemyAppearState;
    void Start()
    {
        enemyAppearState = EnemyState.NONE;
        Invoke("StartGame", 1f);//StartGame in 3 seconds
    }

    void Update()
    {
        EnemyAppear();
    }

    //SpawnEnemy
    void EnemyAppear()
    {
        switch (enemyAppearState)
        {
            case EnemyState.LV1_1:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear("Enemy01",4,0.1f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV1_2:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear("Enemy01",4,0.1f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV2:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear("Enemy01", 4, 0.1f));
                    StartCoroutine(DelayAppear("Enemy04", 50,0.5f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV3:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear("Enemy01", 4, 0.1f));
                    StartCoroutine(DelayAppear("Enemy04", 50, 0.5f));
                    StartCoroutine(DelayAppear("Enemy02", 10, 3f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV4:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear("Enemy03", 10, 2f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV5:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear("Enemy05", 10, 2f));
                    isAppearing = true;
                }
                break;
        }
    }

    void StartGame()
    {
        enemyAppearState = EnemyState.LV3;
    }

    //EnemyDelayAppear
    IEnumerator DelayAppear(string EnemyTag,int EnemyAmount,float DelayTime)
    {
        for (int i = 0; i < EnemyAmount; i++)
        {
            yield return new WaitForSeconds(DelayTime);
            objectsPool.SpawnFromPool(EnemyTag, EnemyPoint[Random.Range(0,5)].position, EnemyPoint[Random.Range(0, 5)].rotation);
        }
    }

    //EnemyDelayAppear_LV1_2
    IEnumerator DelayAppear2()
    {
        for (int i = EnemyPoint.Length-1; i >= 0; i--)
        {
            yield return new WaitForSeconds(0.1f);
            objectsPool.SpawnFromPool("Enemy01", EnemyPoint[i].position, EnemyPoint[i].rotation);
        }
    }
}
