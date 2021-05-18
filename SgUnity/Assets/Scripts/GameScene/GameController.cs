using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform[] EnemyPoint;

    bool isAppearing;

    float GameTime;
    public enum EnemyState
    {
        NONE,
        LV1,
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
        if (GameTime == 5)
        {
            isAppearing = false;
            GameTime += 1;
            StopAllCoroutines();
            enemyAppearState = EnemyState.LV2;
        }
        else if(GameTime == 30)
        {
            isAppearing = false;
            GameTime += 1;
            StopAllCoroutines();
            enemyAppearState = EnemyState.LV3;
        }
    }

    //SpawnEnemy
    void EnemyAppear()
    {
        switch (enemyAppearState)
        {
            case EnemyState.LV1:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear2(false));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV2:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear2(false));
                    StartCoroutine(DelayAppear("Enemy04", 50,0.5f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV3:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear2(false));
                    StartCoroutine(DelayAppear("Enemy04", 1000, 0.5f));
                    StartCoroutine(DelayAppear("Enemy02", 1000, 1.5f));
                    isAppearing = true;
                }
                break;
            /*case EnemyState.LV4:
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
                break;*/
        }
    }

    void StartGame()
    {
        enemyAppearState = EnemyState.LV1;
    }

    //EnemyDelayAppear
    IEnumerator DelayAppear(string EnemyTag,int EnemyAmount,float DelayTime)
    {
        for (int i = 0; i < EnemyAmount; i++)
        {
            yield return new WaitForSeconds(DelayTime);
            objectsPool.SpawnFromPool(EnemyTag, EnemyPoint[Random.Range(0,5)].position, EnemyPoint[Random.Range(0, 5)].rotation);
            GameTime += 1;
        }
    }

    //EnemyDelayAppear_LV1_2
    IEnumerator DelayAppear2(bool isInVerse)
    {
        yield return new WaitForSeconds(1f);
        if (!isInVerse)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.2f);
                objectsPool.SpawnFromPool("Enemy01", EnemyPoint[i].position, EnemyPoint[i].rotation);
                if (i == 4)
                {
                    GameTime += 1;
                    StartCoroutine(DelayAppear2(true));
                }
            }
        }
        else
        {
            for (int i = 4; i >= 0; i--)
            {
                yield return new WaitForSeconds(0.2f);
                objectsPool.SpawnFromPool("Enemy01", EnemyPoint[i].position, EnemyPoint[i].rotation);
                if (i == 0)
                {
                    GameTime += 1;
                    StartCoroutine(DelayAppear2(false));
                }
            }
        }
    }
}
