using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using EDMats.Data.Engineering;

namespace EDMats.ViewModels
{
    public class ModulesBrowserViewModel : ViewModel
    {
        private readonly IEnumerable<Module> _allModules = Module.All;
        private readonly ObservableCollection<Module> _modules;
        private ModuleType _selectedModuleType;
        private string _searchText;
        private Module _selectedModule;

        public ModulesBrowserViewModel()
        {
            _modules = new ObservableCollection<Module>(_allModules);
            Modules = new ReadOnlyObservableCollection<Module>(_modules);
            _selectedModuleType = ModuleTypes[0];
            _searchText = string.Empty;
        }

        public IReadOnlyList<ModuleType> ModuleTypes { get; } = new[]
        {
            new ModuleType(null, "All", Array.Empty<Module>()),
            Module.CoreInternals,
            Module.Hardpoints,
            Module.OptionalInternals,
            Module.UtilityMounts
        };

        public ReadOnlyObservableCollection<Module> Modules { get; }

        public Module SelectedModule
        {
            get => _selectedModule;
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ModuleType SelectedModuleType
        {
            get => _selectedModuleType;
            set
            {
                if (_selectedModuleType != value)
                {
                    _selectedModuleType = value;
                    NotifyPropertyChanged();
                    _FilterModules();
                }
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (_searchText != value)
                {
                    _searchText = value;
                    NotifyPropertyChanged();
                    _FilterModules();
                }
            }
        }

        private void _FilterModules()
        {
            var filterPredicate = _GetFilterPredicate();
            _modules.Clear();
            foreach (var module in filterPredicate is null ? _allModules : _allModules.Where(filterPredicate))
                _modules.Add(module);
        }

        private Func<Module, bool> _GetFilterPredicate()
            => new Func<Module, bool>[]
            {
                _GetModuleTypeFilter(),
                _GetSearchTextFilter()
            }
            .Where(predicate => predicate is object)
            .DefaultIfEmpty(delegate { return true; })
            .Aggregate((result, predicate) => module => result(module) && predicate(module));

        private Func<Module, bool> _GetModuleTypeFilter()
        {
            if (SelectedModuleType == ModuleTypes[0])
                return default;
            else
                return module => module.Type == SelectedModuleType;
        }

        private Func<Module, bool> _GetSearchTextFilter()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
                return default;
            else
                return module => Regex.Split(SearchText, @"\W+").Any(
                    part => module.Name.IndexOf(part, StringComparison.OrdinalIgnoreCase) >= 0
                            || module.Id.IndexOf(part, StringComparison.OrdinalIgnoreCase) >= 0
                            || module.Type.Name.IndexOf(part, StringComparison.OrdinalIgnoreCase) >= 0
                );
        }
    }
}