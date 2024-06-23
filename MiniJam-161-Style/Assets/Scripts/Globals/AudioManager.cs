using System.Collections.Generic;
using UnityEngine;

namespace Globals
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public AudioSource musicSource;
        public AudioSource sfxSource;

        [System.Serializable]
        public class Sound
        {
            public string name;
            public AudioClip clip;
        }

        public List<Sound> musicClips = new List<Sound>();
        public List<Sound> sfxClips = new List<Sound>();

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayMusic(string name)
        {
            Sound s = musicClips.Find(sound => sound.name == name);
            if (s != null)
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }

        public void PlaySFX(string name)
        {
            Sound s = sfxClips.Find(sound => sound.name == name);
            if (s != null)
            {
                sfxSource.PlayOneShot(s.clip);
            }
        }

        public void StopMusic()
        {
            musicSource.Stop();
        }

        public void SetMusicVolume(float volume)
        {
            musicSource.volume = volume;
        }

        public void SetSFXVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}