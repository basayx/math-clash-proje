using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StoryPart", menuName = "StroyPart")]
public class StoryPart : ScriptableObject
{
    public string stroyID;
    [Header("Menu Properties")]
    public string stroyName;
    public string stroyDescription;
    public Sprite storyIconSprite;

    [Header("Enemy Properties")]
    [Tooltip("attacker || defender")]
    public string playerThisRoundSide = "attacker"; // attacker | defender
    public string userName;
    public int level;
    public string profileImageSpriteName;
    [Tooltip("kartID_(kartNo)|(yildizSayisi)\n\ntemelTipCannon_0|0")]
    public string toplamaCannonID, cikartmaCannonID, carpmaCannonID, bolmeCannonID;
    [Tooltip("kartID_(kartNo)|(yildizSayisi)#...\n\nanubis_0|0#forestOrc_1|0#...")]
    public string attackerGameDeckInfo;
    public int totalToplamaQuestionCount, totalCikartmaQuestionCount, totalCarpmaQuestionCount, totalBolmeQuestionCount;
    public int correctAnsweredToplamaQuestionCount, correctAnsweredCikartmaQuestionCount, correctAnsweredCarpmaQuestionCount, correctAnsweredBolmeQuestionCount;

    [System.Serializable]
    public struct StoryTalk
    {
        public StoryTalk(bool isNull)
        {
            InTalkPersonName = "";
            TalkText = "";
            InTalkPersonID = "";
            BackgroundID = "";
            IconsID = "";
        }
        public StoryTalk(StoryTalk storyTalk)
        {
            InTalkPersonName = storyTalk.InTalkPersonName;
            TalkText = storyTalk.TalkText;
            InTalkPersonID = storyTalk.InTalkPersonID;
            BackgroundID = storyTalk.BackgroundID;
            IconsID = storyTalk.IconsID;
        }

        [Header(".")]
        public string InTalkPersonName;
        [TextArea]
        public string TalkText;
        public string InTalkPersonID;
        public string BackgroundID;
        public string IconsID;
    }

    [Header("Story Talk Properties")]
    public List<StoryTalk> BeforeStoryTalk = new List<StoryTalk>();
    public List<StoryTalk> AfterWinStoryTalk = new List<StoryTalk>();
    public List<StoryTalk> AfterLoseStoryTalk = new List<StoryTalk>();
}