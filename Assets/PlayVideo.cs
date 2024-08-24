using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    public VideoPlayer video;
    private void Start()
    {
        video.url = System.IO.Path.Combine(Application.streamingAssetsPath, "MainMenu.mp4");
        video.Play();
    }
}
