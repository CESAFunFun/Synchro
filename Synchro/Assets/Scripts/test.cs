using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class test : MonoBehaviour {

    [SerializeField]
    private TextAsset map;

    [SerializeField]
    private GameObject[] stageObject;


    // Use this for initialization
    void Start()
    {
        int x = 0;
        int y = 0;
        StringReader reader = new StringReader(map.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            GameObject obj = null;
            foreach (string str in values)
            {

                int a = int.Parse(str);
                print(a);
                if (a >= 0 && a < stageObject.Length)
                {
                    obj = Instantiate(stageObject[a], transform);
                    obj.transform.position = new Vector3(x, y, 0);
                    obj.transform.localScale = new Vector3(5, 5, 5);
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
