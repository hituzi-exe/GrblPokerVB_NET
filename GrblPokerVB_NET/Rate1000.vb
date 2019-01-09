Imports GrblPokerVB_NET

Public Class Rate1000
    Implements IRate

    Public Function NotPair() As Integer Implements IRate.NotPair
        Return -1
    End Function

    Public Function HighCard() As Integer Implements IRate.HighCard
        Return 0
    End Function

    Public Function OnePair() As Integer Implements IRate.OnePair
        Return 0
    End Function

    Public Function TwoPair() As Integer Implements IRate.TwoPair
        Return 1
    End Function

    Public Function ThreeOfAKind() As Integer Implements IRate.ThreeOfAKind
        Return 1
    End Function

    Public Function FullHouse() As Integer Implements IRate.FullHouse
        Return 10
    End Function

    Public Function FourOfAKind() As Integer Implements IRate.FourOfAKind
        Return 20
    End Function

    Public Function FiveOfAKind() As Integer Implements IRate.FiveOfAKind
        Return 60
    End Function

    Public Function Straight() As Integer Implements IRate.Straight
        Return 3
    End Function

    Public Function Flush() As Integer Implements IRate.Flush
        Return 4
    End Function

    Public Function StraightFlush() As Integer Implements IRate.StraightFlush
        Return 25
    End Function

    Public Function RoyalStraightFlush() As Integer Implements IRate.RoyalStraightFlush
        Return 250
    End Function
End Class
