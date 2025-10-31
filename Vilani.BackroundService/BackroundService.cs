using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using Vilani.BackroundService.PublicServices;

namespace Vilani.BackroundService
{
    public partial class BackroundService : ServiceBase
    {

        protected System.Timers.Timer timer;


        public BackroundService()
        {
            InitializeComponent();

            string source = ConfigurationSettings.AppSettings["LogFileSource"];
            string logFile = ConfigurationSettings.AppSettings["LogFileName"];

            if (!System.Diagnostics.EventLog.SourceExists(source))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    source, logFile);
            }
            vilaniEventLog.Source = source;
            vilaniEventLog.Log = logFile;

                Debugger.Launch();
              InvokePlottingStatusReportGenerator();
        }

        protected void InitializeTimer()
        {
            if (timer == null)
            {
                timer = new System.Timers.Timer();
                timer.AutoReset = true;
                timer.Interval = Convert.ToDouble(ConfigurationSettings.AppSettings["TimeIntervel"]);
                timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            }
        }

        protected override void OnStart(string[] args)
        {

            vilaniEventLog.WriteEntry("Service is started Successfully...");
            vilaniEventLog.WriteEntry("Timer initializing......");
            InitializeTimer();
            vilaniEventLog.WriteEntry("Timer initialized !");
            timer.Enabled = true;
        }

        protected void timer_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            RunCommands();
        }
        protected void RunCommands()
        {
            vilaniEventLog.WriteEntry("In Timer.");

            try
            {
                InvokePlottingStatusReportGenerator();
                InvokeVilaniTranslatatorForDatabaseTables();
            }
            catch (Exception ex)
            {
                vilaniEventLog.WriteEntry(ex.Message);
            }


        }


        private void InvokeVilaniTranslatatorForDatabaseTables()
        {

            try
            {
                vilaniEventLog.WriteEntry("Vilani Translator Job started....");

                Vilani.LanguageTranslators.RealEstate.VilaniLanguageTranslatorFasade vilaniLanguageTranslatorFasade = new Vilani.LanguageTranslators.RealEstate.VilaniLanguageTranslatorFasade();

                if (Convert.ToBoolean(ConfigurationSettings.AppSettings["TranslatePlottingDomainTables"]))
                {
                    vilaniLanguageTranslatorFasade.TranslatePlottingDomainTables();
                }
                if (Convert.ToBoolean(ConfigurationSettings.AppSettings["TranslateAllDomainTables"]))
                {
                    vilaniLanguageTranslatorFasade.TranslateAllDomainTables();
                }

                vilaniEventLog.WriteEntry("Vilani Translator Job completed successfully !");
            }
            catch (Exception ex)
            {
                vilaniEventLog.WriteEntry("Error While vilani translator Job : " + ex.Message);
            }

        }


        private void InvokePlottingStatusReportGenerator()
        {
            try
            {
                vilaniEventLog.WriteEntry("Plotting Status Report Job started....");

                PublicServicesClient client = new PublicServicesClient();
                client.GeneratePlottingStatusReport("amolstatus", System.DateTime.Now);

                vilaniEventLog.WriteEntry("Plotting Status Report Job completed successfully !");

            }
            catch (Exception ex)
            {
                vilaniEventLog.WriteEntry("Error While creating Plotting Status Report Job : " + ex.Message);
            }

        }


        protected override void OnStop()
        {
            vilaniEventLog.WriteEntry("Service is stopped Successfully...");
        }

        protected override void OnContinue()
        {
            vilaniEventLog.WriteEntry("In OnContinue.");
        }

    }
}
