using CleanerMaui.Helper;
using System.Management;
namespace CleanerMaui;

public partial class RamPage : ContentPage
{
    Sysinfo sysinfo = new Sysinfo(); // Crée une instance de la classe Sysinfo pour obtenir des informations système.

    public RamPage()
	{
		InitializeComponent();
        ShowSystemInfo();
        GetRamUsage();

    }

    /**
         * @brief Méthode pour afficher les informations système.
         */
    public void ShowSystemInfo()
    {
        osVersion.Text = sysinfo.GetWinVer();
        hardWare.Text = sysinfo.GetHardWareInfos();
    }

    public void GetRamUsage()
    {
        try
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT TotalVisibleMemorySize ,FreePhysicalMemory FROM Win32_OperatingSystem");
            ulong totalRam = 0;
            ulong frram = 0;
            foreach (ManagementObject obj in searcher.Get())
            {
                totalRam = Convert.ToUInt64(obj["TotalVisibleMemorySize"]);
                frram = Convert.ToUInt64(obj["FreePhysicalMemory"]);
            }

            //calcul en pourcentage % RAM libre
            int frame2 = Convert.ToInt32(frram);
            int frame3 = Convert.ToInt32(totalRam);
            string frame4 = Convert.ToString(frame2);
            string frame5 = Convert.ToString(frame3);   
            double frame6 = Convert.ToDouble(frame4);
            double frame7 = Convert.ToDouble(frame5);
            double percent = frame6 / frame7 * 100;
            int per2 = (int)Math.Round(percent);

            // fin ddu calcul

            //modification des labels sur l'interface graphique 

            rameUserTxt.Text = 100 - per2 + "%";

            graph.Progress = 1 -(percent/100);

            cellTotal.Detail = totalRam + " Mb.";
            cellFree.Detail = frram + " Mb. (" + per2 + "%).";
            cellUsed.Detail = (totalRam - frram) + " Mb.(" + (100 - per2) + "%)";


            
        }
        catch (Exception ex) { }

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

    private void ButtonRam_Clicked(object sender, EventArgs e)
    {
        graph.IsIndeterminate = true; //pour creer une petite annimation de l'anneau quand on appuie sur le button Optimiser
        OptimizeRam();
    }

    public async void OptimizeRam()
    {

        try
        {
            GC.Collect(1,GCCollectionMode.Forced);
            GC.WaitForPendingFinalizers();

        }
        catch (Exception ex)
        {
            throw;
        }

        await Task.Delay(TimeSpan.FromSeconds(2)); // on met juste le programme en pause pour 2s pour voir l'effet de l'annimation de l'anneau 
        graph.IsIndeterminate = false; // on arrete l'annimation 
        ramCleaned.IsVisible = true;
        GetRamUsage(); // On reaffiche les informations de la RAM
    }
}