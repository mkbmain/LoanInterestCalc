Class PaymentSchedule
    Sub new(loanRequestDto as LoanRequestDto,
            totalInterestPaid as decimal,
            paymentScheduleRows as PaymentScheduleRow())

        Me.LoanRequestDto = loanRequestDto
        Me.PaymentScheduleRows = paymentScheduleRows
        Me.TotalInterestPaid = totalInterestPaid
    End Sub

    Public ReadOnly Property LoanRequestDto as LoanRequestDto
    Public ReadOnly Property TotalInterestPaid as Decimal
    Public ReadOnly Property PaymentScheduleRows as PaymentScheduleRow()
End Class