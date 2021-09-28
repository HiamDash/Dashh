using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{

    public GameObject Landssmall;
    public GameObject Landsbig;
    public GameObject Landsmiddle;

    public Transform GeneratorPointX;
    public Transform GeneratorPointX1;
    public Transform GeneratorPointY;
    public Transform GeneratorPointY1;

    public float distanceBetween;

    float platformWidthLandssmall;
    float platformWidthLandsbig;
    float platformWidthLandsmiddle;

       float randX;

    void Start()
    {
        platformWidthLandssmall = Landssmall.GetComponent<BoxCollider2D>().size.x;
        randX = Random.Range (-10f, 15f);
    }

  
    void Update()
    {
        if(transform.position.x < GeneratorPointX.position.x)
        {
            transform.position = new Vector3(randX + platformWidthLandssmall + distanceBetween, transform.position.y, transform.position.z);
            Instantiate (Landssmall, transform.position , transform.rotation);
        }
    }
}
