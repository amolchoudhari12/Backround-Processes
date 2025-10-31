namespace Vilani.BackroundService
{
    partial class ProjectInstaller
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
            this.vilaniProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.vilaniServiceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // vilaniProcessInstaller
            // 
            this.vilaniProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.vilaniProcessInstaller.Password = null;
            this.vilaniProcessInstaller.Username = null;
            // 
            // vilaniServiceInstaller
            // 
            this.vilaniServiceInstaller.ServiceName = "VilaniBackroundService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.vilaniProcessInstaller,
            this.vilaniServiceInstaller});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller vilaniProcessInstaller;
        private System.ServiceProcess.ServiceInstaller vilaniServiceInstaller;
    }
}