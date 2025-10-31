using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Vilani.LanguageTranslators
{
    public abstract class DataAccessTranslate<T>
    {
        //Default 2 languages are supported
        protected List<int> languagesSupported = new List<int>() { 1, 2 };

        protected List<T> entitiesToTranslate = new List<T>();
        protected List<T> translatedEntities = new List<T>();

        protected abstract void SelectRecords();
        protected abstract void StartTranslate();
        protected abstract void UpdateTranslate();
        
        protected virtual void EndTranslate()
        {
        }


        protected List<string> inputsForTranslator = new List<string>();


        protected abstract T ConvertListOfStringToEntity(List<string> translatedStrings);
        protected abstract List<string> ConvertToListOfString(T inputEntity);

        protected abstract void TranslateToOtherLanguages(T item, int sourceLanaguge);



        // The 'Template Method'
        public void Run()
        {
            SelectRecords();
            StartTranslate();
            UpdateTranslate();
            // EndTranslate(); // Current Not needed as updating IDTranslated flag will be done in above function ie UpdateTranslate()
        }
    }
}
