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

        private void btnReplace_Click(object sender, RoutedEventArgs e)
        {
            string wordToSearch = txtWordToSearch.Text.Trim();
            string wordToReplace = txtWordToReplace.Text.Trim();

            if (string.IsNullOrEmpty(wordToSearch) || string.IsNullOrEmpty(wordToReplace))
            {
                MessageBox.Show("Please enter both words.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string filePath = "text.txt"; 
            string content = File.ReadAllText(filePath);
            int occurrences = ReplaceOccurrences(ref content, wordToSearch, wordToReplace);
            File.WriteAllText(filePath, content);

            MessageBox.Show($"Replaced {occurrences} occurrences of '{wordToSearch}' with '{wordToReplace}'.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            DisplayStatistics();
        }

        private int ReplaceOccurrences(ref string content, string wordToSearch, string wordToReplace)
        {
            int occurrences = 0;
            int index = content.IndexOf(wordToSearch);
            while (index != -1)
            {
                content = content.Remove(index, wordToSearch.Length).Insert(index, wordToReplace);
                occurrences++;
                index = content.IndexOf(wordToSearch, index + wordToReplace.Length);
            }
            return occurrences;
        }

        private void DisplayStatistics()
        {
            string filePath = "text.txt"; 
            int totalWords = File.ReadAllText(filePath).Split(' ', '\n', '\r', '\t').Length;
            lblStatistics.Content = $"Total words in file: {totalWords}";
        }
    }
}