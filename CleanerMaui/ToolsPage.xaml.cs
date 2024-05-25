using CleanerMaui.Helper;

namespace CleanerMaui;

public partial class ToolsPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Crée une instance de la classe Sysinfo pour obtenir des informations système.

    public ToolsPage()
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

    private async void ImageButtonOptions_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OptionsPage());
    }

    private async void ImageButtonRam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RamPage());
    }

    
}