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

        if (randX <= 0f)
            randX -= 0.5f;
        else
            randX += 0.5f;

        if (randY <= 0f)
            randY -= 0.5f;
        else
            randY += 0.5f;

        print(randX);
        print(randY);


        rb = GetComponent<Rigidbody2D>();
        //rb.AddForce(new Vector2(randX * 20 * Time.deltaTime * speed, randY * 20 * Time.deltaTime * speed));
        rb.AddForce(new Vector2(randX * speed, randY * speed));
    }

    void Update()
    {
        
    }
}
