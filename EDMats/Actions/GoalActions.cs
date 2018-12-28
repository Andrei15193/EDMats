using EDMats.ActionsData;

namespace EDMats.Actions
{
    public class GoalActions : ActionSet
    {
        public void UpdateMaterialAmountGoal(string materialId, int amountGoal)
            => Dispatch(new UpdateMaterialGoalActionData(materialId, amountGoal));
    }
}