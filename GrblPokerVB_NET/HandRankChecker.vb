Public Class HandRankChecker

    Private rate As Rate
    Private numOfAKindMap As List(Of Integer)

    Public Sub New(r As Rate)
        rate = r
        numOfAKindMap = CreateNumOfAKindMap()
    End Sub

    Public Function CreateNumOfAKindMap() As List(Of Integer)
        Return {rate.NotPair(), rate.NotPair(), rate.NotPair(),
                rate.NotPair(), rate.OnePair(), rate.TwoPair(),
                rate.NotPair(), rate.ThreeOfAKind(), rate.FullHouse(),
                rate.NotPair(), rate.FourOfAKind(), rate.NotPair(),
                rate.NotPair(), rate.FiveOfAKind(), rate.NotPair()}.ToList
    End Function


    Public Function GetHandRank(hand1, hand2, hand3, hand4, hand5) As Integer

    End Function

    Public Function GetRateNumOfAKind(hand1, hand2, hand3, hand4, hand5) As Integer

        If bitCount((hand1 Or hand2 Or hand3 Or hand4 Or hand5) And (&H1FFF)) = 5 Then
            Return rate.NotPair()
        End If

        Dim resul As Tuple(Of Integer, Integer) = PairCount(hand1, hand2, hand3, hand4, hand5)


        Return numOfAKindMap(resul.Item1 + (resul.Item2 - 1) * 3)
    End Function


    Public Function PairCount(hand1, hand2, hand3, hand4, hand5) As Tuple(Of Integer, Integer)
        Dim cntList = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim cnt = {hand1 & (&H1FFF),
                   hand2 & (&H1FFF),
                   hand3 & (&H1FFF),
                   hand4 & (&H1FFF),
                   hand5 & (&H1FFF)}

        For Each c In cnt
            cntList(MyLog2(c)) += 1
        Next

        Dim maxCnt = cntList.Max() + cntList(13)
        Dim pairCnt = cntList.Count(Function(e) e > 1)


        Return Tuple.Create(cntList.Max() + cntList(13), pairCnt)
    End Function

    Public Function MyLog2(x) As Integer
        If x = 0 Then
            Return 13
        End If

        Return Int(Math.Log(x, 2))
    End Function





    Public Function BitCount(i As Integer) As Integer
        i = i - ((i >> 1) And &H55555555)
        i = (i And &H33333333) + ((i >> 2) And &H33333333)
        i = (i + (i >> 4)) And &HF0F0F0F
        i = i + (i >> 8)
        i = i + (i >> 16)
        Return i And &H3F
    End Function


End Class
