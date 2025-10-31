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
    public class ListerTypeTranslator : DataAccessTranslate<realestate_listertype_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new ListerTypeTranslatorActions())
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
            using (var action = new ListerTypeTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

      
        protected override realestate_listertype_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            realestate_listertype_translate translatedListerType = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedListerType = new realestate_listertype_translate()
                {
                    ListerTypeName = translatedStrings[0],
                };
            }
            return translatedListerType;
        }

        protected override List<string> ConvertToListOfString(realestate_listertype_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.ListerTypeName);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(realestate_listertype_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();


                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    realestate_listertype_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.ListerTypeID = item.ListerTypeID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
