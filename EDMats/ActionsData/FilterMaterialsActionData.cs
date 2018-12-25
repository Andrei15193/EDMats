namespace EDMats.ActionsData
{
    public class FilterMaterialsActionData : ActionData
    {
        public FilterMaterialsActionData(string filterText)
        {
            FilterText = filterText;
        }

        public string FilterText { get; }
    }
}