using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public ObjectsPool objectPool;//objectPoolScript
    public Transform FirePoint;
    public Slider HPSilder;
    public Text ScoreText;
    public Text ProtectText;
    public GameObject ProtectingMask;
    public CameraShake cameraShake;

    bool CanNextFire;//NextFire Is OK?
    bool isProtecting;//Is Protecting?
    public static bool IsStopShoot;//Is Stop Shooting?
    public static bool IsAddShootLine;//Is Add Shooting Line? (1~5)

    public float PlayerMoveSpeed;
    public static float CurrentHP;//PlayerHP
    public int ProtectingAmount;//PlayerCanProtectingAmount
    public static int Score;//PlayerScore
    int ShootLineAmount;//Shoot Line Amount (1~5)

    AudioSource audioSource;
    public AudioClip ProtectAudio;
    public AudioClip GetProtectAudio;
    void Start()
    {
        IsAddShootLine = false;
        ShootLineAmount = 1;
        Score = 0;
        CurrentHP = 100;
        ProtectingMask = transform.GetChild(1).gameObject;
        IsStopShoot = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        HPSilder.value = CurrentHP;

        MovePosition();//PlayerMove

        if (!CanNextFire&&!IsStopShoot)
        {
            Fire();
            CanNextFire = true;
            StartCoroutine(FireTime());
        }

        Protecting();//ProtectingEvent

        ScoreText.text = "Score:" + Score;//PlayerAddScore

        ProtectText.text = "X " + ProtectingAmount;//PlayerProtectingAmount
    }

    //PlayerMove
    void MovePosition()
    {
        if (Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.up * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.down * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(Vector3.left * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PlayerMoveSpeed);
        }
    }

    //Shooting
    void Fire()
    {
        if (IsAddShootLine)
        {
            objectPool.SpawnFromPool("LevelUpEffect", transform.position, transform.rotation);
            objectPool.SpawnFromPool("LevelUpEffect", transform.position, transform.rotation).transform.parent=transform;
            ShootLineAmount += 1;
            IsAddShootLine = false;
        }

        Quaternion leftQuaternoin1 = Quaternion.AngleAxis(4, Vector3.forward);
        Quaternion rightQuaternoin1 = Quaternion.AngleAxis(-4, Vector3.forward);
        Quaternion leftQuaternoin2 = Quaternion.AngleAxis(8, Vector3.forward);
        Quaternion rightQuaternoin2 = Quaternion.AngleAxis(-8, Vector3.forward);
        for (int j = 0; j < ShootLineAmount; j++)
        {
            switch (j)
            {
                case 0:
                    objectPool.SpawnFromPool("PlayerBullet", FirePoint.position, transform.rotation);
                    break;
                case 1:
                    objectPool.SpawnFromPool("PlayerBullet", FirePoint.position, leftQuaternoin1);
                    break;
                case 2:
                    objectPool.SpawnFromPool("PlayerBullet", FirePoint.position, rightQuaternoin1);
                    break;
                case 3:
                    objectPool.SpawnFromPool("PlayerBullet", FirePoint.position, leftQuaternoin2);
                    break;
                case 4:
                    objectPool.SpawnFromPool("PlayerBullet", FirePoint.position, rightQuaternoin2);
                    break;
            }
        }
    }

    void Protecting() //ProtectingEvent
    {
        if(Input.GetKeyDown(KeyCode.Space)&&ProtectingAmount>0&& !isProtecting)
        {
            audioSource.PlayOneShot(ProtectAudio, 0.5f);
            isProtecting = true;
            ProtectingMask.SetActive(true);
            ProtectingAmount -= 1;
            StartCoroutine(ProtectingCoolingTime());
        }
    }


    //Player Fire Time Reciprocal
    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(0.1f);
        CanNextFire = false;
    }

    //Player Protecting Cooling Time
    IEnumerator ProtectingCoolingTime()
    {
        yield return new WaitForSeconds(3f);
        isProtecting = false;
        ProtectingMask.SetActive(false);
    }

    //Player Damage
    public void TakeDamage(int DamageInt)
    {
        CurrentHP -= DamageInt;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet")&&!isProtecting)
        {
            other.gameObject.SetActive(false);
            objectPool.SpawnFromPool("DamageEffect", transform.position, transform.rotation);
            cameraShake.ShakeTime = 0.1f;
            TakeDamage(1);
        }

        if (other.gameObject.CompareTag("ProtectingObject"))
        {
            ProtectingAmount += 1;
            audioSource.PlayOneShot(GetProtectAudio, 0.3f);
            Destroy(other.gameObject);
        }
    }
}
