using CleanerMaui.Helper;

namespace CleanerMaui;

public partial class OptionsPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Cr�e une instance de la classe Sysinfo pour obtenir des informations syst�me.

    public OptionsPage()
	{
		InitializeComponent();
        ShowSystemInfo();
	}

    /**
         * @brief M�thode pour afficher les informations syst�me.
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