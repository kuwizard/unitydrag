using UnityEngine;

public class Parent
{
    private readonly Transform _starting;
    private Transform _potential;

    public Parent(Transform parent)
    {
        _starting = parent;
    }

    public Transform Get()
    {
        return _potential == null ? _starting : _potential;
    }
    
    public void SetPotential(Transform value)
    {
        _potential = value == null ? null : value.transform;
    }

    public void Reset()
    {
        _potential = null;
    }
}