using BingImageSearch.Adapters;

namespace BingImageSearch.Tests.Mocks
{
    public class MockPickerFactory : IPickerFactory
    {
        private MockFileSavePickerAdapter _mockFileSavePicker;
        public MockFileSavePickerAdapter MockFileSavePicker
        {
            get { return _mockFileSavePicker ?? (_mockFileSavePicker = new MockFileSavePickerAdapter()); }
        }

        public IFileSavePicker CreateFileSavePicker()
        {
            return MockFileSavePicker;
        }
    }
}