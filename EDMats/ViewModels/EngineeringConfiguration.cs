using EDMats.Data.Engineering;

namespace EDMats.ViewModels
{
    public class EngineeringConfiguration
    {
        public EngineeringConfiguration(Module module, Blueprint blueprint, ExperimentalEffect experimentalEffect)
            => (Module, Blueprint, ExperimentalEffect) = (module, blueprint, experimentalEffect);

        public Module Module { get; }

        public Blueprint Blueprint { get; }

        public ExperimentalEffect ExperimentalEffect { get; }
    }
}