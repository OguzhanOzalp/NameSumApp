using NameSumCalculator_0;
using NameSumApp;
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

namespace NameSumApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string input = NameInput.Text;

            if (string.IsNullOrWhiteSpace(input))
            {
                ResultText.Text = "Please enter a valid name!";
                ResultText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                return;
            }

            int result = NameSumCalculator.CalculateNameSum(input);
            ResultText.Text = $"The sum of the letters in \"{input}\" is: {result}";
            ResultText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Black);
        }

        private void NameInput_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter) // Enter tuşu kontrolü
            {
                CalculateButton_Click(sender, e); // Calculate butonunun Click olayını çağır
            }
        }

        private void RunTestsButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder testResults = new StringBuilder();
            int totalTests = 0;
            int failedTests = 0;

            // Test cases
            totalTests++; if (!RunTest("A", 1, testResults)) failedTests++;
            totalTests++; if (!RunTest("Z", 26, testResults)) failedTests++;
            totalTests++; if (!RunTest("Az", 27, testResults)) failedTests++;
            totalTests++; if (!RunTest("Az", 35, testResults)) failedTests++;

            totalTests++; if (!RunTest("Test", 64, testResults)) failedTests++;
            totalTests++; if (!RunTest("Te st", 64, testResults)) failedTests++;
            totalTests++; if (!RunTest("Tést", 64, testResults)) failedTests++; // Accented letters ignored
            totalTests++; if (!RunTest("Tést", 59, testResults)) failedTests++;

            totalTests++; if (!RunTest("Ivan", 46, testResults)) failedTests++;
            totalTests++; if (!RunTest("İvan", 46, testResults)) failedTests++;
            totalTests++; if (!RunTest("Hello World!", 124, testResults)) failedTests++;
            totalTests++; if (!RunTest("hello_world!", 124, testResults)) failedTests++;

            totalTests++; if (!RunTest("", 0, testResults)) failedTests++; // Empty string
            totalTests++; if (!RunTest("123", 0, testResults)) failedTests++; // Numbers only
            totalTests++; if (!RunTest("äöüßĞŞİÇ", 0, testResults)) failedTests++; // Non-English letters

            // Summary
            if (failedTests == 0)
            {
                testResults.AppendLine($"All {totalTests} tests passed successfully!");
                TestResultsText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Green);
            }
            else
            {
                testResults.AppendLine($"{failedTests} out of {totalTests} tests failed.");
                TestResultsText.Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
            }

            TestResultsText.Text = testResults.ToString();
        }

        private bool RunTest(string input, int expected, StringBuilder testResults)
        {
            int result = NameSumCalculator.CalculateNameSum(input);

            if (result == expected)
            {
                testResults.AppendLine($"✔️ Test passed: \"{input}\" -> {result}");
                return true;
            }
            else
            {
                testResults.AppendLine($"❌ Test failed: \"{input}\" -> Expected {expected}, got {result}");
                return false;
            }
        }
    }
}