using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMapPanelScript : MonoBehaviour {

    public int margin;
    public float panelWidth;
    public float panelHeight;
    public float panelxx;
    public float panelyy;

    public float planetXmargin;
    public float planetYmargin;


    public List<PlanetIconScript> planetIcons;


    public void BuildCurrentLevelUI(Level currentLevel)
    {
        //Configure Panel
        margin = 12;
        panelWidth = 1024 - 2 * margin;
        panelHeight = 380 - 2 * margin;
        panelxx = margin;
        panelyy = 300 + margin;

        BuildStars();

        planetIcons = new List<PlanetIconScript>();

        for (int i = 0; i < currentLevel.planets.Length; i++)
        {
            //Create gameobject
            GameObject planetIcon = new GameObject("Planet " + i);

            //Add Script component
            PlanetIconScript planetScript = planetIcon.AddComponent<PlanetIconScript>();
            planetScript.Initialize(currentLevel.planets[i], currentLevel, this);
            planetIcons.Add(planetScript);
        }
    }

    public void BuildStars()
    {
        System.Random rdm = new System.Random(GameControllerScript.current.currentLevel.seed);

        int starCount = rdm.Next(15, 50);
        for(int i=0;i<starCount;i++)
        {
            float starSize = (int)(((float)rdm.NextDouble() * 0.7f + 0.3f) * 16);

            GameObject star = new GameObject("Star " + i);
            RectTransform transf = star.AddComponent<RectTransform>();
            star.transform.SetParent(transform);

            //Configure Rect Transform settings
            transf.anchorMin = Vector2.zero;
            transf.anchorMax = Vector2.zero;

            transf.pivot = Vector2.one * 0.5f;
            transf.sizeDelta = Vector2.one * starSize;

            //Compute Position
            Vector2 position = new Vector2();
            position.x = margin + (float)rdm.NextDouble() * panelWidth;
            position.y = margin + (float)rdm.NextDouble() * panelHeight; ;

            transf.anchoredPosition3D = new Vector3(position.x, position.y, 0);
            transf.localRotation = Quaternion.AngleAxis((float)rdm.NextDouble() * 360, Vector3.forward);

            //Add Image component
            Image iconImg = star.AddComponent<Image>();
            //int starIndex = rdm.Next(ResDb.StarsPng.Length);
            //iconImg.sprite = ResDb.StarsPng[starIndex];
            iconImg.sprite = ResDb.StarsPng[1];
        }
    }
    public void CreateSelectionUI()
    {
        planetIcons[GameControllerScript.current.selectedPlanetIndex].CreateSelectionUI();
    }
    public void DestroySelectionUI()
    {
        planetIcons[GameControllerScript.current.selectedPlanetIndex].DestroySelectionUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < planetIcons.Count; i++)
            planetIcons[i].UpdateUI();
    }
}
