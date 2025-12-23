
public static class ValueFormat
{
    private static readonly string[] Units = { "", "K", "M", "B", "T", "Q" };

    public static string Format(long value)
    {
        if(value <= 0) return "0";

        int unitIndex = 0;
        double doubleValue = value;

        while (doubleValue >= 1000 && unitIndex < Units.Length - 1)
        {
            doubleValue /= 1000;
            unitIndex++;
        }

        string format = (unitIndex == 0) ? "N0" : "F1";

        return  doubleValue.ToString(format) + Units[unitIndex];
    }
}
