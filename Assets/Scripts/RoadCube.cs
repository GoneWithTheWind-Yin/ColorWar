using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RoadCube : MonoBehaviour
{
    private GameManage gameManage;

    void Start()
    {
        gameManage = GameManage.GetGameManage();
    }

    void OnMouseDown()
    {
        Debug.Log("Miss Click!!!");
        gameManage.UpdateMissMouseDownTimes();
    }
}