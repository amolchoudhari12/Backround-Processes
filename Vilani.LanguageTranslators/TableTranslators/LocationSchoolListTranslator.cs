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
    public class LocationSchoolListTranslator : DataAccessTranslate<location_schoollist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new LocationsSchoolListTranslatorActions())
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
            using (var action = new LocationsSchoolListTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

        protected override location_schoollist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            location_schoollist_translate translatedRailwayStationList = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedRailwayStationList = new location_schoollist_translate()
                {
                    SchoolName = translatedStrings[0],
                    AboutSchool= translatedStrings[1],
                };
            }
            return translatedRailwayStationList;
        }

        protected override List<string> ConvertToListOfString(location_schoollist_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.SchoolName);
            inputsForTranslator.Add(inputEntity.AboutSchool);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(location_schoollist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    location_schoollist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.SchoolID = item.SchoolID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
