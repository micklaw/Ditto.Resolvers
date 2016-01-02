using System.Collections.Generic;

namespace Our.Umbraco.Ditto.Resolvers.Models.Grid
{
    public class Row
    {
        public string name { get; set; }

        public Area[] areas { get; set; }

        public string id { get; set; }

        public Dictionary<string, string> styles { get; set; }

        public Dictionary<string, string> config { get; set; }
    }
}