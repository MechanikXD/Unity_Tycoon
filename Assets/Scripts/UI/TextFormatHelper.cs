using System.Text;
using Core.Resource;
using UnityEngine;

namespace UI
{
    public static class TextFormatHelper
    {
        private readonly static string[] NumberSuffixes = { "", "K", "M", "B", "T" };
        
        public static string ResourceBundleToString(ResourceBundle bundle)
        {
            var stringBuilder = new StringBuilder();

            if (bundle.Gold > 0)
                stringBuilder.Append(bundle.Gold + ResourceManager.GoldTextIcon);
            if (bundle.Wood > 0)
                stringBuilder.Append(bundle.Wood + ResourceManager.WoodTextIcon);
            if (bundle.Stone > 0)
                stringBuilder.Append(bundle.Stone + ResourceManager.StoneTextIcon);
            if (bundle.Ore > 0)
                stringBuilder.Append(bundle.Ore + ResourceManager.OreTextIcon);
            if (bundle.People > 0)
                stringBuilder.Append(bundle.People + ResourceManager.PeopleTextIcon);

            return stringBuilder.ToString();
        }
        
        public static string NumberToCompactString(long number)
        {
            if (number < 1000) 
                return number.ToString();

             // Supports up to Trillions
            int magnitude = (int)Mathf.Log10(number) / 3;   // Determine suffix index
            double shortNumber = number / Mathf.Pow(1000, magnitude);

            string formatted;

            if (shortNumber >= 100)        // e.g. 111K
                formatted = shortNumber.ToString("0");
            else if (shortNumber >= 10)    // e.g. 17.8K -> 17K (keep <=4 chars)
                formatted = shortNumber.ToString("0.#");
            else                           // e.g. 1.23M
                formatted = shortNumber.ToString("0.##");

            return formatted + NumberSuffixes[magnitude];
        }
    }
}