using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    #region 欄位區域

    [Header("移動速度")]
    [Range(1, 2000)]
    public int speed = 10;
    [Header("旋轉速度"), Tooltip("英雄的旋轉速度"), Range(1.5f, 200f)]
    public float turn = 20.5f;
    [Header("是否完成任務")]
    public bool mission;
    [Header("玩家名稱")]
    public string _name = "HERO";
    public Transform tran;
    public Rigidbody rig;
    public Animator ani;
    [Header("檢物品位置")]
    public Rigidbody rigCatch;
    #endregion

    private void Update()
    {
        Turn();
        Run();
        Attack();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "target" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            Physics.IgnoreCollision(other, GetComponent<Collider>());
            other.GetComponent<HingeJoint>().connectedBody = rigCatch;
        }

        if (other.name == "location" && ani.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        {
            GameObject.Find("target").GetComponent<HingeJoint>().connectedBody = null;
        }
    }

    #region 方法區域
    private void Run()
    {
        // 如果 動畫 為 撿東西 就 跳出
        if (ani.GetCurrentAnimatorStateInfo(0).IsName("attack")) return;

        float v = Input.GetAxis("Vertical");
        rig.AddForce(tran.forward * speed * v * Time.deltaTime);

        ani.SetBool("run", v != 0);
    }

    private void Turn()
    {
        float h = Input.GetAxis("Horizontal");
        tran.Rotate(0, turn * h * Time.deltaTime, 0);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            ani.SetTrigger("attack");
        }
    }
    private void Task()
    {

    }
    #endregion
}
