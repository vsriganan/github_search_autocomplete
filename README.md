# Autocomplete Github Search API

## Local Development

### Prerequisites
1. Install [.NET Framework 5](https://dotnet.microsoft.com/download) Developer Pack
2. Install [Visual Studio](https://visualstudio.microsoft.com/vs/) IDE for Development (2019 Preferred)
3. Github Account with [Personalised Token](https://docs.github.com/en/github/authenticating-to-github/creating-a-personal-access-token) (Preferred for Authentication) 

### Installation
1. Clone the [Repository](https://github.com/vsriganan/github_search_autocomplete) to a local folder
2. Open the cloned folder and unzip the Packages.zip to any folder (Remember the Folder Path)
3. Open the Solution file in Visual Studio (2019 Preferred)
4. Go to Tools > Options > NuGet Package Manager > Package Sources
     * Click on "+" icon
     * Below enter name as "Nuget Offline Packages" (You can enter any name here!)
     * Provide the source path unzipped in Step 2 here
     * Then click "Ok"
5. Right-click AutoComplete_Github_Application and select "Manage NuGet Packages..."
     * From the drop down on Top-Right corner, select the "Package Resource" you created in Step 4.
6. Similarly, Right-click AutoComplete_GitHub_SearchAPI and select "Manage NuGet Packages..."
     * From the drop down on Top-Right corner, select the "Package Resource" you created in Step 4.
7. Finally, Right-click xUnitTest_AutoComplete and select "Manage NuGet Packages..."
     * From the drop down on Top-Right corner, select the "Package Resource" you created in Step 4.
8. Now, Right-Click the Solution file and select "Set Startup Projects..."
     * Under "Startup Project" select "Multiple startup Projects:" radio button
     * Under "Action" column, select "Start" for "AutoComplete_Github_Application" and "AutoComplete_GitHub_SearchAPI"
     * Now press "Ok"
9. Click on "Start" and enjoy Developing!

### Updating your Github Access Token
You may use your own Personalised Access Token, to access the Search API in an Authenticated manner. [(Read more..)](https://docs.github.com/en/rest/reference/search#rate-limit)

1. Expand "AutoComplete_GitHub_SearchAPI" project in the Solution in Visual Studio.
2. Select "appsettings.json" file
3. Replace the "GitHubAuthorization" value for it.
```json
"GitHubAuthorization": "{Your Token Here}"
```
 
