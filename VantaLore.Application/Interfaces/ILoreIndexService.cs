using VantaLore.Application.Models;
using VantaLore.Domain.Entities;

namespace VantaLore.Application.Interfaces
{
    public interface ILoreIndexService
    {
         Task BuildIndexAsync(List<LoreChunk> chunks);
         IReadOnlyList<IndexedLoreChunk> GetIndex();
    }
}