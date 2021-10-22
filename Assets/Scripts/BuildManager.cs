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
    private TurretData selectedTurretData;

    private int numOfStandard = 0;
    private int numOfMissile = 0;
    private int numOfLaser = 0;

    private GameObject selectedTurretGo;//表示当前选择的炮台（场景中的物体）
    private Animator destroyCanvasAnimator;


    // public Text moneyText;

    // public int money = 100;

    // public void ChangeMoney(int change=0) {
    //     money += change;
    //     moneyText.text = "" + money;
    // }

    public Animator moneyAnimator;

    public GameObject destroyCanvas;
    public Button buttonDestroy;

    void Start()
    {
        destroyCanvasAnimator = destroyCanvas.GetComponent<Animator>();
    }

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
                        if (GetComponent<GameManage>().GetMoney() >= selectedTurretData.cost) {
                            GetComponent<GameManage>().DeductMoney(selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                            UpdateNumOfTurret(selectedTurretData);
                        } else {
                            // TODO 提示金钱不够
                            moneyAnimator.SetTrigger("Flicker");
                        }
					} else if (mapCube.turretGo != null){
                        
                       // if(mapCube.turretGo == selectedTurretGo && destroyCanvas.activeInHierarchy)
                      //  {
                      //      StartCoroutine(HideDestroyUI());
                    //    }
                    //    else
                    //    {
                    //        ShowDestroyUI(mapCube.transform.position);
                   //     }
                   //     selectedTurretGo = mapCube.turretGo;
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

    private void UpdateNumOfTurret(TurretData selectedTurretData) {
        if (selectedTurretData.type == TurretType.StandardTurret)
        {
            numOfStandard++;
        }
        else if (selectedTurretData.type == TurretType.MissileTurret)
        {
            numOfMissile++;
        }
        else if (selectedTurretData.type == TurretType.LaserTurret) {
            numOfLaser++;
        }
    }

    public int GetNumOfStandard() {
        return numOfStandard;
    }

    public int GetNumOfMissile() {
        return numOfMissile;
    }

    public int GetNumOfLaser() {
        return numOfLaser;
    }

    void ShowDestroyUI(Vector3 pos)
    {
        StopCoroutine("HideDestroyUI");
        destroyCanvas.SetActive(false);
        destroyCanvas.SetActive(true);
        destroyCanvas.transform.position = pos;
        buttonDestroy.interactable = true;
    }

    IEnumerator HideDestroyUI()
    {
        destroyCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.8f);
        destroyCanvas.SetActive(false);

    }

    public void OnDestroyButtonDown()
    {

    }


}
