using System.ComponentModel;
using System.IO;
using Newtonsoft.Json;

namespace Qoutes;

public partial class MainPage : ContentPage
{
    int count = 0;
    Root quotes = new Root();
    public MainPage()
    {
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        await LoadJson();
        setQuote();
        base.OnAppearing();
    }
    private void btnGenerate_Clicked(object sender, EventArgs e)
    {
        setQuote();
    }
    public async Task LoadJson()
    {

        using Stream fileStream = await FileSystem.Current.OpenAppPackageFileAsync("quotes.json");
        using StreamReader reader = new StreamReader(fileStream);
        string json = await reader.ReadToEndAsync();
        quotes = JsonConvert.DeserializeObject<Root>(json);

    }
    public void setQuote()
    {
        var rd = new Random();
        var currentQuote = quotes.quotes[rd.Next(0, 102)];
        lblQuote.Text = $"{currentQuote.quote} - {currentQuote.author}";
        setBackgroundColor();

    }
    public void setBackgroundColor()
    {
        color1.Color = randomColor();
        color2.Color = randomColor();
    }
    private Color randomColor()
    {
        Random random = new();
        return Color.FromRgb(random.Next(0, 255), random.Next(0, 255), random.Next(0, 255));
    }
}


public class Root
{
    public List<Quote> quotes { get; set; }
}

public class Quote
{
    public string quote { get; set; }
    public string author { get; set; }
}


