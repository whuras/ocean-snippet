using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialVariation : MonoBehaviour
{
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();

        float scaleX = Random.Range(0.5f, 1.5f);
        float scaleY = Random.Range(0.5f, 1.5f);
        rend.materials[0].mainTextureScale = new Vector2(scaleX, scaleY);
    }
}
