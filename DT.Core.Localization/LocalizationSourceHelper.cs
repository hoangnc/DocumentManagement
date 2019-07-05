using System;
using System.Globalization;
using DT.Core.Localization;

namespace Abp.Localization
{
    public static class LocalizationSourceHelper
    {
        public static string ReturnGivenNameOrThrowException(
            ILocalizationConfiguration configuration,
            string sourceName, 
            string name, 
            CultureInfo culture)
        {
            var exceptionMessage = $"Can not find '{name}' in localization source '{sourceName}'!";

            if (!configuration.ReturnGivenTextIfNotFound)
            {
                throw new Exception(exceptionMessage);
            }

            return configuration.WrapGivenTextIfNotFound
                ? $"[notFoundText]"
                : "notFoundText";
        }
    }
}
