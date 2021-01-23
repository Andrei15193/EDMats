using System;

namespace EDMats.ViewModels
{
    public class BlueprintViewModel : ViewModel
    {
        private int _grade1repetitions;
        private int _grade2repetitions;
        private int _grade3repetitions;
        private int _grade4repetitions;
        private int _grade5repetitions;
        private int _experimentalEffectRepetition;
        private bool _isEditable;

        public BlueprintViewModel(string name, string experimentalEffect)
        {
            Name = name;
            ExperimentalEffect = experimentalEffect;
            EnterEditModeCommand = CreateCommand(() => IsEditable = true);
            SaveCommand = CreateCommand(() => IsEditable = false);
        }

        public string Name { get; }

        public string ExperimentalEffect { get; }

        public int Grade1Repetitions
        {
            get => _grade1repetitions;
            set
            {
                if (!_isEditable)
                    throw new InvalidOperationException("The view model is not editable.");

                if (_grade1repetitions != value)
                {
                    _grade1repetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Grade2Repetitions
        {
            get => _grade2repetitions;
            set
            {
                if (!_isEditable)
                    throw new InvalidOperationException("The view model is not editable.");

                if (_grade2repetitions != value)
                {
                    _grade2repetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Grade3Repetitions
        {
            get => _grade3repetitions;
            set
            {
                if (!_isEditable)
                    throw new InvalidOperationException("The view model is not editable.");

                if (_grade3repetitions != value)
                {
                    _grade3repetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Grade4Repetitions
        {
            get => _grade4repetitions;
            set
            {
                if (!_isEditable)
                    throw new InvalidOperationException("The view model is not editable.");

                if (_grade4repetitions != value)
                {
                    _grade4repetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int Grade5Repetitions
        {
            get => _grade5repetitions;
            set
            {
                if (!_isEditable)
                    throw new InvalidOperationException("The view model is not editable.");

                if (_grade5repetitions != value)
                {
                    _grade5repetitions = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public int ExperimentalEffectRepetitions
        {
            get => _experimentalEffectRepetition;
            set
            {
                if (!_isEditable)
                    throw new InvalidOperationException("The view model is not editable.");

                if (_experimentalEffectRepetition != value)
                {
                    _experimentalEffectRepetition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsEditable
        {
            get => _isEditable;
            private set
            {
                if (_isEditable != value)
                {
                    _isEditable = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Command EnterEditModeCommand { get; }

        public Command SaveCommand { get; }

        public Command CancelCommand { get; }

        public void CancelEditing()
        {
            IsEditable = false;
        }
    }
}