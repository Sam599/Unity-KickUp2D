using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            if (s.musicTheme)
            {
                s.source.Play();
                s.source.loop = true;
            }
        }
    }

    public void Play(string soundName, bool needLoop = false)
    {
        int foundSound = 0;
        foreach(Sound s in sounds)
        {
            if(s.name.Equals(soundName))
            {
                foundSound++;
                //Debug.Log("Found Sound!");
            }
        }
        System.Random rndNumber = new System.Random();
        int soundIndex = rndNumber.Next(foundSound);
        Sound sound = Array.Find(sounds, s => s.name == soundName && s.id == soundIndex);

        sound.source.loop = needLoop;
        sound.source.Play();
        //Debug.Log("Playing Sound! Name: " + sound.name + " Id: " + sound.id);
    }

    public void Stop(string soundName)
    {
        Sound[] sound = Array.FindAll(sounds, s => s.name == soundName);

        for(int i = 0; i < sound.Length; i++)
        {
            Sound foundSound = Array.Find(sound, s => s.name == soundName && s.id == i);

            if (foundSound.source.isPlaying)
            {
                foundSound.source.Stop();
                Debug.Log("Stop playing sound, Name: " + foundSound.name + " Id: " + foundSound.id);
            }
        }
        
    }
    public bool isPlaying(string soundName)
    {
        Sound[] sound = Array.FindAll(sounds, s => s.name == soundName);
        bool isPlaying = false;

        for (int i = 0; i < sound.Length; i++)
        {
            Sound foundSound = Array.Find(sound, s => s.name == soundName && s.id == i);

            if (foundSound.source.isPlaying)
            {
                isPlaying = true;
                //Debug.Log(foundSound.name + " Sound is playing");
            }
        }
        return isPlaying;
    }

}
