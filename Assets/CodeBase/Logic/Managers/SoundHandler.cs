using Infrastructure.Services.SoundService;
using Infrastructure;
using UnityEngine;

namespace Logic.Managers
{
    public class SoundHandler : MonoBehaviour
    {
        [SerializeField] private AudioData _gameAudioData;
        [SerializeField] private AudioData _shootAudioData;
        [SerializeField] private AudioData _jumpAudioData;
        [SerializeField] private AudioData _impactAudioData;
        [SerializeField] private AudioData _deadAudioData;
        [SerializeField] private AudioData _trapAudioData;

        public void Init()
        {
            AllServices.Container.RegisterSingle<ISoundService>(new SoundService(_gameAudioData, _shootAudioData,
                _jumpAudioData, _impactAudioData, _deadAudioData, _trapAudioData));

            DontDestroyOnLoad(gameObject);
        }
    }
}
