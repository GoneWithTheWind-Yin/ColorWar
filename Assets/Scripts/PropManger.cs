using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class PropManger : MonoBehaviour
{
    public PropData FrozenPropData;
    public PropData TransportationPropData;
    private PropData selectedPropData;
    private bool isUse = false;

    public PropCD TransportationCD;
    public PropCD FrozenCD;

    private void Start()
    {
        TransportationCD.setCD(TransportationPropData.CD);
        FrozenCD.setCD(FrozenPropData.CD);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("RoadCube"));
            if (isCollider)
            {

                if (selectedPropData != null && isUse)
                {
                    if (GetComponent<GameManage>().GetMoney() >= selectedPropData.cost)
                    {
                        if(selectedPropData == FrozenPropData) UseFrozenProp(hit.point);
                        else if (selectedPropData == TransportationPropData) UseTransportationProp(hit.point);

                        GetComponent<GameManage>().DeductMoney(selectedPropData.cost);
                    }
                    else
                    {
                        // TODO 提示金钱不够
                    }
                }
                isUse = false;
            }
        }

    }
    public void OnFrozenSelected()
    {
        if (FrozenCD.canUsed())
        {
            isUse = true;
            selectedPropData = FrozenPropData;
            FrozenCD.skillUsed();
        }
    }

    public void OnTransportationSelected()
    {
        if (TransportationCD.canUsed()) {
            isUse = true;
            selectedPropData = TransportationPropData;
            TransportationCD.skillUsed();
        }
    }

    void UseFrozenProp(Vector3 position) {
        GameObject prop =  Instantiate(selectedPropData.PropPrefab, position, Quaternion.identity);
        prop.GetComponent<FrozenProp>().setPropData(selectedPropData);
    }
    void UseTransportationProp(Vector3 position)
    {
        GameObject prop = Instantiate(selectedPropData.PropPrefab, position, Quaternion.identity);
        prop.GetComponent<TransportationProp>().setPropData(selectedPropData);
    }
}
