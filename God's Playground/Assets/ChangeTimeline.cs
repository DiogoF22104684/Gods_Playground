using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;



public class ChangeTimeline : MonoBehaviour
{
    private PlayableDirector director;
    

    // Start is called before the first frame update
    void Start()
    {
        director = GetComponent<PlayableDirector>();
    }

    public void ChangeTimelineClip(PlayableAsset clip)
    {
        director.playableAsset = clip;
        director.Play();
    }
}
