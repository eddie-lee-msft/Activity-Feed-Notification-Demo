# Readme

This is a very basic demo of sending activity feed notifications using Graph API.

Key files:

- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Controllers\HomeController.cs contains the logic for generating the models used in generating the demo + auth views
- ActivityFeedNotificationDemo\Microsoft.Teams.Samples.ActivityFeedNotificationDemo\Views\Home\ActivityFeedPage.cshtml contains the code for the UI. It simply has some text and a button that, when pressed, generates an activity feed notification