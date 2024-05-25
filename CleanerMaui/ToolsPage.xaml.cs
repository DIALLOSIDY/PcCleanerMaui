using CleanerMaui.Helper;
using Microsoft.VisualBasic;
using System.Management;
namespace CleanerMaui;

public partial class ToolsPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Cr�e une instance de la classe Sysinfo pour obtenir des informations syst�me.

    public ToolsPage()
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

    private async void ImageButtonOptions_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new OptionsPage());
    }

    private async void ImageButtonRam_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RamPage());
    }
    /*
    private void restaurerTxt_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Acc�s au service WMI SystemRestore
            ManagementClass systemRestore = new ManagementClass("root\\default", "SystemRestore", null);

            // Invoker la m�thode CreateRestorePoint
            ManagementBaseObject inParams = systemRestore.GetMethodParameters("CreateRestorePoint");
            inParams["Description"] = "PC Cleaner restore point";
            inParams["RestorePointType"] = 0; // Application install
            inParams["EventType"] = 100; // BEGIN_SYSTEM_CHANGE

            ManagementBaseObject outParams = systemRestore.InvokeMethod("CreateRestorePoint", inParams, null);

            // V�rifier le retour de la m�thode
            uint returnValue = (uint)(outParams.Properties["ReturnValue"].Value);

            if (returnValue == 0)
            {
                restaurerTxt.Text = "Point de restauration cr��";
            }
            else
            {
                restaurerTxt.Text = "�chec de cr�ation du point de restauration";
            }
        }
        catch (ManagementException ex)
        {
            
        }
       
    }
    */
}