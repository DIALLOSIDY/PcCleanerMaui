using CleanerMaui.Helper;

namespace CleanerMaui
{
    public partial class MainPage : ContentPage
    {
        
        Sysinfo sysinfo =new Sysinfo();

        public MainPage()
        {
            InitializeComponent();

            ShowSystemInfo();


        }

        private async void InfoButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri("http://www.google.com");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }

        public void ShowSystemInfo()
        {
            // os
            osVersion.Text = sysinfo.GetWinVer();

            //CPU
            hardWare.Text =sysinfo.GetHardWareInfos();
        }
    }

}
