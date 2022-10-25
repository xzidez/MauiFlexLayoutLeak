using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace MauiApp1;

public partial class MainPage : ContentPage
{
    int count = 0;
    private readonly ViewModel viewModel;
    public MainPage()
    {
        InitializeComponent();
        viewModel = new ViewModel();
        BindingContext = viewModel;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        viewModel.Update();

    }
}

public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Item> _items;

    public string InstanceCounterText => $"InstanceCount: {Item.InstanceCounter}";

    public ViewModel()
    {
        Items = new ObservableCollection<Item>();
    }

    public void Update()
    {
        Items.Add(new Item());
        if(Items.Count > 1)
        {
            Items.RemoveAt(0);
        }

        GC.Collect();
        OnPropertyChanged(nameof(Items));
        OnPropertyChanged(nameof(InstanceCounterText));
    }
}

public class Item 
{
    public static int InstanceCounter = 0;
    public string Text { get; }
    public Item()
    {
        Text = $"Item number: {InstanceCounter++}";
    }
    ~Item()
    {
        InstanceCounter--;
    }
}