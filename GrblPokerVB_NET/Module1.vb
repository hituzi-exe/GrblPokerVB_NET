Imports System

Module Module1

    Function Main() As Integer
        Return GetMaxExpectation("s1", "s3", "h6", "h8", "dt")
        'Dim args As String() = Environment.GetCommandLineArgs()

        'Return GetMaxExpectation(args(1), args(2), args(3), args(4), args(5))
    End Function

    Function GetMaxExpectation(hand1 As String,
                               hand2 As String,
                               hand3 As String,
                               hand4 As String,
                               hand5 As String) As Integer

        Dim card As New Cards()
        Dim deck = card.CreateDeck()
        Dim hands = {hand1, hand2, hand3, hand4, hand5}.
            ToList().
            Select(Of Integer)(Function(e) card.Convert(e))

        deck.RemoveAll(AddressOf hands.Contains)

        Dim checker As New HandRankChecker(New Rate1000)

        Dim maxExp As Double = 0
        Dim maxExpHand As New List(Of Integer)

        For Each i In {0, 1, 2, 3, 4, 5}
            For Each hand In Combination(Of Integer)(hands.ToList, i)
                Dim sumCount = 0
                Dim sumExp = 0

                For Each d In Combination(Of Integer)(deck, 5 - i)
                    Dim c As Integer() = New Integer(hand.Count + d.Count - 1) {}
                    Array.Copy(hand.ToArray, c, hand.Count)
                    Array.Copy(d.ToArray, 0, c, hand.Count, d.Count)

                    sumExp += checker.getHandRank(c(0), c(1), c(2), c(3), c(4))
                    sumCount += 1

                Next

                If maxExp < (sumExp / sumCount) Then
                    maxExp = (sumExp / sumCount)
                    maxExpHand = hand
                End If

            Next
        Next


        For Each h In maxExpHand
            Console.Write(card.Convert2(h) & ",")
        Next

    End Function

    Function Combination(Of T)(elements As List(Of T), choose As Integer) As List(Of List(Of T))
        Dim ret As New List(Of List(Of T))

        ' 再帰呼び出しの終端
        If elements.Count < choose Then
            Return New List(Of List(Of T))()
        ElseIf choose <= 0 Then
            Dim resList As New List(Of List(Of T)) From {
                New List(Of T)
            }
            Return resList
        End If

        For n As Integer = 1 To elements.Count - 1
            Dim subRet = Combination(elements.Skip(n).ToList(), choose - 1)

            For Each s In subRet
                s.Add(elements(n - 1))

            Next
            ret.AddRange(subRet)
        Next
        Return ret
    End Function





End Module
