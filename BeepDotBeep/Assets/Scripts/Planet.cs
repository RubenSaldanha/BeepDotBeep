using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet {
    public Level level;
    public Universe universe
    {
        get { return level.universe; }
    }

    public int index;
    public int seed;
    public int[][] activations;

    public bool active;
    public bool connected;

    public List<int> connectedPatterns;

    public float verticalPlacement;

    public List<List<int>> minimalRequiredActivationsOptions;
    //public List<float> activationDifficulties;


    public void Initialize(int index, Level level, int seed)
    {
        this.index = index;
        this.level = level;
        this.seed = seed;

        connectedPatterns = new List<int>();

        System.Random rdm = new System.Random(seed);

        verticalPlacement = (float)rdm.NextDouble();

        if (index == 0 && level.index == 0)
        {
            active = true;
            connected = true;
        }
        else if(index == 0)
        {

        }
        else
        {
            active = false;
            connected = false;

            //Create activation configurations
            List<List<int>> tempActivations = new List<List<int>>();
            bool breakit = false;
            while (!(breakit || tempActivations.Count == 8))
            {
                //Build random configuration for activation (partition)
                List<int> singleActiv = new List<int>();

                //Get random partition size
                int activSize;
                if (index == 1)
                    activSize = 1;
                else
                {
                    int lowerBound = 1;
                    int upperBound = Mathf.Min(index, 16);
                    float factor = (float)rdm.NextDouble();
                    factor = Mathf.Pow(factor,4); //tilt randomness towards lower values
                    activSize = lowerBound + (int)(factor * (upperBound - lowerBound));
                }

                //Populate Partition with non repeating planets
                for (int k = 0; k < activSize; k++)
                {
                    float factor = (float)rdm.NextDouble();
                    factor = Mathf.Pow(factor, 1f/4f); //push random factor upwards
                    int tryPlanet = (int)(factor * index);
                    tryPlanet = tryPlanet % index; //Fix possible overflow on index
                    while (singleActiv.Contains(tryPlanet))
                        tryPlanet = (tryPlanet + 1) % index; //Cycle search next

                    singleActiv.Add(tryPlanet);
                }

                //Check if found configuration contains or is contained in some of the current (makes it redundant) , and break in case
                bool redudant = false;
                for (int k = 0; k < tempActivations.Count; k++)
                {
                    List<int> bigOne;
                    List<int> smallOne;
                    if (singleActiv.Count > tempActivations[k].Count)
                    {
                        //Check if contains
                        bigOne = singleActiv;
                        smallOne = tempActivations[k];
                    }
                    else
                    {
                        //Check if contained
                        bigOne = tempActivations[k];
                        smallOne = singleActiv;
                    }

                    redudant = true;
                    for (int l = 0; l < smallOne.Count; l++)
                    {
                        if (!bigOne.Contains(smallOne[l]))
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
                    tempActivations.Add(singleActiv);
            }

            //Push activation configurations to Planet
            List<int[]> temp2 = new List<int[]>();
            for (int k = 0; k < tempActivations.Count; k++)
                temp2.Add(tempActivations[k].ToArray());

            activations = temp2.ToArray();
        }
    }

    public void Update()
    {
        if(index == 0)
        {
            if(level.index == 0)
            {
                active = true;
                connected = true;
            }
            else
            {
                Planet previousLevelLast = level.PreviousLevel.LastPlanet;
                active = previousLevelLast.active;
                connected = previousLevelLast.connected;
            }
        }
        else
        {
            UpdateConnection();
        }
    }

    public void ToggleActive()
    {
        if (index == 0)
            return;

        if(!active)
        {
            if (universe.ComputeActivationPoints(level.index) > 0)
                active = true;
        }
        else
        {
            active = false;
        }

        universe.Update();
    }
    private void UpdateConnection()
    {
        connected = true;
        if (activations == null)
            return;


        connectedPatterns.Clear();
        for(int i=0;i<activations.Length;i++)
        {
            connected = true;
            for(int j=0;j<activations[i].Length;j++)
            {
                int target = activations[i][j];
                if(!(level.planets[target].active && level.planets[target].connected))
                {
                    connected = false;
                    break;
                }
            }

            if (connected)
                connectedPatterns.Add(i);
        }

        if (connectedPatterns.Count > 0)
            connected = true;
    }
}
