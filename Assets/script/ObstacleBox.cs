using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBox : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        print("碰撞到了障碍物");
    }
}
