using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 800f;
    private Transform tr;
    private TrailRenderer trailRenderer;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        tr = transform;
        trailRenderer = GetComponent<TrailRenderer>();

        Invoke("BulletDisavble",2.0f);
    }
    void BulletDisavble()
    {
        this.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        rb.AddForce(tr.forward * speed);
    }
    private void OnDisable()
    {
        trailRenderer.Clear();
        //tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep();
    }


}
