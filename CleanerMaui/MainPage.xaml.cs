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

        long totalCleanSize = 0;

        /**
         * @brief Constructeur de la classe MainPage.
         */
        public MainPage()
        {
            InitializeComponent(); // Initialise les composants de l'interface utilisateur.

            ShowSystemInfo(); // Appelle la méthode ShowSystemInfo pour afficher les informations système.
            InitCheckbStates(); // Appelle la méthode InitCheckbStates pour initialiser les états des cases à cocher.
        }

        /**
         * @brief Méthode pour initialiser les états des cases à cocher à partir des préférences enregistrées.
         */
        public void InitCheckbStates()
        {
            _checkboxFichiersTemp = Preferences.Get("_checkboxFichiersTemp", true);
            checkboxFichiersTemp.IsChecked = _checkboxFichiersTemp;

            _checkboxLogsWindows = Preferences.Get("_checkboxLogsWindows", true);
            checkboxLogsWindows.IsChecked = _checkboxLogsWindows;

            _checkboxFichiersWinUpdate = Preferences.Get("_checkboxFichiersWinUpdate", true);
            checkboxFichiersWinUpdate.IsChecked = _checkboxFichiersWinUpdate;

            _checkboxRapportErreur = Preferences.Get("_checkboxRapportErreur", true);
            checkboxRapportErreur.IsChecked = _checkboxRapportErreur;

            _checkboxCorbeille = Preferences.Get("_checkboxCorbeille", true);
            checkboxCorbeille.IsChecked = _checkboxCorbeille;
        }

        /**
         * @brief Méthode asynchrone pour gérer le clic sur le bouton Info.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement.
         */
        private async void InfoButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                Uri uri = new Uri("http://www.google.com");
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /**
         * @brief Méthode pour afficher les informations système.
         */
        public void ShowSystemInfo()
        {
            osVersion.Text = sysinfo.GetWinVer();
            hardWare.Text = sysinfo.GetHardWareInfos();
        }

        /**
         * @brief Gère les changements d'état de la case à cocher Fichiers Temp.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement de changement d'état.
         */
        private void checkboxFichiersTemp_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _checkboxFichiersTemp = e.Value;
            Preferences.Set("_checkboxFichiersTemp", _checkboxFichiersTemp);
        }

        /**
         * @brief Gère les changements d'état de la case à cocher Logs Windows.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement de changement d'état.
         */
        private void checkboxLogsWindows_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _checkboxLogsWindows = e.Value;
            Preferences.Set("_checkboxLogsWindows", _checkboxLogsWindows);
        }

        /**
         * @brief Gère les changements d'état de la case à cocher Fichiers WinUpdate.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement de changement d'état.
         */
        private void checkboxFichiersWinUpdate_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _checkboxFichiersWinUpdate = e.Value;
            Preferences.Set("_checkboxFichiersWinUpdate", _checkboxFichiersWinUpdate);
        }

        /**
         * @brief Gère les changements d'état de la case à cocher Rapport Erreur.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement de changement d'état.
         */
        private void checkboxRapportErreur_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _checkboxRapportErreur = e.Value;
            Preferences.Set("_checkboxRapportErreur", _checkboxRapportErreur);
        }

        /**
         * @brief Gère les changements d'état de la case à cocher Corbeille.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement de changement d'état.
         */
        private void checkboxCorbeille_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            _checkboxCorbeille = e.Value;
            Preferences.Set("_checkboxCorbeille", _checkboxCorbeille);
        }

        /**
         * @brief Gère le clic sur le bouton Nettoyer.
         * @param sender L'objet qui déclenche l'événement.
         * @param e Les arguments de l'événement de clic.
         */
        private void ButtonNettoyer_Clicked(object sender, EventArgs e)
        {
            ResetValues();
            // si une case est true on execute la fonction correspondante

            if (_checkboxFichiersTemp)
            {
                ClearWindowsTempFolder();
            }

            if (_checkboxLogsWindows)
            {
                ClearWinLogs();
            }

            if (_checkboxFichiersWinUpdate)
            {
                ClearWinUpdate();
            }

            if (_checkboxRapportErreur)
            {
                ClearWinError();
            }

            if (_checkboxCorbeille)
            {
                EmptyRecycleBin();
            }

            progression.Progress = 1; // Met à jour la barre de progression à 100%.

            tableRecap.IsVisible = true; // Rendre visible le tableau récapitulatif.

            //espace total gagné par l'utilisateur suite au nettoyage 

            long totalSizeMb = totalCleanSize / 1000000;
            totalSize.Text = "~ " + totalSizeMb + "MB supprimés !";

        }

        /**
         * @brief Méthode pour supprimer les fichiers du dossier Temp de Windows.
         */
        public void ClearWindowsTempFolder()
        {
            string path = @"C:\Windows\Temp";
            if (Directory.Exists(path))
            {
                detailFichiersTemp.Detail = GetFilesCountInFolder(path) + " Fichiers supprimés.";

                var size = DireSize(new DirectoryInfo(path));
                totalCleanSize = totalCleanSize + size;

                processDirectory(path);
            }
        }

        /**
         * @brief Méthode pour supprimer les fichiers de mise à jour de Windows.
         */
        public void ClearWinUpdate()
        {
            string path = @"C:\Windows\SoftwareDistribution\Download";
            if (Directory.Exists(path))
            {
                detailFichiersWindowsUp.Detail = GetFilesCountInFolder(path) + " Fichiers supprimés.";

                var size = DireSize(new DirectoryInfo(path));
                totalCleanSize = totalCleanSize + size;

                processDirectory(path);
            }
        }

        /**
         * @brief Méthode pour supprimer les rapports d'erreur de Windows.
         */
        public void ClearWinError()
        {
            string path = @"C:\ProgramData\Microsoft\Windows\Wer";
            if (Directory.Exists(path))
            {
                detailErrors.Detail = GetFilesCountInFolder(path) + " Fichiers supprimés.";

                var size = DireSize(new DirectoryInfo(path));
                totalCleanSize = totalCleanSize + size;

                processDirectory(path);
            }
        }

        /**
         * @brief Méthode pour supprimer les journaux Windows.
         */
        public void ClearWinLogs()
        {
            string path = @"C:\Windows\System32\winevt\Logs";
            if (Directory.Exists(path))
            {
                detailLogs.Detail = GetFilesCountInFolder(path) + " Fichiers supprimés.";

                var size = DireSize(new DirectoryInfo(path));
                totalCleanSize = totalCleanSize + size;

                processDirectory(path);
            }
        }

        /**
         * @brief Méthode pour vider la corbeille.
         */
        public void EmptyRecycleBin()
        {
            const int NOCONFIRMATION = 0x00000001;

            try
            {
                SHEmptyRecycleBin(IntPtr.Zero, null, NOCONFIRMATION);
                detailCorbeille.Detail = "Les fichiers de la corbeille ont été supprimés";
            }
            catch (Exception ex)
            {
                // Gérer l'exception ici si nécessaire.
            }
        }

        [DllImport("shell32.dll")] //importun dll extern
        static extern int SHEmptyRecycleBin(IntPtr hWnd, string pszRootPath, uint dwFlags); //appel dune fonction externe pour vider la corbeille

        /**
         * @brief Parcourt un répertoire et supprime les fichiers.
         * @param targetDirectory Le chemin du répertoire cible.
         */
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
                // Gérer l'exception ici si nécessaire.
            }
        }

        /**
         * @brief Supprime un fichier donné.
         * @param path Le chemin du fichier à supprimer.
         */
        public void processFile(string path)
        {
            try
            {
                if (path.Contains("\\Temp"))
                {
                    File.Delete(path);
                }
                if (path.Contains("\\Temp"))
                {
                    File.Delete(path);
                }
                if (path.Contains("\\SoftwareDistribution"))
                {
                    File.Delete(path);
                }
                if (path.Contains("\\winevt\\Logs"))
                {
                    File.Delete(path);
                }
                if (path.Contains("\\Windows\\WER"))
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {
                //on retire le poids du fichier non supprime
                FileInfo f = new FileInfo(path);
                // totalCleanSize -= f.Length;
            }
        }

        /**
         * @brief Compte le nombre de fichiers dans un dossier.
         * @param path Le chemin du dossier.
         * @return Le nombre de fichiers dans le dossier.
         */
        public int GetFilesCountInFolder(string path)
        {
            try
            {
                int count = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Count();
                return count;
            }
            catch
            {
                return 0;
            }
        }

        /**
         * @brief Calcule la taille d'un répertoire.
         * @param d L'objet DirectoryInfo représentant le répertoire.
         * @return La taille du répertoire en octets.
         */
        public static long DireSize(DirectoryInfo d)
        {
            try
            {
                long size = 0;
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }

                // on fait une recursivite pour trouver la taille des sous dossiers
                DirectoryInfo[] dos = d.GetDirectories();
                foreach (DirectoryInfo di in dos)
                {
                    size += DireSize(di);
                }
                return size;
            }
            catch
            {
                return 0;
            }
        }

        /**
         * @brief Réinitialise les valeurs du tableau récapitulatif.
         */
        public void ResetValues()
        {
            totalCleanSize = 0;
            tableRecap.IsVisible = false;
            progression.Progress = 0;
            totalSize.Text = "";

            detailCorbeille.Detail = "ignoré";
            detailErrors.Detail = "ignoré";
            detailFichiersTemp.Detail = "ignoré";
            detailFichiersWindowsUp.Detail = " ignoré";
            detailLogs.Detail = "ignoré ";
        }

        private  async void ImageButtonOptions_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new OptionsPage());
        }

        private async void ImageButtonRam_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RamPage());

        }

        private async void ImageButtonOutils_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ToolsPage());

        }
    }
}
