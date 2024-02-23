using Prism.Commands;
using System;
using System.Windows.Media;

namespace SatisfactoryProductionManager.ViewModel.ButtonModels
{
    public abstract class ObjectButtonVM<T>
    {
        public T InnerObject { get; protected set; }
        public ImageSource ImageSource { get; protected set; }

        public event Action<T> ObjectSelected;

        public DelegateCommand ReturnObject { get; }

        
        public ObjectButtonVM()
        {
            ReturnObject = new DelegateCommand(ReturnObject_CommandHandler);
        }

        protected void ReturnObject_CommandHandler()
        {
            ObjectSelected?.Invoke(InnerObject);
        }
    }
}
