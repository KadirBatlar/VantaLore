using System;
using System.Collections.Generic;
using System.Text;
using VantaLore.Application.Interfaces;
using VantaLore.Domain.Entities;

namespace VantaLore.Infrastructure.Repositories
{
    public class LoreRepository : ILoreRepository
    {
        public List<LoreChunk> GetAll()
        {
            return new List<LoreChunk>
        {
            new LoreChunk
            {
                Id = 1,
                Universe = "LoL",
                Character = "Aatrox",
                Content = "Aatrox is a Darkin warrior who was once a noble Ascended..."
            },
            new LoreChunk
            {
                Id = 2,
                Universe = "LoL",
                Character = "Yasuo",
                Content = "Yasuo is a skilled swordsman from Ionia..."
            }
        };
        }
    }
}
