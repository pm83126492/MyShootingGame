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
        if (Input.GetMouseButton(0) && !CanNextFire)
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
        objectPool.SpawnFromPool("PlayerBullet", FirePoint.position, transform.rotation);
    }

    //FireTimeReciprocal
    IEnumerator FireTime()
    {
        yield return new WaitForSeconds(0.05f);
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
