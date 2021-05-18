using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;
    public bool isEnemy;
    void Start()
    {
        //StartCoroutine("Hide");
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * BulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && isEnemy|| other.gameObject.CompareTag("Enemy") && !isEnemy)
        {
            gameObject.SetActive(false);
        }
    }
}
