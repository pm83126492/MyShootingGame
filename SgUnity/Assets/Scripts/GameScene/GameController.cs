using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform[] EnemyPoint;

    bool isAppearing;
    public enum EnemyAppearState
    {
        NONE,
        LV1,
        LV2,
        LV3,
    }

    public EnemyAppearState enemyAppearState;
    void Start()
    {
        enemyAppearState = EnemyAppearState.NONE;
        Invoke("StartGame", 3f);
    }

    void Update()
    {
        EnemyAppear();
    }

    void EnemyAppear()
    {
        switch (enemyAppearState)
        {
            case EnemyAppearState.LV1:
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
        enemyAppearState = EnemyAppearState.LV1;
    }

    IEnumerator DelayAppear()
    {
        for (int i = 0; i < EnemyPoint.Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            objectsPool.SpawnFromPool("Enemy01", EnemyPoint[i].position, EnemyPoint[i].rotation);
            if (EnemyPoint.Length == 5)
            {
                enemyAppearState = EnemyAppearState.LV2;
            }
        }
    }
}
