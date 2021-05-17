using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rigidbody2;
    void Start()
    {
        StartCoroutine("test");
    }

    void Update()
    {
        transform.Translate(transform.up * 10 * Time.deltaTime);
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
