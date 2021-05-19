using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform[] EnemyPoint;

    public GameObject AddProtectingObject;

    bool isAppearing;
    bool isGetProtecting;
    public float GameTime;
    public enum EnemyState
    {
        NONE,
        LV1,
        LV2,
        LV3,
        LV4,
        LV5,
        BOSS,
    }

    public EnemyState enemyAppearState;
    void Start()
    {
        enemyAppearState = EnemyState.NONE;
        Invoke("StartGame", 1f);//StartGame in 3 seconds
    }

    void Update()
    {
        EnemyAppear();//SpawnEnemy

        GameTimeChangeLevel();//LevelChange 1~5

        ProtectingObject();//AppearProtectingObject
    }

    //SpawnEnemy
    void EnemyAppear()
    {
        switch (enemyAppearState)
        {
            case EnemyState.LV1:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppearLV1Enemy(false));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV2:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppearLV1Enemy(false));
                    StartCoroutine(DelayAppear("Enemy04", 50,0.5f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV3:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppearLV1Enemy(false));
                    StartCoroutine(DelayAppear("Enemy04", 100, 0.5f));
                    StartCoroutine(DelayAppear("Enemy02", 100, 1f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV4:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppearLV1Enemy(false));
                    StartCoroutine(DelayAppear("Enemy04", 100, 0.5f));
                    StartCoroutine(DelayAppear("Enemy02", 100, 1f));
                    StartCoroutine(DelayAppear("Enemy03", 100, 2f));
                    isAppearing = true;
                }
                break;
            case EnemyState.LV5:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppearLV1Enemy(false));
                    StartCoroutine(DelayAppear("Enemy04", 100, 0.5f));
                    StartCoroutine(DelayAppear("Enemy02", 100, 1f));
                    StartCoroutine(DelayAppear("Enemy03", 100, 2f));
                    StartCoroutine(DelayAppear("Enemy05", 100, 1.5f));
                    isAppearing = true;
                }
                break;

        }
    }

    void StartGame()
    {
        enemyAppearState = EnemyState.LV1;
    }

    //LevelChange 1~5
    void GameTimeChangeLevel()
    {
        GameTime += Time.deltaTime;
        if (Mathf.Floor(GameTime) == 10)
        {
            ChangeLevel();
            enemyAppearState = EnemyState.LV2;
        }
        else if (Mathf.Floor(GameTime) == 30)
        {
            ChangeLevel();
            enemyAppearState = EnemyState.LV3;
        }
        else if (Mathf.Floor(GameTime) == 50)
        {
            ChangeLevel();
            enemyAppearState = EnemyState.LV4;
        }
        else if (Mathf.Floor(GameTime) == 70)
        {
            ChangeLevel();
            enemyAppearState = EnemyState.LV5;
        }
    }

    void ChangeLevel()
    {
        isAppearing = false;
        StopAllCoroutines();
    }

    //AppearProtectingObject
    void ProtectingObject()
    {
        if(Mathf.Floor(GameTime) % 15 == 0&& Mathf.Floor(GameTime) !=0&& !isGetProtecting)
        {
            Instantiate(AddProtectingObject, EnemyPoint[Random.Range(0, 5)].position, EnemyPoint[Random.Range(0, 5)].rotation);
            isGetProtecting = true;
        }
        if (Mathf.Floor(GameTime) %2 != 0)
        {
            isGetProtecting = false;
        }
        
        /*
        if (PlayerController.Score % 50==0&& PlayerController.Score != 0&& !isGetProtecting)
        {
            Instantiate(AddProtectingObject, EnemyPoint[Random.Range(0, 5)].position, EnemyPoint[Random.Range(0, 5)].rotation);
            isGetProtecting = true;
        }

        if (PlayerController.Score % 2 != 0)
        {
            isGetProtecting = false;
        }*/
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
    IEnumerator DelayAppearLV1Enemy(bool isInVerse)
    {
        yield return new WaitForSeconds(2f);
        if (!isInVerse)
        {
            for (int i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.2f);
                objectsPool.SpawnFromPool("Enemy01", EnemyPoint[i].position, EnemyPoint[i].rotation);
                if (i == 4)
                {
                    StartCoroutine(DelayAppearLV1Enemy(true));
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
                    StartCoroutine(DelayAppearLV1Enemy(false));
                }
            }
        }
    }
}
