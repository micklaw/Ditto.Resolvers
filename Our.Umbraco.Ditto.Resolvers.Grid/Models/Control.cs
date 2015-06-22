namespace Our.Umbraco.Ditto.Resolvers.Grid.Models
{
    public class Control
    {
        public object value { get; set; }

        public virtual object ConvertedValue { get; set; }

        public Editor editor { get; set; }
    }
}