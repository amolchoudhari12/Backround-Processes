using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BusinessEntities;
using Vilani.BusinessEntities.RealEstateEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class ProjectPhaseTranslator : DataAccessTranslate<realestate_projectphase_translate>
    {
        protected override void SelectRecords()
        {
            using (var action = new ProjectPhaseTranslatorActions())
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
            using (var action = new ProjectPhaseTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

       
        protected override realestate_projectphase_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            realestate_projectphase_translate translatedProjectPhase = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedProjectPhase = new realestate_projectphase_translate()
                {
                    PhaseName = translatedStrings[0],
                    PhaseDetails = translatedStrings[1],
                    PhaseDescription = translatedStrings[2],
                };
            }
            return translatedProjectPhase;
        }

        protected override List<string> ConvertToListOfString(realestate_projectphase_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.PhaseName);
            inputsForTranslator.Add(inputEntity.PhaseDetails);
            inputsForTranslator.Add(inputEntity.PhaseDescription);

            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(realestate_projectphase_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    realestate_projectphase_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.ProjectPhaseID = item.ProjectPhaseID;
                    }
                    translatedEntities.Add(translatedEntity);
                }
            }
        }
    }
}
