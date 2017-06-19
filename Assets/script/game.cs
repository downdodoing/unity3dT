using System.Collections;
using UnityEngine;

public class game : MonoBehaviour {

    public GameObject road,roadAnother;

    public GameObject path1, path2, test_code;
    public GameObject myPath1, myPath2;
    
    public int pathNum = 0;
    public int pathlength = 500;
    public int roadX;
    public ArrayList roadList = new ArrayList();

    //障碍物
    public GameObject bridge;
    public GameObject bridge1;
    public GameObject box;
    public GameObject box1;

    // Use this for initializa,tion
    void Start () {
        myPath1 = Instantiate(path1,new Vector3(-25,0,0),Quaternion.identity);
        pathNum++;
    }

    public void generatRoad() {
        roadX = (int)((GameObject.Find("5S_09Road").GetComponent<MeshFilter>().mesh.bounds.size.y) * 1.5);
        int index = 0;
        for (int z = 20; z <= 500; z += roadX,index++)
        {
            GameObject road1;
            //if (0 == index % 10)
            //{
            //    print((pathNum - 1) * pathlength + z);
            //    road1 = Instantiate(roadAnother, new Vector3(0, -0.7f, (pathNum - 1) * pathlength + z), Quaternion.identity) as GameObject;
            //}
            //else
            //{
                print((pathNum - 1) * pathlength + z + " else");
                road1 = Instantiate(road, new Vector3(0, -0.7f, (pathNum - 1) * pathlength + z), Quaternion.identity) as GameObject;
            //}
             roadList.Add(road1);
        }
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStatus1 gameStatus1 = LoadJSon.loadJsonFromFile();
            int num = gameStatus1.coinList[0].num;
            float beginX = gameStatus1.coinList[0].beginX;
            float beginY = gameStatus1.coinList[0].beginY;
            float beginZ = gameStatus1.coinList[0].beginZ;
            float interval = gameStatus1.coinList[0].interval;

            for (int i = 0; i < num;i++) {
                GameObject code = Instantiate(test_code,new Vector3(beginX,beginY,beginZ + interval * i),Quaternion.identity);
            }
        }
    }
}
