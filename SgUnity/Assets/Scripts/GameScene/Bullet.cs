using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float BulletSpeed;

    void Start()
    {
        StartCoroutine("Hide");
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * BulletSpeed);
    }

    //Hide Bullet
    IEnumerator Hide()
    {
        yield return new WaitForSeconds(5f);
        gameObject.SetActive(false);
    }
}
