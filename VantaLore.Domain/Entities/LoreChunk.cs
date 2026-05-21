namespace VantaLore.Domain.Entities;

public class LoreChunk : IEntity
{
    public int Id { get; set; }    
    public string Universe { get; set; }
    public string Character { get; set; }
    public string Content { get; set; }

    public float[] Embedding { get; set; }
}