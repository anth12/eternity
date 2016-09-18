using PropertyChanged;

namespace Eternity.ViewModels
{
    [ImplementPropertyChanged]
    internal abstract class BaseViewModel<TModel> : BaseViewModel
    {
        
        public TModel Model { get; set; }
        
    }
}
