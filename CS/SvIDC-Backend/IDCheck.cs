using System.Text.RegularExpressions;

namespace SvIDC_Backend;

public class IDCheck
{
    private static bool YearCheck(int year) => year is >= 1900 and <= 2100;

    private static bool MonthCheck(int month) => month is >= 1 and <= 12;
    
    private static bool IsLeapYear(int year) => year % 4 == 0 && year % 100 == 0 && year % 400 != 0;

    private static bool DayCheck(int year, int month, int day) =>
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
        string leID = Regex.Replace(ID, @"\s", ""); // Remove all whitespaces, copied from somewhere
        if (!leID.Contains('-'))
            return leID.Length switch
            {
                12 when YearCheck(int.Parse(leID[..4])) => leID[2..], // Check if year has 4 digit, is it between 1900 and 2100
                10 => leID,
                _ => throw new ArgumentException("ID is not in the correct format or year is not between 1900 or 2100")
            };
        string[] leString = leID.Split("-");
        return string.Join("", leString);
    }
    
    
    // TODO: Luhn algorithm
    public static int LuhnCheckSumGenerator(string inputString)
    {
        int[] digits = Array.ConvertAll(inputString[..^1].Split(), int.Parse);
        int checkDigit = 0;
        
        for (int i = digits.Length - 2; i >= 0; --i)
            checkDigit += ((i & 1) is 0) switch
            {
                true  => digits[i] > 4 ? digits[i] * 2 - 9 : digits[i] * 2,
                false => digits[i]
            };
        return (10 - checkDigit % 10) % 10;
    }

    public static bool IsValidCheckDigit(string cleanedID) => int.Parse(cleanedID[^1].ToString()) == LuhnCheckSumGenerator(cleanedID[..^1]);

    private static bool IsValidYear(string cleanedID) => YearCheck(ToYear4Digit(int.Parse(cleanedID[..2])));
    private static bool IsValidMonth(string cleanedID) => MonthCheck(int.Parse(cleanedID[2..4]));
    private static bool IsValidDate(string cleanedID) => DayCheck(ToYear4Digit(int.Parse(cleanedID[..2])), int.Parse(cleanedID[2..4]), int.Parse(cleanedID[4..6]));
}