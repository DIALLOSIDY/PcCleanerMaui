🧹 CleanerMaui
Bienvenue sur CleanerMaui ! Il s'agit d'une application de nettoyage et d'optimisation de la RAM pour Windows, développée avec .NET MAUI. Elle permet aux utilisateurs de nettoyer les fichiers temporaires, les journaux Windows, les fichiers de mise à jour Windows, les rapports d'erreurs et le contenu de la corbeille, ainsi que d'optimiser l'utilisation de la RAM.


Fonctionnalités
    🧽 Nettoyer les fichiers temporaires : Supprimez les fichiers temporaires inutiles de votre système.
    📝 Effacer les journaux Windows : Supprimez les anciens journaux d'événements Windows pour libérer de l'espace.
    🔄 Supprimer les fichiers de mise à jour Windows : Nettoyez les fichiers résiduels des mises à jour Windows.
    ⚠️ Supprimer les rapports d'erreurs : Éliminez les anciens rapports d'erreurs qui encombrent votre système.
    🗑️ Vider la corbeille : Supprimez définitivement les fichiers de la corbeille.
    🚀 Optimiser l'utilisation de la RAM : Améliorez les performances du système en optimisant l'utilisation de la RAM.
Prise en main
Prérequis
    .NET 6.0 SDK ou supérieur
    Visual Studio 2022 ou supérieur avec la charge de travail MAUI installée
    Système d'exploitation Windows
    Installation
    Clonez le dépôt : git clone https://github.com/votrenomutilisateur/CleanerMaui.git
    
Ouvrez la solution : Ouvrez CleanerMaui.sln avec Visual Studio.

Compilez et exécutez l'application :Compilez la solution et déployez l'application sur votre cible préférée.


Utilisation
Page principale
La page principale de l'application offre des options pour nettoyer différents types de fichiers. Vous pouvez sélectionner les catégories que vous souhaitez nettoyer en cochant les cases correspondantes.

 ![Captures d'écran](CleanerMaui/Resources/Images/nettoyer.png)


Page RAM
La page d'optimisation de la RAM affiche les informations système et les statistiques d'utilisation de la RAM. Cliquez sur le bouton "Optimiser la RAM" pour libérer la RAM inutilisée et améliorer les performances du système.

 ![Captures d'écran](CleanerMaui/Resources/Images/nettoyer.png)

Aperçu du code:
        MainPage.xaml.cs
        ShowSystemInfo : Affiche les informations système.
        InitCheckbStates : Initialise les états des cases à cocher à partir des préférences utilisateur.
        ButtonNettoyer_Clicked : Gère l'événement de clic sur le bouton nettoyer et exécute les opérations de nettoyage en fonction des options sélectionnées.
        ClearWindowsTempFolder, ClearWinLogs, ClearWinUpdate, ClearWinError, EmptyRecycleBin : Méthodes pour nettoyer différentes catégories de fichiers.
        processDirectory, processFile : Méthodes auxiliaires pour supprimer des fichiers et des dossiers.
        GetFilesCountInFolder, DireSize : Méthodes auxiliaires pour obtenir le nombre de fichiers et la taille des répertoires.
        RamPage.xaml.cs
        ShowSystemInfo : Affiche les informations système.
        GetRamUsage : Récupère et affiche les statistiques d'utilisation de la RAM.
        OptimizeRam : Optimise la RAM en forçant la collecte des ordures (Garbage Collection).
Contributions:
    Les contributions sont les bienvenues ! Si vous avez des suggestions, des corrections de bogues ou des améliorations, n'hésitez pas à soumettre une demande de tirage (pull request).

Licence
Ce projet est sous licence MIT.

Contact
Pour toute question ou suggestion, n'hésitez pas à nous contacter à diallosidymohamed99@gmail.com
