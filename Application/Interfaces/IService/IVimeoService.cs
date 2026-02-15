using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adros.Application.Interfaces.IService
{
    public interface IVimeoService
    {
        Task<int> GetDurationAsync(string url);
    }
}
