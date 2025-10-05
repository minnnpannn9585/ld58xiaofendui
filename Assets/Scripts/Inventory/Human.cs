using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Human", menuName = "Inventory/Human")]
public class Human : ScriptableObject
{
    public string name;
    public Sprite humanImage;
    [TextArea]
    public string description;
}
