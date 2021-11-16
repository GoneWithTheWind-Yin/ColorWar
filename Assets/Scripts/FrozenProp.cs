using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenProp : MonoBehaviour
{
    public float timer = 0;
    private PropData propData;
    private bool isfinish = false;
    //public List<GameObject> affectList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            GameObject enemy = col.gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > propData.duration && !isfinish) {
            propFinish();
            isfinish = true;
        }
    }

    public void setPropData(PropData PropData) {
        this.propData = PropData;
    }

    public PropData getPropData()
    {
        return this.propData;
    }
    void propFinish() {

        GameObject.Destroy(this.gameObject);
    }
}
