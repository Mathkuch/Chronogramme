Public Class VoieEEG
    Public Color As Brush
    Public Item As Integer
    Public Nom As String
    Public Vert_pos As Integer
    Public Interval As New List(Of Rectangle)
    Public Hor_pos As Integer
    Public Hor_int_pos As Integer
    Public Label1 As Label
    Dim nomVoie = {"Fp2", "C4", "F8", "T6", "O2", "Cz", "Fp1", "C3", "F7", "T5", "O1"}
    Dim br As New List(Of Brush)
    Public Sub New(ByVal i As Integer, ByVal Hauteurfen As Integer, ByVal margebasse As Integer)
        Dim label As New Label
        br.Add(Brushes.LightPink)
        br.Add(Brushes.Magenta)
        br.Add(Brushes.Red)
        br.Add(Brushes.Chocolate)
        br.Add(Brushes.Maroon)
        br.Add(Brushes.Gainsboro)
        br.Add(Brushes.Aqua)
        br.Add(Brushes.DarkTurquoise)
        br.Add(Brushes.DodgerBlue)
        br.Add(Brushes.Blue)
        br.Add(Brushes.DarkSlateBlue)

        Item = i
        Hor_pos = 20
        Vert_pos = 50 + ((Hauteurfen - margebasse) / 11) * (i - 1)
        Color = br(i - 1)
        Hor_int_pos = 70
        Nom = nomVoie(i - 1)
        label.Content = nomVoie(i - 1)
        label.FontSize = 14
        label.FontWeight = FontWeights.Bold
        Label1 = label

    End Sub

End Class
