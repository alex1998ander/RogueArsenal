using UnityEngine;

public class StatUpgrade
{
    public string Name { get; private set; } = "";
    public int Value { get; private set; } = 0;
    public int Stage { get; private set; } = 0;

    private float _upperBound, _lowerBound, _range;

    private const float Exponent = 0.6f;

    /// <summary>
    /// Constructor for a StatUpgrade
    /// </summary>
    /// <param name="name">Name of stat upgrade</param>
    /// <param name="upperBound">Upper value increase bound</param>
    /// <param name="lowerBound">Lower value increase bound</param>
    /// <param name="range">Number of steps until lowerBound is the increasing value</param>
    public StatUpgrade(string name, float upperBound, float lowerBound, float range)
    {
        Name = name;
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
        Value += Mathf.RoundToInt(_upperBound +
                                  Mathf.Pow((Stage - 1) / _range, Exponent) * (_lowerBound - _upperBound));
    }
}