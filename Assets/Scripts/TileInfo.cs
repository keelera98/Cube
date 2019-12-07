using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public bool isEnd;
    private bool isColored = false;
    public bool cubePlaced;

    void Start()
    {
        if(isEnd)
        {
            var tileRenderer = gameObject.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to green
            tileRenderer.material.SetColor("_Color", Color.green);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        isColored = true;
    }

    void Update()
    {
        if (isColored && !isEnd)
        {
            var tileRenderer = gameObject.GetComponent<Renderer>();

            GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            isColored = false;
        }
    }
}
