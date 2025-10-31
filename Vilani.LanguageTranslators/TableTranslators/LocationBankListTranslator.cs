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
    public class LocationBankListTranslator : DataAccessTranslate<location_banklist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new LocationBankListTranslatorActions())
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
            using (var action = new LocationBankListTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

     
        protected override location_banklist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            location_banklist_translate translatedAirportList = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedAirportList = new location_banklist_translate()
                {
                    BankName = translatedStrings[0],
                    AboutBank = translatedStrings[1],
                };
            }
            return translatedAirportList;
        }

        protected override List<string> ConvertToListOfString(location_banklist_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.BankName);
            inputsForTranslator.Add(inputEntity.AboutBank);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(location_banklist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    location_banklist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.BankID = item.BankID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
