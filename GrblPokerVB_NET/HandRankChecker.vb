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

        'Dim resul As Tuple(Of Integer, Integer) = PairCount(hand1, hand2, hand3, hand4, hand5)

        'Return numOfAKindMap(resul.Item2 + (resul.Item1 - 1) * 3)

        Dim index As Integer = PairCount(hand1, hand2, hand3, hand4, hand5)


        Return numOfAKindMap(index)
    End Function


    Public Function PairCount(hand1 As Integer,
                              hand2 As Integer,
                              hand3 As Integer,
                              hand4 As Integer,
                              hand5 As Integer) As Integer
        Dim cntList As Integer() = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}

        cntList(MyLog2(hand1 And (&H1FFF))) += 1
        cntList(MyLog2(hand2 And (&H1FFF))) += 1
        cntList(MyLog2(hand3 And (&H1FFF))) += 1
        cntList(MyLog2(hand4 And (&H1FFF))) += 1
        cntList(MyLog2(hand5 And (&H1FFF))) += 1

        Dim maxCnt As Integer = 0
        Dim pairCnt As Integer = 0
        For Each c In cntList
            If c = 0 Then
                Continue For
            End If

            If maxCnt < c Then
                maxCnt = c
            End If

            If c > 1 Then
                pairCnt += 1
            End If
        Next

        Return pairCnt + (maxCnt + cntList(13) - 1) * 3
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

        Dim checkbit As Integer = (handNum / (handNum And (-handNum)))

        If (checkbit = &H1F) Or (checkbit = &H1E01) Then
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
        'For i As Integer = 0 To 13
        '    If (x And (1 << i)) > 0 Then
        '        Return i
        '    End If
        'Next

        'Return 13

        Select Case x And (-x)
            Case &B1
                Return 0
            Case &B10
                Return 1
            Case &B100
                Return 2
            Case &B1000
                Return 3
            Case &B10000
                Return 4
            Case &B100000
                Return 5
            Case &B1000000
                Return 6
            Case &B10000000
                Return 7
            Case &B100000000
                Return 8
            Case &B1000000000
                Return 9
            Case &B10000000000
                Return 10
            Case &B100000000000
                Return 11
            Case &B1000000000000
                Return 12
            Case Else
                Return 13
        End Select
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
