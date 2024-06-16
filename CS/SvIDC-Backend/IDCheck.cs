using System.Text.RegularExpressions;

namespace SvIDC_Backend;

public class IDCheck
{
    public static bool YearCheck(int year) => 1900 <= year && year <= 2100;

    public static bool MonthCheck(int month) => 1 <= month && month <= 12;


    private static bool IsLeapYear(int year) => year % 4 == 0 && year % 100 == 0 && year % 400 != 0;

    public static bool DayCheck(int year, int month, int day) =>
        month switch
        {
            2 when IsLeapYear(year) => day <= 29,
            2 => day <= 28,
            1 or 3 or 5 or 7 or 8 or 10 or 12 => day <= 31,
            4 or 6 or 9 or 11 => day <= 30,
            _ => false
        };

    private static int ToYear4Digit(int year) =>
        year switch
        {
            >= 1900 and <= 2100 => year,
            >= 0 and <= 99 => year < new DateTime().Year % 100 ? year + 1900 : year + 2000,
            _ => throw new ArgumentException("Invalid year.")
        };

    private static int ToYear2Digit(int year) => year % 100;

    // TODO: Convert ID into array, then split: yy, mm, dd, xxxx
    /// <summary>
    /// Return ID in the form of yymmddxxxx
    /// </summary>
    /// <param name="ID">
    /// ID can be input in string in the form of
    /// * yyyymmdd-xxxx
    /// * yymmdd-xxxx
    /// * yyyymmddxxxx
    /// * yymmddxxxx
    /// </param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    private static string FixID (string ID) 
    {
        string leID = Regex.Replace(ID, @"\s", "");
        if (!leID.Contains('-'))
            return leID.Length switch
            {
                12 => leID[2..],
                10 => leID,
                _ => throw new ArgumentException("Illegal argument")
            };
        string[] leString = leID.Split("-");
        return string.Join("", leString);
    }
    
    
    // TODO: Luhn algorithm
    public static bool LuhnAlgorithm(string ID)
    {
        int[] myInts = Array.ConvertAll(ID.Split(), int.Parse);
        int sum = 0;
        
        return true;
    }

    private static bool IsValidYear(string cleanedID) => int.Parse(cleanedID[..2]) is >= 0 and <= 99;

    private static bool IsValidMonth(string cleanedID)
    {
        throw new NotImplementedException();
    }
}