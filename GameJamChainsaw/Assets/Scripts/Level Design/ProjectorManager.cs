using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorManager : MonoBehaviour
{
    [SerializeField]
    private GameObject projector;

    public bool shouldSpawnProjector = false;
    private int waveNumber;
    private Colors color;
    private List<GameObject> projectorList = new List<GameObject>();

    public void SpawnProjector(Colors colorToApply)
    {
        float mX = Random.Range(-17f, 17f);
        float mY = Random.Range(-8f, 8f);
        Vector2 pos = new Vector2(mX, mY);
        Instantiate(projector, pos, transform.rotation);
        projector.GetComponent<ProjectorController>().ChangeProjectorColor(colorToApply);
        projectorList.Add(projector);
    }
}
