using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class test : MonoBehaviour {

    [SerializeField]
    private TextAsset[] maps;

    private GameObject[] objects;

    private int currentMap;

    // Use this for initialization
    void Start()
    {
        currentMap = gameObject.transform.parent.GetComponent<Stage>().stageLevel;
        objects = gameObject.transform.parent.GetComponent<Stage>().stageObjects;

        Debug.Log(currentMap);


        int x = 0;
        int y = 0;
        StringReader reader = new StringReader(maps[currentMap - 1].text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            GameObject obj = null;
            foreach (string str in values)
            {

                int a = int.Parse(str);
                print(a);
                if (a >= 0 && a < objects.Length)
                {
                    obj = Instantiate(objects[a], transform);
                    obj.transform.position = new Vector3(x, y, 0F);
                    obj.transform.localScale = new Vector3(4, 4, 4);
                }
                x += 5;
            }
            x = 0;
            y -= 5;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
