using System.Collections.Generic;
using DAL.Entities;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISizeRepository
    {
        Task CreateSizeAsync(Size size);
        Task<IEnumerable<Size>> GetAllSizesAsync();
    }
}
