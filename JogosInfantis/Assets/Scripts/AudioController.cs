using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    public List<AudioSource> IngameAudios = new List<AudioSource>();
    public List<AudioSource> UIAudios = new List<AudioSource>();


    public void AddIngameAudio(AudioSource audioSource)
    {
        IngameAudios.Add(audioSource);
    }


    public void AddUIAudio(AudioSource audioSource)
    {
        UIAudios.Add(audioSource);
    }


    public void ChangeVolumeAllAudios(float value)
    {
        ChangeVolumeIngameAudios(value);
        ChangeVolumeUIAudios(value);
    }


    public void ChangeVolumeIngameAudios(float value)
    {
        foreach (var a in IngameAudios)
            a.volume = value;
    }


    public void ChangeVolumeUIAudios(float value)
    {
        foreach (var a in UIAudios)
            a.volume = value;
    }
}
