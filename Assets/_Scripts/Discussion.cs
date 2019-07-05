using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDiscussion", menuName = "Discussion/Discussion", order = 2)]
public class Discussion : ScriptableObject
{
    public List<TextLine> TextList;
    public DiscussionTrigger ConditionTrigger;
    public enum DiscussionTrigger
    {
        OnStartLevel,
        OnTowerBuilt,
        OnTowerUpgraded,
        OnWaveEnd,
        OnEndLevel,
        OnEndDiscussion,
        none
    }
}
