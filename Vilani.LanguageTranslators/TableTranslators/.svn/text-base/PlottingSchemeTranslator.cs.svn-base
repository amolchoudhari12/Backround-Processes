using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BusinessEntities.PlottingEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;

namespace Vilani.LanguageTranslators
{
    public class PlottingSchemeTranslator : DataAccessTranslate<plot_plottingscheme_translate>
    {
        protected override void SelectRecords()
        {
            using (var action = new PlottingSchemeTranslatorActions())
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
            using (var action = new PlottingSchemeTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }

        protected override void EndTranslate()
        {
            throw new NotImplementedException();
        }


        protected override plot_plottingscheme_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            plot_plottingscheme_translate translatedPlottingScheme = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 3) // 3 because input strings are 3
            {
                translatedPlottingScheme = new plot_plottingscheme_translate()
                {
                    SchemeName = translatedStrings[0],
                    AboutScheme = translatedStrings[1],
                    Description = translatedStrings[2],
                };
            }
            return translatedPlottingScheme;
        }

        protected override List<string> ConvertToListOfString(plot_plottingscheme_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.SchemeName);
            inputsForTranslator.Add(inputEntity.AboutScheme);
            inputsForTranslator.Add(inputEntity.Description);

            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(plot_plottingscheme_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();
           
                    plot_plottingscheme_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());

                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.PlottingSchemeID = item.PlottingSchemeID;
                    }
                    translatedEntities.Add(translatedEntity);


                }
            }
        }
    }
}
