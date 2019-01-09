Imports System

Module Module1

    Function Main() As Integer
        'Dim arguments As String() = Environment.GetCommandLineArgs()

        'Return TestGetHandRank(arguments)

        TestCards()


    End Function

    Function TestGetHandRank(arguments As String())

        Dim hrc As New HandRankChecker(New Rate1000())
        Try
            Dim res = hrc.GetHandRank(Integer.Parse(arguments(1)),
                                      Integer.Parse(arguments(2)),
                                      Integer.Parse(arguments(3)),
                                      Integer.Parse(arguments(4)),
                                      Integer.Parse(arguments(5)))

            Console.WriteLine(res)

            Return res
        Catch ex As System.IndexOutOfRangeException
            Return -1
        End Try
    End Function


    Function TestCards() As Integer
        Dim c As New Cards()

        c.CreateDeck()

        Return 0

    End Function
End Module
