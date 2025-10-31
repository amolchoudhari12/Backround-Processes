using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vilani.BusinessEntities;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;


namespace Vilani.LanguageTranslators.RealEstate
{
    public class PropertyTranslator : DataAccessTranslate<realestate_property_translate>
    {

        protected override realestate_property_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            realestate_property_translate translatedProperty = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedProperty = new realestate_property_translate()
                {
                    PropertyName = translatedStrings[0],
                    PropertyTitle = translatedStrings[1],
                    PropertyDescription = translatedStrings[2]
                };
            }
            return translatedProperty;

        }


        protected override List<string> ConvertToListOfString(realestate_property_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.PropertyName);
            inputsForTranslator.Add(inputEntity.PropertyTitle);
            inputsForTranslator.Add(inputEntity.PropertyDescription);

            return inputsForTranslator;

        }

        protected override void SelectRecords()
        {
            using (var proertyAction = new PropertyTranslatorActions())
            {
                entitiesToTranslate = proertyAction.SelectNonTranslatedEntities();
            }
        }



        protected override void TranslateToOtherLanguages(realestate_property_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    realestate_property_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());


                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.PropertyID = item.PropertyID;
                    }
                    translatedEntities.Add(translatedEntity);


                }
            }

        }

        protected override void UpdateTranslate()
        {
            using (var proertyTranslatorAction = new PropertyTranslatorActions())
            {
                proertyTranslatorAction.UpdateTranslatedEntities(translatedEntities);
            }
        }

  

        protected override void StartTranslate()
        {
            foreach (var item in entitiesToTranslate)
            {
                TranslateToOtherLanguages(item, item.LanguageID);
            }

        }


    }
}
