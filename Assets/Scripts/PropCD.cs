using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropCD : MonoBehaviour
{
    private float currentTime = 0;
    private float SkillCD;
    private bool isCD = false;


    void Update()
    {
        if (isCD){
            currentTime += Time.deltaTime;
            if (currentTime > SkillCD)
            {
                isCD = false;
                currentTime = 0;
            }
        }
    }

    public void skillUsed() {
        isCD = true;
    }

    public void setCD(float cd) {
        this.SkillCD = cd;
    }

    public bool canUsed() {
        return !isCD;
    }
}