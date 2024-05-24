using CleanerMaui.Helper; 
using System.Runtime.InteropServices; 

namespace CleanerMaui 
{
    public partial class MainPage : ContentPage 
    {
        Sysinfo sysinfo = new Sysinfo(); // Crée une instance de la classe Sysinfo pour obtenir des informations système.

        // Déclaration des variables booléennes pour les états des cases à cocher.
        bool _checkboxFichiersTemp = true;
        bool _checkboxLogsWindows = true;
        bool _checkboxFichiersWinUpdate = true;
        bool _checkboxRapportErreur = true;
        bool _checkboxCorbeille = true;

        public MainPage() // Constructeur de la classe MainPage.
        {
            InitializeComponent(); // Initialise les composants de l'interface utilisateur.

            ShowSystemInfo(); // Appelle la méthode ShowSystemInfo pour afficher les informations système.
            InitCheckbStates(); // Appelle la méthode InitCheckbStates pour initialiser les états des cases à cocher.
        }

        public void InitCheckbStates() // Méthode pour initialiser les états des cases à cocher à partir des préférences enregistrées.
        {
            _checkboxFichiersTemp = Preferences.Get("_checkboxFichiersTemp", true); // Récupère l'état de la case à cocher "_checkboxFichiersTemp" des préférences.
            checkboxFichiersTemp.IsChecked = _checkboxFichiersTemp; // Définit l'état de la case à cocher dans l'interface utilisateur.

            _checkboxLogsWindows = Preferences.Get("_checkboxLogsWindows", true); // Idem pour "_checkboxLogsWindows".
            checkboxLogsWindows.IsChecked = _checkboxLogsWindows;

            _checkboxFichiersWinUpdate = Preferences.Get("_checkboxFichiersWinUpdate", true); // Idem pour "_checkboxFichiersWinUpdate".
            checkboxFichiersWinUpdate.IsChecked = _checkboxFichiersWinUpdate;

            _checkboxRapportErreur = Preferences.Get("_checkboxRapportErreur", true); // Idem pour "_checkboxRapportErreur".
            checkboxRapportErreur.IsChecked = _checkboxRapportErreur;

            _checkboxCorbeille = Preferences.Get("_checkboxCorbeille", true); // Idem pour "_checkboxCorbeille".
            checkboxCorbeille.IsChecked = _checkboxCorbeille;
        }

        private async void InfoButton_Clicked(object sender, EventArgs e) // Méthode asynchrone pour gérer le clic sur le bouton Info.
        {
            try
            {
                Uri uri = new Uri("http://www.google.com"); // Crée une URI pour l'URL de Google.
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred); // Ouvre l'URL dans le navigateur par défaut.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); // Affiche le message d'erreur en cas d'exception.
            }
        }

        public void ShowSystemInfo() // Méthode pour afficher les informations système.
        {
            osVersion.Text = sysinfo.GetWinVer(); // Affiche la version de Windows.
            hardWare.Text = sysinfo.GetHardWareInfos(); // Affiche les informations matérielles.
        }

        private void checkboxFichiersTemp_CheckedChanged(object sender, CheckedChangedEventArgs e) // Gère les changements d'état de la case à cocher Fichiers Temp.
        {
            _checkboxFichiersTemp = e.Value; // Met à jour l'état de la variable.
            Preferences.Set("_checkboxFichiersTemp", _checkboxFichiersTemp); // Enregistre l'état dans les préférences.
        }

        private void checkboxLogsWindows_CheckedChanged(object sender, CheckedChangedEventArgs e) // Gère les changements d'état de la case à cocher Logs Windows.
        {
            _checkboxLogsWindows = e.Value; // Idem que ci-dessus.
            Preferences.Set("_checkboxLogsWindows", _checkboxLogsWindows);
        }

