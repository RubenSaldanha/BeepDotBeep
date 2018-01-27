using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingScript : MonoBehaviour {

    RectTransform rectTransf;
    Image img;

    static float pingLife = 1.8f;
    static float pingSize = 96f;
    float timer;



	// Use this for initialization
	void Start () {
        rectTransf = GetComponent<RectTransform>();

        img = gameObject.AddComponent<Image>();
        img.sprite = ResDb.PingPng;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;

        if (timer > pingLife)
        {
            Destroy(gameObject);
            return;
        }

        float ratio = timer / pingLife;
        rectTransf.sizeDelta = Vector2.one * (ratio *0.70f + 0.30f ) * pingSize;

        img.color = new Color(1f, 1f, 1f, 1f - ratio);
    }
}
