using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTxtLine", menuName = "Discussion/Textline", order = 1)]
public class TextLine : ScriptableObject
{
    public Character Speaker;
    public string CharacterTextLine;
    public enum Side
    {
        left,
        right
    }
    public Side CharacterSide;
    public bool DoScreenShake = false;
}
