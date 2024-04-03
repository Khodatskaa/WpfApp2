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

        private void btnModerate_Click(object sender, RoutedEventArgs e)
        {
            string textFilePath = txtTextFilePath.Text.Trim();
            string moderationFilePath = txtModerationFilePath.Text.Trim();

            if (string.IsNullOrEmpty(textFilePath) || string.IsNullOrEmpty(moderationFilePath))
            {
                MessageBox.Show("Please enter both file paths.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                string[] moderationWords = File.ReadAllLines(moderationFilePath);
                string text = File.ReadAllText(textFilePath);

                string moderatedText = ModerateText(text, moderationWords);

                txtResult.Text = moderatedText;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string ModerateText(string text, string[] moderationWords)
        {
            StringBuilder result = new StringBuilder(text);

            foreach (string word in moderationWords)
            {
                string moderatedWord = new string('*', word.Length);
                result = result.Replace(word, moderatedWord);
            }

            return result.ToString();
        }
    }
}