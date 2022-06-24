using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp8.ViewModels
{
    public class MainViewModel:ViewModelBase
    {
        public ObservableCollection<Process> processes { get; set; } = new ObservableCollection<Process>(Process.GetProcesses());

        private string txt_search;
        public string Txt_search { get => txt_search; 
            set { 
                txt_search = value;
                RaisePropertyChanged();
            } }
        private string txt_start;
        public string Txt_start
        {
            get => txt_start;
            set
            {
                txt_start = value;
                RaisePropertyChanged();
            }
        }

        private Process slcprocess;
        public Process Slcprocess { get => slcprocess; set 
            { 
                slcprocess = value;
                RaisePropertyChanged();
            } }

        public RelayCommand TextchangedCommand
        {
            get => new RelayCommand
            (
                () =>
                {
                    if (txt_search != null)
                    {
                        bool t = false;
                        foreach (var process in Process.GetProcesses())
                        {
                            if (process.ProcessName.Contains(Txt_search))
                            {
                                t= true;
                                break;
                            }
                        }

                        if (t == true) {
                            processes.Clear();
                            foreach (var process in Process.GetProcesses().Where(t => t.ProcessName.Contains(Txt_search)))
                            { 
                                processes.Add(process);
                            }
                        }
                        
                    }
                    else
                    {
                        processes = new ObservableCollection<Process>(Process.GetProcesses());
                    }
                }
            );
        }
        public RelayCommand StartCommand
        {
            get => new RelayCommand
            (
                () =>
                {
                    try
                    {
                        if (Txt_start != null)
                        {
                            Process.Start(Txt_start);
                            processes.Clear();
                            foreach (var process in Process.GetProcesses())
                            {
                                processes.Add(process);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            );
        }

        public RelayCommand EndCommand
        {
            get => new RelayCommand
            (
                () =>
                {
                    try
                    {
                        if (Slcprocess != null)
                        {
                            foreach (var process in Process.GetProcesses())
                            {
                                if(process.Id==(Slcprocess as Process).Id)
                                {
                                    process.Kill();
                                }
                            }
                            processes.Clear();
                            foreach (var process in Process.GetProcesses())
                            {
                                processes.Add(process);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            );
        }
        public MainViewModel()
        {
            
        }
    }
}
