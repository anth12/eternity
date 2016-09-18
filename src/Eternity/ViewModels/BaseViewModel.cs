using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using PropertyChanged;
using Eternity.Utilities.Commands;

namespace Eternity.ViewModels
{
    [ImplementPropertyChanged]
    internal abstract class BaseViewModel : INotifyPropertyChanged, IViewModel
    {
        protected BaseViewModel()
        {
            PropertyChanged += BaseViewModel_PropertyChanged;
            AttachEventCommands();
        }
        
        #region Command Helpers

        protected RelayCommand CommandFromFunction(Action<object> func)
        {
            return new RelayCommand(func);
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        #region Property changed event handler
        protected List<KeyValuePair<string, Action>> PropertyChangeHandlers { get; set; } = new List<KeyValuePair<string, Action>>();

        private void BaseViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var handlers = PropertyChangeHandlers.Where(h => h.Key == e.PropertyName);

            handlers.ToList().ForEach(h => h.Value.Invoke());
        }

        protected void OnPropertyChanged(string property, Action eventHandler)
        {
            PropertyChangeHandlers.Add(new KeyValuePair<string, Action>(property, eventHandler));
        }
        #endregion

        #region Command auto-wireup

        private void AttachEventCommands()
        {
            var type = this.GetType();

            var iCommandType = typeof(ICommand);
            var commands =  type.GetProperties()
                                .Where(prop => iCommandType.IsAssignableFrom(prop.PropertyType));

            foreach (var command in commands)
            {
                var eventHandlerName = Regex.Replace(command.Name, "Command$", "");
                var eventHandler = type.GetMethod(eventHandlerName, BindingFlags.Instance 
                                                                  | BindingFlags.NonPublic 
                                                                  | BindingFlags.Public);

                if(eventHandler == null)
                    throw new Exception($"Cannot find event handler for `{eventHandlerName}`");

                command.SetValue(this, new RelayCommand(e=> eventHandler.Invoke(this, null)));
            }
        }
        #endregion
    }
}
