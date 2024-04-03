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

        private void LoadFileButton_Click(object sender, RoutedEventArgs e)
        {
            filePath = FilePathTextBox.Text;

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                MessageBox.Show("Please enter a valid file path.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);

            int sentenceCount = CountSentences(fileContent);
            int capitalLetterCount = CountCapitalLetters(fileContent);
            int smallLetterCount = CountSmallLetters(fileContent);
            int vowelCount = CountVowels(fileContent);
            int consonantCount = CountConsonants(fileContent);
            bool containsDigits = ContainsDigits(fileContent);

            MessageBox.Show($"Number of sentences: {sentenceCount}\n" +
                            $"Number of capital letters: {capitalLetterCount}\n" +
                            $"Number of small letters: {smallLetterCount}\n" +
                            $"Number of vowels: {vowelCount}\n" +
                            $"Number of consonant letters: {consonantCount}\n" +
                            $"Contains digits: {(containsDigits ? "Yes" : "No")}");
        }

        private int CountSentences(string text)
        {
            string[] sentences = text.Split(new[] { '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);
            return sentences.Length;
        }

        private int CountCapitalLetters(string text)
        {
            return text.Count(char.IsUpper);
        }

        private int CountSmallLetters(string text)
        {
            return text.Count(char.IsLower);
        }

        private int CountVowels(string text)
        {
            return text.Count(c => "aeiouAEIOU".Contains(c));
        }

        private int CountConsonants(string text)
        {
            return text.Count(c => char.IsLetter(c) && !"aeiouAEIOU".Contains(c));
        }

        private bool ContainsDigits(string text)
        {
            return text.Any(char.IsDigit);
        }
    }
}