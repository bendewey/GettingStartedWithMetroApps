using System;
using BingImageSearch.Adapters;
using MetroIoc;
using BingImageSearch.Messaging;
using BingImageSearch.Model;
using BingImageSearch.Services;
using BingImageSearch.ViewModels;

namespace BingImageSearch.Common
{
    public class ViewModelLocator
    {
        private Lazy<IContainer> container;
        public IContainer Container
        {
            get { return container.Value; }
        }

        public ViewModelLocator()
        {
            container = new Lazy<IContainer>(IoC.BuildContainer);
        }

        public INavigationService NavigationService
        {
            get { return Container.Resolve<INavigationService>(); }
        }

        public IHub Hub
        {
            get { return Container.Resolve<IHub>(); }
        }

        public IDialogService DialogService
        {
            get { return Container.Resolve<IDialogService>(); }
        }

        public IFileOpenPickerUiManager FileOpenPickerUiManager
        {
            get { return Container.Resolve<IFileOpenPickerUiManager>(); }
        }
        
        public ShellViewModel ShellViewModel
        {
            get { return Container.Resolve<ShellViewModel>(); }
        }

        public SearchHistoryPageViewModel SearchHistoryPageViewModel
        {
            get { return Container.Resolve<SearchHistoryPageViewModel>(); }
        }

        public SearchResultsPageViewModel SearchResultsPageViewModel
        {
            get { return Container.Resolve<SearchResultsPageViewModel>(); }
        }

        public DetailsPageViewModel DetailsPageViewModel
        {
            get { return Container.Resolve<DetailsPageViewModel>(); }
        }

        public FileOpenPickerPageViewModel FileOpenPickerPageViewModel
        {
            get { return Container.Resolve<FileOpenPickerPageViewModel>(); }
        }

        public PreferencesViewModel PreferencesViewModel
        {
            get { return Container.Resolve<PreferencesViewModel>(); }
        }
    }
}
