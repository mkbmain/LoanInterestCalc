internal record PaymentSchedule(
    LoanRequestDto LoanRequestDto,
    PaymentScheduleRow[] PaymentScheduleRows)
{
    public decimal TotalInterestRate => PaymentScheduleRows.Sum(w => w.InterestAmount);
};