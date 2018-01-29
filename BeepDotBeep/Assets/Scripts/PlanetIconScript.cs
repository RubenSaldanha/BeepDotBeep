using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlanetIconScript : MonoBehaviour {

    LevelMapPanelScript LevelMapPanelScript;

    Image iconImg;
    Planet planet;

    public static float iconSize = 24;

    List<ConnectionScript> connections;

    GameObject selection;
    GameObject selectionRotator;

    static float pingPeriod = 5f;
    float pingAcumulator = 0;
    float lastClick = 0;

    public int[] activOption1;
    public int[] activOption2;
    public int[] activOption3;

    public void Initialize(Planet planet, Level level, LevelMapPanelScript LevelMapPanel)
    {
        this.LevelMapPanelScript = LevelMapPanel;
        this.planet = planet;
        if(planet.minimalRequiredActivationsOptions.Count > 0)
            activOption1 = planet.minimalRequiredActivationsOptions[0].ToArray();
        if(planet.minimalRequiredActivationsOptions.Count > 1)
            activOption2 = planet.minimalRequiredActivationsOptions[1].ToArray();
        if (planet.minimalRequiredActivationsOptions.Count > 2)
            activOption3 = planet.minimalRequiredActivationsOptions[2].ToArray();

        float planetXmargin = 20;
        float planetYmargin = 40;

        pingAcumulator = pingPeriod;

        RectTransform parentRect = LevelMapPanel.GetComponent<RectTransform>();
        transform.SetParent(parentRect);

        //Configure Rect Transform settings
        RectTransform transf = gameObject.AddComponent<RectTransform>();

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = Vector2.one * 0.5f;
        transf.sizeDelta = Vector2.one * iconSize;

        //Compute Position
        Vector2 position = new Vector2();
        position.x = planet.index * (parentRect.sizeDelta.x - planetXmargin * 2) / (level.planets.Length - 1) + planetXmargin;
        position.y = planet.verticalPlacement * (parentRect.sizeDelta.y - planetYmargin * 2) + planetYmargin;

        transf.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        // --- Build button ---
        GameObject iconButton = new GameObject("Icon Button");
        transf = iconButton.AddComponent<RectTransform>();
        transf.SetParent(transform);

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = Vector2.one * 0.5f;
        transf.sizeDelta = Vector2.one * iconSize;

        transf.anchoredPosition3D = new Vector3(iconSize/2, iconSize/2, 0);

        //Add Image component
        iconImg = iconButton.AddComponent<Image>();
        iconImg.sprite = ResDb.PlanetIconPngInactive;

        //Add icon component
        Button iconBut = iconButton.AddComponent<Button>();
        iconBut.targetGraphic = iconImg;
        iconBut.onClick.AddListener(() => { OnClick(); });


        // --- Build Label ---
        GameObject label = new GameObject("PlanetIconLabel " + planet.index);
        transf = label.AddComponent<RectTransform>();
        transf.SetParent(transform);
        label.transform.localPosition = new Vector3(0, 25, 0);
        Text labelText = label.AddComponent<Text>();
        labelText.text = "" + planet.index;
        labelText.alignment = TextAnchor.MiddleCenter;
        labelText.font = ResDb.DS_DIGIB;
        labelText.fontSize = 20;
        labelText.color = Color.white;
        transf.sizeDelta = new Vector2(30, 30);

        Outline outline = label.AddComponent<Outline>();
        outline.effectColor = Color.black;
        outline.effectDistance = new Vector2(0.6f,0.6f);
        Shadow shadow = label.AddComponent<Shadow>();
        shadow.effectColor = Color.black;
        shadow.effectDistance = new Vector2(-1f, -1f);


        connections = new List<ConnectionScript>();
        // --- Build Connections ---
        if (planet.activations != null) // first planet case
        {
            for (int i = 0; i < planet.activations.Length; i++)
            {
                for (int j = 0; j < planet.activations[i].Length; j++)
                {
                    PlanetIconScript origin = LevelMapPanelScript.planetIcons[planet.activations[i][j]];

                    Vector2 diff = origin.transform.localPosition - transform.localPosition;
                    //float angle = Vector2.SignedAngle(Vector2.right, diff);
                    float angle = Vector2.Angle(Vector2.right, diff) * Mathf.Sign(diff.y); //Signed alternative

                    GameObject connectionObj = new GameObject("Connection " + origin.planet.index + " :: " + planet.index);
                    transf = connectionObj.AddComponent<RectTransform>();
                    transf.SetParent(transform);

                    connectionObj.layer = LayerMask.NameToLayer("Ignore Raycast");

                    transf.anchorMin = Vector2.one * 0.5f;
                    transf.anchorMax = Vector2.one * 0.5f;

                    transf.pivot = Vector2.one * 0.5f;
                    transf.sizeDelta = new Vector2(diff.magnitude, 10);

                    transf.anchoredPosition3D = new Vector3(diff.x / 2, diff.y / 2, 0);
                    transf.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    ConnectionScript cScript = connectionObj.AddComponent<ConnectionScript>();
                    cScript.Initialize(origin.planet, planet);
                    connections.Add(cScript);

                    //Set parent to parent
                    transf.SetParent(transform.parent);

                }
            }
        }


        UpdateUI();
    }
    public void OnClick()
    {
        if(!(Time.time - lastClick < 0.3))
        {
            // Single click
            GameControllerScript.current.SelectPlanet(planet.index);
        }
        else
        {
            //Double click
            GameControllerScript.current.ToggleActivatePlanet(planet.index);
        }

        lastClick = Time.time;
    }
    public void CreateSelectionUI()
    {
        // --- Build static selector ---
        selection = new GameObject("Selector");
        RectTransform transf = selection.AddComponent<RectTransform>();
        selection.transform.SetParent(transform);
        selection.transform.SetAsFirstSibling();

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.one;

        transf.pivot = Vector2.one * 0.5f;
        transf.sizeDelta = Vector2.one * iconSize* 1.5f;

        //Compute Position
        Vector2 position = new Vector2();
        position.x = 0;
        position.y = 0;

        transf.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        Image selImg = selection.AddComponent<Image>();
        selImg.sprite = ResDb.PlanetIconSelectionPng;


        // --- Build Rotator ---
        selectionRotator = new GameObject("Selector Rotator");
        transf = selectionRotator.AddComponent<RectTransform>();
        selectionRotator.transform.SetParent(transform);
        selectionRotator.transform.SetAsFirstSibling();

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.one;

        transf.pivot = Vector2.one * 0.5f;
        transf.sizeDelta = Vector2.one * iconSize * 2f;

        //Compute Position
        position = new Vector2();
        position.x = 0;
        position.y = 0;

        transf.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        selImg = selectionRotator.AddComponent<Image>();
        selImg.sprite = ResDb.PlanetIconSelectionPngRotator;
    }
    public void DestroySelectionUI()
    {
        GameObject.Destroy(selection);
        GameObject.Destroy(selectionRotator);
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

        if(connections != null)
            for (int i = 0; i < connections.Count; i++)
                connections[i].UpdateUI();
    }

    void Update()
    {
        if(planet != null)
        {
            if (planet.active && planet.connected)
            {
                pingAcumulator += Time.deltaTime;

                if(pingAcumulator > pingPeriod)
                {
                    pingAcumulator -= pingPeriod;
                    SpawnPing();
                }
            }
        }

        float rotatorPeriod = 6f;
        if (selectionRotator != null)
            selectionRotator.transform.localRotation = Quaternion.AngleAxis(Time.time * 360 / rotatorPeriod, Vector3.forward);
    }

    void SpawnPing()
    {
        GameObject ping = new GameObject("Ping");
        RectTransform transf = ping.AddComponent<RectTransform>();
        transf.SetParent(transform);
        transf.SetAsFirstSibling();

        transf.anchorMin = Vector2.one * 0.5f;
        transf.anchorMax = Vector2.one * 0.5f;

        transf.pivot = Vector2.one * 0.5f;
        transf.sizeDelta = new Vector2(0,0);

        transf.anchoredPosition3D = new Vector3(0, 0, 0);

        ping.AddComponent<PingScript>();
    }
}
