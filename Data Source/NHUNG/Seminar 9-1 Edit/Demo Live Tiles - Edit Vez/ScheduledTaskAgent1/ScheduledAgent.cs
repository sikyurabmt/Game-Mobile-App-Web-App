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
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Info;
using System.Linq;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;

        private Random rand = new Random();
        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background
            startTileTask(task);
        }


        private void startTileTask(ScheduledTask task)
        {
            // You must use IconicTileData because TemplateIconic tile template is set in WMAppMainifest.xml.
            // However, if you change the tile template in WMAppMainifest.xml, you must change the constructor below to appropriately match, 
            // such as CycleTileData or StandardTileData.
            ShellTile ShellTile = ShellTile.ActiveTiles.First();
            Uri mp = new Uri("/MainPage.xaml?", UriKind.Relative);
            CycleTileData TileData = new CycleTileData
            {
                Title = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString(),
                Count = new Random().Next(1, 10),
                SmallBackgroundImage = new Uri("/Assets/Tiles/Image1.png", UriKind.Relative),//Gets and sets the front-side background image for the small Tile size
                CycleImages = new Uri[]
                     {
                         new Uri("/Assets/Tiles/Image1.png",UriKind.Relative),
                         new Uri("/Assets/Tiles/Image2.png",UriKind.Relative),
                          new Uri("/Assets/Tiles/Image3.png",UriKind.Relative),
                        
                     }
            };


            ShellTile mainTile = ShellTile.ActiveTiles.First();
            if (mainTile != null)
            {
                mainTile.Update(TileData);
            }

            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(60));
            // ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromTicks(10));
#endif

            // Call NotifyComplete to let the system know the agent is done working.
            NotifyComplete();
        }
    }
}
