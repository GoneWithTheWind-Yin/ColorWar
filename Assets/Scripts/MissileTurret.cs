using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileTurret : Turret {

    // 把伤害改为染色操作，复写Turret方法
	public override bool Attack(GameObject enemy) {
		if (enemy == null) {
			return false;
		}
		// TODO currently only support standard turret
		if (bulletPrefabList.Count == 0) {
			return false;
		}
        GameObject bullet = GameObject.Instantiate(bulletPrefabList[0], firePosition.position, firePosition.rotation);

        bullet.GetComponent<Bullet>().SetTarget(enemy.transform);

        return true;

    }
}
