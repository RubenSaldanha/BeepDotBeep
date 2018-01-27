using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLevelButtonScript : MonoBehaviour {

    public Level level;

    Image img;

    public void Initialize(Level level)
    {
        this.level = level;
        Button butScript = gameObject.AddComponent<Button>();
        butScript.onClick.AddListener(() => { GameControllerScript.current.GoToNextLevel(); });
        img = gameObject.AddComponent<Image>();

        UpdateUI();
    }

    public void UpdateUI()
    {
        Planet last = level.planets[level.planets.Length - 1];
        if (last.active && last.connected)
            img.sprite = ResDb.NextLevelButtonPngActive;
        else
            img.sprite = ResDb.NextLevelButtonPngInactive;
    }
}
