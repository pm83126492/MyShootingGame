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
        LV1,
        LV2,
        LV3,
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
            case EnemyState.LV1:
                if (!isAppearing)
                {
                    StartCoroutine(DelayAppear());
                    isAppearing = true;
                }
                break;
        }
    }

    void StartGame()
    {
        enemyAppearState = EnemyState.LV1;
    }

    //EnemyDelayAppear
    IEnumerator DelayAppear()
    {
        for (int i = 0; i < EnemyPoint.Length; i++)
        {
            yield return new WaitForSeconds(0.1f);
            objectsPool.SpawnFromPool("Enemy01", EnemyPoint[i].position, EnemyPoint[i].rotation);
        }
    }
}
