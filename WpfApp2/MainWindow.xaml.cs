using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        private const int NumberOfIntegers = 100;
        private List<int> primes = new List<int>();
        private List<int> fibonaccis = new List<int>();

        public MainWindow()
        {
            InitializeComponent();
            GenerateIntegers();
        }

        private void GenerateIntegers()
        {
            Random random = new Random();

            for (int i = 0; i < NumberOfIntegers; i++)
            {
                int num = random.Next(2, 1000); 
                if (IsPrime(num))
                    primes.Add(num);
                if (IsFibonacci(num))
                    fibonaccis.Add(num);
            }

            DisplayStatistics();
        }

        private bool IsPrime(int number)
        {
            if (number <= 1)
                return false;

            for (int i = 2; i <= Math.Sqrt(number); i++)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }

        private bool IsFibonacci(int number)
        {
            double phi = (1 + Math.Sqrt(5)) / 2;
            double sqrt5 = Math.Sqrt(5);
            return Math.Abs(Math.Round(Math.Log(number * sqrt5) / Math.Log(phi)) - Math.Log(number * sqrt5) / Math.Log(phi)) < 1E-10;
        }

        private async Task WriteToFileAsync(IEnumerable<int> data, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var item in data)
                {
                    await writer.WriteLineAsync(item.ToString());
                }
            }
        }

        private void DisplayStatistics()
        {
            lblPrimesCount.Content = $"Primes Count: {primes.Count}";
            lblFibonacciCount.Content = $"Fibonacci Count: {fibonaccis.Count}";
        }

        private async void btnSavePrimes_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "primes.txt";
            await WriteToFileAsync(primes, filePath);
            MessageBox.Show($"Primes saved to {filePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private async void btnSaveFibonacci_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "fibonacci.txt";
            await WriteToFileAsync(fibonaccis, filePath);
            MessageBox.Show($"Fibonacci numbers saved to {filePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}