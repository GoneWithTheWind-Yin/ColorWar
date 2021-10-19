using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret {
    public LineRenderer laserRenderer;

    public override bool Attack(GameObject enemy) {
		if (enemy == null) {
			return false;
		}
        if (laserRenderer.enabled == false) {
            laserRenderer.enabled = true;
        }
        float speed = enemy.GetComponent<Enemy>().originSpeed;
        enemy.GetComponent<Enemy>().ChangeSpeed(speed / 2);
		laserRenderer.SetPositions(new Vector3[]{firePosition.position, enemy.transform.position});
        return true;

    }

    public override void Show() {
        laserRenderer.enabled = false;
    }

    // 防止选择一个目标需要修改
    
    // public override bool CheckSlow(GameObject enemy) {
    //     return !enemy.GetComponent<Enemy>().IsSlowed();
    // }
}
