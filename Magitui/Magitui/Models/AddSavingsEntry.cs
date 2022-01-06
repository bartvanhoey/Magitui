using Magitui.Services.Calculator;
using Magitui.Services.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Models
{
    public class AddSavingsEntry : ICalculable, IHaveGuidId
    {
        public Guid Id { get; set; } = Guid.NewGuid();
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
