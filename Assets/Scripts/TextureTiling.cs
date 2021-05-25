using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureTiling : MonoBehaviour
{
    //private Renderer renderer;
    private Material material;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.mainTextureScale = new Vector2(material.mainTextureScale.x * (transform.localScale.x), material.mainTextureScale.y * (transform.localScale.z));
    }
}
