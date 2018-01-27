using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

    public static GameControllerScript current;
    public int seed;

    public Universe currentUniverse;

    public int levelIndex;
    public Level currentLevel
    {
        get { return currentUniverse.levels[levelIndex]; }
    }

    public int selectedPlanetIndex;
    public Planet selectedPlanet
    {
        get { return currentLevel.planets[selectedPlanetIndex]; }
    }

    public GameObject HeaderPanel;
    public HeaderPanelScript HeaderPanelScript;
    public GameObject LevelMapPanel;
    public LevelMapPanelScript LevelMapPanelScript;
    public GameObject PlanetPanel;
    public GameObject ActivationPanel;
    public ActivationPanelScript ActivationPanelScript;

    // Use this for initialization
	void Start () {
        current = this;

        ResDb.Load();

        HeaderPanel = GameObject.Find("HeaderPanel");
        LevelMapPanel = GameObject.Find("LevelMapPanel");
        PlanetPanel = GameObject.Find("PlanetPanel");
        ActivationPanel = GameObject.Find("ActivationPanel");

        seed = 0;
        levelIndex = 0;

        CreateCurrentUniverse();

        CreateCurrentLevelUI();
	}
    
    public void CreateCurrentUniverse()
    {
        currentUniverse = new Universe();
        currentUniverse.Initialize(seed);
    }

    public void GoToPreviousLevel()
    {
        DestroyCurrentLevelUI();

        levelIndex--;

        CreateCurrentLevelUI();
    }
    public void GoToNextLevel()
    {
        DestroyCurrentLevelUI();

        levelIndex++;

        CreateCurrentLevelUI();
    }

    public void CreateCurrentLevelUI()
    {
        BuildHeaderPanel();
        BuildLevelMapPanel();
        BuildActivationPanel();
    }
    public void DestroyCurrentLevelUI()
    {
        foreach (Transform t in HeaderPanelScript.transform)
            GameObject.Destroy(t.gameObject);
        Object.Destroy(HeaderPanelScript);

        foreach (Transform t in LevelMapPanelScript.transform)
            GameObject.Destroy(t.gameObject);
        Object.Destroy(LevelMapPanelScript);

        foreach (Transform t in ActivationPanelScript.transform)
            GameObject.Destroy(t.gameObject);
        Object.Destroy(ActivationPanelScript);
    }

    public void BuildHeaderPanel()
    {
        HeaderPanelScript = HeaderPanel.AddComponent<HeaderPanelScript>();
        HeaderPanelScript.BuildCurrentLevelUI();
    }
    public void BuildLevelMapPanel()
    {
        //Level data must have been created already
        LevelMapPanelScript = LevelMapPanel.AddComponent<LevelMapPanelScript>();
        LevelMapPanelScript.BuildCurrentLevelUI(currentLevel);
    }
    public void BuildActivationPanel()
    {
        ActivationPanelScript = ActivationPanel.AddComponent<ActivationPanelScript>();
    }


    public void SelectPlanet(int index)
    {
        DestroySelectionUI();

        selectedPlanetIndex = index;

        CreateSelectionUI();
    }
    public void DestroySelectionUI()
    {
        ActivationPanelScript.DestroySelectionUI();
    }
    public void CreateSelectionUI()
    {
        LevelMapPanelScript.CreateSelectionUI();
        ActivationPanelScript.CreateSelectionUI();
    }

    public void ToggleActivatePlanet(int index)
    {
        currentLevel.planets[index].ToggleActive();
        UpdateUI();
    }

    public void UpdateUI()
    {
        LevelMapPanelScript.UpdateUI();
        HeaderPanelScript.UpdateUI();
    }
}
