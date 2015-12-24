using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Our.Umbraco.Ditto.Resolvers.Archetype.Models.Abstract
{
    public interface IFieldset
    {
        string Alias { get; set; }

        bool Disabled { get; set; }
    }
}
