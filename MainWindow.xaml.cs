using Fluent;
using iNKORE.UI.WPF.Converters;
using iNKORE.UI.WPF.Helpers;
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

namespace SilicaWriter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (FontFamily fontFamily in Fonts.SystemFontFamilies)
            {
                FontFamilyBox.Items.Add(fontFamily.Source);
            }
            FontFamilyBox.SelectedItem = "Segoe UI";
        }

        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            editor.Cut();
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            editor.Copy();
        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            editor.Paste();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontSizeBox.Text == null | FontSizeBox.Text == "") return;
            string selectedFontSize = FontSizeBox.Text;
            if (editor != null && (selectedFontSize != null | selectedFontSize != "")) {
                editor.FontSize = double.Parse(selectedFontSize);
            }
        }

        private void FontFamilyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FontFamilyBox.SelectedValue == null) return;
            FontFamily selectedFontFamily = new FontFamily((string)FontFamilyBox.SelectedValue);
            if (editor != null)
            {
                editor.FontFamily = selectedFontFamily;
            }
        }

        private void Underline(object sender, RoutedEventArgs e)
        {
            editor.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
        }

        void UpdateItemCheckedState(ToggleButton button, DependencyProperty formattingProperty, object expectedValue)
        {
            object currentValue = editor.Selection.GetPropertyValue(formattingProperty);
            button.IsChecked = (currentValue == DependencyProperty.UnsetValue) ? false : currentValue != null && currentValue.Equals(expectedValue);
        }

        private void editor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            UpdateItemCheckedState(BoldButton, TextElement.FontWeightProperty, FontWeights.Bold);
            UpdateItemCheckedState(ItalicButton, TextElement.FontStyleProperty, FontStyles.Italic);
            UpdateItemCheckedState(UnderlineButton, Inline.TextDecorationsProperty, TextDecorations.Underline);
        }
    }
}