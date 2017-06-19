using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cudeScrip : MonoBehaviour {

    private int speed = 40;
    private Animation mAnimation;
    private bool isRun = true;
    
    private int jumpSpeed = 600;
    private float jumpDistanceSpeed = 5f;

    private float positionY;
    //用于判断是否正在跳跃
    private bool ground = true;
	// Use this for initialization
	void Start () {
        mAnimation = transform.Find("HB_boy10001").GetComponent<Animation>();
        positionY = transform.position.y;
    }

    // Update is called once per frame
    void Update() {

        //防止人物在跳跃完成后和地面发生碰撞导致乱跑
        Vector3 x = Input.GetAxis("Horizontal") * transform.right * Time.deltaTime * jumpDistanceSpeed;
        Vector3 z= Input.GetAxis("Vertical") * transform.forward * Time.deltaTime * jumpDistanceSpeed;

        //transform.Translate(x + z);
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);

        //自动向前移动
        if (Input.GetKey(KeyCode.W)) {
            this.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        }
        if (isRun)
        {
            mAnimation.Play("Run");
        }
        setListener();
    }
    //设置按键监听
    private void setListener() {
        if (Input.GetKeyDown(KeyCode.S))
        {
            isRun = false;
            mAnimation.Play("Down");
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(new Vector3(0, -5, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(new Vector3(0, 5, 0));
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump();
            isRun = false;
            mAnimation.Play("Jump02");
        }

        if (transform.position.y >= positionY + 4)
        {
            transform.GetComponent<Rigidbody>().AddForce(Vector3.down * jumpSpeed);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.Space))
        {
            isRun = true;
        }
    }
    private void jump() {
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        if (ground)
        {
            rigidbody.AddForce(Vector3.up * jumpSpeed);
            ground = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        ground = true;
    }
}
