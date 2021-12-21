using System;

namespace Magitui.Models;

public class AddSavingsEntry
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string BelongsTo { get; set; }
    public string Name { get; set; }
    public float Amount { get; set; }
    public string Company { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime SavedAt { get; set; }
    public DateTime EditedAt { get; set; }
    
    
}