# Readme

This is a very basic demo of sending activity feed notifications using Graph API.

Key files:

- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Controllers\HomeController.cs contains the logic for generating the models used in generating the demo + auth views
- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Views\Home\ActivityFeedPage.cshtml contains the code for the UI. It simply has some text and a button that, when pressed, generates an activity feed notification. It also has some javascript that calls a controller method to create the send request to send a notification
- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Views\Home\Auth.cshtml and  ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Views\Home\AuthDone.cshtml and ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Scripts\teamsapp.js get the user delegated token for the tab.
- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Requests\SendActivityFeedNotificationRequest.cs contains the logic for sending the request to Microsoft Graph to send the notification

## Demo script

git clone https://github.com/eddie-lee-msft/Activity-Feed-Notification-Demo.git

AFN Demo (short for Activity Feed Notification Demo) is a tab application that sends notifications to the user at the press of a button. It is sent via application context.

Spin up ngrok:
[See the 'Locally hosted' section of this to spin up ngrok](https://docs.microsoft.com/en-us/microsoftteams/platform/concepts/build-and-test/debug)
- The port for this app is '3333'.
- You should get some ngrok link like 'https://{ngrokid}.ngrok.io'. Save this link somewhere.

Demo setup:
[Register the app on Azure Active Directory](https://docs.microsoft.com/en-us/azure/active-directory/develop/quickstart-register-app)
- You can name this app whatever you want - just don't forget what the name is.
- For the Redirect URI, use 'https://{ngrokid}.ngrok.io/authdone', where ngrokid is the ngrok ID you retrieved from above.
- Grab the 'Application (client) ID' and save it somewhere.
- Go to 'Certificates & secrets' and create a new client secret. Save the value somewhere.
- Within the 'web.config' file, replace the 'GraphAppId' value with the Application ID above and the 'GraphAppPassword' value with the client secret created above. At the same time, replace the 'TeamsAppId' value with the Teams App ID you retrieved from above.

Manifest setup:
- Find the 'manifest.json' file in the code. This should be in the 'Manifest' folder.
- Replace all instances of ngrok links in this manifest with the ngork link you just got, keeping any link suffixes (like '/configure'), if they exist.
- Under 'webApplicationInfo' replace the value for 'id' with the Application ID you got from Azure Active Directory when registering an app.
- You are free to rename the application under 'name' if you so desire.
- Now navigate to the 'Manifest' folder and a create a zip file from the three items in the folder. This should contain 'AF20x20.png', 'AF96x96.png', and the 'manifest.json' file.

Install into a team:
- First, run the code from Visual Studio. A browser window will open with the address 'localhost:3333'. You can just ignore that.
- Go to https://teams.microsoft.com and launch the webclient.
- Go to 'devspaces' or 'cdshadow' service.
- Go to 'Apps' on the left pane, click 'More apps'
- Click 'Upload a custom app', then click 'Upload for me or my teams'.
- Choose the .zip file you created.
- Click the down-arrow next to the 'Add' button, and click 'Add to a Team'. Do not add this to a chat, as this app won't work with chats :)
- Search for a Team you want to install this app to.
- After a moment, a window will open saying 'Nothing to configure, keep going'. Click 'Save'. This will create the tab for this app.

You're done with setup!
Now click 'Click Me!' and a notification should pop up that brings you to the Team/Channel that the app was installed onto.

