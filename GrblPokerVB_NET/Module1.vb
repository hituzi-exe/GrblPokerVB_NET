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

        Dim maxExp As Double = checker.GetHandRank(hands(0), hands(1), hands(2), hands(3), hands(4))
        Dim maxExpHand As New List(Of Integer) From {hands(0), hands(1), hands(2), hands(3), hands(4)}

        Dim handPattern = CreateHandPattern(hands.ToList)
        For Each hand In handPattern
            Dim tmpExp As Double

            Select Case hand.Count
                Case 0
                    tmpExp = GetMaxExp(checker, deck)
                Case 1
                    tmpExp = GetMaxExp(checker, deck, hand(0))
                Case 2
                    tmpExp = GetMaxExp(checker, deck, hand(0), hand(1))
                Case 3
                    tmpExp = GetMaxExp(checker, deck, hand(0), hand(1), hand(2))
                Case 4
                    tmpExp = GetMaxExp(checker, deck, hand(0), hand(1), hand(2), hand(3))
                Case 5
                    tmpExp = GetMaxExp(checker, deck, hand(0), hand(1), hand(2), hand(3), hand(4))
                Case Else
                    tmpExp = 0
            End Select

            If maxExp < tmpExp Then
                maxExp = tmpExp
                maxExpHand = hand
            End If

        Next

        For Each h In maxExpHand
            Console.Write(card.Convert2(h) & ",")
        Next

    End Function

    Function GetMaxExp(checker As HandRankChecker,
                       deck As List(Of Integer)) As Double

        Dim sumCount = 0
        Dim sumExp = 0

        For i As Integer = 0 To deck.Count - 1
            For j As Integer = i + 1 To deck.Count - 1
                For k As Integer = j + 1 To deck.Count - 1
                    For l As Integer = k + 1 To deck.Count - 1
                        For m As Integer = l + 1 To deck.Count - 1
                            sumExp += checker.GetHandRank(deck(i), deck(j), deck(k), deck(l), deck(m))
                            sumCount += 1
                        Next
                    Next
                Next
            Next
        Next

        Return sumExp / sumCount
    End Function

    Function GetMaxExp(checker As HandRankChecker,
                       deck As List(Of Integer),
                       hand1 As Integer) As Double

        Dim sumCount = 0
        Dim sumExp = 0

        For i As Integer = 0 To deck.Count - 1
            For j As Integer = i + 1 To deck.Count - 1
                For k As Integer = j + 1 To deck.Count - 1
                    For l As Integer = k + 1 To deck.Count - 1
                        sumExp += checker.GetHandRank(hand1, deck(i), deck(j), deck(k), deck(l))
                        sumCount += 1
                    Next
                Next
            Next
        Next

        Return sumExp / sumCount
    End Function

    Function GetMaxExp(checker As HandRankChecker,
                       deck As List(Of Integer),
                       hand1 As Integer,
                       hand2 As Integer) As Double

        Dim sumCount = 0
        Dim sumExp = 0

        For i As Integer = 0 To deck.Count - 1
            For j As Integer = i + 1 To deck.Count - 1
                For k As Integer = j + 1 To deck.Count - 1
                    sumExp += checker.GetHandRank(hand1, hand2, deck(i), deck(j), deck(k))
                    sumCount += 1
                Next
            Next
        Next

        Return sumExp / sumCount
    End Function

    Function GetMaxExp(checker As HandRankChecker,
                       deck As List(Of Integer),
                       hand1 As Integer,
                       hand2 As Integer,
                       hand3 As Integer) As Double

        Dim sumCount = 0
        Dim sumExp = 0

        For i As Integer = 0 To deck.Count - 1
            For j As Integer = i + 1 To deck.Count - 1
                sumExp += checker.GetHandRank(hand1, hand2, hand3, deck(i), deck(j))
                sumCount += 1
            Next
        Next

        Return sumExp / sumCount
    End Function

    Function GetMaxExp(checker As HandRankChecker,
                       deck As List(Of Integer),
                       hand1 As Integer,
                       hand2 As Integer,
                       hand3 As Integer,
                       hand4 As Integer) As Double

        Dim sumCount = 0
        Dim sumExp = 0

        For i As Integer = 0 To deck.Count - 1
            sumExp += checker.GetHandRank(hand1, hand2, hand3, hand4, deck(i))
            sumCount += 1
        Next

        Return sumExp / sumCount
    End Function


    Function GetMaxExp(checker As HandRankChecker,
                       deck As List(Of Integer),
                       hand1 As Integer,
                       hand2 As Integer,
                       hand3 As Integer,
                       hand4 As Integer,
                       hand5 As Integer) As Double

        Dim sumCount = 0
        Dim sumExp = 0

        sumExp += checker.GetHandRank(hand1, hand2, hand3, hand4, hand5)
        sumCount += 1

        Return sumExp / sumCount
    End Function

    Function CreateHandPattern(hands As List(Of Integer)) As List(Of List(Of Integer))
        Dim handPattern As New List(Of List(Of Integer))
        For Each i In {0, 1, 2, 3, 4, 5}

            For Each hand In Combination(Of Integer)(hands.ToList, i)
                handPattern.Add(hand)
            Next
        Next

        Return handPattern
    End Function

    Function Combination(Of T)(elements As List(Of T), choose As Integer) As List(Of List(Of T))
        Dim ret As New List(Of List(Of T))

        ' 再帰呼び出しの終端
        If elements.Count < choose Then
            Return New List(Of List(Of T))()
        ElseIf choose < 0 Then
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
