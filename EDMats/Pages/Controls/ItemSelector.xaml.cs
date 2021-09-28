using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EDMats.Pages.Controls
{
    public partial class ItemSelector : UserControl
    {
        public static readonly DependencyProperty FilterTextProperty = DependencyProperty.Register(
            nameof(FilterText),
            typeof(string),
            typeof(ItemSelector),
            new PropertyMetadata(string.Empty, _FilterOrItemsChanged)
        );
        public static readonly DependencyProperty FilterPropertyNamesProperty = DependencyProperty.Register(
            nameof(FilterPropertysNames),
            typeof(string),
            typeof(ItemSelector),
            new PropertyMetadata(string.Empty, _FilterOrItemsChanged)
        );
        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IEnumerable<object>),
            typeof(ItemSelector),
            new PropertyMetadata(Enumerable.Empty<object>(), _FilterOrItemsChanged)
        );
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem),
            typeof(object),
            typeof(ItemSelector)
        );
        public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register(
            nameof(ItemTemplate),
            typeof(DataTemplate),
            typeof(ItemSelector)
        );

        private static void _FilterOrItemsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            => ((ItemSelector)d)._ApplyFilter();

        private readonly ObservableCollection<object> _filteredItems;

        public ItemSelector()
        {
            _filteredItems = new ObservableCollection<object>();
            FilteredItems = new ReadOnlyObservableCollection<object>(_filteredItems);
            InitializeComponent();
        }

        public string FilterText
        {
            get => (string)GetValue(FilterTextProperty);
            set => SetValue(FilterTextProperty, value ?? string.Empty);
        }

        public string FilterPropertysNames
        {
            get => (string)GetValue(FilterPropertyNamesProperty);
            set => SetValue(FilterPropertyNamesProperty, value ?? string.Empty);
        }

        public IEnumerable<object> ItemsSource
        {
            get => (IEnumerable<object>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value ?? Enumerable.Empty<object>());
        }

        public object SelectedItem
        {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public ReadOnlyObservableCollection<object> FilteredItems { get; }

        private void _DeselectItem(object sender, RoutedEventArgs e)
        {
            SelectedItem = null;
            ((ToggleButton)sender).IsChecked = true;
        }

        private void _ApplyFilter()
        {
            var filterElements = Regex.Split(FilterText, "\\s").Where(filterItem => !string.IsNullOrWhiteSpace(filterItem)).ToArray();
            var filterPropertyNames = FilterPropertysNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
            var items = filterElements.Length > 0 ? ItemsSource.Where(item =>
            {
                var itemType = item.GetType();
                return filterPropertyNames
                    .Select(filterPropertyName => Convert.ToString(itemType.GetProperty(filterPropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty).GetValue(item)))
                    .Any(propertyValue => filterElements.Any(filterElement => propertyValue.Contains(filterElement, StringComparison.OrdinalIgnoreCase)));
            }) : ItemsSource;

            var itemIndex = 0;
            foreach (var item in items)
            {
                var endIndex = _filteredItems.IndexOf(item);
                if (endIndex == -1)
                    _filteredItems.Insert(itemIndex, item);
                else
                    while (itemIndex < endIndex)
                    {
                        _filteredItems.RemoveAt(itemIndex);
                        endIndex--;
                    }
                itemIndex++;
            }

            while (itemIndex < _filteredItems.Count)
                _filteredItems.RemoveAt(itemIndex);
        }
    }
}