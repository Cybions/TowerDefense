using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNarration : MonoBehaviour
{
    public static LevelNarration Instance;

    [SerializeField]
    private List<Discussion> LevelDiscussions;
    private int currentDiscussion = 0;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Invoke("AddListeners", .05f);
    }

    public void StartNextDiscussion()
    {
        NarrationManager.Instance.NewDiscussion(LevelDiscussions[currentDiscussion]);
        currentDiscussion++;
    }

    private void AddListeners()
    {
        LevelManager.Instance.OnLevelStart.AddListener(r_OnStartLevel);
        TowerSelector.Instance.OnTowerBuilt.AddListener(r_OnTowerBuilt);
        TowerSelector.Instance.OnTowerUpgraded.AddListener(r_OnTowerUpgraded);
        LevelManager.Instance.OnWaveEnd.AddListener(r_OnWaveEnd);
        LevelManager.Instance.OnLevelEnd.AddListener(r_OnWaveEnd);
        NarrationManager.Instance.OnEndDiscussion.AddListener(r_OnEndDiscussion);
    }

    private void r_OnStartLevel()
    {
        if (!canTrigger()) { return; }
        if(LevelDiscussions[currentDiscussion].ConditionTrigger == Discussion.DiscussionTrigger.OnStartLevel) { StartNextDiscussion(); }
    }
    private void r_OnTowerBuilt()
    {
        if (!canTrigger()) { return; }
        if (LevelDiscussions[currentDiscussion].ConditionTrigger == Discussion.DiscussionTrigger.OnTowerBuilt) { StartNextDiscussion(); }
    }
    private void r_OnTowerUpgraded()
    {
        if (!canTrigger()) { return; }
        if (LevelDiscussions[currentDiscussion].ConditionTrigger == Discussion.DiscussionTrigger.OnTowerUpgraded) { StartNextDiscussion(); }
    }
    private void r_OnWaveEnd()
    {
        if (!canTrigger()) { return; }
        if (LevelDiscussions[currentDiscussion].ConditionTrigger == Discussion.DiscussionTrigger.OnWaveEnd) { StartNextDiscussion(); }
    }
    private void r_OnEndLevel()
    {
        if (!canTrigger()) { return; }
        if (LevelDiscussions[currentDiscussion].ConditionTrigger == Discussion.DiscussionTrigger.OnEndLevel) { StartNextDiscussion(); }
    }
    private void r_OnEndDiscussion()
    {
        if (!canTrigger()) { return; }
        if (LevelDiscussions[currentDiscussion].ConditionTrigger == Discussion.DiscussionTrigger.OnEndDiscussion) { StartNextDiscussion(); }
    }

    public bool canTrigger()
    {
        return (LevelDiscussions.Count > currentDiscussion);
    }
}
