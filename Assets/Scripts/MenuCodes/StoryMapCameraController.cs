using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tools;

public class StoryMapCameraController : MonoBehaviour
{
    public GameObject StoryMapGroup;
    public Camera Cam;
    public Vector2 XDirectionLimits;
    public Vector2 ZDirectionLimits;
    private bool isTouching;
    public float CamMovementSpeed = 10f;
    void Start()
    {
        TouchManager.Instance.onTouchBegan += TouchBegan;
        TouchManager.Instance.onTouchMoved += TouchMoved;
        TouchManager.Instance.onTouchEnded += TouchEnded;
    }

    private void TouchBegan(TouchInput touch)
    {
        isTouching = true;
    }

    private void TouchMoved(TouchInput touch)
    {
        if (isTouching && StoryMapGroup.activeSelf)
        {
            float xVal = touch.DeltaScreenPosition.x;
            float yVal = touch.DeltaScreenPosition.y;
            xVal = -1 * Mathf.Clamp(xVal, -4f, 4f);
            yVal = -1 * Mathf.Clamp(yVal, -4f, 4f);
            if (!(xVal > 0f && Cam.transform.position.x < XDirectionLimits.y) &&
                !(xVal < 0f && Cam.transform.position.x > XDirectionLimits.x))
                xVal = 0f;
            if (!(yVal > 0f && Cam.transform.position.z < ZDirectionLimits.y) &&
                !(yVal < 0f && Cam.transform.position.z > ZDirectionLimits.x))
                yVal = 0f;
            Cam.transform.position += new Vector3(xVal, 0f, yVal) * CamMovementSpeed * Time.deltaTime;
        }
    }

    private void TouchEnded(TouchInput touch)
    {
        isTouching = false;
    }
}
