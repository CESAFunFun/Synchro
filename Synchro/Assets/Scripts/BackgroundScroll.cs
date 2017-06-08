using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {

    [SerializeField]
    private Player gravity;
    [SerializeField]
    private float speed = 1F;
    Renderer renderer;

	// Use this for initialization
	void Start () {
        renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float scroll = Mathf.Repeat(Time.time * speed * 0.1F, 1);
        if (gravity.downGravity) scroll *= -1F;
        Vector2 offset = new Vector2(0F, scroll);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
	}
}
