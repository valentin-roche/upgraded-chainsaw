using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorImpulsion : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    private float speed = 300;

    void Start()
    {
        float randX = Random.Range(-0.5f, 0.5f);
        float randY = Random.Range(-0.5f, 0.5f);
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(randX * 20 * Time.deltaTime * speed, randY * 20 * Time.deltaTime * speed));
    }

    void Update()
    {
        
    }
}
