using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public void CreateSelectionUI()
    {
        planetIcons[GameControllerScript.current.selectedPlanetIndex].SetSelected();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < planetIcons.Count; i++)
            planetIcons[i].UpdateUI();
    }
}
