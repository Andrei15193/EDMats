using FluxBase;

namespace EDMats.ActionsData
{
    public class FilterMaterialsActionData
    {
        public FilterMaterialsActionData(string filterText)
        {
            FilterText = filterText;
        }

        public string FilterText { get; }
    }
}