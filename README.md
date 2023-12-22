PROJET 03 OC : Tester l'implémentation d'une fonctionnalité .NET ==> https://openclassrooms.com/fr/paths/882/projects/1443/assignment
Fork du dépôt OC : https://github.com/OpenClassrooms-Student-Center/Back-End.NET_Testez_implementation_nouvelle_fonctionnalite_P3

# DotNetEnglishP3
Dépôt de l’étudiant pour le projet 3 du parcours Développeur Back-End .NET. Afin d'être au plus proche d'une situation professionnelle réelle, le code dans ce dépôt est en anglais.

Ce projet possède une base de données intégrée qui sera créée lorsque l’application sera exécutée pour la première fois. Pour la créer correctement, vous devez satisfaire aux prérequis ci-dessous et mettre à jour les chaînes de connexion pour qu’elles pointent vers le serveur MSSQL qui est exécuté sur votre PC en local.

**Prérequis** : MSSQL Developer 2019 ou Express 2019 doit être installé avec Microsoft SQL Server Management Studio (SSMS).
MSSQL : https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads
SSMS : https://docs.microsoft.com/fr-fr/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16
*Remarque : les versions antérieures de MSSQL Server devraient fonctionner sans problèmes, mais elles n’ont pas été testées.
*Dans le projet P3AddNewFunctionalityDotNetCore, ouvrez le fichier appsettings.json.*
Vous avez la section ConnectionStrings qui définit les chaînes de connexion pour les 2 bases de données utilisées dans cette application.
      "ConnectionStrings":
      {
        "P3Referential": "Server=.;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true",
        "P3Identity": "Server=.;Database=Identity;Trusted_Connection=True;MultipleActiveResultSets=true"
      }
  
**P3Referential** - chaîne de connexion à la base de données de l’application.
