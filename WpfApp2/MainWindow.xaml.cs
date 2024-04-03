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
        private string filePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void CreateFileButton_Click(object sender, RoutedEventArgs e)
        {
            filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Please enter a valid file path.");
                return;
            }

            File.WriteAllText(filePath, FileContentTextBox.Text);
            MessageBox.Show("File created and content written successfully.");
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchWord = SearchWordTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Please create a file first or enter a valid file path.");
                return;
            }

            if (string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Please enter a word to search.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);

            bool wordFound = fileContent.Contains(searchWord);
            MessageBox.Show(wordFound ? "Word found." : "Word not found.");

            int wordCount = fileContent.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                       .Count(word => word.Equals(searchWord, StringComparison.OrdinalIgnoreCase));
            MessageBox.Show($"Word '{searchWord}' occurs {wordCount} times in the file.");

            string reverseWord = new string(searchWord.Reverse().ToArray());
            int reverseWordCount = fileContent.Split(new[] { ' ', '\n', '\r', '\t' }, StringSplitOptions.RemoveEmptyEntries)
                                             .Count(word => word.Equals(reverseWord, StringComparison.OrdinalIgnoreCase));
            MessageBox.Show($"Word '{searchWord}' (in reverse order) occurs {reverseWordCount} times in the file.");
        }
    }
}