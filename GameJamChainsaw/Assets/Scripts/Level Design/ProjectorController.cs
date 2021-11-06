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

        float randModification = Random.Range(-1, 1);
        speed += randModification;

        sprite = GetComponentInChildren<SpriteRenderer>();
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
