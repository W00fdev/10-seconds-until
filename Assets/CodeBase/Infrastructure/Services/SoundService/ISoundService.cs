namespace Infrastructure.Services.SoundService
{
    public interface ISoundService : IService
    {
        void PlaySoundOfType(SoundType type);
    }
}

