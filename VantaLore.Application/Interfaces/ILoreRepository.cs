using VantaLore.Domain.Entities;

namespace VantaLore.Application.Interfaces
{
    public interface ILoreRepository
    {
        List<LoreChunk> GetAll();
    }
}
