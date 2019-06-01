using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "TD/Tower", order = 1)]
public class Tower : ScriptableObject
{
    public GameObject T_Prefab;
    public Sprite T_Icon;
    public float T_Price;
    public string T_Name;
    public string T_Description;

    public Tower(Tower Template)
    {
        T_Prefab = Template.T_Prefab;
        T_Icon = Template.T_Icon;
        T_Price = Template.T_Price;
        T_Name = Template.T_Name;
        T_Description = Template.T_Description;
    }
}
