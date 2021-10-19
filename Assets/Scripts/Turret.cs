using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
	public List<GameObject> attackList = new List<GameObject>();
	
	// TODO 检测敌人的逻辑需要修改 如果敌人在攻击范围内时建造会导致不在攻击列表
	// 可以考虑在Start进行一个检测操作
    public void OnTriggerEnter(Collider col) {
        if (col.tag == "Enemy") {
			attackList.Add(col.gameObject);
        }
    }

    public void OnTriggerExit(Collider col) {
        if (col.tag == "Enemy") {
			attackList.Remove(col.gameObject);
        }
    }

    //多少秒攻击一次
	public float bulletLoadingTime = 2;
    public float timer = 0;
    public List<GameObject> bulletPrefabList = new List<GameObject>();
    public Transform firePosition;
    public int times = 0;
    public Transform head;

    public void Start() {
		timer = bulletLoadingTime;
    }


    public void Update() {
		RemoveNull();

		if (timer < bulletLoadingTime) {
			timer += Time.deltaTime;
		}

		// 指定Enemy为集合当中第一个未被锁定的
		GameObject enemy = null;
		for (int i = 0; i < attackList.Count; ++i) {
			GameObject temp = attackList[i];
			bool isLocked = temp.GetComponent<Enemy>().IsLocked();
			if (!isLocked && CheckSlow(temp)) {
				enemy = attackList[i];
				break;
			}
		}

		if (enemy != null) {
			// TODO currently only support standard turret
			if (head != null) {
				Vector3 targetPosition = enemy.transform.position;
				targetPosition.y = head.position.y;
				head.LookAt(targetPosition);
			}

			// 生成子弹的逻辑也要改为集合中存在未被锁定的敌人
			if (timer >= bulletLoadingTime) {
				bool attackSuccess = Attack(enemy);
				if (attackSuccess) {
					timer = 0;
				}
			}
		} else {
			Show();
		}
    }

	public virtual void Show() {}

	public virtual bool CheckSlow(GameObject enemy) {
		return true;
	} 

    // 把伤害改为染色操作
	public virtual bool Attack(GameObject enemy) {
		if (enemy == null) {
			return false;
		}
		// TODO currently only support standard turret
		if (bulletPrefabList.Count == 0) {
			return false;
		}
        GameObject bullet = GameObject.Instantiate(bulletPrefabList[times], firePosition.position, firePosition.rotation);
        times++;
        times %= bulletPrefabList.Count;

        bullet.GetComponent<Bullet>().SetTarget(enemy.transform);

        return true;

    }

	public void RemoveNull() {
		int i = 0;
		while (i < attackList.Count) {
			if (attackList [i] == null) {
				attackList.RemoveAt (i);
			} else {
				i++;
			}
		}
    }
}
