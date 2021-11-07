using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    private Rigidbody2D rb;

    public Colors color;
    private Colors actualColor;
    private Vector3 lastVelocity;
    private int overlapping;

    private SpriteRenderer sprite;

    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        actualColor = color;
        overlapping = 0;

        sprite = GetComponentInChildren<SpriteRenderer>();
        SetProjectorColor(actualColor);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollisionController>().ChangeColor(actualColor);
        }

        if(collision.CompareTag("Projector"))
        {
            ChangeSelfColor(GetColorMix(color, collision.GetComponent<ProjectorController>().color));
            SetProjectorColor(actualColor);
            overlapping += 1;

            //penser à vérifier overlapping pour checker a couleur à mettre après mix
        }

        if(collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {

            var speed = lastVelocity.magnitude;

            float mX = 0;
            float mY = 0;
            if(collision.gameObject.name.StartsWith("Top") || collision.gameObject.name.StartsWith("Bottom"))
            {
                mX = 0;
                mY = 1;
            }
            else
            {
                mX = 1;
                mY = 0;
            }
            Vector2 normal = new Vector2(mX, mY);
            var newDirection = Vector2.Reflect(lastVelocity.normalized, normal);

            rb.velocity = newDirection * Mathf.Max(speed, 0f);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Projector"))
        {
            overlapping -= 1;
            if(overlapping <= 0)
            {
                ChangeSelfColor(color);
                SetProjectorColor(actualColor);
            }

            //else refaire le mix de couleur
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollisionController>().ChangeColor(Colors.White);
        }
    }

    private void ChangeSelfColor(Colors newColor)
    {
        actualColor = newColor;
    }

    private Colors GetColorMix(Colors color1, Colors color2)
    {
        // A remplir pour mixer les couleurs des projecteurs

        return Colors.White;
    }


    void SetProjectorColor(Colors colorProj)
    {
        switch (colorProj)
        {
            case Colors.White:
                sprite.material.SetColor("Color", Color.white);
                sprite.material.SetColor("_EmissionColor", Color.white);

                break;
            case Colors.Red:
                sprite.material.SetColor("_Color", Color.red);
                sprite.material.SetColor("_EmissionColor", Color.red * 1.22f);
                break;
            case Colors.Green:
                sprite.material.SetColor("_Color", Color.green);
                sprite.material.SetColor("_EmissionColor", Color.green * 1.22f);
                break;
            case Colors.Blue:
                sprite.material.SetColor("_Color", Color.blue);
                sprite.material.SetColor("_EmissionColor", Color.blue * 1.22f);
                break;
            case Colors.Pink:
                sprite.material.SetColor("_Color", Color.magenta);
                sprite.material.SetColor("_EmissionColor", Color.magenta * 1.22f);
                break;
        }
    }
}
