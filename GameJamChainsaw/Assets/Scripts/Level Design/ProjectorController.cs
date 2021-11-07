using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorController : MonoBehaviour
{
    private Rigidbody2D rb;
    private int id;

    // color c'est la couleur c'est censé être et actualColor c'est la couleur actuel de mix
    public SpriteColor color;
    public SpriteColor actualColor;
    private Vector3 lastVelocity;
    private int overlapping = 0;

    public SpriteRenderer sprite;

    [SerializeField]
    private SpriteColor whiteColor;         
    [SerializeField]
    private SpriteColor yellowPinkColor;         
    [SerializeField]
    private SpriteColor yellowBlueColor;         
    [SerializeField]
    private SpriteColor pinkBlueColor;
    [SerializeField]
    private SpriteColor pinkBlueYellowColor;

    void Start()
    {
        rb = gameObject.GetComponentInChildren<Rigidbody2D>();
        actualColor = color;

        //sprite = GetComponent<SpriteRenderer>();
        //SetProjectorColor(actualColor);
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollisionController>().AddProjector(this);
        }

        if(collision.CompareTag("Projector"))
        {
            overlapping++;
            ChangeSelfColor(GetColorMix(actualColor, collision.GetComponent<ProjectorController>()));
            SetProjectorColor(actualColor);
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
            overlapping--;
            if(overlapping <= 0)
            {
                ChangeSelfColor(color);
            }
            else if(overlapping == 1)
            {
                GetColorMixInverse(actualColor, collision.GetComponent<ProjectorController>().color);
            }
            SetProjectorColor(actualColor);
        }

        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerCollisionController>().RemoveProjector(this);
        }
    }

    private void ChangeSelfColor(SpriteColor newColor)
    {
        actualColor = newColor;
    }

    private SpriteColor GetColorMix(SpriteColor color1, ProjectorController projectorColor)
    {
        SpriteColor color2 = projectorColor.GetActualColor();
        if ((color1.color == Colors.Pink || color2.color == Colors.Pink) && (color1.color == Colors.Blue || color2.color == Colors.Blue))
            return pinkBlueColor;
        else if ((color1.color == Colors.Pink || color2.color == Colors.Pink) && (color1.color == Colors.Yellow || color2.color == Colors.Yellow))
            return yellowPinkColor;
        else if ((color1.color == Colors.Yellow || color2.color == Colors.Yellow) && (color1.color == Colors.Blue || color2.color == Colors.Blue))
            return yellowBlueColor;
        else if (overlapping == 1 && projectorColor.GetOverlapping() == 1)
            return projectorColor.GetActualColor();
        else
            return pinkBlueYellowColor;
    }

    private SpriteColor GetColorMixInverse(SpriteColor color1, SpriteColor color2)
    {
        if (color1.color == Colors.PinkBlueYellow && color2.color == Colors.Blue)
            return yellowPinkColor;
        else if (color1.color == Colors.PinkBlueYellow && color2.color == Colors.Yellow)
            return pinkBlueColor;
        else 
            return yellowBlueColor;
    }


    void SetProjectorColor(SpriteColor colorProj)
    {
        if (sprite != null)
        {
            sprite = GetComponent<SpriteRenderer>();
        }

        sprite.material.SetColor("_Color", new Color(colorProj.rgbCode.r / 255f, colorProj.rgbCode.g / 255f, colorProj.rgbCode.b / 255f, 0.1f));
        sprite.material.SetColor("_EmissionColor", new Color(colorProj.rgbCode.r / 255f, colorProj.rgbCode.g / 255f, colorProj.rgbCode.b / 255f, 0.1f) * 6f);
    }

    public void ChangeProjectorColor(SpriteColor newTrueColor)
    {
        color = newTrueColor;
        ChangeSelfColor(color);
        sprite = GetComponent<SpriteRenderer>();
        SetProjectorColor(actualColor);
    }

    public void SetId(int projectorId)
    {
        id = projectorId;
    }

    public int GetIt()
    {
        return id;
    }

    public SpriteColor GetActualColor()
    {
        return actualColor;
    }

    public int GetOverlapping()
    {
        return overlapping;
    }
}
