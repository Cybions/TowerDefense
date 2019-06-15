using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextLine : MonoBehaviour
{
    public int CharacterIndex;
    public string CharacterTextLine;
    public enum Side
    {
        left,
        right
    }
    public Side CharacterSide;
}
