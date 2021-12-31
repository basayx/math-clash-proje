using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlace : MonoBehaviour
{
    float clickDelay = 0f;
    public StoryPart previousStoryPart;
    public StoryPart storyPart;
    public StoryManager storyManager;

    public SpriteRenderer connectSpriteToNextStage;
    public Color32 openedConnectSprite;
    public GameObject darkFog;

    private void Start()
    {
        if(PlayerPrefs.GetInt(storyPart.stroyID + "_Completed") == 1)
        {
            if(darkFog != null)
                darkFog.SetActive(false);
            if (connectSpriteToNextStage != null)
                connectSpriteToNextStage.color = openedConnectSprite;
        }
    }

    private void Update()
    {
        if (clickDelay > 0f)
            clickDelay -= 1f * Time.deltaTime;
    }

    public void ClickDown()
    {
        if(previousStoryPart == null || PlayerPrefs.GetInt(previousStoryPart.stroyID + "_Completed") == 1)
            clickDelay = 1f;
    }

    public void ClickUp()
    {
        if (clickDelay > 0f)
        {
            storyManager.StartThisStory(storyPart);
            clickDelay = 0f;
        }
    }
}
