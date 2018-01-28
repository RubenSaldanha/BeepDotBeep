using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour {

    static float clearTime = 2;
    static float fadeOutTime = 2;
    float timer;

    Image img;
    public GameObject tutorial;

    public bool bypass;

    // Use this for initialization
	void Start () {
        img = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        if(bypass)
        {
            Destroy(gameObject);
            return;
        }

        timer += Time.deltaTime;

        if(timer > clearTime + fadeOutTime)
        {
            timer = float.MinValue;
            Destroy(gameObject);
            ActivateTutorial();
        }

        if(timer > clearTime)
        {
            float ratio = (timer - clearTime) / fadeOutTime;
            ratio = 1f - ratio;
            ratio *= ratio;

            img.color = new Color(1f, 1f, 1f, ratio);
        }
	}

    void ActivateTutorial()
    {
        tutorial.SetActive(true);
    }
}
