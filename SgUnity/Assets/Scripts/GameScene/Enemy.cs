using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform FirePoint;
    public float MoveSpeed;
    public GameObject target;
    int FireTime;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("Player");
        objectsPool = GameObject.Find("BulletObjectPool").GetComponent<ObjectsPool>();
        InvokeRepeating("EnemyFire", 1f, 0.1f);//EnemyShooting
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();//EnemyMoving

        //Stop Shooting
        if (FireTime > 10)
        {
            StartCoroutine(ReloadFire());
            CancelInvoke("EnemyFire");
            FireTime = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            PlayerController.Score += 1;
            gameObject.SetActive(false);
            CancelInvoke("EnemyFire");
        }
    }

    void EnemyMove()
    {
        if (transform.position.y >= 3)
        {
            transform.Translate(Vector3.up * Time.deltaTime * MoveSpeed);
        }
        else
        {
            //Enemy Turn to Player
            Vector3 PlayerAndEnemyDifference = target.transform.position - transform.position;
            float rotationZ = Mathf.Atan2(PlayerAndEnemyDifference.y, PlayerAndEnemyDifference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ-90);
        }
    }

    //EnemyShooting
    void EnemyFire()
    {
        objectsPool.SpawnFromPool("EnemyBullet", FirePoint.position, FirePoint.rotation);
        FireTime += 1;
    }

    //EnemyReloadShooting
    IEnumerator ReloadFire()
    {
        yield return new WaitForSeconds(2f);
        InvokeRepeating("EnemyFire", 0f, 0.1f);
    }
}
