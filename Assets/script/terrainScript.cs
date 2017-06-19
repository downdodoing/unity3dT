using System.Collections;
using UnityEngine;

public class terrainScript : MonoBehaviour {

    public GameObject[] treeArray;
    private GameObject tree_l,tree_r;
    private ArrayList treelist = new ArrayList();

    // Use this for initialization
    void Start() {
        int pathNum = GameObject.Find("GameObject").GetComponent<game>().pathNum;
        int pathLength = GameObject.Find("GameObject").GetComponent<game>().pathlength;
 
        for (int z = 5; z < 500; z += Random.Range(20, 40))
        {
            tree_l = Instantiate(treeArray[Random.Range(0, 3)], new Vector3(20, 1, pathLength * (pathNum - 1) + z), Quaternion.identity) as GameObject;
            tree_r = Instantiate(treeArray[Random.Range(0, 3)], new Vector3(-20, 1, pathLength * (pathNum - 1) + z), Quaternion.identity) as GameObject;

            treelist.Add(tree_l);
            treelist.Add(tree_r);
        }
       
        //生成路面
        if (1 == pathNum)
        {
            GameObject.Find("GameObject").GetComponent<game>().generatRoad();
        }
        generateObstacle();
    }
    //生成障碍物
    private void generateObstacle() {
        game rigidbody = GameObject.Find("GameObject").GetComponent<game>();
        //生成拱桥
        rigidbody.bridge1 = Instantiate(rigidbody.bridge, new Vector3(-0.6451613f, 0.6877375f, Random.Range(70, 400)), Quaternion.identity) as GameObject;    
        //生成box
        rigidbody.box1 = Instantiate(rigidbody.box, new Vector3(0, 2, Random.Range(70, 400)), Quaternion.identity) as GameObject;      
    }
    // Update is called once per frame
    void Update () {
		
	}

    private void OnDestroy()
    {
        Destroy(GameObject.Find("GameObject").GetComponent<game>().box1);
        Destroy(GameObject.Find("GameObject").GetComponent<game>().bridge1);
        foreach (GameObject tree in treelist)
        {
            Destroy(tree.gameObject);
        }
        treelist.Clear();
    }
}
