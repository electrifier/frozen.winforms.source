﻿using electrifier.Core.Components;
using electrifier.Core.Components.DockContents;
using electrifier.Core.Forms;
using RibbonLib.Controls.Events;
using RibbonLib.Interop;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using Vanara.PInvoke;

namespace RibbonLib.Controls
{
    /// <summary>
    /// 
    /// This partial class file contains the creation of the Ribbon.
    /// </summary>
    public partial class RibbonItems
    {
        #region Fields ====================================================================================================


        private Shell32.FOLDERVIEWMODE shellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_AUTO;

        #endregion Fields =================================================================================================

        #region Properties =====================================================================================================

        public ApplicationWindow ApplicationWindow { get; }


        #endregion =============================================================================================================

        public Shell32.FOLDERVIEWMODE ShellFolderViewMode
        {
            get => this.shellFolderViewMode;
            set
            {
                //  AppContext.TraceDebug($"Ribbon: ShellFolderViewMode = {value}");

                if (this.shellFolderViewMode == value)
                    return;

                this.ApplicationWindow.BeginInvoke(new MethodInvoker(delegate ()
                {
                    // https://docs.microsoft.com/en-us/windows/win32/windowsribbon/windowsribbon-reference-properties-uipkey-booleanvalue

                    this.shellFolderViewMode = value;
                    this.BtnHomeViewExtraLargeIcons.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_THUMBNAIL);
                    this.BtnHomeViewLargeIcons.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_ICON);
                    this.BtnHomeViewMediumIcons.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_THUMBSTRIP);
                    this.BtnHomeViewSmallIcons.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_SMALLICON);
                    this.BtnHomeViewList.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_LIST);
                    this.BtnHomeViewDetails.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_DETAILS);
                    this.BtnHomeViewTiles.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_TILE);
                    this.BtnHomeViewContent.BooleanValue = (value == Shell32.FOLDERVIEWMODE.FVM_CONTENT);
                }));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationWindow"></param>
        /// <param name="ribbon"></param>
        public RibbonItems(ApplicationWindow applicationWindow, Ribbon ribbon)
            : this(ribbon)
        {
            this.ApplicationWindow = applicationWindow ?? throw new ArgumentNullException(nameof(applicationWindow));

            //ribbon.LoadSettingsFromStream(str);
            //ribbon.SetColors(Color.Wheat, Color.IndianRed, Color.BlueViolet);


            #region Event Handlers ============================================================================================

            //
            // Quick Access Toolbar Commands ==================================================================================
            //
            this.BtnQATOpenNewShellBrowserPanel.ExecuteEvent += this.ApplicationWindow.CmdAppOpenNewShellBrowserPane_ExecuteEvent;

            //
            // Application Menu Items =========================================================================================
            //
            this.BtnAppOpenNewWindow.Enabled = false;
            this.BtnAppOpenNewShellBrowserPanel.ExecuteEvent += this.ApplicationWindow.CmdAppOpenNewShellBrowserPane_ExecuteEvent;
            this.BtnAppOpenCommandPrompt.Enabled = false;
            this.BtnAppOpenWindowsPowerShell.Enabled = false;
            this.BtnAppChangeElectrifierOptions.Enabled = false;
            this.BtnAppChangeFolderAndSearchOptions.Enabled = false;
            this.BtnAppHelp_AboutElectrifier.ExecuteEvent += this.ApplicationWindow.CmdAppHelpAboutElectrifier_ExecuteEvent;
            this.BtnAppHelp_AboutWindows.ExecuteEvent += this.ApplicationWindow.CmdAppHelpAboutWindows_ExecuteEvent;
            this.BtnAppClose.ExecuteEvent += this.ApplicationWindow.CmdAppClose_ExecuteEvent;

            //
            // Command Group: Home -> Clipboard ===============================================================================
            //
            this.BtnClipboardCopyFullFilePaths.Enabled = false;
            this.BtnClipboardCopyFileNames.Enabled = false;
            this.BtnClipboardCopyDirectoryPaths.Enabled = false;
            this.BtnClipboardPasteAsNewFile.Enabled = false;
            this.BtnClipboardPasteAsNewTextFile.Enabled = false;
            this.BtnClipboardPasteAsNewBMPFile.Enabled = false;
            this.BtnClipboardPasteAsNewJPGFile.Enabled = false;
            this.BtnClipboardPasteAsNewPNGFile.Enabled = false;
            this.BtnClipboardPasteAsNewGIFFile.Enabled = false;
            this.BtnClipboardHistory.Enabled = false;

            //
            // Command Group: Home -> Organise ================================================================================
            //
            this.BtnOrganiseMoveTo.Enabled = false;
            this.BtnOrganiseCopyTo.Enabled = false;
            this.BtnOrganiseDelete.Enabled = false;
            this.BtnOrganiseRename.Enabled = false;

            //
            // Command Group: Home -> Select ==================================================================================
            //

            //
            // Command Group: Home -> Layout ==================================================================================
            //
            this.BtnHomeViewExtraLargeIcons.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewLargeIcons.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewMediumIcons.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewSmallIcons.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewList.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewDetails.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewTiles.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;
            this.BtnHomeViewContent.ExecuteEvent += this.CmdBtnHomeView_ExecuteEvent;

            //
            // Command Group: Desktop -> Icon Layout ==========================================================================
            //
            this.BtnDesktopIconSettingsLaunchControlPanel.Enabled = false;
            this.CbxDesktopIconSettingsSpacingVertical.Enabled = false;
            this.CbxDesktopIconSettingsSpacingHorizontal.Enabled = false;
            this.BtnDesktopShortcutsDefaults.Enabled = false;
            this.BtnDesktopShortcutsValidate.Enabled = false;

            #endregion Event Handlers =========================================================================================

            //// TODO: Iterate through and disable all child elements that have no ExecuteEvent-Handler to get rid of those "Enabled=false"-Statements


            // TODO: For test purposes, enable all available Contexts
            this.TabGrpDesktopTools.ContextAvailable = ContextAvailability.Active;
        }

        private void CmdBtnHomeView_ExecuteEvent(object sender, ExecuteEventArgs _)
        {
            Debug.Assert(sender is RibbonToggleButton);

            if (sender is null) throw new ArgumentNullException(nameof(sender));

            this.ApplicationWindow.BeginInvoke(new MethodInvoker(delegate ()
            {
                var cmdID = (sender as BaseRibbonControl).CommandID;
#pragma warning disable CS0219 // Variable is assigned but its value is never used
                Shell32.FOLDERVIEWMODE newShellFolderViewMode;
#pragma warning restore CS0219 // Variable is assigned but its value is never used

                switch (cmdID)
                {
                    case Cmd.BtnHomeViewExtraLargeIcons:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_THUMBNAIL;
                        break;
                    case Cmd.BtnHomeViewLargeIcons:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_ICON;
                        break;
                    case Cmd.BtnHomeViewMediumIcons:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_THUMBSTRIP;
                        break;
                    case Cmd.BtnHomeViewSmallIcons:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_SMALLICON;
                        break;
                    case Cmd.BtnHomeViewList:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_LIST;
                        break;
                    case Cmd.BtnHomeViewDetails:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_DETAILS;
                        break;
                    case Cmd.BtnHomeViewTiles:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_TILE;
                        break;
                    case Cmd.BtnHomeViewContent:
                        newShellFolderViewMode = Shell32.FOLDERVIEWMODE.FVM_CONTENT;
                        break;
                    default:
                        throw new IndexOutOfRangeException(nameof(cmdID));
                }

// TODO:       // Finally, apply new ViewMode if ActiveDockContent is of type ElNavigableDockContent
//                if (this.ApplicationWindow.ActiveDockContent is NavigableDockContent navigableDockContent)
//                    navigableDockContent.ShellFolderViewMode = newShellFolderViewMode;
            }));
        }


    }
}
