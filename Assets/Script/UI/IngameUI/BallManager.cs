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

    public static void UpdateBalls(int mult, bool isSum)
    {
        if (!isSum)
        {
            BallsRemaining = Mathf.Max(0, BallsRemaining - mult);
        }
        else
        {
            BallsRemaining += mult;
        }

        UIManager.BallOut = true;
    }

    public static int GetBalls()
    {
        return BallsRemaining;
    }

    public static void ResetBallsRemaining()
    {
        BallsRemaining = BallPoolManager.GetDefaultCapacity();
    }
}