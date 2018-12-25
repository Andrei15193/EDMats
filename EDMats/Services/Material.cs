namespace EDMats.Services
{
    public class Material
    {
        internal Material(string id, string name, MaterialGrade grade, MaterialCategory category)
        {
            Id = id;
            Name = name;
            Grade = grade;
            Category = category;
        }

        public string Id { get; }

        public string Name { get; }

        public MaterialGrade Grade { get; }

        public MaterialCategory Category { get; }

        public MaterialType Type
            => Category.Type;
    }
}