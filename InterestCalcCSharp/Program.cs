const int amount = 200_000;
const double interestRate = 4.15;

Console.WriteLine(
    $"Years,PaymentAmount,PrincipalAmount,InterestAmount,RemainingBalance,YearTotal, 3 year total,5 year total, 10 year total");
for (int year = 5; year <= 30; year += 5)
{
    var paymentSchedule = CalculateAmortizationSchedule(new LoanRequestDto(year * 12, amount, interestRate));
    var totalPaid3Year = paymentSchedule.PaymentScheduleRows.Take(3 * 12).Sum(w => w.PrincipalAmount);
    var totalPaidFiveYear = paymentSchedule.PaymentScheduleRows.Take(5 * 12).Sum(w => w.PrincipalAmount);
    var totalPaid10Year = paymentSchedule.PaymentScheduleRows.Take(10 * 12).Sum(w => w.PrincipalAmount);
    Console.WriteLine(
        ($"{year} year, ").PadRight(10) +
        $"Payment amount:{paymentSchedule.PaymentScheduleRows.First().PaymentAmount:C} " +
        $"Total Paid:{paymentSchedule.PaymentScheduleRows.Sum(w => w.PaymentAmount):C}, " +
        $"Total principal at 3 years:{totalPaid3Year:C}, Total at 5 years {totalPaidFiveYear}," +
        $"Total at 10 years {totalPaid10Year:C}");
}


static PaymentSchedule CalculateAmortizationSchedule(LoanRequestDto loan)
{
    if (loan is null) throw new NullReferenceException($"Parameter {nameof(loan)} cannot be NULL");

    if (loan.LoanAmount <= 0)
        throw new NullReferenceException($"Invalid loan amount! The loan amount cannot be less than or equal to 0");

    var amortizationSchedule = new List<PaymentScheduleRow>();
    var monthlyInterestRate = (decimal)(loan.InterestRate / 12.0 / 100.0);
    var monthlyPayment =
        Math.Round(CalculateMonthlyPayment(loan.LoanAmount, monthlyInterestRate, loan.NumberOfPayments), 2);

    var remainingBalance = loan.LoanAmount;

    for (var number = 1; number <= loan.NumberOfPayments; number++)
    {
        var interestPayment = Math.Round(remainingBalance * monthlyInterestRate, 2);
        var principalPayment = monthlyPayment - interestPayment;

        remainingBalance -= principalPayment;
        if (remainingBalance < principalPayment)
        {
            principalPayment += remainingBalance;
            monthlyPayment += remainingBalance;
            remainingBalance = 0;
        }

        amortizationSchedule.Add(new PaymentScheduleRow(number, principalPayment, interestPayment, monthlyPayment,
            remainingBalance));
    }

    return new PaymentSchedule(loan, amortizationSchedule.Sum(w => w.InterestAmount),
        amortizationSchedule.ToArray());
}

static decimal CalculateMonthlyPayment(decimal loanAmount, decimal monthlyInterestRate, int numberOfPayments)
{
    if (monthlyInterestRate <= 0)
        return loanAmount / numberOfPayments;

    var power = (decimal)Math.Pow(1 + (double)monthlyInterestRate, numberOfPayments);
    return loanAmount * (monthlyInterestRate * power) / (power - 1);
}