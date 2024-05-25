using CleanerMaui.Helper;

namespace CleanerMaui;

public partial class RamPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Cr�e une instance de la classe Sysinfo pour obtenir des informations syst�me.

    public RamPage()
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

    private async void ImageButtonOptions_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OptionsPage());
    }

    private async void ImageButtonTools_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ToolsPage());
    }

    private async void ImageButtonNettoyage_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}