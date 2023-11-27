using CodeBase.Logic;

namespace CodeBase.Services.LogicFactory
{
    public interface ILogicFactoryService : IService
    {
        InventorySlotsHandler InventorySlotsHandler { get; set; }

        void InitInventorySlotsHandler();

        void Cleanup();
    }
}