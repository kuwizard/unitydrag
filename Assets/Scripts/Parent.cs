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
        if (_potential == null) 
        {
            Debug.Log ("_potential was null :( ");
            return _starting;
        }
        return _potential;
    }
    
    public void SetPotential(Transform value) 
    {         
        if (value == null) {
            Debug.Log ("parent has been reset");
            _potential = null;
        } else {
            Debug.Log ("parentToReturn set to: " + value.name);
            _potential = value.transform;
        } 
    }

    public void Reset()
    {
        _potential = null;
    }
    

}