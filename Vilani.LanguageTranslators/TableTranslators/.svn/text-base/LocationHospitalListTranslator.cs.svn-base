using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BusinessEntities.PlottingEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.RealEstateContext;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class LocationHospitalListTranslator : DataAccessTranslate<location_hospitallist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new LocationHospitalListTranslatorActions())
            {
                entitiesToTranslate = action.SelectNonTranslatedEntities();
            }
        }

        protected override void StartTranslate()
        {
            foreach (var item in entitiesToTranslate)
            {
                TranslateToOtherLanguages(item, (int)item.LanguageID);
            }
        }

        protected override void UpdateTranslate()
        {
            using (var action = new LocationHospitalListTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

  
        protected override location_hospitallist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            location_hospitallist_translate translatedAirportList = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedAirportList = new location_hospitallist_translate()
                {
                    HospitalName = translatedStrings[0],
                    AboutHospital = translatedStrings[1],
                };
            }
            return translatedAirportList;
        }

        protected override List<string> ConvertToListOfString(location_hospitallist_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.HospitalName);
            inputsForTranslator.Add(inputEntity.AboutHospital);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(location_hospitallist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    location_hospitallist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.HospitalID = item.HospitalID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
