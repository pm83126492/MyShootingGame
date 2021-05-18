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

    bool CanNextFire;//NextFire Is OK?

    public float PlayerMoveSpeed;
    public int CurrentHP;//PlayerHP

    public static int Score;

    void Start()
    {
        CurrentHP = 100;
        //FirePoint = GetComponentInChildren<Transform>();
    }

    void Update()
    {
        //PlayerMove
        MovePosition();

        //Shooting
        if ((Input.GetMouseButton(0)|| Input.GetKey(KeyCode.Space)) && !CanNextFire)
        {
            Fire();
            CanNextFire = true;
            StartCoroutine(FireTime());
        }

        //PlayerAddScore
        ScoreText.text = "Score:" + Score;
    }

    //PlayerMove
    void MovePosition()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * Time.deltaTime * PlayerMoveSpeed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * Time.deltaTime * PlayerMoveSpeed);
        }
    }

    //Shooting
    void Fire()
    {
        if (Score < 100)
        {
            ShootLevel(1);
        }
        else if (Score>=100&&Score < 200)
        {
            ShootLevel(2);
        }
        else if (Score >= 200 && Score < 300)
        {
            ShootLevel(3);
        }
        else if (Score >= 300 && Score <400)
        {
            ShootLevel(4);
        }
        else if (Score >= 400)
        {
            ShootLevel(5);
        }
    }

    void ShootLevel(int ShootNumber)
    {
        Quaternion leftQuaternoin1 = Quaternion.AngleAxis(4, Vector3.forward);
        Quaternion rightQuaternoin1 = Quaternion.AngleAxis(-4, Vector3.forward);
        Quaternion leftQuaternoin2 = Quaternion.AngleAxis(8, Vector3.forward);
        Quaternion rightQuaternoin2 = Quaternion.AngleAxis(-8, Vector3.forward);
        for (int j = 0; j < ShootNumber; j++)
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

    //FireTimeReciprocal
    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(0.1f);
        CanNextFire = false;
    }

    //Player Damage
    public void TakeDamage(int DamageInt)
    {
        CurrentHP -= DamageInt;
        if (CurrentHP <= 0)
        {
            CurrentHP = 0;
        }
        HPSilder.value = CurrentHP;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
        }
    }
}
