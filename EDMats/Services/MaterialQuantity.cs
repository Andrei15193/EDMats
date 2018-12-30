namespace EDMats.Services
{
    public class MaterialQuantity
    {
        public MaterialQuantity()
        {
        }

        public MaterialQuantity(Material material, int amount)
        {
            Material = material;
            Amount = amount;
        }

        public Material Material { get; set; }

        public int Amount { get; set; }
    }
}