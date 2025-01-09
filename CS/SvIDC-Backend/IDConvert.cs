namespace SvIDC_Backend;

public class IDConvert
{
    private static int IDType(String rawID)
    {
        throw new NotImplementedException();
    }

    public static int ToYear4Digit(int year) =>
        year switch
        {
            >= 1900 and <= 2100 => year,
            >= 0 and <= 99 => year < new DateTime().Year % 100 ? year + 1900 : year + 2000,
            _ => throw new ArgumentException("Invalid year.")
        };

    private static int ToYear2Digit(int year) => year % 100;
}