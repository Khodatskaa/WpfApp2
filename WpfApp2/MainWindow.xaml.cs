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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnFlip_Click(object sender, RoutedEventArgs e)
        {
            string filePath = txtFilePath.Text.Trim();

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Please enter the file path.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string flippedContent = FlipFileContent(filePath);
                string newFilePath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(filePath), "flipped_" + System.IO.Path.GetFileName(filePath));

                File.WriteAllText(newFilePath, flippedContent);

                MessageBox.Show($"Flipped content saved to {newFilePath}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string FlipFileContent(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            Array.Reverse(lines);

            StringBuilder result = new StringBuilder();
            foreach (string line in lines)
            {
                result.AppendLine(line);
            }

            return result.ToString();
        }
    }
}