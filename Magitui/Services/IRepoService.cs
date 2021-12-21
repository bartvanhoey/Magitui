using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magitui.Services
{
    public interface IRepoService
    {
        Task<string> ReadSavingsFileAsync();
        Task UpdateSavingsFileAsync<T>(T content);
        Task<string> ReadDebtsFileAsync();
        Task UpdateDebtsFileAsync(string content);
    }


}
