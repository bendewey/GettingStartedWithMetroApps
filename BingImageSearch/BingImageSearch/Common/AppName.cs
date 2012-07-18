using System;
namespace BingImageSearch.Common
{
    public class AppName
    {
        private readonly Lazy<string> _name;

        public AppName(string name) : this (new Lazy<string>(() => name))
        {
        }

        public AppName(Lazy<string> name)
        {
            _name = name;
        }

        public override string ToString()
        {
            return _name.Value.ToString();
        }

        public static implicit operator string(AppName name)
        {
            return name == null ? null : name.ToString();
        }

        public static implicit operator AppName(string name)
        {
            return new AppName(name);
        }
    }
}
