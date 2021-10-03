using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float speed = 10;

    private Transform[] positions;
    private int index = 0;
    public int status = 0;

	// Use this for initialization
	void Start () {
        positions = Waypoints.positions;
	}

	
	// Update is called once per frame
	void Update () {
        Move();
        Lock();
	}

    void Lock() {
        if (Input.GetMouseButtonDown(0)) {
            if (EventSystem.current.IsPointerOverGameObject() == false) {
                // 判断锁定Enemy颜色
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Enemy"));
                // test++;
                if (isCollider) {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    // status为1的时候代表锁定，此时炮台不应该攻击，颜色也不会发生改变
                    enemy.status ^= 1;
                }
            }
        }
    }


    void Move()
    {
        if (index > positions.Length - 1) return;
        transform.Translate((positions[index].position - transform.position).normalized * Time.deltaTime * speed);
        if (Vector3.Distance(positions[index].position, transform.position) < 0.2f)
        {
            index++;
        }
        if (index > positions.Length - 1) {
            ReachDestination();
        }
    }

    void ReachDestination() {
        // 如果颜色一致的话进行加分操作 如果颜色不一致的话进行扣分操作
        // 现阶段默认基地颜色为绿色
        Color baseColor = new Color(0, 1, 0, 1);
        if (this.gameObject.GetComponent<Renderer>().material.color == baseColor) {
            GameManage.Bouns(10);
        } else {
            // 掉血
            GameManage.Damage(1);
        }

        GameObject.Destroy(this.gameObject);
    }

    void OnDestroy() {
        EnemySpawner.CountEnemyAlive--;
    }

    public void ChangeColor(Color color) {
        this.gameObject.GetComponent<Renderer>().material.color=color;
    }

    public bool CheckStatus() {
        return this.status == 0;
    }
}
