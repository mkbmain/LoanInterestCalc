internal record PaymentScheduleRow(
    int PaymentNumber,
    decimal PrincipalAmount,
    decimal InterestAmount,
    decimal PaymentAmount,
    decimal RemainingBalance);