Public Class VoieEEG
    Public Color As Brush
    Public Item As Integer
    Public Nom As String
    Public Vert_pos As Integer
    Public Interval As New List(Of Rectangle)
    Public Hor_pos As Integer
    Public Hor_int_pos As Integer
    Public Label1 As Label
    Dim nomVoie = {"Fp2", "F8", "C4", "T6", "O2", "Cz", "Fp1", "F7", "C3", "T5", "O1"}
    Dim br As New List(Of Brush)
    Public Sub New(ByVal i As Integer, ByVal Hauteurfen As Integer, ByVal margebasse As Integer, ByVal nbVoie2 As Integer)
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
        br.Add(Brushes.Black)
        If (nbVoie2 Mod 2) <> 0 Then
            If i < Int((nbVoie2) / 2) + 1 Then
                Color = br(i - 1)
            ElseIf i = Int((nbVoie2) / 2) + 1 Then
                Color = br(5)
            ElseIf i <= nbVoie2 Then
                Color = br(i - nbVoie2 / 2 + 5 - 1)
            Else
                Color = br(11)
            End If
        Else
            If i <= nbVoie2 / 2 Then
                Color = br(i)
            ElseIf i <= nbVoie2 + 1 Then
                Color = br(i - nbVoie2 / 2 + 5)
            Else
                Color = br(11)
            End If
        End If

        Item = i
        Hor_pos = 20
        Hor_int_pos = 70
        Nom = nomVoie(i - 1)
        label.Content = nomVoie(i - 1)
        label.FontSize = 22
        label.FontWeight = FontWeights.Bold
        Label1 = label
    End Sub

End Class
