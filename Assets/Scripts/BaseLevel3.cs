using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseLevel3 : Base
{
    public Color[] colorArr = new Color[] { Color.yellow, Color.red, Color.green };
    private int index = 0;
    private int colorChangeInterval = 15;
    private float timer = 0;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= colorChangeInterval)
        {
            timer -= colorChangeInterval;
            UpdateColor();

        }
    }

    private void UpdateColor()
    {
        this.gameObject.GetComponent<Renderer>().material.color = colorArr[index];
        index++;
        index %= 3;
    }
}
