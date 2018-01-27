using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level{
    public Universe universe;

    public int index;
    public int seed;

    public Planet[] planets;

    public Planet LastPlanet
    {
        get { return planets[planets.Length - 1]; }
    }

    public Level PreviousLevel
    {
        get
        {
            if (index == 0)
                return null;
            else
                return universe.levels[index - 1];
        }
    }

    public int bonusActivationPoints;

    public void Initialize(int index, Universe universe, int seed)
    {
        this.universe = universe;
        this.index = index;
        this.seed = seed;
        int difficulty = index;
        bonusActivationPoints = 10;

        System.Random rdm = new System.Random(seed);

        //Choose planet count
        int planetCount = index + 5;
        planets = new Planet[planetCount];

        //Build planets
        for (int i = 0; i < planets.Length; i++)
        {
            Planet pl = new Planet();
            pl.Initialize(i, this,  rdm.Next());
            planets[i] = pl;
        }
    }

    public int GetUsedActivationPoints()
    {
        int count = 0;
        for (int i = 1; i < planets.Length; i++)
            count += planets[i].active ? 1 : 0;

        return count;
    }

    public void Update()
    {
        for(int i=0;i<planets.Length;i++)
        {
            planets[i].Update();
        }
    }
}
