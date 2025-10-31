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
    public class LocationRailwayStationListTranslator : DataAccessTranslate<location_railwaystationlist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new LocationRailwayStationListTranslatorActions())
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
            using (var action = new LocationRailwayStationListTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

        protected override location_railwaystationlist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            location_railwaystationlist_translate translatedRailwayStationList = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedRailwayStationList = new location_railwaystationlist_translate()
                {
                    RailwayStationName = translatedStrings[0],
                    AboutRailwayStation = translatedStrings[1],
                };
            }
            return translatedRailwayStationList;
        }

        protected override List<string> ConvertToListOfString(location_railwaystationlist_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.RailwayStationName);
            inputsForTranslator.Add(inputEntity.AboutRailwayStation);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(location_railwaystationlist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    location_railwaystationlist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.RailwayStationID = item.RailwayStationID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
