using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if ENABLE_CLOUD_SERVICES_ANALYTICS
using UnityEngine.Analytics;
#endif

public class GameManage : MonoBehaviour {
    public Text moneyText;
    public Text hpText;

    public int money = 200;
    public int HP = 10;
    private int moneyCosted = 0;
    private int moneyEarned = 0;
    private bool win = false;
    private int totalColorNotChangeTimes = 0;

    public GameObject endUIWin;
    public GameObject endUIFail;
    public Text endMessage;
    private EnemySpawner enemySpawner;
    private BuildManager buildManager;
    private string sceneName;
    private float noMissMouseDownTimes = 0f;
    private float missMouseDownTimes = 0f;
    private float timer = 0f;
    private bool isOver = false;

    public static GameManage Instance;

    void Awake() {
        Instance = this;
        enemySpawner = GetComponent<EnemySpawner>();
        buildManager = GetComponent<BuildManager>();
        sceneName = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        if (HP >= 0)
        {
            moneyText.text = "$" + money;
            hpText.text = "HP: " + HP;
        }
        timer += Time.deltaTime;
    }

    public static GameManage GetGameManage() {
		return Instance;
	}

    public void DeductMoney(int change=0) {
        money -= change;
        moneyCosted += change;
    }

    public void EarnMoney(int change=0) {
        money += change;
        moneyEarned += change;
    }

    public void Damage(int damage) {
        HP -= damage;
        if (HP <= 0 && !isOver) {
            Instance.Fail();
        }
    }

    public int GetMoney() {
        return money;
    }

    public void UpdateNoMissMouseDownTimes() {
        noMissMouseDownTimes++;
    }

    public void UpdateMissMouseDownTimes()
    {
        missMouseDownTimes++;
    }

    public void UpdateTotalColorNotChangeTimes()
    {
        totalColorNotChangeTimes++;
    }

    public void Win() {
        win = true;
        isOver = true;
        SendMessage();
        endMessage.text = "YOU WIN!!!";
        endUIWin.SetActive(true);
    }

    public void Fail() {
        win = false;
        isOver = true;
        SendMessage();
        enemySpawner.Stop();
        endMessage.text = "YOU FAIL...";
        endUIFail.SetActive(true);
    }

    public void OnButtonReplay() {
        if (win)
        {
            ReportReplayTimesAfterWin();
        }
        else {
            ReportReplayTimesAfterFail();
        }
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

	public void OnButtonMenu() {
        SceneManager.LoadScene(0);
    }

    public void OnButtonNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1) {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
    }

    // 1
    private void ReportCostedMoney() {
        Debug.Log("Money Costed: " + moneyCosted);
        Analytics.CustomEvent(sceneName + " win", new Dictionary<string, object>
        {
            { "Money Costed", moneyCosted }
        });
    }

    // 2
    private void ReportEarnedMoney() {
        Debug.Log("Money Earned: " + moneyEarned);
        Analytics.CustomEvent(sceneName + " win", new Dictionary<string, object>
        {
            { "Money Earned", moneyEarned }
        });
    }

    // 3
    private void ReportTussle() {
        int numOfStandard = buildManager.GetNumOfStandard();
        int numOfMissile = buildManager.GetNumOfMissile();
        int numofLaser = buildManager.GetNumOfLaser();

        Debug.Log("Standard Turret: " + numOfStandard);
        Debug.Log("Missile Turret: " + numOfMissile);
        Debug.Log("Laser Turret: " + numofLaser);

        Analytics.CustomEvent(sceneName + " win", new Dictionary<string, object>
        {
            { "Standard Turret", numOfStandard},
            { "Missile Turret", numOfMissile},
            { "Laser Turret", numofLaser}
        });
    }

    //4
    private void ReportTotalColorNotChangeTimes() {
        Debug.Log("Color Not Changed Times: " + totalColorNotChangeTimes);
        Analytics.CustomEvent(sceneName + " general", new Dictionary<string, object>
        {
            { "Color Not Changed Times", totalColorNotChangeTimes }
        });
    }

    // 5
    private void ReportGameResults() {
        string result = win ? "Win Times" : "Fail Times";
        Debug.Log(result);
        Analytics.CustomEvent(sceneName + " general", new Dictionary<string, object>
         {
             {"Total Times", 1},
             {result, 1},
         });
    }

    // 6
    private void ReportMouseClickMissRate() {
        float mouseMissingRate;

        if ((missMouseDownTimes + noMissMouseDownTimes) == 0)
        {
            mouseMissingRate = 0f;
        }
        else {
            mouseMissingRate = missMouseDownTimes / (missMouseDownTimes + noMissMouseDownTimes);
            mouseMissingRate = (float) Math.Round(mouseMissingRate, 2);
        }

        Debug.Log("Mouse Click Miss Rate: " + mouseMissingRate);
        Analytics.CustomEvent(sceneName + " general", new Dictionary<string, object>
         {
             { "Mouse Click Miss Rate", mouseMissingRate }
         });
    }

    // 7
    private void ReportRemainingHP() {
        Debug.Log("HP: " + HP);
        Analytics.CustomEvent(sceneName + " win", new Dictionary<string, object>
         {
             { "HP", HP }
         });
    }

    // 8
    private void ReportReplayTimesAfterWin() {
        Debug.Log("Replay After Win");
        Analytics.CustomEvent(sceneName + " win", new Dictionary<string, object>
         {
             { "Replay Times After Win", 1}
         });
    }
    private void ReportReplayTimesAfterFail()
    {
        Debug.Log("Replay After Fail");
        Analytics.CustomEvent(sceneName + " fail", new Dictionary<string, object>
         {
             { "Replay Times After Fail", 1},
         });
    }

    // 9
    private void ReportFailedWave()
    {
        int wave = EnemySpawner.GetWave();
        Debug.Log("Failed Wave: " + wave);
        Analytics.CustomEvent(sceneName + " fail", new Dictionary<string, object>
         {
             {"Failed Wave", wave}
         });
    }

    // 10
    private void ReportFailedTime() {
        Debug.Log("Failed Time: " + timer);
        Analytics.CustomEvent(sceneName + " fail", new Dictionary<string, object>
         {
             {"Failed Time", timer}
         });
    }

    private void SendMessage() {
#if ENABLE_CLOUD_SERVICES_ANALYTICS
        Debug.Log("analytics");
        ReportGameResults();
        ReportMouseClickMissRate();
        ReportTotalColorNotChangeTimes();
        if (win) {
            ReportCostedMoney();
            ReportEarnedMoney();
            ReportTussle();
            ReportRemainingHP();
        }
        else {
            ReportFailedWave();
            ReportFailedTime();
        }
#endif
    }
}
