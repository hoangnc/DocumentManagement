namespace Abp.Localization
{
    public class LocalizationSettingProvider
    {

        private static LocalizableString L(string name)
        {
            return new LocalizableString(name, "LocalizationSourceName");
        }
    }
}