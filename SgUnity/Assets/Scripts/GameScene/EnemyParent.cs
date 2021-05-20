using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyParent : MonoBehaviour
{
    public ObjectsPool objectsPool;
    public Transform[] FirePoint;
    public float MoveSpeed;
    public GameObject target;
    public bool isTurnToPlayer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        target = GameObject.Find("Player");
        objectsPool = GameObject.Find("BulletObjectPool").GetComponent<ObjectsPool>();
        FirePoint = GetComponentsInChildren<Transform>().Where(item => item != this.transform).ToArray();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            PlayerController.Score += 1;
            isTurnToPlayer = false;
            gameObject.SetActive(false);
            objectsPool.SpawnFromPool("ExplosionEffect", transform.position, transform.rotation);
            CancelInvoke("EnemyFire");
        }

        if (other.gameObject.CompareTag("Bottom"))
        {
            gameObject.SetActive(false);
            CancelInvoke("EnemyFire");
        }
    }

    //EnemyShooting
    public virtual void Shoot(string bulletTag,Vector3 pos, Quaternion dir)
    {
        objectsPool.SpawnFromPool(bulletTag, pos, dir);
    }

    //EnemyMoving
    public virtual void Move(float moveSpeed)
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }

    public virtual void InvokeShoot(string Name,float FirstTime,float ReplayTime)
    {
        InvokeRepeating("EnemyFire", FirstTime, ReplayTime);
    }
}
