using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class VilaniLanguageTranslatorFasade
    {
        #region properties
        private PlottingSchemeTranslator _plottingSchemeTranslator;
        private PropertyTranslator _propertyTranslator;
        private ProjectTranslator _projectTranslator;
        private DeveloperTranslator _developerTranslator;
        private OfficeAddressTranslator _officeAddressTranslator;
        private PlottingAddressTranslator _plottingAddressTranslator;
        private ListerTypeTranslator _listerTypeTranslator;
        private ProjectPhaseTranslator _projectPhaseTranslator;
        private PropertyAddressTranslator _propertyAddressTranslator;
        private PropertyListerTranslator _propertyListerTranslator;
        private LocationAirportListTranslator _locationAirportListTranslator;
        private LocationAtmListTranslator _locationAtmListTranslator;
        private LocationBankListTranslator _locationBankListTranslator;
        private LocationBusStopListTranslator _locationBusStopListTranslator;
        private LocationHospitalListTranslator _locationHospitalListTranslator;
        private LocationMallListTranslator _locationMallListTranslator;
        private LocationMarketListTranslator _locationMarketListTranslator;
        private LocationRailwayStationListTranslator _locationRailwayStationListTranslator;
        private LocationSchoolListTranslator _locationSchoolListTranslator;
        private ContactPersonTranslator _contactPersonTranslator;

        #endregion

        public VilaniLanguageTranslatorFasade()
        {
            _plottingSchemeTranslator = new PlottingSchemeTranslator();
            _propertyTranslator = new PropertyTranslator();
            _projectTranslator = new ProjectTranslator();
            _developerTranslator = new DeveloperTranslator();
            _officeAddressTranslator = new OfficeAddressTranslator();
            _plottingAddressTranslator = new PlottingAddressTranslator();
            _listerTypeTranslator = new ListerTypeTranslator();
            _projectPhaseTranslator = new ProjectPhaseTranslator();
            _propertyAddressTranslator = new PropertyAddressTranslator();
            _propertyListerTranslator = new PropertyListerTranslator();
            _locationAirportListTranslator = new LocationAirportListTranslator();
            _locationAtmListTranslator = new LocationAtmListTranslator();
            _locationBankListTranslator = new LocationBankListTranslator();
            _locationBusStopListTranslator = new LocationBusStopListTranslator();
            _locationHospitalListTranslator = new LocationHospitalListTranslator();
            _locationMallListTranslator = new LocationMallListTranslator();
            _locationMarketListTranslator = new LocationMarketListTranslator();
            _locationRailwayStationListTranslator = new LocationRailwayStationListTranslator();
            _locationSchoolListTranslator = new LocationSchoolListTranslator();
            _contactPersonTranslator = new ContactPersonTranslator();

        }

        public void TranslatePlottingDomainTables()
        {
            _developerTranslator.Run();
            _plottingSchemeTranslator.Run();
            _plottingAddressTranslator.Run();
            _officeAddressTranslator.Run();          
            _propertyTranslator.Run();
            _contactPersonTranslator.Run();
        }

        public void TranslateAllDomainTables()
        {
            _plottingSchemeTranslator.Run();
            _propertyTranslator.Run();
            _projectTranslator.Run();
            _developerTranslator.Run();
            _officeAddressTranslator.Run();
            _plottingAddressTranslator.Run();
            _listerTypeTranslator.Run();
            _projectPhaseTranslator.Run();
            _propertyAddressTranslator.Run();
            _propertyListerTranslator.Run();
            _locationAirportListTranslator.Run();
            _locationAtmListTranslator.Run();
            _locationBankListTranslator.Run();
            _locationBusStopListTranslator.Run();
            _locationHospitalListTranslator.Run();
            _locationMallListTranslator.Run();
            _locationMarketListTranslator.Run();
            _locationRailwayStationListTranslator.Run();
            _locationSchoolListTranslator.Run();

        }
    }
}
