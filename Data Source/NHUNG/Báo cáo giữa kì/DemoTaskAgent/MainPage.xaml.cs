/* 
    Copyright (c) 2012 - 2013 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://code.msdn.microsoft.com/wpapps
  
*/
#define DEBUG_AGENT

using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;

namespace sdkPeriodicAgentCS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // There are two periodic tasks that are being demonstrated. 
        // The first periodic task is associated with a Toast notification.
        // The second periodic task is associated with a Tile update.
        PeriodicTask toastPeriodicTask;
        PeriodicTask tilePeriodicTask;

        // Main strings
        const string tileTaskName = "TilePeriodicAgent";
        const string startStopButtonMessage = "Select Toast or Tile notification type, then press Start Agent.";
        const string startAgentString = "Start Agent";
        const string stopAgentString = "Stop Agent";
        const string periodicTaskDesc = "This demonstrates a periodic task.";
        const string defaultTaskDesc = "To start a background periodic agent, press the Start Agent button.";
        const string tileAgentDetails = "To see the updated Tile for this app, first pin this app to the Start screen. When the background agent is triggered, the Tile will update.";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Display the default instructions.
            agentDetails.Text = defaultTaskDesc;

            // Find running periodic task.
            tilePeriodicTask = ScheduledActionService.Find(tileTaskName) as PeriodicTask;

            // Update the UI based on the running periodic task.
            if (toastPeriodicTask != null)
            {
                // Update the UI to display the currently running toast task info.
                PeriodicStackPanel.DataContext = toastPeriodicTask;
                TileRadioBtnOption.IsEnabled = false;
                startStopButton.Content = stopAgentString;
            }

            else if (tilePeriodicTask != null)
            {
                // Update the UI to display the currently running Tile task info.
                PeriodicStackPanel.DataContext = tilePeriodicTask;
                TileRadioBtnOption.IsChecked = true;
                TileRadioBtnOption.IsEnabled = false;
                startStopButton.Content = stopAgentString;
                agentDetails.Text = tileAgentDetails;
            }
            else
            {
                // No running task is running. Select Tile radio button.
                TileRadioBtnOption.IsChecked = true;
            }
        }

        private void startStopButton_Click(object sender, RoutedEventArgs e)
        {
            // Find a reference to each of the two possibe periodic agents.
            tilePeriodicTask = ScheduledActionService.Find(tileTaskName) as PeriodicTask;

            // If either periodic agent is running, end it.
            // Otherwise, run the periodic agent based on the radio
            // button selection.
            if ((tilePeriodicTask != null))
            {

                // If the Tile periodic task is running, end it.
                if (tilePeriodicTask != null)
                {
                    RemoveAgent(tileTaskName);
                }
            }
            else
            {
                // Run the periodic agent based on the radio button selection.
                if ((bool)TileRadioBtnOption.IsChecked)
                {
                    StartPeriodicAgent(tileTaskName);
                    agentDetails.Text = tileAgentDetails;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(startStopButtonMessage);
                }
            }
        }

        private void StartPeriodicAgent(string taskName)
        {
            // Obtain a reference to the period task, if one exists
            toastPeriodicTask = ScheduledActionService.Find(taskName) as PeriodicTask;

            // If the task already exists and background agent is enabled for the
            // app, remove the task and then add it again to update 
            // the schedule.
            if (toastPeriodicTask != null)
            {
                RemoveAgent(taskName);
            }
            toastPeriodicTask = new PeriodicTask(taskName);

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the phone.
            toastPeriodicTask.Description = periodicTaskDesc;

            // Place the call to add a periodic agent. This call must be placed in 
            // a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(toastPeriodicTask);
                PeriodicStackPanel.DataContext = toastPeriodicTask;

                // If debugging is enabled, use LaunchForTest to launch the agent in one minute.
#if(DEBUG_AGENT)
                ScheduledActionService.LaunchForTest(taskName, TimeSpan.FromSeconds(1));
#endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }
                else if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                    MessageBox.Show("BNS Error: The maximum number of ScheduledActions of this type have already been added.");
                }
                else
                {
                    MessageBox.Show("An InvalidOperationException occurred.");
                }
            }
            catch (SchedulerServiceException)
            {
                // No user action required.
            }
            finally
            {
                // Determine if there is a running periodic agent and update the UI.
                toastPeriodicTask = ScheduledActionService.Find(taskName) as PeriodicTask;
                if (toastPeriodicTask != null)
                {
                    TileRadioBtnOption.IsEnabled = false;
                    startStopButton.Content = stopAgentString;
                }
            }
        }

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
                // Handle exception code here.
            }

            // Reset UI.
            PeriodicStackPanel.DataContext = null;
            TileRadioBtnOption.IsEnabled = true;
            agentDetails.Text = defaultTaskDesc;
            startStopButton.Content = startAgentString;
        }
    }
}
