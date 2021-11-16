using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationNoFun : MonoBehaviour
{
    private PropData propData;
    public float timer = 0;
    private bool isfinish = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > propData.duration && !isfinish)
        {
            propFinish();
            isfinish = true;
        }
    }

    public void setPropData(PropData PropData)
    {
        this.propData = PropData;
    }

    public PropData getPropData()
    {
        return this.propData;
    }
    void propFinish()
    {
        GameObject.Destroy(this.gameObject);
    }
}
