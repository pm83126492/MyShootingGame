using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public bool isEnemy;

    protected virtual void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * BulletSpeed);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Enemy") && !isEnemy)|| other.gameObject.CompareTag("Bottom"))
        {
            gameObject.SetActive(false);
        }
    }
}
