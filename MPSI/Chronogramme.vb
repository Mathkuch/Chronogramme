
Imports Excel = Microsoft.Office.Interop.Excel
Imports System.Collections.ObjectModel
Imports System.Math
Imports System.Drawing.Printing
Imports System.Printing


Class MainWindow
    Dim Nbinterval, Hauteurfen, B, marge, Lfenetre, Vert(11) As Integer
    Dim Seuil2 As New ObservableCollection(Of GraphPoint)
    Dim SeuilDebut As New ObservableCollection(Of GraphPoint)
    Dim SeuilFinal As New ObservableCollection(Of GraphPoint)
    Dim Timeline As New List(Of Line)
    Dim listtemp As New List(Of Label)
    Dim Br As New List(Of Brush) 'couleur des voies EEG
    Dim ListofArray As New List(Of List(Of List(Of Double)))
    Dim liste_voie As New List(Of VoieEEG)
    Dim graph As New List(Of ObservableCollection(Of GraphPoint))
    Dim prd As New PrintDocument()
    Dim nT6Array1 As New List(Of Double)
    Dim Dureemax As Integer
    Dim verticale As New List(Of Integer)
    Dim debut, final As Integer
    Dim liste_voie2 As New List(Of String)
    Dim Nbvoie As Integer
    Private Sub windows1_Loaded(sender As Object, e As RoutedEventArgs) Handles windows1.Loaded
        comboBox1.Items.Add("Bande Delta")
        comboBox1.Items.Add("Bande Theta")
        comboBox1.Items.Add("Bande Alpha")
        comboBox1.Items.Add("Bande Beta")
        B = windows1.ActualWidth
        Hauteurfen = windows1.ActualHeight
        marge = 450
        Panneau_Commande()

        '''''Bouton pour injecter le fichier Excel
    End Sub
    Private Sub Initialiser()
        ' Initialisation des voie
        Dim Vert_pos As Integer
        Dim Fp2 As New VoieEEG(1, Hauteurfen, marge, Nbvoie)
        Dim C4 As New VoieEEG(2, Hauteurfen, marge, Nbvoie)
        Dim f8 As New VoieEEG(3, Hauteurfen, marge, Nbvoie)
        Dim T6 As New VoieEEG(4, Hauteurfen, marge, Nbvoie)
        Dim O2 As New VoieEEG(5, Hauteurfen, marge, Nbvoie)
        Dim cz As New VoieEEG(6, Hauteurfen, marge, Nbvoie)
        Dim fp1 As New VoieEEG(7, Hauteurfen, marge, Nbvoie)
        Dim c3 As New VoieEEG(8, Hauteurfen, marge, Nbvoie)
        Dim f7 As New VoieEEG(9, Hauteurfen, marge, Nbvoie)
        Dim t5 As New VoieEEG(10, Hauteurfen, marge, Nbvoie)
        Dim o1 As New VoieEEG(11, Hauteurfen, marge, Nbvoie)
        liste_voie.Add(Fp2)
        liste_voie.Add(C4)
        liste_voie.Add(f8)
        liste_voie.Add(T6)
        liste_voie.Add(O2)
        liste_voie.Add(cz)
        liste_voie.Add(fp1)
        liste_voie.Add(c3)
        liste_voie.Add(f7)
        liste_voie.Add(t5)
        liste_voie.Add(o1)

        For i As Integer = 1 To Nbvoie
            liste_voie(i - 1).Label1.Content = liste_voie2(Nbvoie - i)
            Canvas1.Children.Add(liste_voie(i - 1).Label1)
            Vert_pos = 50 + ((Hauteurfen - marge) / liste_voie2.Count) * (i - 1)
            verticale.Add(Vert_pos)
            Canvas.SetTop(liste_voie(i - 1).Label1, verticale(i - 1))
            Canvas.SetLeft(liste_voie(i - 1).Label1, liste_voie(i - 1).Hor_pos)
        Next
        ' lignes représentant le temps /100s
        For iline As Integer = 1 To 15
            Dim ligne As New Line
            Dim label1 As New Label
            listtemp.Add(label1)
            Canvas1.Children.Add(listtemp(iline - 1))
            Canvas.SetTop(label1, 0)
            'B taille de la fenêtre
            Lfenetre = (B - 70 - liste_voie(2 - 1).Label1.ActualWidth) / 15
            Canvas.SetLeft(label1, 65 + (Lfenetre * (iline - 1)))
            Timeline.Add(ligne)
            Timeline(iline - 1).Stroke = Brushes.LightSteelBlue
            Timeline(iline - 1).X1 = 70 + (Lfenetre * (iline - 1))
            Timeline(iline - 1).X2 = 70 + (Lfenetre * (iline - 1))
            Timeline(iline - 1).Y1 = 40
            Timeline(iline - 1).Y2 = (Hauteurfen - marge) + 25
            Timeline(iline - 1).StrokeThickness = 0.5
            Canvas1.Children.Add(Timeline(iline - 1))
        Next

    End Sub
    Private Sub Panneau_Commande()
        Boutonchoix.Content = "Choisir un fichier excel"
        Boutonchoix.Height = 30
        Boutonchoix.Width = 150
        buttonimp.Height = 30
        buttonimp.Width = 150
        buttonchange.Height = 30
        buttonchange.Width = 150
        Canvas.SetTop(Boutonchoix, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        Canvas.SetLeft(Boutonchoix, 20)
        Canvas.SetTop(Boutonchrono, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        Canvas.SetLeft(Boutonchrono, Boutonchoix.ActualWidth + comboBox1.ActualWidth + 60)
        Canvas.SetTop(buttonimp, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        Canvas.SetLeft(buttonimp, Boutonchoix.ActualWidth + comboBox1.ActualWidth + Boutonchrono.ActualWidth + 90)
        Canvas.SetTop(buttonchange, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        Canvas.SetLeft(buttonchange, Boutonchoix.ActualWidth + comboBox1.ActualWidth + Boutonchrono.ActualWidth + buttonimp.ActualWidth + 120)
        Canvas.SetTop(textBox1, (((Hauteurfen - marge) / 11) * 11 + 25 + 75))
        Canvas.SetLeft(textBox1, 20)
        comboBox1.Text = "Bande Delta"
        Canvas.SetLeft(comboBox1, 20 + Boutonchoix.ActualWidth + 20)
        Canvas.SetTop(comboBox1, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        textBox1.Text = "Fichier Excel de travail"
        Canvas.SetLeft(Seuil, 20)
        Canvas.SetTop(Seuil, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 85)
        Canvas.SetTop(textBoxSeuil, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 85)
        Canvas.SetLeft(textBoxSeuil, 20 + 140 + 20)
        textBoxSeuil.Width = 90
        MyChart.Width = B / 2 - 40
        MyChart.Height = windows1.ActualHeight - (buttonchange.ActualHeight + 15 + (((Hauteurfen - marge) / 11) * 11 + 25 + 30)) - 10
        Canvas.SetTop(MyChart, (buttonchange.ActualHeight + 15 + (((Hauteurfen - marge) / 11) * 11 + 25 + 30)))
        Canvas.SetLeft(MyChart, (B / 4) * 2)
        Canvas.SetLeft(labelduree, 20)
        Canvas.SetTop(labelduree, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 55)
        Canvas.SetTop(Lbldeb, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 55 + 40)
        Canvas.SetLeft(Lbldeb, 20)
        Canvas.SetTop(Txtbxdeb, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 55 + 40)
        Canvas.SetLeft(Txtbxdeb, 20 + Lbldeb.ActualWidth)
        Txtbxdeb.IsEnabled = False
        Txtbxdeb.Text = "0"
        Canvas.SetTop(Lblfin, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 55 + 40 + 40)
        Canvas.SetLeft(Lblfin, 20)
        Canvas.SetTop(Txtfin, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 55 + 40 + 42)
        Canvas.SetLeft(Txtfin, 20 + Lblfin.ActualWidth)
        Txtfin.IsEnabled = False
        Txtfin.Text = ""
        Canvas.SetLeft(nbint, 20 + labelduree.ActualWidth)
        Canvas.SetTop(nbint, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 60)
        Canvas.SetTop(buttonCrise, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75 + 55 + 40 + 40 + 40)
        Canvas.SetLeft(buttonCrise, 20)
        buttonCrise.IsEnabled = False
    End Sub
    Private Sub Boutonchoix_Click(sender As Object, e As RoutedEventArgs) Handles Boutonchoix.Click
        Choice()
    End Sub
    Private Sub Choice()
        Dim nOFD As New Microsoft.Win32.OpenFileDialog()
        Dim nResultOFD As Nullable(Of Boolean) = nOFD.ShowDialog()
        Dim Nawras As String
        Nawras = ""
        If nResultOFD = True Then
            textBox1.Text = nOFD.FileName
            readExcelFile()
            Nbvoie = liste_voie2.Count
            Initialiser()
            Dureemax = nT6Array1.Count
            Txtfin.Text = Dureemax
            Txtfin.IsEnabled = True
            Txtbxdeb.IsEnabled = True
            buttonCrise.IsEnabled = True
        End If
    End Sub
    Private Sub readExcelFile()
        Dim nO1Array, nT5Array, nC3Array, nF7Array, nFp1Array, nCzArray, nO2array, nT6Array, nC4Array, nF8Array, nFp2Array As New List(Of Double)
        Dim nO1Array1, nT5Array1, nC3Array1, nF7Array1, nFp1Array1, nCzArray1, nO2array1, nC4Array1, nF8Array1, nFp2Array1 As New List(Of Double)
        Dim nO1Array2, nT5Array2, nC3Array2, nF7Array2, nFp1Array2, nCzArray2, nO2array2, nT6Array2, nC4Array2, nF8Array2, nFp2Array2 As New List(Of Double)
        Dim nO1Array3, nT5Array3, nC3Array3, nF7Array3, nFp1Array3, nCzArray3, nO2array3, nT6Array3, nC4Array3, nF8Array3, nFp2Array3 As New List(Of Double)
        Dim nO1Array4, nT5Array4, nC3Array4, nF7Array4, nFp1Array4, nCzArray4, nO2array4, nT6Array4, nC4Array4, nF8Array4, nFp2Array4 As New List(Of Double)
        Dim arrayList, arrayList1, arraylist2, arraylist3, arraylist4 As New List(Of List(Of Double))
        Dim Loca As Integer
        Dim nApp As Excel.Application
        Dim nWorkbook As Excel.Workbook
        Dim nWorksheet As Excel.Worksheet
        nApp = New Excel.Application
        Loca = 1
        nWorkbook = nApp.Workbooks.Open(textBox1.Text)
        nWorksheet = nWorkbook.Worksheets("P D")
        Dim nRange As Excel.Range = nWorksheet.UsedRange
        Dim nArray(,) As Object = nRange.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize As Integer = nArray.GetUpperBound(0)
        Dim j As Integer
        j = 2
        While nArray(1, j) <> "" And nArray(1, j) <> "Somme"
            liste_voie2.Add(nArray(1, j))
            j = j + 1
        End While
        For i As Integer = 2 To nSize
            nO1Array.Add(nArray(i, Loca + 1))
            nT5Array.Add(nArray(i, Loca + 2))
            nC3Array.Add(nArray(i, Loca + 3))
            nF7Array.Add(nArray(i, Loca + 4))
            nFp1Array.Add(nArray(i, Loca + 5))
            nCzArray.Add(nArray(i, Loca + 6))
            nO2array.Add(nArray(i, Loca + 7))
            nT6Array.Add(nArray(i, Loca + 8))
            nC4Array.Add(nArray(i, Loca + 9))
            nF8Array.Add(nArray(i, Loca + 10))
            nFp2Array.Add(nArray(i, Loca + 11))
        Next
        arrayList.Add(nFp2Array)
        arrayList.Add(nF8Array)
        arrayList.Add(nC4Array)
        arrayList.Add(nT6Array)
        arrayList.Add(nO2array)
        arrayList.Add(nCzArray)
        arrayList.Add(nFp1Array)
        arrayList.Add(nF7Array)
        arrayList.Add(nC3Array)
        arrayList.Add(nT5Array)
        arrayList.Add(nO1Array)

        nWorksheet = nWorkbook.Worksheets("P T")
        Dim nRange1 As Excel.Range = nWorksheet.UsedRange
        Dim nArray1(,) As Object = nRange1.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize1 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize1
            nO1Array1.Add(nArray1(i, Loca + 1))
            nT5Array1.Add(nArray1(i, Loca + 2))
            nC3Array1.Add(nArray1(i, Loca + 3))
            nF7Array1.Add(nArray1(i, Loca + 4))
            nFp1Array1.Add(nArray1(i, Loca + 5))
            nCzArray1.Add(nArray1(i, Loca + 6))
            nO2array1.Add(nArray1(i, Loca + 7))
            nT6Array1.Add(nArray1(i, Loca + 8))
            nC4Array1.Add(nArray1(i, Loca + 9))
            nF8Array1.Add(nArray1(i, Loca + 10))
            nFp2Array1.Add(nArray1(i, Loca + 11))
        Next
        arrayList1.Add(nFp2Array1)
        arrayList1.Add(nF8Array1)
        arrayList1.Add(nC4Array1)
        arrayList1.Add(nT6Array1)
        arrayList1.Add(nO2array1)
        arrayList1.Add(nCzArray1)
        arrayList1.Add(nFp1Array1)
        arrayList1.Add(nF7Array1)
        arrayList1.Add(nC3Array1)
        arrayList1.Add(nT5Array1)
        arrayList1.Add(nO1Array1)

        nWorksheet = nWorkbook.Worksheets("P A")
        Dim nRange2 As Excel.Range = nWorksheet.UsedRange
        Dim nArray2(,) As Object = nRange2.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize2 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize2
            nO1Array2.Add(nArray2(i, Loca + 1))
            nT5Array2.Add(nArray2(i, Loca + 2))
            nC3Array2.Add(nArray2(i, Loca + 3))
            nF7Array2.Add(nArray2(i, Loca + 4))
            nFp1Array2.Add(nArray2(i, Loca + 5))
            nCzArray2.Add(nArray2(i, Loca + 6))
            nO2array2.Add(nArray2(i, Loca + 7))
            nT6Array2.Add(nArray2(i, Loca + 8))
            nC4Array2.Add(nArray2(i, Loca + 9))
            nF8Array2.Add(nArray2(i, Loca + 10))
            nFp2Array2.Add(nArray2(i, Loca + 11))
        Next
        arraylist2.Add(nFp2Array2)
        arraylist2.Add(nF8Array2)
        arraylist2.Add(nC4Array2)
        arraylist2.Add(nT6Array2)
        arraylist2.Add(nO2array2)
        arraylist2.Add(nCzArray2)
        arraylist2.Add(nFp1Array2)
        arraylist2.Add(nF7Array2)
        arraylist2.Add(nC3Array2)
        arraylist2.Add(nT5Array2)
        arraylist2.Add(nO1Array2)

        nWorksheet = nWorkbook.Worksheets("P B")
        Dim nRange3 As Excel.Range = nWorksheet.UsedRange
        Dim nArray3(,) As Object = nRange3.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize3 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize3
            nO1Array3.Add(nArray3(i, Loca + 1))
            nT5Array3.Add(nArray3(i, Loca + 2))
            nC3Array3.Add(nArray3(i, Loca + 3))
            nF7Array3.Add(nArray3(i, Loca + 4))
            nFp1Array3.Add(nArray3(i, Loca + 5))
            nCzArray3.Add(nArray3(i, Loca + 6))
            nO2array3.Add(nArray3(i, Loca + 7))
            nT6Array3.Add(nArray3(i, Loca + 8))
            nC4Array3.Add(nArray3(i, Loca + 9))
            nF8Array3.Add(nArray3(i, Loca + 10))
            nFp2Array3.Add(nArray3(i, Loca + 11))
        Next
        arraylist3.Add(nFp2Array3)
        arraylist3.Add(nF8Array3)
        arraylist3.Add(nC4Array3)
        arraylist3.Add(nT6Array3)
        arraylist3.Add(nO2array3)
        arraylist3.Add(nCzArray3)
        arraylist3.Add(nFp1Array3)
        arraylist3.Add(nF7Array3)
        arraylist3.Add(nC3Array3)
        arraylist3.Add(nT5Array3)
        arraylist3.Add(nO1Array3)

        nWorksheet = nWorkbook.Worksheets("P G")
        Dim nRange4 As Excel.Range = nWorksheet.UsedRange
        Dim narray4(,) As Object = nRange4.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize4 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize4
            nO1Array4.Add(narray4(i, Loca + 1))
            nT5Array4.Add(narray4(i, Loca + 2))
            nC3Array4.Add(narray4(i, Loca + 3))
            nF7Array4.Add(narray4(i, Loca + 4))
            nFp1Array4.Add(narray4(i, Loca + 5))
            nCzArray4.Add(narray4(i, Loca + 6))
            nO2array4.Add(narray4(i, Loca + 7))
            nT6Array4.Add(narray4(i, Loca + 8))
            nC4Array4.Add(narray4(i, Loca + 9))
            nF8Array4.Add(narray4(i, Loca + 10))
            nFp2Array4.Add(narray4(i, Loca + 11))
        Next
        arraylist4.Add(nFp2Array4)
        arraylist4.Add(nF8Array4)
        arraylist4.Add(nC4Array4)
        arraylist4.Add(nT6Array4)
        arraylist4.Add(nO2array4)
        arraylist4.Add(nCzArray4)
        arraylist4.Add(nFp1Array4)
        arraylist4.Add(nF7Array4)
        arraylist4.Add(nC3Array4)
        arraylist4.Add(nT5Array4)
        arraylist4.Add(nO1Array4)

        ListofArray.Add(arrayList)
        ListofArray.Add(arrayList1)
        ListofArray.Add(arraylist2)
        ListofArray.Add(arraylist3)
        ListofArray.Add(arraylist4)
    End Sub
    Private Sub Boutonchrono_Click(sender As Object, e As RoutedEventArgs) Handles Boutonchrono.Click
        chrono()
    End Sub
    Private Sub chrono()
        Dim temps As String()
        Dim Somme As Double
        Dim Maxi As Integer
        Dim tinterval As Integer
        Dim Itembande As Integer
        Dim ISeuil As Integer
        Dim Coef As Double
        Try
            temps = {0 & "s", Floor(ListofArray(Itembande)(1).Count / 15) & "s", Floor((2 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((3 * ListofArray(Itembande)(1).Count) / 15) &
            "s", Floor((4 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((5 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((6 * ListofArray(Itembande)(1).Count) / 15) &
            "s", Floor((7 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((8 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((9 * ListofArray(Itembande)(1).Count) / 15) &
            "s", Floor((10 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((11 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((12 * ListofArray(Itembande)(1).Count) / 15) &
            "s", Floor((13 * ListofArray(Itembande)(1).Count) / 15) & "s", Floor((14 * ListofArray(Itembande)(1).Count) / 15) & "s"}
            For itime As Integer = 1 To 15
                listtemp(itime - 1).Content = temps(itime - 1)
                listtemp(itime - 1).FontSize = 22
            Next
            ISeuil = Convert.ToInt32(textBoxSeuil.Text)
            Maxi = 0
            tinterval = Convert.ToInt32(nbint.Text)
            Itembande = comboBox1.SelectedIndex
            If Nbinterval <> 0 Then
                For iVoie = 1 To Nbvoie
                    For itemps As Integer = 1 To Nbinterval
                        Canvas1.Children.Remove(liste_voie(iVoie - 1).Interval(itemps - 1))
                    Next
                    liste_voie(iVoie - 1).Interval.Clear()

                Next
                Nbinterval = (Int(ListofArray(Itembande)(1).Count / tinterval) - 1)
            Else
                Nbinterval = (Int(ListofArray(Itembande)(1).Count / tinterval) - 1)
            End If
            Dim tableau(Int(ListofArray(Itembande)(1).Count / tinterval) - 1, Nbvoie)
            For xtab As Integer = 0 To Nbvoie - 1
                For imoy As Integer = 1 To Nbinterval
                    Somme = 0
                    For itot = 0 To (tinterval - 1)
                        Somme = Somme + ListofArray(Itembande)(xtab + 11 - Nbvoie)((imoy * tinterval) + itot)
                    Next
                    tableau(imoy, xtab) = Int(Somme / tinterval)
                    If Maxi < Int(Somme / tinterval) Then
                        Maxi = Int(Somme / tinterval)
                    End If
                Next
            Next
            If textBox1.Text = "Fichier Excel de travail" Then
                MsgBox("Merci de choisir un fichier Excel avant de demander le chronogramme")
                Exit Sub
            End If
            If Seuil2.Count > 0 Then
                Seuil2.Clear()
                graph.Clear()
            End If
            For iVoie = 1 To Nbvoie
                Dim Serie As New ObservableCollection(Of GraphPoint)
                Select Case tinterval
                    Case 1
                        Coef = 0.5
                    Case 2 To 4
                        Coef = 0.3
                    Case 5 To 9
                        Coef = 0.15
                    Case Else
                        Coef = 0.05
                End Select
                For itemps As Integer = 1 To Nbinterval + 1
                    Serie.Add((New GraphPoint() With {.PxNum = itemps * tinterval, .Puissance_spectrale = tableau(itemps - 1, iVoie - 1)}))
                    Dim Intervall = New Rectangle()
                    liste_voie(iVoie - 1).Interval.Add(Intervall)
                    If tableau(itemps - 1, iVoie - 1) > ISeuil Then
                        liste_voie(iVoie - 1).Interval(itemps - 1).Height = (tableau(itemps - 1, iVoie - 1) * 100) / Maxi
                    Else
                        liste_voie(iVoie - 1).Interval(itemps - 1).Height = 0
                    End If
                    'liste_voie(iVoie - 1).Interval(itemps - 1).Width = CInt((Lfenetre * 15 / Nbinterval)) - 1.5
                    liste_voie(iVoie - 1).Interval(itemps - 1).Width = CInt(((Lfenetre * 15) / Nbinterval) - Coef * CInt((Lfenetre * 15 / Nbinterval)))
                    liste_voie(iVoie - 1).Interval(itemps - 1).Stroke = liste_voie(iVoie - 1).Color
                    liste_voie(iVoie - 1).Interval(itemps - 1).StrokeThickness = 2
                    liste_voie(iVoie - 1).Interval(itemps - 1).Fill = liste_voie(iVoie - 1).Color
                    Canvas1.Children.Add(liste_voie(iVoie - 1).Interval(itemps - 1))
                    Canvas.SetLeft(Intervall, 70 + CInt((Lfenetre * 15 / Nbinterval) * (itemps - 1) + Coef * CInt((Lfenetre * 15 / Nbinterval))))
                    Canvas.SetTop(Intervall, verticale(iVoie - 1) + liste_voie(2).Label1.ActualHeight / 2 - (liste_voie(iVoie - 1).Interval(itemps - 1).Height) / 2)
                Next
                graph.Add(Serie)
            Next
            Dim Deb As Int32
            Deb = Convert.ToInt32(textBoxSeuil.Text)
            Seuil2.Add((New GraphPoint() With {.PxNum = 0, .Puissance_spectrale = Deb}))
            Seuil2.Add((New GraphPoint() With {.PxNum = graph(0).Count * tinterval, .Puissance_spectrale = Deb}))
            graph.Add(Seuil2)
            While graph.Count < 12
                graph.Add(Seuil2)
            End While
            Tracer()
        Catch ex As Exception
            MsgBox("Le nom de fichier n'est pas un fichier excel valide." & Chr(10) & "Merci d'en choisir un valide", MsgBoxStyle.Critical, "Erreur Critique")
        End Try
    End Sub
    Private Sub Tracer()
        Try

            Fp2line.DataContext = graph(0)
            Fp2line.Background = liste_voie(0).Color
            F8line.DataContext = graph(1)
            F8line.Background = liste_voie(1).Color
            C4line.DataContext = graph(2)
            C4line.Background = liste_voie(2).Color
            T6line.DataContext = graph(3)
            T6line.Background = liste_voie(3).Color
            O2line.DataContext = graph(4)
            O2line.Background = liste_voie(4).Color
            Czline.DataContext = graph(5)
            Czline.Background = liste_voie(5).Color
            Fp1line.DataContext = graph(6)
            Fp1line.Background = liste_voie(6).Color
            F7line.DataContext = graph(7)
            F7line.Background = liste_voie(7).Color
            C3line.DataContext = graph(8)
            C3line.Background = liste_voie(8).Color
            T5line.DataContext = graph(9)
            T5line.Background = liste_voie(9).Color
            O1line.DataContext = graph(10)
            O1line.Background = liste_voie(10).Color
            Fp2line.Opacity = 100
            C4line.Opacity = 100
            F8line.Opacity = 100
            T6line.Opacity = 100
            O2line.Opacity = 100
            Czline.Opacity = 100
            Fp1line.Opacity = 100
            C3line.Opacity = 100
            F7line.Opacity = 100
            T5line.Opacity = 100
            O1line.Opacity = 100
            Seuil1.DataContext = graph(11)
            Seuil1.Background = Brushes.Black
            Seuil1.Opacity = 100
            SeuilDebut.Add((New GraphPoint() With {.PxNum = debut, .Puissance_spectrale = 0}))
            SeuilDebut.Add((New GraphPoint() With {.PxNum = debut, .Puissance_spectrale = AxeY.ActualMaximum}))
            graph.Add(SeuilDebut)
            Seuildeb.DataContext = graph(12)
            Seuildeb.Opacity = 100
            Seuildeb.Background = Brushes.Indigo
            SeuilFinal.Add((New GraphPoint() With {.PxNum = final, .Puissance_spectrale = 0}))
            SeuilFinal.Add((New GraphPoint() With {.PxNum = final, .Puissance_spectrale = AxeY.ActualMaximum}))
            graph.Add(SeuilFinal)
            Seuilfin.DataContext = graph(13)
            Seuilfin.Opacity = 100
            Seuilfin.Background = Brushes.Indigo
        Catch ex As Exception

        End Try
    End Sub
    Private Sub comboBox1_DropDownClosed(sender As Object, e As EventArgs) Handles comboBox1.DropDownClosed
        chrono()
    End Sub
    Private Sub buttonimp_Click(sender As Object, e As RoutedEventArgs) Handles buttonimp.Click
        Dim Rapport As Integer
        Dim Dialog As New PrintDialog()
        Dialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue()
        Dialog.PrintTicket = Dialog.PrintQueue.DefaultPrintTicket
        Dialog.PrintTicket.PageOrientation = PageOrientation.Landscape
        Rapport = Math.Ceiling(100 - (((Pow(21, 2) + Pow(29.7, 2)) / (Pow(Canvas1.ActualHeight * (2.54 / 96), 2) + Pow(Canvas1.ActualWidth * (2.54 / 96), 2))) * 100))
        Dialog.PrintTicket.PageScalingFactor = Rapport - 5
        If Dialog.ShowDialog = True Then
            Dialog.PrintVisual(Canvas1, "My Canvas")
        End If
    End Sub
    Private Sub buttonchange_Click(sender As Object, e As RoutedEventArgs) Handles buttonchange.Click
        If Nbinterval <> 0 Then
            For iVoie = 1 To Nbvoie
                Canvas1.Children.Remove(liste_voie(iVoie - 1).Label1)
                For itemps As Integer = 1 To Nbinterval
                    Canvas1.Children.Remove(liste_voie(iVoie - 1).Interval(itemps - 1))
                Next
            Next
            For itemps = 1 To 15
                Canvas1.Children.Remove(listtemp(itemps - 1))
                Canvas1.Children.Remove(Timeline(itemps - 1))
            Next
        End If
        graph.Clear()
        Nbinterval = 0
        liste_voie.Clear()
        listtemp.Clear()
        Timeline.Clear()
        ListofArray.Clear()
        Seuil2.Clear()
        SeuilDebut.Clear()
        Br.Clear
        Nbvoie = 0
        verticale.Clear
        liste_voie2.Clear()

        SeuilFinal.Clear










        Seuil1.Opacity = 0
        Fp2line.Opacity = 0
        C4line.Opacity = 0
        F8line.Opacity = 0
        T6line.Opacity = 0
        O2line.Opacity = 0
        Czline.Opacity = 0
        Fp1line.Opacity = 0
        C3line.Opacity = 0
        F7line.Opacity = 0
        T5line.Opacity = 0
        O1line.Opacity = 0
        Choice()
    End Sub
    Private Sub buttonCrise_Click(sender As Object, e As RoutedEventArgs) Handles buttonCrise.Click

        Try
            debut = Txtbxdeb.Text
            final = Txtfin.Text
            If final > Dureemax Or debut < 0 Then
                MsgBox("Le chiffre entré est plus important que la durée du tracée" & Chr(10) & "Merci d'en entrer un autre", MsgBoxStyle.Critical, "Erreur Critique")
            Else
                For iVoie As Integer = 1 To Nbvoie
                    For itemps As Integer = 1 To Nbinterval + 1
                        If itemps < debut Or itemps > final Then
                            liste_voie(iVoie - 1).Interval(itemps - 1).Visibility = True
                        Else
                            liste_voie(iVoie - 1).Interval(itemps - 1).Visibility = False
                        End If
                    Next
                Next
                SeuilFinal.Clear()
                SeuilDebut.Clear()
                Tracer()
            End If
        Catch ex As Exception
            MsgBox("un nombre entier est attendu" & Chr(10) & "Merci d'en entrer un", MsgBoxStyle.Critical, "Erreur Critique")
        End Try

    End Sub
End Class

