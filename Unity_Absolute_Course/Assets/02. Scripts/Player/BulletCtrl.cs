using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{

    //총알의 파괴력
    public float damage = 20.0f;
    //총알의 발사 속도
    public float speed = 1000.0f;

    private Transform tr;
    private Rigidbody rb;
    private TrailRenderer trail;


    private void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        trail = GetComponent<TrailRenderer>();
    }

    private void OnEnable()
    {
        rb.AddForce(transform.forward * speed);
    }
    private void OnDisable()
    {
        trail.Clear();
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rb.Sleep();
    }
}
