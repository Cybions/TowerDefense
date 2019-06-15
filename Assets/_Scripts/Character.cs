using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "TD/Character", order = 2)]
public class Character : ScriptableObject
{
    public string Name;
    public Color BackgroundColor;
    public Sprite Icon;
}
