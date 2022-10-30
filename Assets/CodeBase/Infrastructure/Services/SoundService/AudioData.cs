using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Infrastructure.Services.SoundService
{
    [Serializable]
    public class AudioData
    {
        public AudioSource Source;
        public AudioClip[] Clips;
        public SoundType[] Types;

        public bool OneShotClip;

        public void PlaySoundType(SoundType type)
        {
            int i = 0;
            foreach (SoundType extractedType in Types)
            {
                if (extractedType == type)
                {
                    if (OneShotClip == true)
                    {
                        Source.pitch = 1.0f + Random.Range(0.2f, 0.4f);
                        Source.PlayOneShot(Clips[i]);
                    }
                    else
                    {
                        Source.clip = Clips[i];
                        Source.Play();
                    }

                    return;
                }

                i++;
            }

            throw new System.ArgumentException("SoundType doesn't exist: " + type);
        }
    }

}

