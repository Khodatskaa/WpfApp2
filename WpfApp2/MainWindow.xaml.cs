using Microsoft.Win32;
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
        private int[] array;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (array == null || array.Length == 0)
            {
                MessageBox.Show("Array is empty. Please enter some values first.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath))
                    {
                        foreach (int value in array)
                        {
                            writer.WriteLine(value);
                        }
                    }
                    MessageBox.Show("Array saved to file successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(ValueTextBox.Text, out int value))
            {
                ArrayTextBox.Text += $"{value}{Environment.NewLine}";
                if (array == null)
                {
                    array = new int[1];
                }
                else
                {
                    Array.Resize(ref array, array.Length + 1);
                }
                array[array.Length - 1] = value;
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ValueTextBox.Clear();
            ValueTextBox.Focus();
        }
    }
}