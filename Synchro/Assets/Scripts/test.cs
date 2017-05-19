using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class test : MonoBehaviour {

    [SerializeField]
    private TextAsset a;

    // Use this for initialization
    void Start()
    {
        int x = 0;
        int y = 0;
        StringReader reader = new StringReader(a.text);
        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');
            GameObject obj;
            foreach (string str in values)
            {
                print(str);
                switch (str)
                {
                    case "0":
                        obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        break;
                    case "2":
                        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        break;
                    case "3":
                        obj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        break;
                    default:
                        obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                        break;
                }

                if (obj != null) obj.transform.position = new Vector3(x, y, 0);
                if (obj != null) obj.transform.localScale = new Vector3(5, 5, 5);
                obj.transform.SetParent(transform);
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
