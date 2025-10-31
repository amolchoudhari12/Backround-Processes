using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vilani.BusinessEntities;
using Vilani.BusinessEntities.PlottingEntities;
using Vilani.BusinessActions.LanguageTranslatorActions;

namespace Vilani.LanguageTranslators.RealEstate
{
    public class PlottingAddressTranslator : DataAccessTranslate<plot_plottingaddress_translate>
    {
        protected override void SelectRecords()
        {
            using (var action = new PlottingAddressTranslatorActions())
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
            using (var action = new PlottingAddressTranslatorActions())
            {
                action.UpdateTranslatedEntities(translatedEntities);
            }
        }


        protected override plot_plottingaddress_translate ConvertListOfStringToEntity(List<string> translatedStrings)
        {
            plot_plottingaddress_translate translatedPlottingAddress = null;

            // This will validate if the output came from Lanaguge translated service has not missed any input paramater
            if (translatedStrings.Count == 4) // 3 because input strings are 3
            {
                translatedPlottingAddress = new plot_plottingaddress_translate()
                {
                    LocationName = translatedStrings[0],
                    StreetName = translatedStrings[1],
                    NearestLandmark = translatedStrings[2],
                    AddressLine = translatedStrings[3],
                };
            }
            return translatedPlottingAddress;
        }

        protected override List<string> ConvertToListOfString(plot_plottingaddress_translate inputEntity)
        {
            inputsForTranslator.Clear();

            inputsForTranslator.Add(inputEntity.LocationName);
            inputsForTranslator.Add(inputEntity.StreetName);
            inputsForTranslator.Add(inputEntity.NearestLandmark);
            inputsForTranslator.Add(inputEntity.AddressLine);

            return inputsForTranslator;
        }

        protected override void TranslateToOtherLanguages(plot_plottingaddress_translate item, int sourceLanaguge)
        {
            foreach (var languageToTranslate in languagesSupported)
            {
                if (languageToTranslate != sourceLanaguge)
                {
                    VilaniTranalatorServiceWrapper vilaniTranalatorServiceWrapper = new VilaniTranalatorServiceWrapper();

                    string[] translatedStrings = vilaniTranalatorServiceWrapper.TranslateStringArray(ConvertToListOfString(item), LanguageMapper.ToLanguageStringCode(sourceLanaguge), LanguageMapper.ToLanguageStringCode(languageToTranslate)).ToArray();

                    plot_plottingaddress_translate translatedEntity = ConvertListOfStringToEntity(translatedStrings.ToList());


                    if (translatedEntity != null)
                    {
                        translatedEntity.LanguageID = languageToTranslate;
                        translatedEntity.PlottingAddressID = item.PlottingAddressID;
                    }
                    translatedEntities.Add(translatedEntity);


                }
            }
        }
    }
}
