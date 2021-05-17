using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ObjectsPool objectPool;//objectPoolScript
    public Transform FirePoint;

    bool CanNextFire;//NextFire Is OK?

    public float PlayerMoveSpeed;

    void Start()
    {
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
}
