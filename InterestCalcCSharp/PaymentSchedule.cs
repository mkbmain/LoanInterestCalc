internal record PaymentSchedule(
    LoanRequestDto LoanRequestDto,
    PaymentScheduleRow[] PaymentScheduleRows)
{
    public decimal TotalInterestPaid => PaymentScheduleRows.Sum(w => w.InterestAmount);
};