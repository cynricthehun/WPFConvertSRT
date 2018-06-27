using System.Windows;

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
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            string convertedFilePath = convert.ConvertFile();
            SaveTxtBlock.Text = convert.getConvertedFileText(convertedFilePath);
            MessageBox.Show("File " + convertedFilePath + " Created!");
        }

        private void FindFile_Click(object sender, RoutedEventArgs e)
        {
            fileName = convert.selectFile();
            SrtTxtBlock.Text = convert.getFileText();
        }
    }
}
