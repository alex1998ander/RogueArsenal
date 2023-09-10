using System;
using UnityEngine;

public class StatUpgrade
{
    public int ValueIncrease { get; private set; } = 0;
    public int Stage { get; private set; } = 0;

    private float _upperBound, _lowerBound, _range;

    private const float Exponent = 0.6f;

    /// <summary>
    /// Constructor for a StatUpgrade
    /// </summary>
    /// <param name="upperBound">Upper value increase bound</param>
    /// <param name="lowerBound">Lower value increase bound</param>
    /// <param name="range">Number of steps until lowerBound is increased</param>
    public StatUpgrade(float upperBound, float lowerBound, float range)
    {
        _upperBound = upperBound;
        _lowerBound = lowerBound;
        _range = range;
    }

    /// <summary>
    /// Increases the value represented by this instance by one stage
    /// </summary>
    public void Upgrade()
    {
        Stage++;
        ValueIncrease += Mathf.RoundToInt(_upperBound + Mathf.Pow(Stage / _range, Exponent) * (_lowerBound - _upperBound));
    }
}