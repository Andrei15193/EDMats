using System;
using System.Linq;

namespace EDMats
{
    public class ReflectionServiceRegistry : ServiceRegistry
    {
        public ReflectionServiceRegistry()
        {
            var assembly = GetType().Assembly;
            foreach (var @class in assembly.DefinedTypes.Where(type => type.IsClass))
            {
                Add(@class, @class);
                foreach (var @interface in @class.GetInterfaces())
                    if (@interface.Assembly == assembly || string.Equals($"I{@class.Name}", @interface.Name, StringComparison.OrdinalIgnoreCase))
                        Add(@interface, @class);
            }
        }
    }
}