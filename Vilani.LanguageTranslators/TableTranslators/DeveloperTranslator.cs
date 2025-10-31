using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Vilani.BusinessEntities;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class DeveloperTranslator : DataAccessTranslate<common_developerlist_translate>
    {

        protected override void SelectRecords()
        {
            using (var action = new DeveloperTranslatorActions())
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
            using (var action = new DeveloperTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }



        protected override common_developerlist_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            common_developerlist_translate translatedDeveloper = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 4) // 3 because input strings are 3
            {
                translatedDeveloper = new common_developerlist_translate()
                {
                    DeveloperName = translatedStrings[0],
                    CompanyName = translatedStrings[1],
                    Description = translatedStrings[2],
                    AboutUs = translatedStrings[3]

                };
            }
            return translatedDeveloper;
        }

        protected override List<string> ConvertToListOfString(common_developerlist_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.DeveloperName);
            inputsForTranslator.Add(inputEntity.CompanyName);
            inputsForTranslator.Add(inputEntity.Description);
            inputsForTranslator.Add(inputEntity.AboutUs);

            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(common_developerlist_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    common_developerlist_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.DeveloperID = item.DeveloperID;
                    }

                    translatedEntities.Add(translatedEntity);


                }
            }

        }


    }
}
