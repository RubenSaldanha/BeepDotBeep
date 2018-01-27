using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResDb {

    // --- Header Panel Resources ---
    // Header Panel
    public static Sprite NextLevelButtonPngActive;
    public static Sprite NextLevelButtonPngInactive;
    public static Sprite PreviousLevelButtonPng;

    // --- Map Panel Resources ---
    // Planet Icons
    public static Sprite PlanetIconPngInactive;
    public static Sprite PlanetIconPngDisconnected;
    public static Sprite PlanetIconPngActive;
    public static Sprite PlanetIconPngAttention;
    public static Sprite PlanetIconPngSelection;
    public static Sprite PingPng;

    // Connections
    public static Sprite ConnectionPng;
    public static Material ConnectionMaterial;

    // --- Activation Panel Resources ---
    // Activation Button
    public static Sprite ActivationButtonPngInactive;
    public static Sprite ActivationButtonPngActive;

    // Activation Patterns
    public static Sprite PlanetBoxPngInactive;
    public static Sprite PlanetBoxPngActive;
    public static Sprite PlanetBoxPngDisconnected;




    // --- Fonts ---
    public static Font DS_DIGI;
    public static Font DS_DIGIB;


    public static void Load()
    {
        // --- Header Panel Resources ---
        // Next Level Button
        NextLevelButtonPngInactive = Resources.Load<Sprite>("NextLevelButtonPngInactive");
        NextLevelButtonPngActive = Resources.Load<Sprite>("NextLevelButtonPngActive");

        // Previous Level Button
        PreviousLevelButtonPng = Resources.Load<Sprite>("PreviousLevelButtonPng");

        // --- Map Panel Resources ---
        // Planet Icons
        PlanetIconPngInactive = Resources.Load<Sprite>("PlanetIconPngInactive");
        PlanetIconPngDisconnected = Resources.Load<Sprite>("PlanetIconPngDisconnected");
        PlanetIconPngActive = Resources.Load<Sprite>("PlanetIconPngActive");
        PlanetIconPngAttention = Resources.Load<Sprite>("PlanetIconPngAttention");
        PingPng = Resources.Load<Sprite>("PingPng");
        //PlanetIconPngSelection = Resources.Load<Sprite>("PlanetIconPngSelection");

        // Connection
        ConnectionPng = Resources.Load<Sprite>("ConnectionPng");
        ConnectionMaterial = Resources.Load<Material>("ConnectionMaterial");

        // --- Activation Panel Resources ---
        // Activation Button
        ActivationButtonPngInactive = Resources.Load<Sprite>("ActivationButtonPngInactive");
        ActivationButtonPngActive = Resources.Load<Sprite>("ActivationButtonPngActive");

        //Activation Patterns
        PlanetBoxPngInactive = Resources.Load<Sprite>("PlanetBoxPngInactive");
        PlanetBoxPngActive = Resources.Load<Sprite>("PlanetBoxPngActive");
        PlanetBoxPngDisconnected = Resources.Load<Sprite>("PlanetBoxPngDisconnected");

        // --- Fonts ---
        DS_DIGI = Resources.Load<Font>("DS-DIGI");
        DS_DIGIB = Resources.Load<Font>("DS-DIGIB");
    }
}
