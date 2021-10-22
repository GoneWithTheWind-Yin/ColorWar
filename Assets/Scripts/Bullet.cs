using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    // 后续把伤害设置为颜色 每一个子弹都有自己的颜色

    public float speed = 20;

    public bool isMissile = false;

    private Transform target;

    private float distanceArriveTarget = 1.2f;

    public void SetTarget(Transform _target) {
        this.target = _target;
    }

    void Update() {

        if (target == null) {
            Destroy(this.gameObject);
            return;
        }

        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude < distanceArriveTarget) {
            if (isMissile) {
                target.GetComponent<Enemy>().ChangeColor(GetComponent<Base>().GetBaseColor());
            } else {
                // 改颜色，把原本的伤害更改为颜色
                target.GetComponent<Enemy>().ChangeColor(this.gameObject.GetComponent<Renderer>().material.color);
            }
            Destroy(this.gameObject);
        }
    }
}
