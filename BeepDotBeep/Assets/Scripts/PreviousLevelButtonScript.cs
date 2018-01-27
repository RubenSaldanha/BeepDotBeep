using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviousLevelButtonScript : MonoBehaviour {

    public Level level;

    Image img;

    public void Initialize(Level level)
    {
        this.level = level;
        Button butScript = gameObject.AddComponent<Button>();
        
        butScript.onClick.AddListener(() => { GameControllerScript.current.GoToPreviousLevel(); });

        img = gameObject.AddComponent<Image>();

        img.sprite = ResDb.PreviousLevelButtonPng;
    }

}
