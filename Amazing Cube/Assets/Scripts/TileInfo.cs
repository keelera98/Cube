using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInfo : MonoBehaviour
{
    public bool isEnd;

    void Start()
    {
        if(isEnd)
        {
            var tileRenderer = gameObject.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to red
            tileRenderer.material.SetColor("_Color", Color.green);
        }
    }
}
