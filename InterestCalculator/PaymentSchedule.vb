Class PaymentSchedule
    Sub new(loanRequestDto as LoanRequestDto,
            paymentScheduleRows as PaymentScheduleRow())

        Me.LoanRequestDto = loanRequestDto
        Me.PaymentScheduleRows = paymentScheduleRows
    End Sub

    Public ReadOnly Property LoanRequestDto as LoanRequestDto

   Public readonly Property TotalInterestPaid() As Decimal
        Get
            return PaymentScheduleRows.Sum(Function(e) e.InterestAmount)
        End Get
    End Property
    Public ReadOnly Property PaymentScheduleRows as PaymentScheduleRow()
End Class