using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The fields of this clss will show in the inspector
[System.Serializable]
public class Stat
{

    [SerializeField]
    private int baseValue;
    // Starting value for given stat

    private List<int> modifiers = new List<int>();

    public int GetValue()
    {
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x);
        return finalValue;
    }

    public void AddModifier (int modifier)
    {
        if (modifier != 0)
            modifiers.Add(modifier);
    }

    public void RemoveModifier (int modifier)
    {
        if (modifier != 0)
            modifiers.Remove(modifier);
    }
}
