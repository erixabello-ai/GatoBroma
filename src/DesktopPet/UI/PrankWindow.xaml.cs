using System.Windows;
using System.Windows.Controls;

namespace DesktopPet.UI;

public enum PrankTrigger
{
    FirstClick = -1
}

public partial class PrankWindow : Window
{
    public bool IsProgrammed { get; private set; }
    public event Action<PrankTrigger, string>? Programmed;

    public PrankWindow()
    {
        InitializeComponent();
        TimeBox.SelectedIndex = 4;
    }

    private void Programar_Click(object sender, RoutedEventArgs e)
    {
        if (TimeBox.SelectedItem is not ComboBoxItem item) return;
        string tag = item.Tag?.ToString() ?? "";
        PrankTrigger trigger = tag == "first"
            ? PrankTrigger.FirstClick
            : (PrankTrigger)int.Parse(tag);

        IsProgrammed = true;
        string message = string.IsNullOrWhiteSpace(MessageBox.Text)
            ? "Tu nivel de estupidez es muy alto."
            : MessageBox.Text.Trim();
        Hide();
        Programmed?.Invoke(trigger, message);
    }

    private void Cancelar_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
