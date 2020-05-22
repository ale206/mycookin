using System.ServiceProcess;

namespace TaechIdeas.Core.Network.Dispatcher
{
    partial class DispatcherInstaller
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.dispatcherService = new System.ServiceProcess.ServiceInstaller();

            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceProcessInstaller.Username = null;
            this.serviceProcessInstaller.Password = null;

            // 
            // paymentService
            // 
            this.dispatcherService.ServiceName = ExecutorService.DispatcherName;
            this.dispatcherService.StartType = ServiceStartMode.Automatic;

            // 
            // ProjectInstaller
            // 
            this.Installers.Add(serviceProcessInstaller);
            this.Installers.Add(dispatcherService);
        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller dispatcherService;
    }
}