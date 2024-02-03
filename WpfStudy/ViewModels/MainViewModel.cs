using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfStudy.Views;

namespace WpfStudy.ViewModels
{

    class MainViewModel : INotifyPropertyChanged
    {
        private int progressValue;
        public ICommand TestClick { get; set; }

        public ICommand SecondShow { get; set; }
        public MainViewModel()
        {
            //TestClick = new RelayCommand<object>(ExecuteMyButton, CanMyButton);
            TestClick = new AsyncRelayCommand(ExecuteMyButton2);
            SecondShow = new AsyncRelayCommand(ExecuteMyButton3);
            // Task t = ExecuteMyButton2();
        }
        public int ProgressValue
        {
            get { return progressValue; }
            set { 
                progressValue = value;
                NotifyPropertyChanged(nameof(ProgressValue));
            }
        }

        bool CanMyButton(object param)
        {
            if (param == null)
            {
                return true;
            }
            return param.ToString().Equals("ABC")? true : false;
        }

        void ExecuteMyButton(object param)
        {
            int w = 0;
            //MessageBox.Show(param.ToString() + "AAA");
            Task task1 = Task.Run(() => 
            {
                for(int i = 0; i < 10; i++)
                {
                    ProgressValue = i;
                }
            });

            Task rtn2 = Task.Run(() =>
            {
                for(int i = 0; i < 50; i++)
                {
                    ProgressValue = i;
                    w = i;
                    Thread.Sleep(2000);
                }
            });

            //rtn2.Wait();
            MessageBox.Show(w + "");
        }

        public async Task ExecuteMyButton2()
        {
            int w = 0;
            //MessageBox.Show(param.ToString() + "AAA");

            Task<int> rtn2 = Task.Run(() =>
            {
                for (int i = 0; i < 50; i++)
                {
                    ProgressValue = i;
                    w = i;
                    Thread.Sleep(2000);
                }
                int j = 5;
                return j;
            });

            w = await rtn2;

            MessageBox.Show(w + "");
        }

        public async Task ExecuteMyButton3()
        {
            SecondView secondView = new SecondView();
            SecondViewModel aa = new SecondViewModel();
            aa.ProgressValue = 70;
            secondView.DataContext = aa;

            secondView.ShowDialog();

            await Task.Delay(0);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
