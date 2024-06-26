using System.Collections;
using UnityEngine;

namespace ThreeMG.Helper.AudioManagement
{
    public static class AudioFadeScript
    {
        public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
        {
            if(!audioSource.isPlaying)
            {
                yield return null;
            }

            audioSource.enabled = true;
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
        }

        public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
        {
            float startVolume = 0.2f;

            audioSource.volume = 0;
            audioSource.Play();

            while (audioSource.volume < 1.0f)
            {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime;

                yield return null;
            }

            audioSource.volume = 1f;
        }
    }
}
