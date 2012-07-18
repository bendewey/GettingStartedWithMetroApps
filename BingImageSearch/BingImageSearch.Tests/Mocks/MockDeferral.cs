using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Adapters;

namespace BingImageSearch.Tests.Mocks
{
    public class MockDeferral : IDeferral
    {
        public MockDeferral()
        {
        }

        public bool WasCompleted { get; set; }

        public void Complete()
        {
            WasCompleted = true;
        }
    }
}
