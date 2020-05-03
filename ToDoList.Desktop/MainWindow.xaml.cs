using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToDoList.Core;

namespace ToDoList.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;

        public MainWindow()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
        }

        private async void ButtonBase_OnClick_GetWeatherForecast(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(@"http://localhost:5000/weather_forecast");

            var response = await _httpClient.GetAsync(uri);

            var result = await response.Content.ReadAsStringAsync();

            var weatherForecast = JsonSerializer.Deserialize<WeatherForecast[]>(result);
        }
    }
}
