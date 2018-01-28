using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeaderPanelScript : MonoBehaviour {

    public GameObject PreviousLevelButton;
    public PreviousLevelButtonScript _PreviousLevelButtonScript;
    public GameObject ActivationPointsLabel;
    public GameObject NextLevelButton;
    public NextLevelButtonScript _NextLevelButtonScript;

    public void Initialize()
    {
        

    }
    public void BuildCurrentLevelUI()
    {
        if(GameControllerScript.current.levelIndex != 0)
            BuildPreviousLevelButton();

        BuildActivationPointsLabel();

        if(GameControllerScript.current.levelIndex != GameControllerScript.current.currentUniverse.levels.Length - 1)
            BuildNextLevelButton();
    }

    public void BuildPreviousLevelButton()
    {
        PreviousLevelButton = new GameObject("PreviousLevelButton");
        RectTransform transf = PreviousLevelButton.AddComponent<RectTransform>();
        PreviousLevelButton.transform.SetParent(transform);

        _PreviousLevelButtonScript = PreviousLevelButton.AddComponent<PreviousLevelButtonScript>();
        _PreviousLevelButtonScript.Initialize(GameControllerScript.current.currentLevel);

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = new Vector2(0, 0);

        transf.sizeDelta = new Vector2(300, 68);
        transf.anchoredPosition3D = new Vector3(0, 0, 0);
    }
    public void BuildActivationPointsLabel()
    {
        ActivationPointsLabel = new GameObject("ActivationPointsLabel");
        RectTransform transf = ActivationPointsLabel.AddComponent<RectTransform>();
        ActivationPointsLabel.transform.SetParent(transform);
        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = new Vector2(0, 0);

        Text labelText = ActivationPointsLabel.AddComponent<Text>();
        labelText.text = "Energy : " + GameControllerScript.current.currentUniverse.ComputeActivationPoints(GameControllerScript.current.levelIndex);
        labelText.font = ResDb.DS_DIGIB;
        labelText.color = Color.white;
        labelText.fontSize = 34;
        labelText.alignment = TextAnchor.MiddleCenter;

        transf.sizeDelta = new Vector2(424, 68);
        transf.anchoredPosition3D = new Vector3(300, 0, 0);
    }
    public void BuildNextLevelButton()
    {
        NextLevelButton = new GameObject("NextLevelButton");
        RectTransform transf = NextLevelButton.AddComponent<RectTransform>();
        NextLevelButton.transform.SetParent(transform);

        _NextLevelButtonScript = NextLevelButton.AddComponent<NextLevelButtonScript>();
        _NextLevelButtonScript.Initialize(GameControllerScript.current.currentLevel);

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = new Vector2(0, 0);

        transf.sizeDelta = new Vector2(300, 68);
        transf.anchoredPosition3D = new Vector3(1024 - 300, 0, 0);
    }

    public void UpdateUI()
    {
        UpdateActivationPointsLabel();

        if(_NextLevelButtonScript != null)
            _NextLevelButtonScript.UpdateUI();
    }
    public void UpdateActivationPointsLabel()
    {
        ActivationPointsLabel.GetComponent<Text>().text = "Energy : " + GameControllerScript.current.currentUniverse.ComputeActivationPoints(GameControllerScript.current.levelIndex);
    }
}
