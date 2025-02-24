﻿using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using electrifier.Core.Forms;
using System.Runtime.CompilerServices;

namespace electrifier.Core
{
    public sealed class AppContext
      : System.Windows.Forms.ApplicationContext
    {
        #region Properties ====================================================================================================

        public Icon Icon { get; }
        public Bitmap Logo { get; }
        public static bool IsBetaVersion => true;
        public static string FormTitleAffix => $"electrifier { AssemblyVersion.ToString(3) }";

        #endregion ============================================================================================================

        #region Static Assembly Attribute Accessors ===========================================================================

        public static Version AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version;

        public static string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes.Length > 0)
                {
                    var titleAttribute = (AssemblyTitleAttribute)attributes[0];

                    if (!string.IsNullOrWhiteSpace(titleAttribute.Title))
                        return titleAttribute.Title;
                }

                return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public static string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);

                return (attributes.Length == 0) ? string.Empty : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public static string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);

                return (attributes.Length == 0) ? string.Empty : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public static string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);

                return (attributes.Length == 0) ? string.Empty : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public static string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);

                return (attributes.Length == 0) ? string.Empty : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        #endregion ============================================================================================================

        // TODO: See new AppContext-class, .net >4.6: https://docs.microsoft.com/en-us/dotnet/api/system.appcontext?view=netcore-3.1

        /// <summary>
        /// 
        /// <see cref="AppContext"/> is the main entry point of electrifier Application.
        /// 
        /// </summary>
        /// <param name="args">The argument list given when starting the application</param>
        /// <param name="appIcon">The icon resource used by this application</param>
        /// <param name="appLogo">The logo resource used by this application</param>
        /// <param name="splashScreenForm">The form representing the logo as splash screen</param>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlAppDomain)]
        public AppContext(string[] args, Icon appIcon, Bitmap appLogo, Form splashScreenForm)
          : base()
        {
            bool isPortable = false, isIncognito = false;

            if (null == args)
                throw new ArgumentNullException(nameof(args));

            if (null == splashScreenForm)
                throw new ArgumentNullException(nameof(splashScreenForm));

            this.Icon = appIcon ?? throw new ArgumentNullException(nameof(appIcon));
            this.Logo = appLogo; // 22.03.20: This may be null if started with nosplash... So remove this, and use other way to get the bitmap if it's needed // ?? throw new ArgumentNullException(nameof(appLogo));

            // Initialize Exception handlers
            Application.ThreadException += this.Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomain_UnhandledException;

            // Parse command line paramaters
            foreach (var arg in args)
            {
                // Portable: Issue #5: Add "-portable" command switch, storing configuration in application directory instead of "LocalApplicationData"
                if (arg.Equals("/portable", StringComparison.OrdinalIgnoreCase))
                    isPortable = true;

                // Incognito: Don't modify configuration file on exit, thus don't make session parameters/changes persistent
                if (arg.Equals("/incognito", StringComparison.OrdinalIgnoreCase))
                    isIncognito = true;
            }

            // Initialize logging context
            LogContext.Initialize();
            LogContext.Trace();

            // Add ThreadExit-handler to save configuration when closing
            this.ThreadExit += new EventHandler(this.AppContext_ThreadExit);
            Application.ApplicationExit += this.Application_ApplicationExit;

            // Create Session Context, including Database Context, and show SessionSelector Screen
            try
            {
                // TODO: Store sessioncontext?!?
                var sessionContext = new SessionContext(this.Icon, DetermineBaseDirectory(isPortable), isIncognito);
                sessionContext.MainFormChange += this.SessionContext_MainFormChange;
                sessionContext.CreateInitialForm(this);

                // Finally close splash screen...
                //TODO: Fade it out :)
                splashScreenForm.Close();
                splashScreenForm.Dispose();














                //if (result == DialogResult.OK)
                {
                    // Ablauf: 07/06/20, 23:18:
                    // SessionContext erzeugen(wie jetzt)
                    // SessionContext.PreviousSessions => Liste der bekannten, bisherigen Sessions
                    // SessionContext.Resume(SessionEntity sessionToResume) ODER SessionContext.Induce() ODER (SessionContext.Run(SessionEntity sessionToResume => may be null, => neue Session)

//                    var sescnt = sessionContext.PreviousSessions.Count;

                    // Currently, select the session with the highest id. In the future, take the session from the session selector or the last used session
                    // Another idea is: Use a callback for selecting a session object
//                    SessionEntity session = sessionContext.PreviousSessions.OrderByDescending(i => i.Id).FirstOrDefault();

                    //if (session is null)
                    //{
                    //    // TODO: Create new session object!
                    //    //this.Name = $"Session on { DateTime.Now.DayOfWeek }";         // TODO: Put into config!

                    //    //this.Id = this.DataContext.CreateNewEntity(typeof(SessionContext), (sqlCmd) =>
                    //    //{
                    //    //    sqlCmd.CommandText = $"INSERT INTO Session (Name) VALUES ($Name)";
                    //    //    sqlCmd.Parameters.AddWithValue("$Name", this.Name);
                    //    //});

                    //}

                    //sessionSelector.Dispose();

//                    this.MainForm = sessionContext.Run(session);
                }


                //// Ablauf: 07/06/20, 23:18:
                //// SessionContext erzeugen(wie jetzt)
                //// SessionContext.PreviousSessions => Liste der bekannten, bisherigen Sessions
                //// SessionContext.Resume(SessionEntity sessionToResume) ODER SessionContext.Induce() ODER (SessionContext.Run(SessionEntity sessionToResume => may be null, => neue Session)

                //var sescnt = SessionContext.PreviousSessions.Count;

                //// Currently, select the session with the highest id. In the future, take the session from the session selector or the last used session
                //// Another idea is: Use a callback for selecting a session object
                //SessionEntity session = SessionContext.PreviousSessions.OrderByDescending(i => i.Id).FirstOrDefault();

                //if (session is null)
                //{
                //    // TODO: Create new session object!
                //    //this.Name = $"Session on { DateTime.Now.DayOfWeek }";         // TODO: Put into config!

                //    //this.Id = this.DataContext.CreateNewEntity(typeof(SessionContext), (sqlCmd) =>
                //    //{
                //    //    sqlCmd.CommandText = $"INSERT INTO Session (Name) VALUES ($Name)";
                //    //    sqlCmd.Parameters.AddWithValue("$Name", this.Name);
                //    //});

                //}


            }
            catch (Exception ex)
            {
                LogContext.Error(ex.Message);
                throw;
            }



            //// TODO: Incognito? IsPortable?

            //this.DataContext = new EntityLighter.DataContext($"{this.BaseDirectory}\\electrifier.config",
            //    new Type[] { typeof(SessionContext) });        // TODO: Add each entity with on its own AFTER creation of DataContext

            //// Initialize session object
            ////
            //// TODO: Check if another instance is already running. If so, create new session with different name and fresh settings; optionally copy default session to new session settings!
            ////

            //// TODO: Param: "/Incognito" - Dont' save session configuration on application exit... Thus, use In-Memeory-DB which will be loaded, but NOT saved
            ////// TODO: 19/04/20: As long as we don't use a session-selector, use session #1
            ////// ...or select(max(id)) from session...
            ////SessionEntity sessionEntity = new SessionEntity(AppContext.ElEntityStore);
            //this.Session = new SessionContext(this);
            //this.MainForm = this.Session.ApplicationWindow;





            // TODO: Implement own MessageBox with "This is ALPHA-sign" and warning-text, perhaps use logo again
            /* Commented out for Issue #25
                        if (AppContext.IsBetaVersion())
                        {
                            if (false == Properties.Settings.Default.Disable_BETAVersion_Warning)
                            {
                                // Disable TopMost-property of SplashScreenForm to ensure Beta-Warning is shown in front of it
                                splashScreenForm.TopMost = false;

                                string msgText = "Hello and welcome to electrifier!\n\n" +
                                    "Please be aware that this is beta software and you should not use it in productive environments. Although carefully tested " +
                                    "there will be bugs, which may result in data loss, crashes, system instability and / or rage attacks!\n\n" +
                                    "No one can be held responsible for any damage that is caused by using this software!\n\n" +
                                    "Use at your own risk. By continuing using this software you show that you understand the implications.\n\n" +
                                    "Click Yes to see this warning again, or No to disable it forever!";

                                if (DialogResult.No == MessageBox.Show(msgText, "electrifier - Warning!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning))
                                {
                                    Properties.Settings.Default.Disable_BETAVersion_Warning = true;
                                }
                            }
                        }
            // Commented out for Issue #25 */






        }

        private void SessionContext_MainFormChange(object sender, MainFormChangeEventArgs args)
        {
            //if(sender != this.sessionContext)
            this.MainForm = args.NewMainForm;
        }

        [Conditional("DEBUG")]
        public static void AddDebugRemark(ref string targetString) => targetString = string.Concat(targetString, " ‖ DEBUG ☻");

        public static string BuildDefaultFormText(string baseFormText = null)
        {
            var text = string.IsNullOrWhiteSpace(baseFormText) ?
                FormTitleAffix :
                $"{baseFormText} - {FormTitleAffix}";

            AddDebugRemark(ref text);

            return text;
        }

        // ====================================================================================================================

        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            LogContext.Trace();

            //            // Save configuration file
            //            if (false == this.IsIncognito)
            //                this.Session.SaveConfiguration();
        }

        private void AppContext_ThreadExit(object sender, EventArgs e)
        {
            LogContext.Trace();
        }

        private void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // TODO: Use Vanara.Windows.Forms.TaskDialog;
            var exMessage = $"Thread exception occured: { e.Exception.GetType().FullName }\n" +
                            $"\n{ e.Exception.Message }\n" +
                            $"\n{ e.Exception.StackTrace }\n";

            if (null != e.Exception.InnerException)
                exMessage += $"\nInner Exception: { e.Exception.InnerException.Message }\n";

            LogContext.Error(exMessage);
            MessageBox.Show(exMessage, "D'oh! That shouldn't have happened...");
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // TODO: Use Vanara.Windows.Forms.TaskDialog;
            var exMessage = $"Domain exception occured: { e.ExceptionObject.GetType().FullName }" +
                            $"\n\n{ e.ExceptionObject }\n";

            LogContext.Error(exMessage);
            MessageBox.Show(exMessage, "D'oh! That shouldn't have happened...");
        }

        private static string DetermineBaseDirectory(bool isPortable)
        {
            if (isPortable)
                return Application.StartupPath;

            var baseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                AppContext.AssemblyCompany);

            // Ensure the directory exists
            if (!Directory.CreateDirectory(baseDirectory).Exists)
                throw new DirectoryNotFoundException();

            return baseDirectory;
        }

        #region Environment helper methods ====================================================================================

        /// <summary>
        /// Beginning with .net framework 4.5, a registry value has to be evaluated to get the exact .net framekwork version.
        /// 
        /// This static method returns the exact framework version by using this technique.
        /// 
        /// <seealso href="https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed"/>
        /// </summary>
        /// <param name="check64Bit">Check for 64 bit environment if true.</param>
        /// <returns>A string value representing the exact .net framework version</returns>
        public static string GetDotNetFrameworkVersion(bool check64Bit = true)
        {
            const string regSubKey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";
            const string regValue = @"Release";

            using (var regKeyLocalMachine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32))
            {
                using (var regKeyNetDeveloperPlatform = regKeyLocalMachine?.OpenSubKey(regSubKey))
                {
                    if (null != regKeyNetDeveloperPlatform?.GetValue(regValue))
                    {
                        return string.Format("electrifier is using .NET Framework" +
                            $" v{CheckFor45PlusVersion((int)regKeyNetDeveloperPlatform.GetValue(regValue))}" +
                            (check64Bit ? $", {(Environment.Is64BitProcess ? "64" : "32")} bit" : ""));
                    }
                }
            }

            return @".NET Framework Version 4.5 or later is not detected.";

            // Checking the version using for forward compatibility.
            string CheckFor45PlusVersion(int releaseKey)
            {
                if (releaseKey > 461814)
                    return "4.7.2 or later";
                if (releaseKey >= 461808)
                    return "4.7.2";
                if (releaseKey >= 461308)
                    return "4.7.1";
                if (releaseKey >= 460798)
                    return "4.7";
                if (releaseKey >= 394802)
                    return "4.6.2";
                if (releaseKey >= 394254)
                    return "4.6.1";
                if (releaseKey >= 393295)
                    return "4.6";
                if (releaseKey >= 379893)
                    return "4.5.2";
                if (releaseKey >= 378675)
                    return "4.5.1";
                if (releaseKey >= 378389)
                    return "4.5";
                // This code should never execute. A non-null release key should mean that 4.5 or later is installed.
                return "No 4.5 or later version detected";
            }
        }

        #endregion ============================================================================================================
    }
}
