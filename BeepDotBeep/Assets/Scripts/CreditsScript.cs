using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {

    List<string> roles;

    List<string> thanks;
    private void Start()
    {
        roles.Add("Game Designer");
        roles.Add("Official Plumber");
        roles.Add("Resident Mathematician");
        roles.Add("Graphic Artist");
        roles.Add("Dog Walker");
        roles.Add("Director");
        roles.Add("Sub Director");
        roles.Add("Coffee fetcher");
        roles.Add("Lead Code Monkey");
        roles.Add("Programming team");
        roles.Add("Beta testing");
        roles.Add("Gamma testing");
        roles.Add("Other roles");


        thanks.Add("Miguel Brás");
        thanks.Add("Barata");
        thanks.Add("Nelson Filipe");
        thanks.Add("João Costa");
        thanks.Add("Liliana Vale");
    }

    private void Update()
    {
        
    }

    private void Build()
    {
        Vector2 cursor = Vector2.zero;

        float roleTitleTextHeight = 30;
        float rolePersonTextHeight = 30;
        float roleSpacing = 50;
        for(int i=0;i<roles.Count;i++)
        {
            GameObject roleText = new GameObject(roles[i]);
            RectTransform transf = roleText.AddComponent<RectTransform>();
            transf.anchoredPosition3D = new Vector3(0, cursor.y, 0);

            

        }
    }
}
