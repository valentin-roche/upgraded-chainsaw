using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject projectorPrefab;
    private int count = 0;

    public List<GameObject> projectorList = new List<GameObject>();

    [SerializeField]
    private List<SpriteColor> colors;                                       // Les différentes couleurs du jeu
    private List<SpriteColor> colorsLeft;                                   // Les couleurs pas encore dans la liste des projecteurs

    private void Start()
    {
       colorsLeft = colors;
       SpawnProjector();
       SpawnProjector();
       SpawnProjector();
    }

    private void Update()
    {

    }

    public void SpawnProjector()
    {
        float mX = Random.Range(-15f, 15f);
        float mY = Random.Range(-6f, 6f);
        Vector2 pos = new Vector2(mX, mY);

        GameObject projector = Instantiate(projectorPrefab, pos, Quaternion.identity, transform);
        projector.GetComponent<ProjectorController>().SetId(count);
        count++;
        int randInt = Random.Range(0, colorsLeft.Count);
        projector.GetComponent<ProjectorController>().ChangeProjectorColor(colorsLeft[randInt]);
        colorsLeft.Remove(colorsLeft[randInt]);
        projectorList.Add(projector);
    }
}
