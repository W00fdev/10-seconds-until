namespace Infrastructure.Services.SoundService
{
    public class SoundService : ISoundService
    {
        private readonly AudioData _gameAudioData;
        private readonly AudioData _shootAudioData;
        private readonly AudioData _jumpAudioData;
        private readonly AudioData _impactAudioData;
        private readonly AudioData _deadAudioData;
        private readonly AudioData _trapAudioData;

        public SoundService(AudioData gameAudioData, AudioData shootAudioData, AudioData jumpAudioData,
            AudioData impactAudioData, AudioData deadAudioData, AudioData trapAudioData)
        {
            _gameAudioData = gameAudioData;
            _shootAudioData = shootAudioData;
            _jumpAudioData = jumpAudioData;
            _impactAudioData = impactAudioData;
            _deadAudioData = deadAudioData;
            _trapAudioData = trapAudioData;
        }


        public void PlaySoundOfType(SoundType type)
        {
            switch(type)
            {
                case SoundType.GAMELOOP:
                    _gameAudioData.PlaySoundType(type);
                    break;

                case SoundType.GAMELOSE:
                    _gameAudioData.PlaySoundType(type);
                    break;

                case SoundType.GAMEVICTORY:
                    _gameAudioData.PlaySoundType(type);
                    break;

                case SoundType.SHOOT:
                    _shootAudioData.PlaySoundType(type);
                    break;

                case SoundType.JUMP:
                    _jumpAudioData.PlaySoundType(type);
                    break;

                case SoundType.DEAD:
                    _deadAudioData.PlaySoundType(type);
                    break;

                case SoundType.IMPACT:
                    _impactAudioData.PlaySoundType(type);
                    break;

                case SoundType.TRAP:
                    _trapAudioData.PlaySoundType(type);
                    break;

                default:
                    throw new System.ArgumentException("SoundType doesn't exist: " + type);
            }
        }

    }
}

