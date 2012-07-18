using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;

namespace BingImageSearch.Tests
{
    [TestClass]
    public class IocTests
    {
        [TestMethod]
        public void WhenBuildingContainer_ShouldSucceed()
        {
            var container = IoC.BuildContainer();
            Assert.IsNotNull(container);
        }
    }
}
