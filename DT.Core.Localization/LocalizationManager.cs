using Abp.Localization.Dictionaries;
using Abp.Localization.Sources;
using DT.Core.Localization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Web.Mvc;

namespace Abp.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        private readonly ILanguageManager _languageManager;
        private readonly ILocalizationConfiguration _configuration;
        private readonly IDependencyResolver _iocResolver;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationManager(
            ILanguageManager languageManager,
            ILocalizationConfiguration configuration,
            IDependencyResolver iocResolver)
        {
             _languageManager = languageManager;
            _configuration = configuration;
            _iocResolver = iocResolver;
            _sources = new Dictionary<string, ILocalizationSource>();
        }

        public void Initialize()
        {
            InitializeSources();
        }

        private void InitializeSources()
        {
            if (!_configuration.IsEnabled)
            {
                return;
            }

            foreach (ILocalizationSource source in _configuration.Sources)
            {
                if (_sources.ContainsKey(source.Name))
                {
                    throw new Exception("There are more than one source with name: " + source.Name + "! Source name must be unique!");
                }

                _sources[source.Name] = source;
                source.Initialize(_configuration, _iocResolver);

                //Extending dictionaries
                if (source is IDictionaryBasedLocalizationSource)
                {
                    IDictionaryBasedLocalizationSource dictionaryBasedSource = source as IDictionaryBasedLocalizationSource;
                    List<LocalizationSourceExtensionInfo> extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
                    foreach (LocalizationSourceExtensionInfo extension in extensions)
                    {
                        extension.DictionaryProvider.Initialize(source.Name);
                        foreach (ILocalizationDictionary extensionDictionary in extension.DictionaryProvider.Dictionaries.Values)
                        {
                            dictionaryBasedSource.Extend(extensionDictionary);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets a localization source with name.
        /// </summary>
        /// <param name="name">Unique name of the localization source</param>
        /// <returns>The localization source</returns>
        public ILocalizationSource GetSource(string name)
        {
            if (!_configuration.IsEnabled)
            {
                return NullLocalizationSource.Instance;
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            ILocalizationSource source;
            if (!_sources.TryGetValue(name, out source))
            {
                throw new Exception("Can not find a source with name: " + name);
            }

            return source;
        }

        /// <summary>
        /// Gets all registered localization sources.
        /// </summary>
        /// <returns>List of sources</returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }
    }
}