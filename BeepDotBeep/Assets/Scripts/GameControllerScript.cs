using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerScript : MonoBehaviour {

    public static GameControllerScript current;
    public int seed;
    public int levelIndex;
    
    public GameObject LevelMapPanel;
    public GameObject PlanetPanel;
    public GameObject ActivationPanel;

    public Level currentLevel;

    int selectedPlanet;

    // Use this for initialization
	void Start () {
        current = this;

        LevelMapPanel = GameObject.Find("LevelMapPanel");
        PlanetPanel = GameObject.Find("PlanetPanel");
        ActivationPanel = GameObject.Find("ActivationPanel");

        seed = 0;
        levelIndex = 0;

        CreateCurrentLevel();
	}
    
    public void CreateCurrentLevel()
    {
        int difficulty = levelIndex;

        Level lvl = new Level();
        lvl.index = levelIndex;
        lvl.seed = levelIndex + seed;

        int planetCount = levelIndex + 5;
        lvl.planets = new Planet[planetCount];

        //Build planets
        for (int i = lvl.planets.Length - 1; i > 0; i--)
        {
            Planet pl = new Planet();
            lvl.planets[i] = pl;


            //Create activation configurations
            List<List<int>> activations = new List<List<int>>();
            bool breakit = false;
            while(!breakit)
            {
                //Find random configuration for activation (partition)
                int activSize = Random.Range(1, i + 1);
                List<int> singleActiv = new List<int>();

                for(int k=0;k<activSize;k++)
                {
                    int tryPlanet = Random.Range(0, i);
                    while (singleActiv.Contains(tryPlanet))
                        tryPlanet++;

                    singleActiv.Add(tryPlanet);
                }

                //Check if found configuration contains or is contained in some of the current (makes it redundant) , and break in case
                bool redudant = false;
                for(int k=0;k<activations.Count;k++)
                {
                    List<int> bigOne;
                    List<int> smallOne;
                    if(singleActiv.Count > activations[k].Count)
                    {
                        //Check if contains
                        bigOne = singleActiv;
                        smallOne = activations[k];
                    }
                    else
                    {
                        //Check if contained
                        bigOne = activations[k];
                        smallOne = singleActiv;
                    }

                    redudant = true;
                    for(int l=0;l<smallOne.Count;l++)
                    {
                        if(!bigOne.Contains(smallOne[l]))
                        {
                            redudant = false;
                            break;
                        }
                    }

                    if (redudant)
                        break;
                }

                //Add configuration to activations list or stop
                if (redudant)
                    breakit = true;
                else
                    activations.Add(singleActiv);
            }


            //Push activation configurations to Planet
            List<int[]> mm = new List<int[]>();
            for (int k = 0; k < activations.Count; k++)
                mm.Add(activations[k].ToArray());

            pl.activations = mm.ToArray();
        }

        //First planet case
        lvl.planets[0] = new Planet();

        currentLevel = lvl;

        BuildLevelMapPanel();
    }

    public void BuildLevelMapPanel()
    {
        //Level data must have been created already
        Sprite iconImgPng = Resources.Load<Sprite>("PlanetIcon");
        float panelWidth = 1000;
        float panelHeight = 380;
        float panelxx = 12;
        float panelyy = -458;

        for(int i=0;i<currentLevel.planets.Length;i++)
        {
            //Create gameobject
            GameObject planetIcon = new GameObject("Planet " + i);
            planetIcon.transform.SetParent(LevelMapPanel.transform);

            //Configure Rect Transform settings
            planetIcon.AddComponent<RectTransform>();
            RectTransform transf = planetIcon.GetComponent<RectTransform>();

            transf.anchorMin = Vector2.up;
            transf.anchorMax = Vector2.up;

            transf.pivot = Vector2.one * 0.5f;
            transf.sizeDelta = Vector2.one * 14;


            //Add Image component
            Image iconImg = planetIcon.AddComponent<Image>();
            iconImg.sprite = iconImgPng;

            //Add icon component
            Button iconBut = planetIcon.AddComponent<Button>();
            iconBut.targetGraphic = iconImg;
            int kk = i;
            iconBut.onClick.AddListener(() => { SelectPlanet(kk); });

            //Compute Position
            Vector2 position = new Vector2();
            position.x = i * panelWidth / (currentLevel.planets.Length - 1);
            position.y = Random.value * panelHeight;

            transf.anchoredPosition3D = new Vector3(panelxx + position.x, panelyy + position.y, 0);
        }

        Debug.Log("Potato");
    }

    public void DestroyCurrentLevel()
    {

    }

    public void CompleteCurrentLevel()
    {
        DestroyCurrentLevel();

        levelIndex++;

        CreateCurrentLevel();
    }

    public void ResetUI()
    {

    }

    public void SelectPlanet(int index)
    {
        selectedPlanet = index;
    }

	// Update is called once per frame
	void Update () {
		
	}

    public class GameData
    {

    }

    public class Level
    {
        public int index;
        public int seed;

        public Planet[] planets;
    }

    public class Planet
    {
        public int[][] activations;
    }
}
