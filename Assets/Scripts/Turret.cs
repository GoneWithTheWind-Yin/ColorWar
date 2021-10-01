using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    void OnTriggerEnter(Collider col) {
        if (col.tag == "Enemy") {
            enemys.Add(col.gameObject);
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.tag == "Enemy") {
            enemys.Remove(col.gameObject);
        }
    }

    //多少秒攻击一次
    public float attackRateTime = 2;
    public float timer = 0;
    public List<GameObject> bulletPrefabList = new List<GameObject>();
    public Transform firePosition;
    public int times = 0;
    public Transform head;

    void Start() {
        timer = attackRateTime;
    }


    void Update() {
        timer += Time.deltaTime;
        // 生成子弹的逻辑也要改为集合中存在未被锁定的敌人
        if (enemys.Count > 0 && timer >= attackRateTime) {
            bool flag = Attack();
            if (flag) {
                timer = 0;
            }
        }

    }

    // 把伤害改为染色操作

    bool Attack() {
        // 确保整个enemys中没有为null的
        UpdateEnemys();
        if (enemys.Count == 0) {
            timer = attackRateTime;
            return false;
        }

        // 指定Enemy为集合当中第一个未被锁定的
        GameObject enemy = null;
        for (int i = 0; i < enemys.Count; ++i) {
            GameObject temp = enemys[i];
            bool flag = temp.GetComponent<Enemy>().CheckStatus();
            if (flag) {
                enemy = enemys[i];
                break;
            }
        }

        if (enemy == null) {
            return false;
        }

        Vector3 targetPosition = enemy.transform.position;
        targetPosition.y = head.position.y;
        head.LookAt(targetPosition);

        // 可以设置不同的bullet的prefab，搞成一个list去在里面遍历
        GameObject bullet = GameObject.Instantiate(bulletPrefabList[times], firePosition.position, firePosition.rotation);
        times++;
        times %= bulletPrefabList.Count;

        bullet.GetComponent<Bullet>().SetTarget(enemy.transform);

        return true;

    }

    void UpdateEnemys() {
        List<int> emptyList = new List<int>();
        for (int i = 0; i < enemys.Count; ++i) {
            if (enemys[i] == null) {
                emptyList.Add(i);
            }
        }

        for (int i = 0; i < emptyList.Count; ++i) {
            enemys.RemoveAt(emptyList[i] - i);
        }
    }
}
