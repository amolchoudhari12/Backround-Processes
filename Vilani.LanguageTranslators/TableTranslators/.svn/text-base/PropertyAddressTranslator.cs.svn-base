using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class PropertyAddressTranslator : DataAccessTranslate<realestate_propertyaddress_translate>
    {
        protected override void SelectRecords()
        {
            using (var action = new PropertyAddressTranslatorActions())
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
            using (var action = new PropertyAddressTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }




        protected override realestate_propertyaddress_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            realestate_propertyaddress_translate translatedPropertyAddress = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedPropertyAddress = new realestate_propertyaddress_translate()
                {
                    LocationName = translatedStrings[0],
                    StreetName = translatedStrings[1],
                    NearestLandmark = translatedStrings[2],
                };
            }
            return translatedPropertyAddress;
        }

        protected override List<string> ConvertToListOfString(realestate_propertyaddress_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.LocationName);
            inputsForTranslator.Add(inputEntity.StreetName);
            inputsForTranslator.Add(inputEntity.NearestLandmark);
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(realestate_propertyaddress_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();
                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    realestate_propertyaddress_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.PropertyAddressID = item.PropertyAddressID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
