using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewColor : MonoBehaviour
{
    public Color newColor;
    private SpriteRenderer rend;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.color = newColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
