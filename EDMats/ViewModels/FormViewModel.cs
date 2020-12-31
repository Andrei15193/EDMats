using System.Collections.Generic;
using System.ComponentModel;

namespace EDMats.ViewModels
{
    public class FormViewModel : ViewModel
    {
        protected FormViewModel()
            : base()
        {
        }

        protected FormViewModel(IEnumerable<INotifyPropertyChanged> chainedObservables)
            : base(chainedObservables)
        {
        }

        protected FormViewModel(params INotifyPropertyChanged[] chainedObservables)
            : base(chainedObservables)
        {
        }

        protected void WatchFields(params IFormFieldViewModel[] fields)
        {
            foreach (var field in fields)
                if (field.GetType().IsConstructedGenericType && typeof(FormFieldViewModel<>) == field.GetType().GetGenericTypeDefinition())
                    field.PropertyChanged += FieldPropertyChanged;
        }

        protected virtual void FieldPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (sender is IFormFieldViewModel field)
            {
                FieldChanged(field);
                if (string.Equals(e.PropertyName, nameof(IFormFieldViewModel.Value), System.StringComparison.OrdinalIgnoreCase))
                    FieldValueChanged(field);
            }
        }

        protected virtual void FieldChanged(IFormFieldViewModel field)
        {
        }

        protected virtual void FieldValueChanged(IFormFieldViewModel field)
        {
        }
    }

    public interface IFormFieldViewModel : INotifyPropertyChanged
    {
        object Value { get; set; }
    }

    public class FormFieldViewModel<TValue> : ViewModel, IFormFieldViewModel
    {
        private TValue _value;

        public FormFieldViewModel()
        {
        }

        public TValue Value
        {
            get => _value;
            set
            {
                if (!Equals(_value, value))
                {
                    _value = value;
                    NotifyPropertyChanged(nameof(Value));
                }
            }
        }

        object IFormFieldViewModel.Value
        {
            get => Value;
            set => Value = (TValue)value;
        }
    }
}