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
using Path = System.IO.Path;

namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAnalyze_Click(object sender, RoutedEventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter the file path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                int positiveCount = 0;
                int negativeCount = 0;
                int twoDigitCount = 0;
                int fiveDigitCount = 0;

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (int.TryParse(line, out int number))
                        {
                            if (number > 0)
                                positiveCount++;
                            else if (number < 0)
                                negativeCount++;

                            if (number >= 10 && number <= 99)
                                twoDigitCount++;
                            else if (number >= 10000 && number <= 99999)
                                fiveDigitCount++;
                        }
                    }
                }

                lblPositiveCount.Content = $"Positive Numbers: {positiveCount}";
                lblNegativeCount.Content = $"Negative Numbers: {negativeCount}";
                lblTwoDigitCount.Content = $"Two-Digit Numbers: {twoDigitCount}";
                lblFiveDigitCount.Content = $"Five-Digit Numbers: {fiveDigitCount}";

                CreateFiles(filePath, positiveCount, negativeCount, twoDigitCount, fiveDigitCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CreateFiles(string filePath, int positiveCount, int negativeCount, int twoDigitCount, int fiveDigitCount)
        {
            string directory = Path.GetDirectoryName(filePath);
            string positiveFilePath = Path.Combine(directory, "positive_numbers.txt");
            string negativeFilePath = Path.Combine(directory, "negative_numbers.txt");
            string twoDigitFilePath = Path.Combine(directory, "two_digit_numbers.txt");
            string fiveDigitFilePath = Path.Combine(directory, "five_digit_numbers.txt");

            using (StreamWriter writer = new StreamWriter(positiveFilePath))
            {
                writer.WriteLine($"Positive Numbers: {positiveCount}");
            }

            using (StreamWriter writer = new StreamWriter(negativeFilePath))
            {
                writer.WriteLine($"Negative Numbers: {negativeCount}");
            }

            using (StreamWriter writer = new StreamWriter(twoDigitFilePath))
            {
                writer.WriteLine($"Two-Digit Numbers: {twoDigitCount}");
            }

            using (StreamWriter writer = new StreamWriter(fiveDigitFilePath))
            {
                writer.WriteLine($"Five-Digit Numbers: {fiveDigitCount}");
            }
        }
    }
}