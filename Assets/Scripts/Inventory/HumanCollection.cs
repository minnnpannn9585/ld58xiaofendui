using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HumanCollection", menuName = "Inventory/HumanCollection")]
public class HumanCollection : ScriptableObject
{
    public List<Human> humans = new List<Human>();
}
