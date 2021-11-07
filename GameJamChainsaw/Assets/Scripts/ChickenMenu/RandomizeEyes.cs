using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeEyes : MonoBehaviour
{
    float timeBetweenAnims = 2f;
    bool openEyesState = true;
    [SerializeField]
    private Animator chickenEyesAnims;
    // Start is called before the first frame update
    void Start()
    {
        chickenEyesAnims = gameObject.GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        timeBetweenAnims -= Time.deltaTime;
        if (timeBetweenAnims < 0)
        {
            openEyesState = !openEyesState;
            chickenEyesAnims.SetBool("OpenEyes",openEyesState);
            timeBetweenAnims = Random.Range(6f, 30f);

        }
    }
}
