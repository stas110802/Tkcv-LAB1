using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Tkcv_LAB1.Core;
using Tkcv_LAB1.MVVM.Models;

namespace Tkcv_LAB1.MVVM.ViewModels;

public class MainWindowVM : ObservableObject
{
    private string _searchData;
    private string _selectedSort;
    private string _selectedFiltrationSetting;
    private int _totalPage;
    private ObservableCollection<string> _sortTypes;
    private ObservableCollection<string> _filtrationSettings;
    private ObservableCollection<Product> _allProducts;// all product
    private ObservableCollection<Product> _products;// view data
    private ObservableCollection<Button> _buttons;
    
    public MainWindowVM()
    {
        _totalPage = 1;
        _sortTypes = new ObservableCollection<string>();
        _filtrationSettings = new ObservableCollection<string>();
        _products = new ObservableCollection<Product>();
        _buttons = new ObservableCollection<Button>();
        _allProducts = new ObservableCollection<Product>();
        
        InitButtons();

        // for test, then del 
        SortTypes.Add("Alphabet (a-Z)");
        SortTypes.Add("Type");
        _filtrationSettings.Add("Low price to upper");
        _filtrationSettings.Add("Upper price to lower");
        AddTestData(25);
        WriteViewList(0, 4);
    }

    public void GetPressedButtonNumber(object sender)
    {
        var content = sender.ToString();
        var result = int.TryParse(content, out var newNumberPage);
        
        if (result is false)
        {
            ChangeTotalPageByStep(content);
        }
        else
        {
            if(_totalPage == newNumberPage) return;
            _totalPage = newNumberPage;
        }
        
        var to = _totalPage * 4;
        var from = to - 4;
        
        if (to > _allProducts.Count) to = _allProducts.Count;
        if(from >= _allProducts.Count || from < 0) return;

        WriteViewList(from, to);
        ChangeButtons(_totalPage);
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

    public ObservableCollection<Button> Buttons
    {
        get => _buttons;
        set => Set(ref _buttons, value, nameof(Buttons));
    }

    public ObservableCollection<Product> AllProducts
    {
        get => _allProducts;
        set => Set(ref _allProducts, value, nameof(AllProducts));
    }

    // test method [DELETE THIS]
    private void AddTestData(int count)
    {
        for (var i = 0; i < count; i++)
        {
            AllProducts.Add(new Product()
            {
                Name = $"Name of product{i}",
                Price = new Random().Next(100, 20000),
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
        }
    }

    private void InitButtons()
    {
        var btn = new string[] {"-", "1", "2", "3", "4", "5", "+" };

        _buttons.Clear();
        foreach (var item in btn)
        {
            _buttons.Add(new Button()
            {
                Content = item,
                Command = new BaseCommand(GetPressedButtonNumber),
                CommandParameter = item,
                Width = 20
            });
        }
    }

    private void ChangeButtons(int clickedNumber)
    {
        if(clickedNumber * 4 > _allProducts.Count) return; 

        if (clickedNumber <= 3)
        {
            InitButtons();
            return;
        }

        var length = _buttons.Count;
        var leftCount = length / 2 - 1;
        var first = clickedNumber - leftCount;
        
        for (var i = 1; i < length - 1; i++)
        {
            _buttons[i].Content = first;
            _buttons[i].CommandParameter = first;
            first++;
        }
    }

    private void WriteViewList(int from, int to)
    {
        // write view list
        _products.Clear();
        for (var i = from; i < to; i++)
        {
            _products.Add(_allProducts[i]);
        }
    }

    private void ChangeTotalPageByStep(string content)
    {
        if (content == "+") _totalPage++;
        else if (content == "-" && _totalPage > 1) _totalPage--;
    }
}