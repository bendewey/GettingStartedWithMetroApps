namespace BingImageSearch.Adapters
{
    public class PickerFactory : IPickerFactory
    {
        public IFileSavePicker CreateFileSavePicker()
        {
            return new FileSavePickerAdapter();
        }
    }
}