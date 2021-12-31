using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryPartUIGroup : MonoBehaviour
{
    public StoryManager storyManager;
    public StoryPart storyPart;

    [Header("UI")]
    public Image iconImage;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    public void StartToStory()
    {
        storyManager.PlayThisStory(storyPart);
    }
}
