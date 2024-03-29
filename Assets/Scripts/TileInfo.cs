﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileInfo : MonoBehaviour
{
    public bool isEnd;
    public bool cubePlaced;
    public Material endMaterial;

    void Start()
    {
        if(isEnd)
        {
            var tileRenderer = gameObject.GetComponent<Renderer>();

            //Call SetColor using the shader property name "_Color" and setting the color to green
            tileRenderer.material = endMaterial;
        }
    }
}
