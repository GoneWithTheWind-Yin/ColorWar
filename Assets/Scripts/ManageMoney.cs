using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageMoney : MonoBehaviour {
    public Text moneyText;

    public static int money = 100;

    public void ChangeMoney(int change=0) {
        money += change;
        moneyText.text = "" + money;
    }

    public static void BounsOrDamage(int change=0) {
        money += change;
        // moneyText.text = "" + money;
    }

    public int GetMoney() {
        return money;
    }

    void Update() {
        moneyText.text = "" + money;
    }
}
