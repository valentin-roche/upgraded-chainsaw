using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Colors color;
    private Colors actualColor;

    private SpriteRenderer sprite;

    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        actualColor = color;
        float randX = Random.Range(-1f, 1f); 
        float randY = Random.Range(-1f, 1f);
        Vector2 vectorDirection = new Vector2(randX, randY);

        float randModification = Random.Range(1f, 2f);
        speed += randModification;

        rb.velocity = vectorDirection * speed;

        sprite = GetComponentInChildren<SpriteRenderer>();
        switch (color)
        {
            case Colors.White:
                sprite.material.SetColor("Color", Color.white);
                sprite.material.SetColor("_EmissionColor", Color.white);

                break;
            case Colors.Red:
                sprite.material.SetColor("_Color", Color.red);
                sprite.material.SetColor("_EmissionColor", Color.red * 1.75f);
                break;
            case Colors.Green:
                sprite.material.SetColor("_Color", Color.green);
                sprite.material.SetColor("_EmissionColor", Color.green * 1.75f);
                break;
            case Colors.Blue:
                sprite.material.SetColor("_Color", Color.blue);
                sprite.material.SetColor("_EmissionColor", Color.blue * 1.75f);
                break;
            case Colors.Pink:
                sprite.material.SetColor("_Color", Color.magenta);
                sprite.material.SetColor("_EmissionColor", Color.magenta * 1.75f);
                break;
        }
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
            switch (actualColor)
            {
                case Colors.White:
                    sprite.color = Color.white;
                    break;
                case Colors.Red:
                    sprite.color = Color.red;
                    break;
                case Colors.Green:
                    sprite.color = Color.green;
                    break;
                case Colors.Blue:
                    sprite.color = Color.blue;
                    break;
                case Colors.Pink:
                    sprite.color = Color.magenta;
                    break;
            }
        }

        if(collision.CompareTag("Wall") || collision.CompareTag("Door"))
        {
            print("collision détectée");
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
            var newDirection = Vector2.Reflect(rb.velocity, normal);
            rb.velocity = newDirection;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Projector"))
        {
            ChangeSelfColor(color);
            switch (color)
            {
                case Colors.White:
                    sprite.color = Color.white;
                    break;
                case Colors.Red:
                    sprite.color = Color.red;
                    break;
                case Colors.Green:
                    sprite.color = Color.green;
                    break;
                case Colors.Blue:
                    sprite.color = Color.blue;
                    break;
                case Colors.Pink:
                    sprite.color = Color.magenta;
                    break;
            }
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
}
