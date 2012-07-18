using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BingImageSearch.Services
{
    public interface ISuspensionManager
    {
        Dictionary<string, object> SessionState { get; }
        List<Type> KnownTypes { get; }
        Task SaveAsync();
        Task RestoreAsync();
    }
}
