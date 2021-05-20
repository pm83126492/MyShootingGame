using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float MoveSpeed;

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * MoveSpeed);
 
        if (transform.position.y <= -14)
        {
            transform.position = new Vector3(0, 18, transform.position.z);
        }
    }
}
