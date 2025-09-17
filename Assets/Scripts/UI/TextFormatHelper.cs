using System.Text;
using Core.Resource;

namespace UI
{
    public static class TextFormatHelper
    {
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
    }
}