using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectingObjectController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 2);
    }
}
