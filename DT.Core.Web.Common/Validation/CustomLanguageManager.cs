using Abp.Localization;
using System.Globalization;

namespace DT.Core.Web.Common.Validation
{
    public class CustomLanguageManager : FluentValidation.Resources.LanguageManager
    {
        private readonly ILanguageManager _languageManager;
        public CustomLanguageManager(ILanguageManager languageManager)
        {
            _languageManager = languageManager;
            foreach (LanguageInfo languageInfo in _languageManager.GetLanguages())
            {
                CultureInfo cultureInfo = new CultureInfo(languageInfo.Name);

                AddTranslation(languageInfo.Name,
                    ValidatorNames.CreditCardValidator,
                    LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                    ValidatorNames.CreditCardValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                    ValidatorNames.EmailValidator,
                    LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                    ValidatorNames.EmailValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                    ValidatorNames.EmptyValidator,
                    LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                    ValidatorNames.EmptyValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                    ValidatorNames.EnumNameValidator,
                    LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                    ValidatorNames.EnumNameValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                    ValidatorNames.EnumValidator,
                    LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                    ValidatorNames.EnumValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.EqualValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.EqualValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.ExclusiveBetweenValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.ExclusiveBetweenValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.GreaterThanOrEqualValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.GreaterThanOrEqualValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.GreaterThanValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.GreaterThanValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.InclusiveBetweenValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.InclusiveBetweenValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.LengthValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.LengthValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.LessThanOrEqualValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.LessThanOrEqualValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.LessThanValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.LessThanValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.MaxLengthValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.MaxLengthValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.MinLengthValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.MinLengthValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.NotEmptyValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.NotEmptyValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.NotEqualValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.NotEqualValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.NotNullValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.NotNullValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                    ValidatorNames.NullValidator,
                    LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                    ValidatorNames.NullValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.PredicateValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.PredicateValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.RegularExpressionValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.RegularExpressionValidator, cultureInfo));

                AddTranslation(languageInfo.Name,
                   ValidatorNames.ScalePrecisionValidator,
                   LocalizationHelper.GetString(DTWebConsts.LocalizationSourceName,
                   ValidatorNames.ScalePrecisionValidator, cultureInfo));
            }
        }
    }
}
