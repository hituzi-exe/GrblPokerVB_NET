Public Class HandRankChecker

    Private rate As IRate
    Public Property numOfAKindMap As List(Of Integer)

    Public Sub New(r As IRate)
        rate = r
        numOfAKindMap = {rate.NotPair(), rate.NotPair(), rate.NotPair(),
                         rate.NotPair(), rate.OnePair(), rate.TwoPair(),
                         rate.NotPair(), rate.ThreeOfAKind(), rate.FullHouse(),
                         rate.NotPair(), rate.FourOfAKind(), rate.NotPair(),
                         rate.NotPair(), rate.FiveOfAKind(), rate.NotPair()}.ToList
    End Sub

    Public Function GetHandRank(hand1 As Integer,
                                hand2 As Integer,
                                hand3 As Integer,
                                hand4 As Integer,
                                hand5 As Integer) As Integer

        Dim flushFlg As Boolean = IsFlush(hand1, hand2, hand3, hand4, hand5)

        If Not flushFlg Then
            Dim pair As Integer = GetRateNumOfAKind(hand1, hand2, hand3, hand4, hand5)
            If pair <> rate.NotPair() Then
                Return pair
            End If
        End If

        Dim straightFlg As Boolean = IsStraight(hand1, hand2, hand3, hand4, hand5)
        If Not (flushFlg Or straightFlg) Then
            Return rate.HighCard()
        End If


        If flushFlg And straightFlg Then
            If IsRoyalStraightFlush(hand1, hand2, hand3, hand4, hand5) Then
                Return rate.RoyalStraightFlush()

            End If
            Return rate.StraightFlush()
        End If

        If flushFlg Then
            Return rate.Flush()

        End If

        Return rate.Straight()

    End Function

    Public Function GetRateNumOfAKind(hand1 As Integer,
                                      hand2 As Integer,
                                      hand3 As Integer,
                                      hand4 As Integer,
                                      hand5 As Integer) As Integer

        If BitCount((hand1 Or hand2 Or hand3 Or hand4 Or hand5) And (&H1FFF)) = 5 Then
            Return rate.NotPair()
        End If

        Dim resul As Tuple(Of Integer, Integer) = PairCount(hand1, hand2, hand3, hand4, hand5)

        Return numOfAKindMap(resul.Item2 + (resul.Item1 - 1) * 3)
    End Function


    Public Function PairCount(hand1 As Integer,
                              hand2 As Integer,
                              hand3 As Integer,
                              hand4 As Integer,
                              hand5 As Integer) As Tuple(Of Integer, Integer)
        Dim cntList As Integer() = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        Dim cnt = {hand1 And (&H1FFF),
                   hand2 And (&H1FFF),
                   hand3 And (&H1FFF),
                   hand4 And (&H1FFF),
                   hand5 And (&H1FFF)}

        For Each c In cnt
            cntList(MyLog2(c)) += 1
        Next

        Dim maxCnt As Integer = cntList.Max() + cntList(13)
        Dim pairCnt As Integer = cntList.Count(Function(e) e > 1)

        Return Tuple.Create(maxCnt, pairCnt)
    End Function

    Public Function IsFlush(hand1 As Integer,
                            hand2 As Integer,
                            hand3 As Integer,
                            hand4 As Integer,
                            hand5 As Integer) As Boolean
        Return (hand1 And hand2 And hand3 And hand4 And hand5 And (&HF0000)) > 0
    End Function

    Public Function IsStraight(hand1 As Integer,
                               hand2 As Integer,
                               hand3 As Integer,
                               hand4 As Integer,
                               hand5 As Integer) As Boolean

        Dim handNum As Integer = (hand1 Or hand2 Or hand3 Or hand4 Or hand5) And (&H1FFF)

        Dim checkbit As Integer = Int(handNum / (handNum And (-handNum)))

        If (checkbit = &H1F) Then
            Return True
        End If

        If checkbit = &H1E01 Then
            Return True
        End If

        Dim inJoker = ((hand1 = &HF0000) Or (hand2 = &HF0000) Or (hand3 = &HF0000) Or (hand4 = &HF0000) Or (hand5 = &HF0000))
        ' Jokerがなければストレート不成立
        If Not inJoker Then
            Return False
        End If

        Dim straightFlg = (&HF = checkbit) Or
                          (&H1D = checkbit) Or
                          (&H1B = checkbit) Or
                          (&H17 = checkbit) Or
                          (&H1C01 = checkbit) Or
                          (&H1601 = checkbit) Or
                          (&H1A01 = checkbit) Or
                          (&HC01 = checkbit)

        Return straightFlg
    End Function



    Public Function IsRoyalStraightFlush(hand1 As Integer,
                                         hand2 As Integer,
                                         hand3 As Integer,
                                         hand4 As Integer,
                                         hand5 As Integer) As Boolean

        Dim handlist = {hand1, hand2, hand3, hand4, hand5}
        ' Jokerがあれば不成立
        If handlist.ToList().Any(Function(e) e = &HF0000) Then
            Return False
        End If

        Return ((hand1 Or hand2 Or hand3 Or hand4 Or hand5) And (&H1FFF)) = (&B1111000000001)
    End Function


    Public Function MyLog2(x As Integer) As Integer
        For i As Integer = 0 To 13
            If (x And (1 << i)) > 0 Then
                Return i
            End If
        Next

        Return 13
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
