using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsDi.Services
{
    public interface IWaitService
    {
        Task Wait(int milliseconds);
    }

    public class WaitService : IWaitService
    {
        public async Task Wait(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }
    }
}
