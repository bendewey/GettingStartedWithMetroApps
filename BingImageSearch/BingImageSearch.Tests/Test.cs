using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BingImageSearch.Adapters;
using BingImageSearch.Messaging;
using BingImageSearch.Messaging.Handlers;
using BingImageSearch.Model;
using NotificationsExtensions.ToastContent;

namespace BingImageSearch.Tests
{
    public class Test<TScenario> where TScenario : class, new()
    {
        private TScenario _scenario;

        public TScenario Given
        {
            get { return _scenario ?? (_scenario = new TScenario()); }
        }

        public TScenario Then
        {
            get { return _scenario ?? (_scenario = new TScenario()); }
        }

        public TScenario When
        {
            get { return _scenario ?? (_scenario = new TScenario()); }
        }
    }
}
