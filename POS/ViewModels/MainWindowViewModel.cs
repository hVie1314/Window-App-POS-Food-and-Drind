using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace POS.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private StorageFolder ? _inputFolder;

    [ObservableProperty]
    private StorageFolder? _outputFolder;

    public MainWindowViewModel()
    {

    }
}

