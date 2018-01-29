using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Universe{

    public int seed;

    public Level[] levels;

    public void Initialize(int seed)
    {
        this.seed = seed;

        int levelCount = 12;

        System.Random rdm = new System.Random(seed);

        levels = new Level[levelCount];
        for(int i=0;i<levels.Length;i++)
        {
            levels[i] = new Level();
            levels[i].Initialize(i, this, rdm.Next());
        }

        int initialMargin = 5;
        int removedMargin = 0;
        for(int i=0;i<levels.Length;i++)
        {
            int removal = 0;
            if (removedMargin > 1)
            {
                if (rdm.Next() > (initialMargin - 1f) / levels.Length)
                    removal = 1;
            }
            else if (i == levels.Length - 1)
                removal = initialMargin - removedMargin; // Last level put margin to 0


            levels[i].bonusActivationPoints = levels[i].requiredPoints - removal;
            removedMargin += removal;
        }
        levels[0].bonusActivationPoints += initialMargin;

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
