using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    private static int BallsRemaining;

    void Start()
    {
        BallsRemaining = BallPoolManager.GetDefaultCapacity();
    }

    public static void UpdateBalls(int mult)
    {
        BallsRemaining -= mult;
        UIManager.BallOut = true;
    }

    public static int GetBalls()
    {
        return BallsRemaining;
    }
}