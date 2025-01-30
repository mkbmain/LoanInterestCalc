Class LoanRequestDto
    Sub new(numberOfPayments as Integer, loanAmount as Decimal, interestRate as Double)
        Me.NumberOfPayments = numberOfPayments
        Me.LoanAmount = loanAmount
        Me.InterestRate = interestRate
    End Sub

    Public ReadOnly Property NumberOfPayments as Integer
    Public ReadOnly Property LoanAmount as Decimal
    Public ReadOnly Property InterestRate as Double
End Class