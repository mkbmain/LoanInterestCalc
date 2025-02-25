Imports System

Module Program
    private Const Amount as Integer = 130_000
    private Const InterestRate as double = 4.15

    Sub Main(args As String())
        Console.WriteLine(
            $"Years,PaymentAmount,PrincipalAmount,InterestAmount,RemainingBalance,YearTotal, 3 year total,5 year total, 10 year total")
        For year As Integer = 5 To 30 Step 5
            Dim paymentSchedule As PaymentSchedule = CalculateAmortizationSchedule(new LoanRequestDto(year*12, Amount, InterestRate))

            Dim totalPaid3Year = paymentSchedule.PaymentScheduleRows.Take(3*12).Sum(Function(w)  w.PrincipalAmount)
            Dim totalPaidFiveYear = paymentSchedule.PaymentScheduleRows.Take(5*12).Sum(Function(w) w.PrincipalAmount)
            Dim totalPaid10Year = paymentSchedule.PaymentScheduleRows.Take(10*12).Sum(Function(w)  w.PrincipalAmount)
            Console.WriteLine(
                ($"{year} year, ").PadRight(9) +
                $"Payment amount:{paymentSchedule.PaymentScheduleRows.First().PaymentAmount:C} " +
                $"Total Paid:{paymentSchedule.PaymentScheduleRows.Sum(Function(w) w.PaymentAmount):C}, " +
                $"Total principal at 3 years:{totalPaid3Year:C}, Total at 5 years {totalPaidFiveYear:C}" +
                $", Total at 10 years {totalPaid10Year:C}")
        Next
    End Sub


    private Function CalculateAmortizationSchedule(loanRequest as LoanRequestDto) As PaymentSchedule
        
        if loanRequest is Nothing Then
            Throw new NullReferenceException($"Parameter {NameOf(loanRequest)} cannot be NULL")
        End If
        
        if loanRequest.LoanAmount <=0 Then
            throw new NullReferenceException($"Invalid loan amount! The loan amount cannot be less than or equal to 0")
        End If
               
        Dim schedule = New List(Of PaymentScheduleRow)()
        Dim monthlyInterestRate As decimal = loanRequest.InterestRate/12/100
        Dim monthlyPayment = Math.Round(
            CalculateMonthlyPayment(loanRequest.LoanAmount, monthlyInterestRate, loanRequest.NumberOfPayments), 2)

        Dim remainingBalance = loanRequest.LoanAmount

        For number As Integer = 1 To loanrequest.NumberOfPayments
            Dim interestPayment = Math.Round(remainingBalance*monthlyInterestRate, 2)
            Dim principalPayment = monthlyPayment - interestPayment

            remainingBalance -= principalPayment
            
            if (remainingBalance < principalPayment) Then
                principalPayment += remainingBalance
                monthlyPayment += remainingBalance
                remainingBalance = 0
            End If

            schedule.Add(New PaymentScheduleRow(number, principalPayment, interestPayment, monthlyPayment,
                                                remainingBalance))
        Next

        Return New PaymentSchedule(loanRequest, schedule.ToArray())
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
