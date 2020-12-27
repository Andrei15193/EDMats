namespace EDMats.Data.Materials
{
    public class MaterialQuantity
    {
        public MaterialQuantity(Material material, int amount)
        {
            Material = material;
            Amount = amount;
        }

        public Material Material { get; }

        public int Amount { get; }
    }
}