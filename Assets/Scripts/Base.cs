using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public static Base baseInstance;
    // Start is called before the first frame update
    void Start()
    {
        baseInstance = this;
    }

    public static Base GetBase() {
        return baseInstance;
    }

    public Color GetBaseColor() {
        return this.gameObject.GetComponent<Renderer>().material.color;
    }
}
