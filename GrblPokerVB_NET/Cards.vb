Public Class Cards
    Public Shared JOKER As Integer = &HF0000

    Public Function CreateDeck()
        ' S = suit bit(sdhc), N = num bit(a23456789tjqk)
        ' card : chds ---k qjt9 8765 432a
        Dim num = Enumerable.Range(0, 13).Select(Of Integer)(Function(e) 1 << e)
        Dim suit = Enumerable.Range(15, 4).Select(Of Integer)(Function(e) 1 << e)

        Dim deck As New ArrayList()
        For Each n In num
            For Each s In suit
                deck.Add(n + s)
            Next
        Next

        deck.Add(Cards.JOKER)

        Return deck
    End Function

    Public Function Convert(hand)

        If hand = "J" Then
            Return Cards.JOKER
        End If

        Dim suitsStr = "sdhc"
        Dim numsStr = "a23456789tjqk"


        Dim suit = hand.Substring(0, 1)
        Dim num = hand.Substring(1, 1)

        Try
            Return (1 << (suitsStr.IndexOf(suit) + 16)) Or (1 << numsStr.IndexOf(num))

        Catch ex As Exception
            Return -1
        End Try

    End Function

    Public Function Convert2(hand)

        If hand = Cards.JOKER Then
            Return "J"
        End If

        Dim suits = (hand And &HF0000) >> 16
        Dim num = hand And &H1FFF

        Dim suitsStr = "sdhc"
        Dim numsStr = "a23456789tjqk"

        Return GetChar(suitsStr, MyLog2(suits)) & GetChar(numsStr, MyLog2(num))

    End Function

    Public Function MyLog2(x As Integer) As Integer
        If x = 0 Then
            Return 13
        End If

        Return Int(Math.Log(x, 2))
    End Function

End Class
