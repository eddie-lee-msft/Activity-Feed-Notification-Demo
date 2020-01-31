# Activity Feed Notification Demo

This is a very basic demo for sending activity feed notifications using Graph API. At the click of a button, you may get a very basic notification.

Key files:

- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Controllers\HomeController.cs contains the logic for generating the models used in generating the demo.
- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Views\Home\ActivityFeedPage.cshtml contains the code for the UI. It simply has some text and a button that, when pressed, generates an activity feed notification. It also has some javascript that calls a controller method to create the send request to send a notification
- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Requests\SendActivityFeedNotificationRequest.cs contains the logic for sending the request to Microsoft Graph to send the notification

## Demo Instructions

git clone https://github.com/eddie-lee-msft/Activity-Feed-Notification-Demo.git

Spin up ngrok:
- [See the 'Locally hosted' section of this to spin up ngrok](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/build-and-test/debug)
- The port for this app is '3333'.
- You should get some ngrok link like 'https://{ngrokid}.ngrok.io'. Save this link somewhere.

Azure Active Directory Application setup:
- [Register the app on Azure Active Directory](https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app)
- The app should be registered under the tenant you're (eventually) installing this demo app in.
- You can name this app whatever you want - just don't forget what the name is.
- You can just leave the redirect URI blank.
- Grab the 'Application (client) ID' and save it somewhere.
- Go to 'Certificates & secrets' and create a new client secret. Save the value somewhere.

Manifest setup:
- Find the 'manifest.json' file in the code. This should be in the 'Manifest' folder.
- Replace all instances of ngrok links in this manifest with the ngork link you just got, keeping any link suffixes (like '/configure'), if they exist.
- Under 'webApplicationInfo' replace the value for 'id' with the Application ID you got from Azure Active Directory when registering an app.
- You are free to rename the application under 'name' if you so desire.
- Now navigate to the 'Manifest' folder and a create a zip file from the three items in the folder. This should contain 'AF20x20.png', 'AF96x96.png', and the 'manifest.json' file.

Install into a team (part 1):
- Go to https://teams.microsoft.com and launch the webclient.
- Go to 'devspaces' or 'cdshadow' service.
- Go to 'Teams', select a team you want to install the app into and then click 'Manage Team'.
- Click 'Upload a custom app' 
- Go to 'Apps' near the top and click 'Upload a custom app'. Find your .zip file and click 'Add'.

Install into a team (part 2):
- [Retrieve the Teams App ID of the app you just installed, see Example 2](https://docs.microsoft.com/en-us/graph/api/teamsappinstallation-list?view=graph-rest-1.0&tabs=http). You may have to hit the canary dev endpoint instead of the prod graph endpoint (https://canary.graph.microsoft.com/testprodbetatestTeamsGraphSvcDev/). Save it somewhere.
- Go to the code and within the 'web.config' file, replace the 'GraphAppId' value with the Application ID above and the 'GraphAppPassword' value with the client secret created above. At the same time, replace the 'TeamsAppId' value with the Teams App ID you retrieved from above.
- Now run the code from Visual Studio. A browser window will open with the address 'localhost:3333'. You can just ignore that.
- Now create a new tab for that app under any channel in the team. Click 'Save' on the configuration popup.

You're done with setup!
Now click 'Click Me!' and a notification should pop up that brings you to the Team/Channel that the app was installed onto.