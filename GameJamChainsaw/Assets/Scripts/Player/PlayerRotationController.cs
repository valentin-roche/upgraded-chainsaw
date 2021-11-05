using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationController : MonoBehaviour
{
    private Vector3 mousePosition;                                    // La position de la souris dans le monde
    private Vector3 direction = new Vector3(0, 0, 0);                 // La direction vers laquelle est la souris 
    private Transform mainCamera;

    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    {
        // On convertit la position de la souris en coordonnées
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.nearClipPlane;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Shenanigan dû à la 2D
        mousePosition *= -mainCamera.position.z;

        // On calcule la direction
        direction.x = mousePosition.x - transform.position.x;
        direction.y = mousePosition.y - transform.position.y;
        direction = direction.normalized;

        print("transform : " + transform.position);
        print("mouse position : " + mousePosition);
        print("direction : " + direction);

        // et on modifie directement le transform
        transform.up = direction;
    }
}
