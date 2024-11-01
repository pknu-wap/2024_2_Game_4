using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    void Awake()
    {
        Screen.SetResolution(1920, 1080, false);
    }
}
