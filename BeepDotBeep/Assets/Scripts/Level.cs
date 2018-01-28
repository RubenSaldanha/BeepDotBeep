using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level{
    public Universe universe;

    public int index;
    public int seed;

    public Planet[] planets;

    public int requiredPoints;

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

        ComputePlanetDifficultyMetrics();
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

    public void ComputePlanetDifficultyMetrics()
    {
        for(int i=0;i<planets.Length;i++)
        {
            ComputePlanetDifficultyMetrics(i);
        }

        requiredPoints = int.MaxValue;
        for(int i=0;i< LastPlanet.minimalRequiredActivationsOptions.Count;i++)
        {
            if (LastPlanet.minimalRequiredActivationsOptions[i].Count < requiredPoints)
                requiredPoints = LastPlanet.minimalRequiredActivationsOptions[i].Count - 1;
        }
    }
    public void ComputePlanetDifficultyMetrics(int planetIndex)
    {
        // ---- ASSUMES Previous Planets have had their metrics calculated ----
        Planet planet = planets[planetIndex];
        if (planetIndex == 0)
        {
            planet.minimalRequiredActivationsOptions = new List<List<int>>();
            planet.minimalRequiredActivationsOptions.Add(new List<int>());
            planet.minimalRequiredActivationsOptions[0].Add(0);
            return;
        }


        //Pattern Options Difficulties
        //List<List<float>> activationDifficulties = new List<List<float>>(); //For each pattern, each option has a difficulty
        //Sets of Minimal Activations Required per Pattern
        List<List<List<int>>> patternRequirements = new List<List<List<int>>>(); //For each pattern, several options (which are lists of ints)
        int minReq = int.MaxValue;
        //float minDif = float.MaxValue;
        for (int i = 0; i < planet.activations.Length; i++)
        {
            // --- For each pattern ---

            List<List<int>> singlePatternReqOptions = new List<List<int>>();
            //List<float> patternOptionsDifs = new List<float>();
            for (int j = 0; j < planet.activations[i].Length; j++)
            {
                // --- For each node ---
                Planet target = planets[planet.activations[i][j]];

                //Sum difficulty
                //dif += target.activationDifficulty;

                //Append requirements
                List<List<int>> nodeReqs = target.minimalRequiredActivationsOptions;

                singlePatternReqOptions = CartesianMerge(singlePatternReqOptions, nodeReqs);
            }

            //Add self planet to pattern activation options
            for (int k = 0; k < singlePatternReqOptions.Count; k++)
            {
                singlePatternReqOptions[k].Add(planetIndex);

                //Get minimal option, and minimal difficulty
                if(singlePatternReqOptions[k].Count <= minReq)
                {
                    minReq = singlePatternReqOptions[k].Count;
                    //if (dif < minDif)
                    //    minDif = dif;
                }
            }

            //activationDifficulties[i] = dif;
            patternRequirements.Add(singlePatternReqOptions);
        }

        //Compute difficulty
        //float difficulty = 0;
        //for (int i = 0; i < planet.activations.Length; i++)
        //{
        //    for (int k = 0; k < patternRequirements[i].Count; k++)
        //    {
        //        float difConfusion;
        //        int lengthConfusion;

        //        //Length confusion
        //        if (patternRequirements[i][k].Count == minReq)
        //            lengthConfusion = 0;
        //        else
        //            lengthConfusion = 1 / (patternRequirements[i][k].Count - minReq);

        //        //Difficulty confusion
        //        if (activationDifficulties[i] <= minDif)
        //            difConfusion = (minDif - activationDifficulties[i]);
        //        else
        //            difConfusion = 1 / (activationDifficulties[i] - minDif);

        //        difficulty += difConfusion * lengthConfusion;
        //    }
        //}

        //planet.activationDifficulty = difficulty + 1;
        List<List<int>> minimalReqs = new List<List<int>>();
        for(int i=0;i<patternRequirements.Count;i++)
            for(int j=0;j<patternRequirements[i].Count;j++)
            {
                if (patternRequirements[i][j].Count == minReq)
                    minimalReqs.Add(patternRequirements[i][j]);
            }

        planet.minimalRequiredActivationsOptions = minimalReqs;
    }
    private List<List<int>> CartesianMerge(List<List<int>> aa, List<List<int>> bb)
    {
        List<List<int>> result = new List<List<int>>();

        if (aa.Count == 0)
        {
            //return copy of bb
            for(int i=0;i<bb.Count;i++)
            {
                result.Add(new List<int>());
                for (int j = 0; j < bb[i].Count; j++)
                    result[i].Add(bb[i][j]);
            }
            return result;
        }

        if (bb.Count == 0)
        {
            //return copy of aa
            for (int i = 0; i < aa.Count; i++)
            {
                result.Add(new List<int>());
                for (int j = 0; j < aa[i].Count; j++)
                    result[i].Add(aa[i][j]);
            }
            return result;
        }

        bool redudant;
        for (int i=0;i<aa.Count;i++)
        {
            for(int j=0;j<bb.Count;j++)
            {
                //Copy aa element
                List<int> activations = new List<int>(aa[i]);

                // Merge bb element with aa element
                for (int k = 0; k < bb[j].Count; k++)
                    if (!activations.Contains(bb[j][k]))
                        activations.Add(bb[j][k]);

                //Check if smaller pattern already exists in result, keep smaller
                redudant = false;
                for (int k=0;k<result.Count;k++)
                {
                    if(result[k].Count <= activations.Count)
                    {
                        redudant = true;
                        for (int ll = 0; ll < result[k].Count; ll++)
                            if (!activations.Contains(result[k][ll]))
                            {
                                redudant = false;
                                break;
                            }

                        if (redudant)
                            break;
                    }
                }

                if (redudant)
                    continue;

                //Check if bigger patterns exist in result, and pop them
                for (int k = 0; k < result.Count; k++)
                {
                    if (result[k].Count > activations.Count)
                    {
                        redudant = true;
                        for (int ll = 0; ll < activations.Count; ll++)
                            if (!result[k].Contains(activations[ll]))
                            {
                                redudant = false;
                                break;
                            }

                        if (redudant)
                        {
                            result.RemoveAt(k);
                            k--;
                        }
                    }
                }

                //Add new pattern
                result.Add(activations);
            }
        }

        return result;
    }
}
