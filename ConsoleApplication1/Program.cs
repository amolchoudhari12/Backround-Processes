using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BackroundService;
using Vilani.LanguageTranslators;
using Vilani.LanguageTranslators.RealEstate;




namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {

            TranslataTemp();
            //TempTranslate();
            //   RunTranslatorJob();
            //  RunTranslatorJobForPlottingScheme();
            Console.ReadKey();

        }

        private static void TranslataTemp()
        {
            VilaniLanguageTranslatorFasade vilaniLanguageTranslatorFasade = new VilaniLanguageTranslatorFasade();
            vilaniLanguageTranslatorFasade.TranslatePlottingDomainTables();
        }


        private static void RunTranslatorJobForPlottingScheme()
        {
            PlottingSchemeTranslator plottingScheme = new PlottingSchemeTranslator();
            plottingScheme.Run();

            PropertyTranslator property1 = new PropertyTranslator();
            property1.Run();

            DeveloperTranslator developer = new DeveloperTranslator();
            developer.Run();

            OfficeAddressTranslator officeAddress = new OfficeAddressTranslator();
            officeAddress.Run();

            PlottingAddressTranslator plottinAddress = new PlottingAddressTranslator();
            plottinAddress.Run();



        }

        private static void RunTranslatorJob()
        {

            PlottingSchemeTranslator plottingScheme = new PlottingSchemeTranslator();
            plottingScheme.Run();

            PropertyTranslator property1 = new PropertyTranslator();
            property1.Run();

            ProjectTranslator project = new ProjectTranslator();
            project.Run();

            DeveloperTranslator developer = new DeveloperTranslator();
            developer.Run();

            OfficeAddressTranslator officeAddress = new OfficeAddressTranslator();
            officeAddress.Run();

            PlottingAddressTranslator plottinAddress = new PlottingAddressTranslator();
            plottinAddress.Run();



            ListerTypeTranslator listerType = new ListerTypeTranslator();
            listerType.Run();

            ProjectPhaseTranslator phaseTranslator = new ProjectPhaseTranslator();
            phaseTranslator.Run();

            PropertyAddressTranslator propertyAddress = new PropertyAddressTranslator();
            propertyAddress.Run();

            PropertyListerTranslator propertyLister = new PropertyListerTranslator();
            propertyAddress.Run();

            LocationAirportListTranslator airport = new LocationAirportListTranslator();
            airport.Run();

            LocationAtmListTranslator atm = new LocationAtmListTranslator();
            atm.Run();

            LocationBankListTranslator bank = new LocationBankListTranslator();
            bank.Run();

            LocationBusStopListTranslator busstop = new LocationBusStopListTranslator();
            busstop.Run();

            LocationHospitalListTranslator hospital = new LocationHospitalListTranslator();
            hospital.Run();

            LocationMallListTranslator mall = new LocationMallListTranslator();
            mall.Run();

            LocationMarketListTranslator market = new LocationMarketListTranslator();
            market.Run();

            LocationRailwayStationListTranslator railwayStation = new LocationRailwayStationListTranslator();
            railwayStation.Run();


            LocationSchoolListTranslator schoolList = new LocationSchoolListTranslator();
            schoolList.Run();



        }

        private static void TempTranslate()
        {
            VilaniTranslatorServices.VilaniTranslatorServicesClient client = new VilaniTranslatorServices.VilaniTranslatorServicesClient();
            // ServiceReference1.VilaniTranslatorServicesClient client = new ServiceReference1.VilaniTranslatorServicesClient();
            Dictionary<string, string> inputParams = new Dictionary<string, string>();
            inputParams.Add("CEO", "Amol");
            inputParams.Add("COO", "Pramod");
            inputParams.Add("VP", "Priya");
            DateTime start1 = DateTime.Now;


            Dictionary<string, string> output = client.ConvertToMarathi(inputParams);


            DateTime stop1 = DateTime.Now;

            double diff = stop1.Minute - start1.Minute;


            start1 = DateTime.Now;
            string[] outPutArray = client.ConvertToEnglishArray(new string[] { "Amol", "Pramod", "Priya", "diff", "sdfg", "sdfgs", "dsfg", "अमोल" });
            stop1 = DateTime.Now;


            double diff1 = stop1.Minute - start1.Minute;

            foreach (var item in output)
            {
                Console.WriteLine("{0} is {1} \n", item.Key, item.Value);

            }
        }



    }
}
