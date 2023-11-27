namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadProgressService : IService
    {
        void LoadProgress();

        void SaveProgress();
    }
}