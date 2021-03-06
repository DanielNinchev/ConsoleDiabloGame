using ConsoleDiablo.App.Entities.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ConsoleDiablo.App.Core
{
    public class TypeCollector
    {
        public Type[] GetAllInheritingTypes<T>() where T : class
        {
            Type parentType = typeof(T);

            if (!parentType.IsAbstract && !parentType.IsInterface)
            {
                throw new ArgumentException($"Provided class {parentType.Name} is neither Abstract, not Interface!");
            }

            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => parentType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToArray();
        }
    }
}
