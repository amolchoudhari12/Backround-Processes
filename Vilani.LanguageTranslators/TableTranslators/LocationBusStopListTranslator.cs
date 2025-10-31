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
    public class LocationBusStopListTranslator : DataAccessTranslate<location_busstoplist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new LocationBusStopListTranslatorActions())
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
            using (var action = new LocationBusStopListTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

        protected override location_busstoplist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            location_busstoplist_translate translatedAirportList = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedAirportList = new location_busstoplist_translate()
                {
                    BusStopName = translatedStrings[0],
                    AboutBusStop = translatedStrings[1],
                };
            }
            return translatedAirportList;
        }

        protected override List<string> ConvertToListOfString(location_busstoplist_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.BusStopName);
            inputsForTranslator.Add(inputEntity.AboutBusStop);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(location_busstoplist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    location_busstoplist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.BusStopID = item.BusStopID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
