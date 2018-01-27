using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetIconScript : MonoBehaviour {

    Image iconImg;
    Planet planet;

    public void Initialize(Planet planet, Level level, LevelMapPanelScript LevelMapPanel)
    {
        this.planet = planet;

        float planetXmargin = 20;
        float planetYmargin = 40;

        RectTransform parentRect = LevelMapPanel.GetComponent<RectTransform>();
        transform.SetParent(parentRect);

        //Configure Rect Transform settings
        RectTransform transf = gameObject.AddComponent<RectTransform>();

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = Vector2.one * 0.5f;
        transf.sizeDelta = Vector2.one * 20;

        //Add Image component
        iconImg = gameObject.AddComponent<Image>();
        iconImg.sprite = ResDb.PlanetIconPngInactive;

        //Add icon component
        Button iconBut = gameObject.AddComponent<Button>();
        iconBut.targetGraphic = iconImg;
        int kk = planet.index;
        iconBut.onClick.AddListener(() => { GameControllerScript.current.SelectPlanet(kk); });

        //Compute Position
        Vector2 position = new Vector2();
        position.x = planet.index * (parentRect.sizeDelta.x - planetXmargin * 2) / (level.planets.Length - 1) + planetXmargin;
        position.y = Random.value * (parentRect.sizeDelta.y - planetYmargin * 2) + planetYmargin;
        
        transf.anchoredPosition3D = new Vector3(position.x, position.y, 0);


        //Build Label
        GameObject label = new GameObject("PlanetIconLabel " + planet.index);
        label.transform.SetParent(transform);
        label.transform.localPosition = new Vector3(0, 25, 0);
        Text labelText = label.AddComponent<Text>();
        labelText.text = "" + planet.index;
        labelText.alignment = TextAnchor.MiddleCenter;
        labelText.font = ResDb.DS_DIGIB;
        labelText.fontSize = 20;
        labelText.color = Color.white;


        UpdateUI();
    }
    public void SetSelected()
    {
        //iconImg.sprite = ResDb.PlanetIconPngActive;
    }
    public void UpdateUI()
    {
        if(planet.active)
        {
            if(planet.connected)
            {
                iconImg.sprite = ResDb.PlanetIconPngActive;
            }
            else
            {
                iconImg.sprite = ResDb.PlanetIconPngDisconnected;
            }
        }
        else
        {
            if(planet.connected)
            {
                iconImg.sprite = ResDb.PlanetIconPngAttention;
            }
            else
            {
                iconImg.sprite = ResDb.PlanetIconPngInactive;
            }
        }
    }
}
