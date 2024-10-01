namespace EXE201_BE_ThrivoHR.Application.Common.Method;

public static class SalaryMethod
{
    private static (decimal Threshold, decimal Rate, decimal Deduction)[] ParseTaxLevels(Dictionary<string, decimal> taxLevels)
    {
        var brackets = new List<(decimal, decimal, decimal)>();
        for (int i = 1; i <= 7; i++)
        {
            if (taxLevels.TryGetValue($"TaxabeLevel{i}Income", out var threshold) &&
                taxLevels.TryGetValue($"TaxLevel{i}", out var rate) &&
                taxLevels.TryGetValue($"TaxLevel{i}Minus", out var deduction))
            {
                brackets.Add((threshold, rate, deduction));
            }
        }
        return [.. brackets.OrderBy(b => b.Item1)];
    }
    public static decimal CalculatePersonalIncomeTax(decimal taxableIncome, Dictionary<string, decimal> TaxLevels)
    {
        var brackets = ParseTaxLevels(TaxLevels);
        for (int i = 0; i < brackets.Length; i++)
        {
            if (taxableIncome <= brackets[i].Threshold || i == brackets.Length - 1)
            {
                return Math.Max(brackets[i].Rate * taxableIncome - brackets[i].Deduction, 0);
            }
        }
        return 0;
    }
}
