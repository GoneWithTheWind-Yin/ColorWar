using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PropCD : MonoBehaviour
{
    private float currentTime = 0;
    private float SkillCD;
    private bool isCD = false;
    private Image cdImage;

    void Start()
    {
        cdImage = transform.GetComponent<Image>();
    }


    void Update()
    {
        if (isCD){
            currentTime += Time.deltaTime;
            cdImage.fillAmount = 1-(currentTime / SkillCD);

            if (currentTime > SkillCD)
            {
                isCD = false;
                currentTime = 0;
                cdImage.fillAmount = 0f;
            }
        }
    }

    public void skillUsed() {
        isCD = true;
        cdImage.fillAmount = 1f;
    }

    public void setCD(float cd) {
        this.SkillCD = cd;
    }

    public bool canUsed() {
        return !isCD;
    }
}