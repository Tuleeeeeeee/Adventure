using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedFrame : MonoBehaviour
{
    public int fps;
    private void Start()
    {
        
        Application.targetFrameRate = fps;
    }
}
