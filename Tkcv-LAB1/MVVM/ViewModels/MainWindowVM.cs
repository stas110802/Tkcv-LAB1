using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Tkcv_LAB1.Core;
using Tkcv_LAB1.Helpers;
using Tkcv_LAB1.MVVM.Models;

namespace Tkcv_LAB1.MVVM.ViewModels;

public class MainWindowVM : ObservableObject
{
    private string _searchData;
    private string _selectedSort;
    private string _selectedFiltrationSetting;
    private int _totalPage;
    private readonly int _maxCountDisplay;

    private ObservableCollection<string> _sortTypes;
    private ObservableCollection<string> _filtrationSettings;
    private ObservableCollection<Product> _allProducts;// all products [rework to list]
    private ObservableCollection<Product> _displayedDisplayedProducts;// displayed products
    private List<Product> _viewProducts;
    
    private ObservableCollection<Button> _buttons;

    public MainWindowVM()
    {
        _maxCountDisplay = 4;
        _sortTypes = new ObservableCollection<string>();
        _filtrationSettings = new ObservableCollection<string>();
        _displayedDisplayedProducts = new ObservableCollection<Product>(); // max - _countView
        _buttons = new ObservableCollection<Button>();
        _allProducts = new ObservableCollection<Product>();
        _viewProducts = new List<Product>();
        
        InitButtons();

        // for test, then del 
        SortTypes.Add("Price: Low to high");
        SortTypes.Add("Price: High to low");

        AddTestData(235);
        TotalPage = 1;
    }

    public void ChangeTotalPageByButtonContent(object sender)
    {
        var content = sender.ToString();
        var result = int.TryParse(content, out var newNumberPage);
        
        if (result is false)
        {
            ChangeTotalPageByStep(content);
        }
        else
        {
            if(TotalPage == newNumberPage) return;
            TotalPage = newNumberPage;
        }
    }

    public string SearchData
    {
        get => _searchData;
        set
        {
            Set(ref _searchData, value, nameof(SearchData));
            OnChangeSearchBox();
            TotalPage = 1;
        }
    }

    public string SelectedFiltrationSetting
    {
        get => _selectedFiltrationSetting;
        set
        {
            Set(ref _selectedFiltrationSetting, value, nameof(SelectedFiltrationSetting));
            OnFiltrationSettingChanged();
        } 
    }

    public string SelectedSort
    {
        get => _selectedSort;
        set
        {
            Set(ref _selectedSort, value, nameof(SelectedSort));
            OnChangedSort();
            TotalPage = 1;
        }
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

    public ObservableCollection<Product> DisplayedProducts
    {
        get => _displayedDisplayedProducts;
        set => Set(ref _displayedDisplayedProducts, value, nameof(DisplayedProducts));
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

    private int TotalPage
    {
        get => _totalPage;
        set
        {
            _totalPage = value;
            OnTotalPageChanged();
        }
    }

    private void OnTotalPageChanged()
    {
        var to = TotalPage * _maxCountDisplay;
        var from = to - _maxCountDisplay;
        
        if (to > _viewProducts.Count) to = _viewProducts.Count;
        if(from >= _viewProducts.Count || from < 0) return;

        DisplayTotalPageProducts(from, to);
        ChangePaginationButtons(TotalPage);
    }

    private void OnChangeSearchBox()
    {
        _viewProducts = _allProducts.Where(x => x.Name
                .Contains(_searchData, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }

    private void OnChangedSort()
    {
        try
        {
            var totalSortType = AttributesHelperExtension.GetEnumValueFromDescription<SortType>(SelectedSort);
            if (totalSortType == SortType.LowToHighPrice)
            {
                _viewProducts = _viewProducts.OrderBy(x => x.Price).ToList();
            }
            else if (totalSortType == SortType.HighToLowPrice)
            {
                _viewProducts = _viewProducts.OrderByDescending(x => x.Price).ToList();
            }
        }
        catch (Exception) { }
    }

    private void OnFiltrationSettingChanged()
    {
        
    }

    private void AddTestData(int count)// test method [DELETE THIS]
    {
        var names = new string[] {"Cola", "Monster", "Beer", "Potato", "Cabbage"};
        var random = new Random();
        
        for (var i = 0; i < count; i++)
        {
            _allProducts.Add(new Product()
            {
                Name = $"{names[random.Next(0, names.Length)]}{i}",
                Price = random.Next(100, 20000),
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
        // change this
        _viewProducts = _allProducts
            .Where(x => x.Materials.Count > 0)
            .ToList();
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
                Command = new BaseCommand(ChangeTotalPageByButtonContent),
                CommandParameter = item,
                Width = 20
            });
        }
    }

    private void ChangePaginationButtons(int clickedNumber)
    {
        if(clickedNumber * _maxCountDisplay > _allProducts.Count) return; 

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

    private void DisplayTotalPageProducts(int from, int to)
    {
        _displayedDisplayedProducts.Clear();
        
        if(_viewProducts.Count == 0)
            return;
        
        for (var i = from; i < to; i++)
        {
            _displayedDisplayedProducts.Add(_viewProducts[i]);
        }
    }

    private void ChangeTotalPageByStep(string content)
    {
        if (content == "+") TotalPage++;
        else if (content == "-" && TotalPage > 1) TotalPage--;
    }
}