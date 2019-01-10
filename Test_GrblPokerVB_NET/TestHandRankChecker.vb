Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports GrblPokerVB_NET

<TestClass()> Public Class TestHandRankChecker

    <TestMethod()> Public Sub TestBitCount()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        Dim res = c.BitCount(&B11100111)

        Assert.AreEqual(res, 6)
    End Sub

    <TestMethod()> Public Sub TestConvert_joker()
        Dim card As New Cards()
        Dim c1 = card.Convert("J")

        Assert.AreEqual(c1, &B11110000000000000000)
    End Sub

    <TestMethod()> Public Sub TestConvert_s1()
        Dim card As New Cards()
        Dim c1 = card.Convert("s1")

        Assert.AreEqual(c1, &B10000000000000001)
    End Sub

    <TestMethod()> Public Sub TestIsFlush()
        Dim card As New Cards()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        Dim c1 = card.Convert("J")
        Dim c2 = card.Convert("s1")
        Dim c3 = card.Convert("s7")
        Dim c4 = card.Convert("s9")
        Dim c5 = card.Convert("st")

        Dim res = c.IsFlush(c1, c2, c3, c4, c5)

        Assert.IsTrue(res)

    End Sub

    <TestMethod()> Public Sub TestGetHandRank_Flush()
        Dim card As New Cards()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        Dim c1 = card.Convert("J")
        Dim c2 = card.Convert("s1")
        Dim c3 = card.Convert("s7")
        Dim c4 = card.Convert("s9")
        Dim c5 = card.Convert("st")

        Dim res = c.GetHandRank(c1, c2, c3, c4, c5)

        Assert.AreEqual(res, 4)
    End Sub

    <TestMethod()> Public Sub TestGetRateNumOfAKind()
        Dim card As New Cards()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        Dim c1 = card.Convert("J")
        Dim c2 = card.Convert("s1")
        Dim c3 = card.Convert("s7")
        Dim c4 = card.Convert("s9")
        Dim c5 = card.Convert("st")

        Dim res = c.GetRateNumOfAKind(c1, c2, c3, c4, c5)

        Assert.AreEqual(res, -1)
    End Sub

    <TestMethod()> Public Sub TestPairCount()
        Dim card As New Cards()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        Dim c1 = card.Convert("J")
        Dim c2 = card.Convert("s1")
        Dim c3 = card.Convert("s7")
        Dim c4 = card.Convert("s9")
        Dim c5 = card.Convert("st")

        'Dim resul As Tuple(Of Integer, Integer) =
        Dim res = c.PairCount(c1, c2, c3, c4, c5)

        Dim max = res.Item1
        Dim pairNum = res.Item2

        Assert.AreEqual(max, 2)
        Assert.AreEqual(pairNum, 0)
    End Sub


    <TestMethod()> Public Sub TestGetRateNumOfAKind_case1()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        Dim res = c.GetRateNumOfAKind(&B10000000000000010,
                                      &B10000000000001000,
                                      &B10000000000010000,
                                      &B10000000000100000,
                                      &B10000000100000000)

        Assert.AreEqual(res, -1)
    End Sub

    <TestMethod()> Public Sub TestGetRateNumOfAKind_OnePair()

        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)
        '                            cdhs---kqjt987654321
        Dim res = c.GetRateNumOfAKind(&B10000000000001000,
                                      &B10000000100000000,
                                     &B100000000000001000,
                                      &B10000000000010000,
                                      &B10000000000100000)

        Assert.AreEqual(res, r.OnePair)
    End Sub

    <TestMethod()> Public Sub TestGetRateNumOfAKind_TwoPair()
        Dim r As New Rate1000()
        Dim c As New HandRankChecker(r)

        '                            cdhs---kqjt987654321
        Dim res = c.GetRateNumOfAKind(&B10000000000001000,
                                      &B10000000100000000,
                                     &B100000000000001000,
                                      &B10000000000010000,
                                     &B100000000000010000)
        Assert.AreEqual(res, r.TwoPair)
    End Sub

End Class