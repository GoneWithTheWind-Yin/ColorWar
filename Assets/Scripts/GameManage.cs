using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManage : MonoBehaviour {
    public Text moneyText;
    public Text hpText;

    public static int money = 100;
    public static int HP = 10;

    public GameObject endUI;
    public Text endMessage;
    private EnemySpawner enemySpawner;

    public static GameManage Instance;

    void Awake() {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
    }

    public void ChangeMoney(int change=0) {
        money += change;
        moneyText.text = "" + money;
    }

    public static void Bouns(int change=0) {
        money += change;
    }

    public static void Damage(int damage) {
        HP -= damage;
        if (HP <= 0) {
            Instance.Fail();
        }
    }

    public int GetMoney() {
        return money;
    }

    void Update() {
        moneyText.text = "$" + money;
        hpText.text = "HP: " + HP;
    }

    public void Win() {
        Time.timeScale = 0;
        endMessage.text = "WIN";
        endUI.SetActive(true);
    }

    public void Fail() {
        // enemySpawner.Stop();
        Time.timeScale = 0;
        endMessage.text = "GAME OVER";
        endUI.SetActive(true);
    }
}
