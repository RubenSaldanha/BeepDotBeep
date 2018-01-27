using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe{

    public int seed;

    public Level[] levels;

    public void Initialize(int seed)
    {
        this.seed = seed;

        int levelCount = 32;

        System.Random rdm = new System.Random(seed);

        levels = new Level[levelCount];
        for(int i=0;i<levels.Length;i++)
        {
            levels[i] = new Level();
            levels[i].Initialize(i, this, rdm.Next());
        }

        Update();
    }

    public void Update()
    {
        for (int i = 0; i < levels.Length; i++)
            levels[i].Update();
    }
    public int ComputeActivationPoints(int level)
    {
        int count = 0;
        for(int i=0;i<= level;i++)
        {
            if(levels[i].planets[0].active && levels[i].planets[0].connected)
                count += levels[i].bonusActivationPoints;

            count -= levels[i].GetUsedActivationPoints();
        }

        return count;
    }
}
