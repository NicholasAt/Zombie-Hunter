using CodeBase.Logic;
using CodeBase.Services.PersistentProgress;

namespace CodeBase.Services.LogicFactory
{
    public class LogicFactoryService : ILogicFactoryService
    {
        public InventorySlotsHandler InventorySlotsHandler { get; set; }
        private readonly IPersistentProgressService _persistentProgressService;

        public LogicFactoryService(IPersistentProgressService persistentProgressService)
        {
            _persistentProgressService = persistentProgressService;
        }

        public void Cleanup()
        {
            InventorySlotsHandler?.Clean();
            InventorySlotsHandler = null;
        }

        public void InitInventorySlotsHandler() =>
            InventorySlotsHandler = new InventorySlotsHandler(_persistentProgressService);
    }
}