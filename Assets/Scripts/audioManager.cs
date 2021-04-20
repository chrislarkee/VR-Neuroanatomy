using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public static AudioSource audio1;
    public static AudioSource audio2;
    public static audioManager am;

    private bool flip = false;
    //public string status;
    private float lastPlay = -1f;

    private void OnEnable()
    {
        audioManager.audio1 = transform.GetChild(0).GetComponent<AudioSource>();
        audioManager.audio2 = transform.GetChild(1).GetComponent<AudioSource>();
        audioManager.am = this;
        //am.statusUpdate("Ready.");
    }

    //maintain 2 audio players, incase they need to overlap.
    public bool flipper(){
        flip = !flip;
        return flip;
    }

    //public void statusUpdate(string s){
    //    status = s;
    //}

    public static void play(string clipName){
        if (Time.time == am.lastPlay && audio1.clip == audio2.clip) return;

        AudioSource audio3;
        if (am.flipper()) audio3 = audio1;
        else audio3 = audio2;

        AudioClip newAudio = Resources.Load(clipName) as AudioClip;
        if (newAudio == null) {
            Debug.Log("Invalid audio clip name: " + clipName);
            return;
        }
        audio3.clip = newAudio;
        audio3.Play();

        //am.statusUpdate("Playing '" + audio3.clip.name + "' on " + audio3.name);
        am.lastPlay = Time.time;
    }
}
