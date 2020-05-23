using System.Linq;

namespace CMLSmartHomeController.Chart
{
    public class CMLChartXAsix
    {
        public string[] Values { get; set; }

        public CMLValuesType ValuesType { get; set; }

        public CMLChartXAsix()
        {
            ValuesType = CMLValuesType.String;
        }
        public CMLChartXAsix(string[] values, CMLValuesType valuesType)
        {
            Values = values;
            ValuesType = valuesType;
        }

        public CMLChartXAsix(double[] values, CMLValuesType valuesType)
        {
            Values = values.Select(x => x.ToString()).ToArray();
            ValuesType = valuesType;
        }
    }
    public enum CMLValuesType
    {
        Hourly,
        String
    }

}
