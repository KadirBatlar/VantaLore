using VantaLore.Domain.Entities;

namespace VantaLore.Application.Interfaces;

public interface IRetrievalService
{
    Task<List<LoreChunk>> Retrieve(string query);
}