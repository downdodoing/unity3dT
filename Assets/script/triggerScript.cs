
using UnityEngine;
using System.Collections;

public class triggerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        int num = GameObject.Find("GameObject").GetComponent<game>().pathNum;
        int pathLength = GameObject.Find("GameObject").GetComponent<game>().pathlength;

        GameObject path1 = GameObject.Find("GameObject").GetComponent<game>().path1;
        GameObject path2 = GameObject.Find("GameObject").GetComponent<game>().path2;
    
        if (1 == num % 2)
        {
            GameObject.Find("GameObject").GetComponent<game>().myPath2 = Instantiate(path2, new Vector3(-25, 0, num * pathLength), Quaternion.identity);
        }
        else
        {
            GameObject.Find("GameObject").GetComponent<game>().myPath1 = Instantiate(path1, new Vector3(-25, 0, num * pathLength), Quaternion.identity);
        }

        Destroy(GameObject.Find("GameObject").GetComponent<game>().bridge1.gameObject);
        
        generateObstacle(num,pathLength);

        destroyForward();
        GameObject.Find("GameObject").GetComponent<game>().pathNum++;
        GameObject.Find("GameObject").GetComponent<game>().generatRoad();  
    }
   
    //生成障碍物
    private void generateObstacle(int num,int pathLength)
    {
        game rigidbody = GameObject.Find("GameObject").GetComponent<game>();
        //生成拱桥
        rigidbody.bridge1 = Instantiate(GameObject.Find("GameObject").GetComponent<game>().bridge, new Vector3(-0.6451613f, 0.6877375f, num * pathLength + Random.Range(70, 400)), Quaternion.identity) as GameObject;
        //生成box
        rigidbody.box1 = Instantiate(rigidbody.box, new Vector3(0, 2, num * pathLength + Random.Range(70, 400)), Quaternion.identity) as GameObject;
    }
    //销毁上一个路段
    void destroyForward() {
        ArrayList roadList = GameObject.Find("GameObject").GetComponent<game>().roadList;
        foreach (GameObject road in roadList)
        {
            Destroy(road.gameObject);
        }
        roadList.Clear();
    }
    private void OnTriggerExit(Collider other)
    {   
        int num = GameObject.Find("GameObject").GetComponent<game>().pathNum;
        
        if (0 == num % 2)
        {
            Destroy(GameObject.Find("GameObject").GetComponent<game>().myPath1.gameObject);   
        }
        else
        {
            Destroy(GameObject.Find("GameObject").GetComponent<game>().myPath2.gameObject); 
        }
    }
}
