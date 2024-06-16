namespace SvIDC_Backend;

public class IDCheck
{
    public static Boolean YearCheck(int year) => 1900 <= year && year <= 2100;

    public static Boolean MonthCheck(int month) => 1 <= month && month <= 12;

    public static Boolean DayCheck(int year, int month, int day)
    {
        // TODO: Check for leap year
        
        // TODO: Check for month
        
        // TODO: check for correct date
        return true;
    }

    private static int ToYear4Digit(int year)
    {
        int currentYear = new DateTime().Year % 100;
        switch (year)
        {
            case >= 1900 and <= 2100:
                return year;
            case >= 0 and <= 99:
            {
                return year < currentYear ? year + 1900 : year + 2000;
            }
            default:
                throw new ArgumentException("Invalid year.");
        }
    }
    
    // TODO: Convert ID into array, then split: yy, mm, dd, xxx, c
    // TODO: Luhn algorithm

    public static Boolean LuhnAlgorithm(String ID)
    {
        int[] myInts = Array.ConvertAll(ID.Split(), int.Parse);
        int sum = 0;
        
        return true;
    }
}