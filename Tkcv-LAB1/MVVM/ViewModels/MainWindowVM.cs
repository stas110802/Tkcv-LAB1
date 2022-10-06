using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Tkcv_LAB1.Core;
using Tkcv_LAB1.MVVM.Models;

namespace Tkcv_LAB1.MVVM.ViewModels;

public class MainWindowVM : ObservableObject
{
    private string _searchData;
    private string _selectedSort;
    private string _selectedFiltrationSetting;
    private ObservableCollection<string> _sortTypes;
    private ObservableCollection<string> _filtrationSettings;
    private ObservableCollection<Product> _products;

    public MainWindowVM()
    {
        _sortTypes = new ObservableCollection<string>();
        _filtrationSettings = new ObservableCollection<string>();
        _products = new ObservableCollection<Product>();
        
        // for test, then del 
        SortTypes.Add("Alphabet (a-Z)");
        SortTypes.Add("Type");
        _filtrationSettings.Add("Low price to upper");
        _filtrationSettings.Add("Upper price to lower");
        
        Products.Add(new Product()
        {
            Name = "Name of product",
            Price = 2300m,
            VendorCode = "220-444",
            Materials = new List<string>()
            {
                "mat1",
                "mat2",
                "mat3"
            },
            Type = "Type of product",
            PathImg = "D:\\meme\\280fbcdca7af8211808471ecaf067654.jpg"
        });
        Products.Add(new Product()
        {
            Name = "jopaPis",
            Price = 22300m,
            VendorCode = "550-831",
            Materials = new List<string>()
            {
                "mat1",
                "mat2",
            },
            Type = "Tyf produt2",
            PathImg = "D:\\meme\\280fbcdca7af8211808471ecaf067654.jpg"
        });
    }

    public string SearchData
    {
        get => _searchData;
        set => Set(ref _searchData, value, nameof(SearchData));
    }

    public string SelectedFiltrationSetting
    {
        get => _selectedFiltrationSetting;
        set => Set(ref _selectedFiltrationSetting, value, nameof(SelectedFiltrationSetting));
    }

    public string SelectedSort
    {
        get => _selectedSort;
        set => Set(ref _selectedSort, value, nameof(SelectedSort));
    }

    public ObservableCollection<string> SortTypes
    {
        get => _sortTypes;
        set => Set(ref _sortTypes, value, nameof(SortTypes));
    }

    public ObservableCollection<string> FiltrationSettings
    {
        get => _filtrationSettings;
        set => Set(ref _filtrationSettings, value, nameof(FiltrationSettings));
    }

    public ObservableCollection<Product> Products
    {
        get => _products;
        set => Set(ref _products, value, nameof(Products));
    }
}