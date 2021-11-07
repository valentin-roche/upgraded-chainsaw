using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    private Rigidbody2D rb;

    public SpriteColor color;
    private SpriteColor actualColor;
    private Vector3 lastVelocity;
    private int overlapping;

    private SpriteRenderer sprite;

    private SpriteColor whiteColor;         // A SUPPRIMER C UN PLACEHOLDER POUR REMPLACER LA LOGIQUE ACTUELLE

    void Start()
    {
        // CODE PLACEHOLDER
        CodeRGB rgb;
        rgb.r = 1;
        rgb.g = 1;
        rgb.b = 1;
        whiteColor.rgbCode = rgb;
        whiteColor.color = Colors.White;
        // code placeholder

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
            collision.GetComponent<PlayerCollisionController>().ChangeColor(whiteColor);
        }
    }

    private void ChangeSelfColor(SpriteColor newColor)
    {
        actualColor = newColor;
    }

    private SpriteColor GetColorMix(SpriteColor color1, SpriteColor color2)
    {
        // A remplir pour mixer les couleurs des projecteurs

        return whiteColor;
    }


    void SetProjectorColor(SpriteColor colorProj)
    {
        sprite.material.SetColor("_Color", new Color(colorProj.rgbCode.r / 255f, colorProj.rgbCode.g / 255f, colorProj.rgbCode.b / 255f));
        sprite.material.SetColor("_EmissionColor", new Color(colorProj.rgbCode.r / 255f, colorProj.rgbCode.g / 255f, colorProj.rgbCode.b / 255f) * 1.22f);
    }
}
