using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public int BallsOfTheLevel;
    private static int BallsRemaining;

    void Start()
    {
        BallsRemaining = BallsOfTheLevel;
    }

    public static void UpdateBalls()
    {
        BallsRemaining--;
        UIManager.BallOut = true;
    }

    public static int GetBalls()
    {
        return BallsRemaining;
    }
}