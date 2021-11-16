using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;


public class PropManger : MonoBehaviour
{
    public PropData TransportationPropData;
    private PropData selectedPropData;
    private bool isUse = false;
    private Transform startposition;

    public PropCD TransportationCD;

    private void Start()
    {
        startposition = Waypoints.positions[0];
        TransportationCD.setCD(TransportationPropData.CD);
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
                        if (selectedPropData == TransportationPropData) UseTransportationProp(hit.point);

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

    public void OnTransportationSelected()
    {
        if (TransportationCD.canUsed()) {
            isUse = true;
            selectedPropData = TransportationPropData;
            TransportationCD.skillUsed();
        }
    }

    void UseTransportationProp(Vector3 position)
    {
        GameObject prop = Instantiate(selectedPropData.PropPrefab, position, Quaternion.identity);
        GameObject prop2 = Instantiate(selectedPropData.PropPrefabNoFun, startposition.position, Quaternion.identity);

        prop.GetComponent<TransportationProp>().setPropData(selectedPropData);
        prop2.GetComponent<TransportationNoFun>().setPropData(selectedPropData);
    }
}
