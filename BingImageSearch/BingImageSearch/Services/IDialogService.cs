using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingImageSearch.Services
{
    public interface IDialogService
    {
        Task ShowMessageAsync(string message);
        Task ShowResourceMessageAsync(string key);
    }
}
