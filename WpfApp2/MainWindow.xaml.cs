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
        private const int TotalNumbers = 10000;
        private const string EvenFilePath = "even_numbers.txt";
        private const string OddFilePath = "odd_numbers.txt";

        private int evenCount;
        private int oddCount;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateIntegers_Click(object sender, RoutedEventArgs e)
        {
            Random random = new Random();
            evenCount = 0;
            oddCount = 0;

            using (StreamWriter evenWriter = new StreamWriter(EvenFilePath))
            using (StreamWriter oddWriter = new StreamWriter(OddFilePath))
            {
                for (int i = 0; i < TotalNumbers; i++)
                {
                    int number = random.Next();
                    if (number % 2 == 0)
                    {
                        evenCount++;
                        evenWriter.WriteLine(number);
                    }
                    else
                    {
                        oddCount++;
                        oddWriter.WriteLine(number);
                    }
                }
            }

            MessageBox.Show("Integers generated and saved to files successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DisplayStatistics_Click(object sender, RoutedEventArgs e)
        {
            EvenCountLabel.Content = "Even Count: " + evenCount;
            OddCountLabel.Content = "Odd Count: " + oddCount;
            StatisticsTextBox.Text = $"Total Numbers: {TotalNumbers}\nEven Count: {evenCount}\nOdd Count: {oddCount}";
        }
    }
}