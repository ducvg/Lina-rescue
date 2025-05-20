using System;
using UnityEngine;

public class ScreenEnforcer : MonoBehaviour
{
    [SerializeField] private bool isLandscape = true;

    void Start()
    {
        if (isLandscape)
        {
            SetLandscapeMode();
        }
        else
        {
            SetPortraitMode();
        }       

    }

    private void SetPortraitMode()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
    }

    private void SetLandscapeMode()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }
}
