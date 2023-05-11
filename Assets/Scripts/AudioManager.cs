using UnityEngine.Audio;
using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;
    // Start is called before the first frame update

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();

            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

        }
    }

    private void Start()
    {

        Play("theme");
        
        
    }


    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        else
        {
            try
            {
                s.source.Play();
            }
            catch (NullReferenceException ex)
            {
                Debug.Log("Found null here" + ex);
                return;
            }
        }

    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        else
        {
            s.source.Pause();

        }
    }

    public void VolumeChange(string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        else
        {
            s.source.volume = value;
        }
    }

    public void PitchChange(string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        else
        {
            s.source.pitch = value;
        }
    }

    public bool playCheck(string name)
    {


        Sound s = Array.Find(instance.sounds, sound => sound.name == name);

        try
        {
            if (s.source.isPlaying)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (NullReferenceException e) { return false; }
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        else
        {
            s.source.Stop();

        }
    }

    public void Unpause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log(name + " not found");
            return;
        }
        else
        {
            s.source.UnPause();

        }
    }


    public IEnumerator FadeOut(string name)
    {
        float start = PlayerPrefs.GetFloat("musicVolume");
        float duration = 2.0f;
        float end = 0f;

        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            float current = Mathf.Lerp(start, end, progress);
            VolumeChange(name, current);
            yield return null;
        }
        Stop(name);
    }
    public IEnumerator FadeIn(string name)
    {
        float start = 0.0f;
        float duration = 2.0f;
        float end = PlayerPrefs.GetFloat("musicVolume"); ;
        if (playCheck(name))
        {
            yield return null;
        }
        Play(name);
        for (float timer = 0; timer < duration; timer += Time.deltaTime)
        {
            float progress = timer / duration;
            float current = Mathf.Lerp(start, end, progress);
            VolumeChange(name, current);
            yield return null;
        }

    }

    public void toogleMusic(bool value)
    {
        
        foreach (Sound s in sounds)
        {
            if (s.category == "music")
            {
                s.source.mute = !value;
            }
        }
    }

    public void toogleSound(bool value)
    {
        
        foreach (Sound s in sounds)
        {
            if (s.category == "sound")
            {
                s.source.mute = !value;
            }
        }
    }

    public void musicVolume(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        foreach (Sound s in sounds)
        {
            if (s.category == "music")
            {
                VolumeChange(s.name, value);
            }
        }
    }

    public void soundVolume(float value)
    {
        PlayerPrefs.SetFloat("soundVolume", value);
        foreach (Sound s in sounds)
        {
            if (s.category == "sound")
            {
                VolumeChange(s.name, value);
            }
        }
    }
}
