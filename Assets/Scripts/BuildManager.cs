using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{
    public TurretData laserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;

    // 表示选择创建的Turret
    public TurretData selectedTurretData;

    // public Text moneyText;

    // public int money = 100;

    // public void ChangeMoney(int change=0) {
    //     money += change;
    //     moneyText.text = "" + money;
    // }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.IsPointerOverGameObject() == false) {
                // 建造炮台
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("MapCube"));
                if (isCollider) {
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData != null && mapCube.turretGo == null) {
                        // 可以创建
                        if (GetComponent<GameManage>().GetMoney() > selectedTurretData.cost) {
                            GetComponent<GameManage>().ChangeMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                        } else {
                            // TODO 提示金钱不够
                        }
                    } else {
                        // TODO 升级处理
                    }
                }
            }
        }
    }

    public void OnLaserSelected(bool isOn) {
        if (isOn) {
            selectedTurretData = laserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn) {
        if (isOn) {
            selectedTurretData = missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn) {
        if (isOn) {
            selectedTurretData = standardTurretData;
        }
    }

}
