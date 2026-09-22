using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuoteManager : MonoBehaviour
{
    [SerializeField] private int Quote;
    private static int PointsRequired;

    void Start()
    {
        PointsRequired = Quote;
    }

    public static int GetPointsRequired()
    {
        return PointsRequired;
    }
}