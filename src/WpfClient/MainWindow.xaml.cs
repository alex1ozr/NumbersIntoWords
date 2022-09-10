using AErmilov.NumbersIntoWords.Api.Client.Implementations;
using System.Globalization;
using System.Windows;
using System.Windows.Input;

namespace WpfClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public sealed partial class MainWindow : Window
{
    private readonly INumbersIntoWordsClient client;

    public MainWindow(INumbersIntoWordsClient client)
    {
        InitializeComponent();
        this.client = client;
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        NumberTextBox.Focusable = true;
        NumberTextBox.Focus();
    }

    // still no async Task in WPF??? o_0 OMG
    private async void ConvertNumber_Click(object sender, RoutedEventArgs e)
    {
        await ConvertNumberIntoWords();
    }

    private async void NumberTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Return)
        {
            await ConvertNumberIntoWords();
        }
    }

    private void FocusAndSelectInput()
    {
        NumberTextBox.Focus();
        NumberTextBox.SelectAll();
    }

    private async Task ConvertNumberIntoWords()
    {
        WordsTextBox.Text = string.Empty;

        if (!TryParseWithCommaSeparator(NumberTextBox.Text, out var number))
        {
            MessageBox.Show("Please enter a decimal value", "Wrong value", MessageBoxButton.OK, MessageBoxImage.Error);
            FocusAndSelectInput();
            return;
        }

        try
        {
            var result = await client.ConvertNumberIntoWordsAsync(number, CancellationToken.None);
            WordsTextBox.Text = result.Words;
        }
        catch (ApiException<ValidationProblemDetails> ex)
        {
            MessageBox.Show(ex.Result.Detail, "Conversion error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Internal Server Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        FocusAndSelectInput();
    }

    private static bool TryParseWithCommaSeparator(string text, out decimal number)
            => decimal.TryParse(
                text,
                NumberStyles.Number,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = ",",
                    NumberGroupSeparator = ""
                }, out number);
}
