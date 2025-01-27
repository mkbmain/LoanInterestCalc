Imports System

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

    Public Property PaymentNumber As Integer
    Public Property PrincipalAmount As Decimal
    Public Property InterestAmount As Decimal
    Public Property PaymentAmount As Decimal
    Public Property RemainingBalance As Decimal
End Class

Module Program
    private Const Amount as Integer = 200_000
    private Const InterestRate as double = 4.15

    Sub Main(args As String())
        Console.WriteLine(
            $"Years,PaymentAmount,PrincipalAmount,InterestAmount,RemainingBalance,YearTotal, 3 year total,5 year total, 10 year total")
        For year As Integer = 5 To 30 Step 5
            Dim paymentSchedule = CalculateAmortizationSchedule(new LoanRequestDto(year*12, Amount, InterestRate))

            Dim totalPaid3Year = paymentSchedule.PaymentScheduleRows.Take(3*12).Sum(Function(w)  w.PrincipalAmount)
            Dim totalPaidFiveYear = paymentSchedule.PaymentScheduleRows.Take(5*12).Sum(Function(w) w.PrincipalAmount)
            Dim totalPaid10Year = paymentSchedule.PaymentScheduleRows.Take(10*12).Sum(Function(w)  w.PrincipalAmount)
            Console.WriteLine(
                ($"{year} year, ").PadRight(10) +
                $"Payment amount:{paymentSchedule.PaymentScheduleRows.First().PaymentAmount:C} " +
                $"Total Paid:{paymentSchedule.PaymentScheduleRows.Sum(Function(w) w.PaymentAmount):C}, " +
                $"Total principal at 3 years:{totalPaid3Year:C}, Total at 5 years {totalPaidFiveYear}" +
                $", Total at 10 years {totalPaid10Year:C}")
        Next
    End Sub


    private Function CalculateAmortizationSchedule(loanRequest as LoanRequestDto) As PaymentSchedule

        Dim schedule = New List(Of PaymentScheduleRow)()
        Dim monthlyInterestRate As decimal = loanRequest.InterestRate/12/100
        Dim monthlyPayment = Math.Round(
            CalculateMonthlyPayment(loanRequest.LoanAmount, monthlyInterestRate, loanRequest.NumberOfPayments), 2)

        Dim remaining = loanRequest.LoanAmount

        For number As Integer = 1 To loanrequest.NumberOfPayments
            Dim interestPayment = Math.Round(remaining*monthlyInterestRate, 2)
            Dim principalPayment = monthlyPayment - interestPayment

            remaining -= principalPayment
            if (remaining < principalPayment) Then

                principalPayment += remaining
                monthlyPayment += remaining
                remaining = 0
            End If

            schedule.Add(New PaymentScheduleRow(number, principalPayment, interestPayment, monthlyPayment, remaining))
        Next

        Return New PaymentSchedule(loanRequest, schedule.Sum(Function(e) e.InterestAmount), schedule.ToArray())
    End Function

    Private Function CalculateMonthlyPayment(loanAmount as Decimal,
                                             monthlyInterest as Decimal,
                                             numberOfPayments as Integer) As Decimal
        if (monthlyInterest <= 0) Then
            return loanAmount/numberOfPayments
        End If
        Dim power as Decimal = Math.Pow(1 + CType(monthlyInterest, Double), numberOfPayments)
        return loanAmount*(monthlyInterest*power)/(power - 1)
    End Function
End Module
