using System.Linq;

namespace GraphToBitmap.Chart
{
    public class CMLXAsix
    {
        public string[] Values { get; set; }

        public CMLValuesType ValuesType { get; set; }

        public CMLXAsix()
        {
            ValuesType = CMLValuesType.String;
        }
        public CMLXAsix(string[] values, CMLValuesType valuesType)
        {
            Values = values;
            ValuesType = valuesType;
        }

        public CMLXAsix(double[] values, CMLValuesType valuesType)
        {
            Values = values.Select(x => x.ToString()).ToArray();
            ValuesType = valuesType;
        }
    }
}

namespace GraphToBitmap
{
    public enum CMLValuesType
    {
        Hourly,
        String
    }
}