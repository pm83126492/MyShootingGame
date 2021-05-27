using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform[] EnemyPoint;

    public GameObject AddProtectingObject;
    public GameObject GameOverPanel;
    public GameObject WinPanel;
    public GameObject BulletObject;

    public PlayableDirector playableDirector;//TimeLine
    public BossController bossController;

    bool isAppearing;
    bool isGetProtecting;
    public float GameTime;
    public AudioSource BgmAudioSource;
    public AudioClip BossBgm;
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
        Time.timeScale = 1;
        enemyAppearState = EnemyState.NONE;
        Invoke("StartGame", 1f);//StartGame in 3 seconds
        BgmAudioSource = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    void Update()
    {
        EnemyAppear();//SpawnEnemy

        GameTimeChangeLevel();//LevelChange 1~5

        ProtectingObject();//AppearProtectingObject

        GameOverEvent();//Game Over Event
    }

    void GameOverEvent()
    {
        if (PlayerController.CurrentHP == 0)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            BgmAudioSource.Pause();
        }
        else if (BossController.BossHP == 0)
        {
            BulletObject.SetActive(false);
            Invoke("WinGame", 3.1f);
        }
    }

    void WinGame()
    {
        Time.timeScale = 0;
        WinPanel.SetActive(true);
        BgmAudioSource.Pause();
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
            case EnemyState.BOSS:
                BgmAudioSource.clip = BossBgm;
                BgmAudioSource.Play();
                //PlayerController.IsStopShoot = true;
                playableDirector.enabled=true;
                if (PlayerController.CurrentHP <= 100)
                {
                    PlayerController.CurrentHP += 0.3f;
                }
                if (float.Parse(playableDirector.time.ToString("0.0")) == 6f)
                {
                    enemyAppearState = EnemyState.NONE;
                    bossController.attackState = BossController.AttackState.SECTORATTACK;
                    //PlayerController.IsStopShoot = false;
                    bossController.isBeginBoss = true;
                    playableDirector.Pause();
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
        else if (Mathf.Floor(GameTime) == 100)
        {
            ChangeLevel();
            enemyAppearState = EnemyState.NONE;
        }
        else if (Mathf.Floor(GameTime) == 105)
        {
            enemyAppearState = EnemyState.BOSS;
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
        if(Mathf.Floor(GameTime) % 20 == 0&& Mathf.Floor(GameTime) !=0&& !isGetProtecting)
        {
            Instantiate(AddProtectingObject, EnemyPoint[Random.Range(0, 5)].position, EnemyPoint[Random.Range(0, 5)].rotation);
            isGetProtecting = true;
        }

        if (Mathf.Floor(GameTime) %20 == 1)
        {
            isGetProtecting = false;
        }
    }

    public void RePlayButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RevivalButton()
    {
        SceneManager.LoadScene(0);
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
