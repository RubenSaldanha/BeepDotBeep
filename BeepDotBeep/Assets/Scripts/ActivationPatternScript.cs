using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationPatternScript : MonoBehaviour {
    int index;
    Planet planet;

    public static float boxHeight = 30;
    public static float boxWidth = 40;
    public static float boxMarginX = 5;
    public static float boxMarginY = 5;

    public void Initialize(int index, Planet planet, ActivationPanelScript panel)
    {
        this.index = index;
        this.planet = planet;

        int[] pattern = planet.activations[index];

        Level level = planet.level;
        for(int i=0;i<pattern.Length;i++)
        {
            int target = pattern[i];
            Planet targetPlanet = level.planets[target];

            //Base Planet Icon object
            GameObject planetPIcon = new GameObject("PlanetPIcon " + targetPlanet.index);
            planetPIcon.transform.SetParent(transform);

            RectTransform transf = planetPIcon.AddComponent<RectTransform>();
            transf.sizeDelta = new Vector2(boxWidth, boxHeight);

            transf.anchoredPosition3D = new Vector3(i * (boxWidth + boxMarginX), 0, 0);

            //Underlying children box
            GameObject box = new GameObject("Box");
            box.transform.SetParent(planetPIcon.transform);
            transf = box.AddComponent<RectTransform>();
            transf.anchoredPosition3D = new Vector3(0, 0, 0);
            Image boxImg = box.AddComponent<Image>();
            if (targetPlanet.active && targetPlanet.connected)
                boxImg.sprite = ResDb.PlanetBoxPngActive;
            else if (targetPlanet.active && !targetPlanet.connected)
                boxImg.sprite = ResDb.PlanetBoxPngDisconnected;
            else
                boxImg.sprite = ResDb.PlanetBoxPngInactive;

            transf.sizeDelta = new Vector2(boxWidth, boxHeight);

            //Underlying children text
            GameObject label = new GameObject("Label");
            label.transform.SetParent(planetPIcon.transform);
            transf = label.AddComponent<RectTransform>();
            transf.anchoredPosition3D = new Vector3(0, 0, 0);
            Text labelText = label.AddComponent<Text>();
            labelText.text = "" + target;
            labelText.font = ResDb.DS_DIGIB;
            labelText.fontSize = 22;
            labelText.alignment = TextAnchor.MiddleCenter;
            labelText.color = Color.black;
            transf.sizeDelta = new Vector2(boxWidth, boxHeight);
        }
    }
}
