using CleanerMaui.Helper;

namespace CleanerMaui;

public partial class OptionsPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Crée une instance de la classe Sysinfo pour obtenir des informations système.

    public OptionsPage()
	{
		InitializeComponent();
        ShowSystemInfo();
	}

    /**
         * @brief Méthode pour afficher les informations système.
         */
    public void ShowSystemInfo()
    {
        osVersion.Text = sysinfo.GetWinVer();
        hardWare.Text = sysinfo.GetHardWareInfos();
    }

    private async void ImageButtonNettoyage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void ImageButtonTools_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToolsPage());
    }

    private async void ImageButtonRam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RamPage());
    }
}