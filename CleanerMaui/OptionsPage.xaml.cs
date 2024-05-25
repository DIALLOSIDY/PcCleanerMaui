using CleanerMaui.Helper;
using System.Windows.Input;
namespace CleanerMaui;

public partial class OptionsPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Cr�e une instance de la classe Sysinfo pour obtenir des informations syst�me.

    public ICommand TapCommand =>new Command <string> (async(url)=>await Launcher.OpenAsync(url));

    public OptionsPage()
	{
		InitializeComponent();
        ShowSystemInfo();
        BindingContext =this;
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