using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vilani.LanguageTranslators.BingTranslatorService;


namespace Vilani.LanguageTranslators
{

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class VilaniTranalatorServiceWrapper : IVilaniTranslatorServiceWrapper
    {
        public VilaniTranalatorServiceWrapper()
        {

        }
        BingTranslatorService.LanguageServiceClient client = new BingTranslatorService.LanguageServiceClient();

        string bingKey = "88D6C9F6DD963FBCB7897598DA521C6B7D7596CE";

        public Dictionary<string, string> ConvertToEnglish(Dictionary<string, string> inputParams)
        {

            Dictionary<string, string> outputParams = new Dictionary<string, string>();

            List<string> translatedStringArray = ConvertToEnglishArray(inputParams.Values.ToList());

            int counter = 0;

            foreach (var item in inputParams)
            {
                outputParams.Add(item.Key, translatedStringArray[counter]);
                counter++;
            }

            if (inputParams.Count == counter)
                return outputParams;
            else
                return inputParams;
        }

        public Dictionary<string, string> ConvertToMarathi(Dictionary<string, string> inputParams)
        {

            Dictionary<string, string> outputParams = new Dictionary<string, string>();

            List<string> translatedStringArray = ConvertToMarathiArray(inputParams.Values.ToList());

            int counter = 0;

            foreach (var item in inputParams)
            {
                outputParams.Add(item.Key, translatedStringArray[counter]);
                counter++;
            }

            if (inputParams.Count == counter)
                return outputParams;
            else
                return inputParams;
        }



        public List<string> ConvertToEnglishArray(List<string> inputParams)
        {
            TranslateArrayResponse[] translated = client.TranslateArray(bingKey, inputParams.ToArray(), "hi", "en", new TranslateOptions() { ContentType = "text/html", Category = "general" });


            List<string> outPutString = new List<string>();

            foreach (var item in translated)
            {
                outPutString.Add(item.TranslatedText);
            }
            return outPutString;
        }


        public List<string> ConvertToMarathiArray(List<string> inputParams)
        {

            TranslateArrayResponse[] translated = client.TranslateArray(bingKey, inputParams.ToArray(), "en", "hi", new TranslateOptions() { ContentType = "text/html", Category = "general" });

            List<string> outPutString = new List<string>();

            foreach (var item in translated)
            {
                outPutString.Add(item.TranslatedText);
            }
            return outPutString;
        }

        public List<string> TranslateStringArray(List<string> inputParams, string sourceLanguage, string destinationLanaguge)
        {
            try
            {
                for (int i = 0; i < inputParams.Count; i++)
                {
                    if (inputParams[i] == null)
                        inputParams[i] = string.Empty;
                }

                TranslateArrayResponse[] translated = client.TranslateArray(bingKey, inputParams.ToArray(), sourceLanguage, destinationLanaguge, new TranslateOptions() { ContentType = "text/html", Category = "general" });

                List<string> outPutString = new List<string>();

                foreach (var item in translated)
                {
                    outPutString.Add(item.TranslatedText);
                }
                return outPutString;
            }
            catch (Exception)
            {
                return inputParams;
            }
          
        }
    }
}
