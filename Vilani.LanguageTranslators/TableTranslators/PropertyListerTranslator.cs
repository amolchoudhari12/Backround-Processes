using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BusinessActions.LanguageTranslatorActions;
using Vilani.BusinessEntities.RealEstateEntities;


namespace Vilani.LanguageTranslators.RealEstate
{
    public class PropertyListerTranslator : DataAccessTranslate<realestate_propertylister_translate>
    {
        protected override void SelectRecords()
        {
            using (var action = new PropertyListerTranlatorActions())
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
            using (var action = new PropertyListerTranlatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

     


        protected override realestate_propertylister_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            realestate_propertylister_translate translatedPropertyLister = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 2) // 3 because input strings are 3
            {
                translatedPropertyLister = new realestate_propertylister_translate()
                {
                    ListerName = translatedStrings[0],
                    ListerCompany = translatedStrings[1],
                };
            }
            return translatedPropertyLister;
        }

        protected override List<string> ConvertToListOfString(realestate_propertylister_translate inputEntity)
        {
            inputsForTranslator.Clear();
            inputsForTranslator.Add(inputEntity.ListerName);
            inputsForTranslator.Add(inputEntity.ListerCompany);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(realestate_propertylister_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    realestate_propertylister_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.PropertyListerID = item.PropertyListerID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
