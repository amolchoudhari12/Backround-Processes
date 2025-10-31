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
    public class LocationAtmListTranslator : DataAccessTranslate<location_atmlist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new LocationAtmListTranslatorActions())
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
            using (var action = new LocationAtmListTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

        protected override location_atmlist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            location_atmlist_translate translatedAirportList = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedAirportList = new location_atmlist_translate()
                {
                    ATMName = translatedStrings[0],
                    AboutATM= translatedStrings[1],
                };
            }
            return translatedAirportList;
        }

        protected override List<string> ConvertToListOfString(location_atmlist_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.ATMName);
            inputsForTranslator.Add(inputEntity.AboutATM);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(location_atmlist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    location_atmlist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.ATMID = item.ATMID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
