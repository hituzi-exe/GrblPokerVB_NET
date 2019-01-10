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

End Class