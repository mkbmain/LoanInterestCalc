internal record PaymentSchedule(
    LoanRequestDto LoanRequestDto,
    decimal TotalInterestRate,
    PaymentScheduleRow[] PaymentScheduleRows);