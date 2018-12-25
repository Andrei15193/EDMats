namespace EDMats.Services
{
    public class Material
    {
        internal Material(string id, string name, MaterialGrade grade, MaterialSubcategory subcategory)
        {
            Id = id;
            Name = name;
            Grade = grade;
            Subcategory = subcategory;
        }

        public string Id { get; }

        public string Name { get; }

        public MaterialGrade Grade { get; }

        public MaterialSubcategory Subcategory { get; }
    }
}