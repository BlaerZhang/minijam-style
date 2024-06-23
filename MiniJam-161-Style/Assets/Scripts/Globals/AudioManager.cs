using System;
using System.Collections.Generic;
using UnityEngine;

namespace Globals
{
    public class AudioManager : MonoBehaviour
    {
        public AudioSource musicSource;
        public AudioSource sfxSource;

        [Serializable]
        public class Sound
        {
            public string name;
            public AudioClip clip;
        }

        public List<Sound> musicClips = new List<Sound>();
        public List<Sound> sfxClips = new List<Sound>();

        public void PlayMusic(string musicName)
        {
            Sound s = musicClips.Find(sound => sound.name == musicName);
            if (s != null)
            {
                musicSource.clip = s.clip;
                musicSource.Play();
            }
        }

        public void PlaySfx(string sfxName)
        {
            Sound s = sfxClips.Find(sound => sound.name == sfxName);
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

        public void SetSfxVolume(float volume)
        {
            sfxSource.volume = volume;
        }
    }
}