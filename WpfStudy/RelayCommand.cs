﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfStudy
{
    class RelayCommand<T> : ICommand
    {
        readonly Action<T> _execute;
        readonly Predicate<T> _canexecute;
        public RelayCommand(Action<T> excute, Predicate<T> predicate)
        {
            _execute = excute;
            _canexecute = predicate;
        }
        public RelayCommand(Action<T> action) :this(action, null)
        {

        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }

        }


        public bool CanExecute(object parameter)
        {
            return _canexecute == null ? true : _canexecute((T)parameter);

        }
        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
