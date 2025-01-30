Class PaymentScheduleRow
    Sub new(paymentNumber as Integer,
            principalAmount as Decimal,
            interestAmount as Decimal,
            paymentAmount as Decimal,
            remainingBalance as Decimal)
        Me.PaymentNumber = paymentNumber
        Me.PrincipalAmount = principalAmount
        Me.InterestAmount = interestAmount
        Me.PaymentAmount = paymentAmount
        Me.RemainingBalance = remainingBalance
    End Sub

    Public ReadOnly Property PaymentNumber As Integer
    Public ReadOnly Property PrincipalAmount As Decimal
    Public ReadOnly Property InterestAmount As Decimal
    Public ReadOnly Property PaymentAmount As Decimal
    Public ReadOnly Property RemainingBalance As Decimal
End Class