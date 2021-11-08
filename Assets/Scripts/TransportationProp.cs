using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportationProp : MonoBehaviour
{
    private PropData propData;
    public float timer = 0;
    private bool isfinish = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            GameObject enemy = col.gameObject;
            enemy.GetComponent<Enemy>().Transport();
        }
    }
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