        private void checkboxFichiersWinUpdate_CheckedChanged(object sender, CheckedChangedEventArgs e) // Gère les changements d'état de la case à cocher Fichiers WinUpdate.
        {
            _checkboxFichiersWinUpdate = e.Value; // Idem que ci-dessus.
            Preferences.Set("_checkboxFichiersWinUpdate", _checkboxFichiersWinUpdate);
        }

        private void checkboxRapportErreur_CheckedChanged(object sender, CheckedChangedEventArgs e) // Gère les changements d'état de la case à cocher Rapport Erreur.
        {
            _checkboxRapportErreur = e.Value; // Idem que ci-dessus.
            Preferences.Set("_checkboxRapportErreur", _checkboxRapportErreur);
        }

        private void checkboxCorbeille_CheckedChanged(object sender, CheckedChangedEventArgs e) // Gère les changements d'état de la case à cocher Corbeille.
        {
            _checkboxCorbeille = e.Value; // Idem que ci-dessus.
            Preferences.Set("_checkboxCorbeille", _checkboxCorbeille);
        }

        private void ButtonNettoyer_Clicked(object sender, EventArgs e) // Gère le clic sur le bouton Nettoyer.
        {
            if (_checkboxFichiersTemp) 
            {
                ClearWindowsTempFolder();
            }
            if (_checkboxLogsWindows) 
            {
                
            }
            if (_checkboxFichiersWinUpdate) // Si la case à cocher Fichiers WinUpdate est cochée...
            {
                // Implémenter la logique pour nettoyer les fichiers de mise à jour Windows ici.
            }
            if (_checkboxRapportErreur) // Si la case à cocher Rapport Erreur est cochée...
            {
                // Implémenter la logique pour nettoyer les rapports d'erreurs ici.
            }
            if (_checkboxCorbeille) // Si la case à cocher Corbeille est cochée...
            {
                EmptyRecycleBin(); // Appelle la méthode pour vider la corbeille.
            }

            progression.Progress = 1; // Met à jour la barre de progression à 100%.

            tableRecap.IsVisible = true; // Rendre visible le tableau récapitulatif.
        }

        public void ClearWindowsTempFolder()
        {
            string path = @"C:\Windows\Temp";
            if (Directory.Exists(path)) {
                detailFichiersTemp.Detail =GetFilesCountInFolder(path) + "Fichiers supprimés .";

                processDirectory(path);
            }
        }

        public void processDirectory(string targetDirectory)
        {
            try
            {
                string[] files = Directory.GetFiles(targetDirectory);
                foreach (string file in files)
                {
                    processFile(file);
                }
                string[] subdirectoryEntry = Directory.GetDirectories(targetDirectory);
                foreach (string sub in subdirectoryEntry)
                    processDirectory(sub);
            }
            catch (Exception ex)
            {

            }
            
        }

        public void processFile(string path)
        {
            try
            {
                if (path.Contains("\\Temp"))
                {
                    File.Delete(path);
                }
                
            }
            catch
            {
            }

        }

        public int GetFilesCountInFolder(string path)
        {
            try
            {
                int count = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Count();
                return count;
            }

            catch
            {
                return -1;
            }
        }

        public void EmptyRecycleBin() // Méthode pour vider la corbeille.
        {
            const int NOCONFIRMATION = 0x00000001; // Drapeau pour annuler la confirmation.

            try
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, NOCONFIRMATION); // Appelle la fonction SHEmptyRecycleBin pour vider la corbeille.
                detailCorbeille.Detail = "Les fichiers de la corbeille ont été supprimés"; // Met à jour l'interface utilisateur pour indiquer que la corbeille a été vidée.
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici si nécessaire.
            }
        }

        [DllImport("shell32.dll")] // Déclare une fonction externe à partir de la bibliothèque shell32.dll.
        static extern int SHEmptyRecycleBin(IntPtr hWnd, string pszRootPath, uint dwFlags); // Signature de la fonction SHEmptyRecycleBin.
    }
}
