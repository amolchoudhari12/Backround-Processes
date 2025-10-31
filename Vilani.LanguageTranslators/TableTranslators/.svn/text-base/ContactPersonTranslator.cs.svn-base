using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Vilani.BusinessEntities;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class ContactPersonTranslator : DataAccessTranslate<common_propertycontactperson_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new  PropertyContactPersonTranslatorActions())
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
            using (var action = new PropertyContactPersonTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }



        protected override common_propertycontactperson_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            common_propertycontactperson_translate translatedContactPerson = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedContactPerson = new common_propertycontactperson_translate()
                {
                    ContactPersonName = translatedStrings[0],
                    FullName = translatedStrings[1],
                    Address = translatedStrings[2]              
                    
                };
            }
            return translatedContactPerson;
        }

        protected override List<string> ConvertToListOfString(common_propertycontactperson_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.ContactPersonName);
            inputsForTranslator.Add(inputEntity.FullName);
            inputsForTranslator.Add(inputEntity.Address);
     
            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(common_propertycontactperson_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();


                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    common_propertycontactperson_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());


                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.ContactPersonID = item.ContactPersonID;
                    }
                    translatedEntities.Add(translatedEntity);


                }
            }

        }
    }
}
