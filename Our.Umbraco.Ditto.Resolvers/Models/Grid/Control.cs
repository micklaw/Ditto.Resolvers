namespace Our.Umbraco.Ditto.Resolvers.Models.Grid
{
    public class Control
    {
        public object value { get; set; }

        public virtual object ConvertedValue { get; set; }

        public Editor editor { get; set; }
    }
}