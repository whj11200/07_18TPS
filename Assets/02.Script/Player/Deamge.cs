using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deamge : MonoBehaviour
{
    private readonly string E_BulletTag = "E_Bullet";

    void Start()
    {


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(E_BulletTag))
        {
            collision.gameObject.SetActive(false);
        }
    }
}
