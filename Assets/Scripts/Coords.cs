using UnityEngine;
using System.Collections.Generic;

public class Coords
{
    private List<float> _xCoords;
    private float _wasBetweenLeft;
    private float _wasBetweenRight;
    private int _lastPosition;
    
    public delegate void Position(int newPlaceholderPosition);
    public static event Position PositionChanged;

    public Coords(List<float> xCoordsOnScreen)
    {
        var xCoords = new List<float> {0};
        xCoords.AddRange(xCoordsOnScreen);
        xCoords.Add(Screen.width);
        _xCoords = xCoords;
        _lastPosition = -1;
    }

    public void SetCurrentX(float x)
    {
        if (_lastPosition == -1)
        {
            CalculateBetweens(x);
            if (PositionChanged != null) PositionChanged(_lastPosition);
        }

        if (x < _wasBetweenLeft || _wasBetweenRight < x)
        {
            CalculateBetweens(x);
            if (PositionChanged != null) PositionChanged(_lastPosition);
        }
    }

    private void CalculateBetweens(float x)
    {
        int i = 0;
        while (x > _xCoords[i])
        {
            i++;
        }
        _wasBetweenLeft = _xCoords[i - 1];
        _wasBetweenRight = _xCoords[i];
        _lastPosition = i - 1;
    }
}