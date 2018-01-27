using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivationPanelScript : MonoBehaviour {

    public static float boxHeight = 30;
    public static float boxWidth = 20;
    public static float boxMarginX = 5;
    public static float boxMNarginY = 5;

    public void CreateSelectionUI()
    {
        Planet selectedPlanet = GameControllerScript.current.selectedPlanet;

        float margin = 20;

        RectTransform parentRect = GetComponent<RectTransform>();
        transform.SetParent(parentRect);

        float activationButtonHeight = 40;

        //Create Activation button
        GameObject activationBut = new GameObject("Activation Button");
        activationBut.transform.SetParent(transform);

        //Configure Rect Transform settings
        activationBut.AddComponent<RectTransform>();
        RectTransform transf = activationBut.GetComponent<RectTransform>();

        transf.anchorMin = Vector2.zero;
        transf.anchorMax = Vector2.zero;

        transf.pivot = new Vector2(1, 0);
        transf.sizeDelta = new Vector2(200, activationButtonHeight);

        //Add Image component
        Image buttonImg = activationBut.AddComponent<Image>();
        buttonImg.sprite = ResDb.ActivationButtonPngInactive;

        //Add Button component
        Button buttonComp = activationBut.AddComponent<Button>();
        buttonComp.targetGraphic = buttonImg;
        int kk = GameControllerScript.current.selectedPlanetIndex;
        buttonComp.onClick.AddListener(() => { GameControllerScript.current.ToggleActivatePlanet(kk); });

        //Compute Position
        Vector2 position = new Vector2();
        position.x = parentRect.sizeDelta.x - margin;
        position.y = 0 + margin;

        transf.anchoredPosition3D = new Vector3(position.x, position.y, 0);

        float zonewidth = 300;
        //Create Activation Patterns
        if (selectedPlanet.activations != null) // first planet case
        {
            for (int i = 0; i < selectedPlanet.activations.Length; i++)
            {
                GameObject pattern = new GameObject("ActivationPattern " + i);
                pattern.transform.SetParent(this.transform);

                transf = pattern.AddComponent<RectTransform>();

                transf.anchorMin = Vector2.zero;
                transf.anchorMax = Vector2.zero;

                transf.pivot = Vector2.zero;
                transf.sizeDelta = Vector2.one * 20;

                transf.anchoredPosition3D = new Vector3(0 + margin, parentRect.sizeDelta.y - (i + 1) * (boxHeight + boxMNarginY) - margin, 0);

                ActivationPatternScript patternScript = pattern.AddComponent<ActivationPatternScript>();
                patternScript.Initialize(i, selectedPlanet, this);
            }
        }
    }
    public void DestroySelectionUI()
    {
        foreach (Transform t in transform)
            GameObject.Destroy(t.gameObject);
    }
}
