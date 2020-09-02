using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blast : MonoBehaviour
{
    void Awake()
    {
        Vector3 direction = new Vector3(3.3f,2.9f,0)- transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0,0,angle);
    }

}
