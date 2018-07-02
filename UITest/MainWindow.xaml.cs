using System.Windows;
using System.Windows.Controls;

namespace UITest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        FileConvert convert = new FileConvert();
        string fileName;

        public MainWindow()
        {
            InitializeComponent();
            ConvertBtn.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string convertedFilePath = convert.ConvertFile(LanguageTypeTxtBox.Text);
            SaveTxtBlock.Text = convert.getConvertedFileText(convertedFilePath);
            ttmlFileTitle.Text = convertedFilePath.ToString();
            MessageBox.Show("File " + convertedFilePath + " Created!");
        }

        private void FindFile_Click(object sender, RoutedEventArgs e)
        {
            fileName = convert.selectFile();
            SrtTxtBlock.Text = convert.getFileText();
            if (SrtTxtBlock != null)
            {
                SrtFileTitle.Text = fileName;
                ConvertBtn.Visibility = System.Windows.Visibility.Visible;
            }
        }
    }
}
