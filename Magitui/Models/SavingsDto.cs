using Magitui.Services.Calculator;
using Magitui.Services.File;

namespace Magitui.Models
{
    public class SavingsDto : ICalculable, IHaveGuidId
    {
        public Guid Id { get; set; } 
        public string BelongsTo { get; set; }
        public string Name { get; set; }
        public float Amount { get; set; }
        public string Currency { get; set; } 
        public string Institution { get; set; }
        public string ReferenceNumber { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime SavedAt { get; set; } = DateTime.Now;
        public DateTime EditedAt { get; set; } = DateTime.Now;
    }
}
