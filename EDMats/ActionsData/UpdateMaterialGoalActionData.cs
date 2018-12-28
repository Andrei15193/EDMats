namespace EDMats.ActionsData
{
    public class UpdateMaterialGoalActionData : ActionData
    {
        public UpdateMaterialGoalActionData(string materialId, int amount)
        {
            MaterialId = materialId;
            Amount = amount;
        }

        public string MaterialId { get; }

        public int Amount { get; }
    }
}