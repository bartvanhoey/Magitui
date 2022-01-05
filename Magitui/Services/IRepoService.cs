using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magitui.Models;

namespace Magitui.Services
{
    public interface IRepoService
    {
        Task<List<AddSavingsEntry>> ReadSavingsFileAsync();
        Task UpdateSavingsFileAsync<T>(T content);
        Task<string> ReadDebtsFileAsync();
        Task UpdateDebtsFileAsync(string content);
    }


}
