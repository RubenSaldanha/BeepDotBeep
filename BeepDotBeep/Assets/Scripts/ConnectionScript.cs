using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionScript : MonoBehaviour {

    Image img;

    Planet origin;
    Planet target;

    static float phasePeriod = 6f;
    float phase;
    float phaseOffset;

    Color currentcolor;

    public void Initialize(Planet origin, Planet target)
    {
        this.origin = origin;
        this.target = target;
        img = gameObject.AddComponent<Image>();
        img.sprite = ResDb.ConnectionPng;
        img.type = Image.Type.Sliced;
        img.material = ResDb.ConnectionMaterial;
        img.raycastTarget = false;
        img.color = Color.clear;
        phaseOffset = Random.value;
    }

    public void UpdateUI()
    {
        transform.SetAsFirstSibling();
    }

    void Update()
    {
        phase = (Mathf.Sin((Time.time/phasePeriod + phaseOffset) * Mathf.PI * 2) + 1) / 2f;
        phase = phase * phase;

        if (origin.active && origin.connected)
        {
            if (target.active && target.connected)
            {
                //Origin active (SHOULD CHECK IF PATTERN ACTIVE)
                //Check if one of active patterns contain origin
                bool originInPattern = false;
                for(int i=0;i<target.connectedPatterns.Count;i++)
                {
                    int patternIndex = target.connectedPatterns[i];
                    int[] pattern = target.activations[patternIndex];
                    for (int j = 0; j < pattern.Length; j++)
                    {
                        if (pattern[j] == origin.index)
                        {
                            originInPattern = true;
                            break;
                        }
                    }

                    if (originInPattern)
                        break;
                }

                if(originInPattern)
                {
                    //Active Pattern
                    img.color = new Color(1f, 1f, 1f, phase * 0.1f + 0.9f);
                }
                else
                {
                    //Origin potencial (CODE REPLICATION)
                    img.color = new Color(1f, 1f, 1f, phase * 0.05f + 0.1f);
                }
            }
            else
            {
                //Origin Potencial (CODE REPLICATION)
                img.color = new Color(1f, 1f, 1f, phase * 0.05f + 0.1f);
            }
        }
        else
        {
            //No connection
            img.color = Color.clear;
        }
    }
}
