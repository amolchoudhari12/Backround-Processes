using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Vilani.BusinessEntities;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;


namespace Vilani.LanguageTranslators.RealEstate
{
    public class ProjectTranslator : DataAccessTranslate<realestate_project_translate>
    {


        protected override realestate_project_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            realestate_project_translate translatedProject = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 4) // 3 because input strings are 3
            {
                translatedProject = new realestate_project_translate()
                {
                    ProjectName = translatedStrings[0],
                    ProjectTitle = translatedStrings[1],
                    PunchLine = translatedStrings[2],
                    ProjectDescription = translatedStrings[3]
                    
                };
            }
            return translatedProject;
        }

        protected override List<string> ConvertToListOfString(realestate_project_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.ProjectName);
            inputsForTranslator.Add(inputEntity.ProjectTitle);
            inputsForTranslator.Add(inputEntity.PunchLine);
            inputsForTranslator.Add(inputEntity.ProjectDescription);

            return inputsForTranslator;
        }

        protected override void SelectRecords()
        {
            using (var action = new ProjectTranslatorActions())
            {
                entitiesToTranslate = action.SelectNonTranslatedEntities();
            }
        }

        protected override void TranslateToOtherLanguages(realestate_project_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    realestate_project_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());


                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.ProjectID = item.ProjectID;
                    }
                    translatedEntities.Add(translatedEntity);


                }
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
            using (var action = new ProjectTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

     

       
    }
}
