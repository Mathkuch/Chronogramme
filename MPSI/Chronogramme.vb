﻿Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Shapes
Imports System.Threading
Imports System.Windows.Threading
Imports System.ComponentModel
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Windows.Data
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media.Imaging
Imports System.Windows.Navigation
Imports System.Collections



Class MainWindow
    Dim Hauteurfen As Integer
    Dim marge As Integer
    Dim Timeline As New List(Of Line)
    Dim temps = {"0s", "100s", "200s", "300s", "400s", "500s", "600s", "700s", "800s", "900s", "1000s", "1100s", "1200s", "1300s", "1400s"}
    Dim listtemp As New List(Of Label)
    Dim Lfenetre As Integer
    Dim Vert(11) As Integer
    Dim Interval1 As New List(Of Rectangle)
    Dim Br As New List(Of Brush)
    'Puissance
    '''données Excel
    Dim nO1Array, nT5Array, nC3Array, nF7Array, nFp1Array, nCzArray, nO2array, nT6Array, nC4Array, nF8Array, nFp2Array As New List(Of Double)
    Dim nO1Array1, nT5Array1, nC3Array1, nF7Array1, nFp1Array1, nCzArray1, nO2array1, nT6Array1, nC4Array1, nF8Array1, nFp2Array1 As New List(Of Double)
    Dim nO1Array2, nT5Array2, nC3Array2, nF7Array2, nFp1Array2, nCzArray2, nO2array2, nT6Array2, nC4Array2, nF8Array2, nFp2Array2 As New List(Of Double)
    Dim nO1Array3, nT5Array3, nC3Array3, nF7Array3, nFp1Array3, nCzArray3, nO2array3, nT6Array3, nC4Array3, nF8Array3, nFp2Array3 As New List(Of Double)
    Dim nO1Array4, nT5Array4, nC3Array4, nF7Array4, nFp1Array4, nCzArray4, nO2array4, nT6Array4, nC4Array4, nF8Array4, nFp2Array4 As New List(Of Double)
    Dim arrayList, arrayList1, arraylist2, arraylist3, arraylist4 As New List(Of List(Of Double))
    Dim ListofArray As New List(Of List(Of List(Of Double)))
    Dim liste_voie As New List(Of VoieEEG)
    Private Sub windows1_Loaded(sender As Object, e As RoutedEventArgs) Handles windows1.Loaded
        Dim B As Integer
        B = windows1.ActualWidth
        Hauteurfen = windows1.ActualHeight
        marge = 450
        ' Initialisation des voie
        Dim Fp2 As New VoieEEG(1, Hauteurfen, marge)
        Dim C4 As New VoieEEG(2, Hauteurfen, marge)
        Dim f8 As New VoieEEG(3, Hauteurfen, marge)
        Dim T6 As New VoieEEG(4, Hauteurfen, marge)
        Dim O2 As New VoieEEG(5, Hauteurfen, marge)
        Dim cz As New VoieEEG(6, Hauteurfen, marge)
        Dim fp1 As New VoieEEG(7, Hauteurfen, marge)
        Dim c3 As New VoieEEG(8, Hauteurfen, marge)
        Dim f7 As New VoieEEG(9, Hauteurfen, marge)
        Dim t5 As New VoieEEG(10, Hauteurfen, marge)
        Dim o1 As New VoieEEG(11, Hauteurfen, marge)
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

        For i As Integer = 1 To 11
            Canvas1.Children.Add(liste_voie(i - 1).Label1)
            Canvas.SetTop(liste_voie(i - 1).Label1, liste_voie(i - 1).Vert_pos)
            Canvas.SetLeft(liste_voie(i - 1).Label1, liste_voie(i - 1).Hor_pos)
        Next
        ' lignes représentant le temps /100s
        For iline As Integer = 1 To 15
            Dim ligne As New Line
            Dim label1 As New Label
            listtemp.Add(label1)
            Canvas1.Children.Add(listtemp(iline - 1))
            Canvas.SetTop(label1, 0)
            Lfenetre = (B - 70 - liste_voie(2 - 1).Label1.ActualWidth) / 15
            Canvas.SetLeft(label1, 65 + (Lfenetre * (iline - 1)))
            listtemp(iline - 1).Content = temps(iline - 1)
            Timeline.Add(ligne)
            Timeline(iline - 1).Stroke = Brushes.LightSteelBlue
            Timeline(iline - 1).X1 = 70 + (Lfenetre * (iline - 1))
            Timeline(iline - 1).X2 = 70 + (Lfenetre * (iline - 1))
            Timeline(iline - 1).Y1 = 40
            Timeline(iline - 1).Y2 = (Hauteurfen - marge) + 25
            Timeline(iline - 1).StrokeThickness = 0.5
            Canvas1.Children.Add(Timeline(iline - 1))
        Next
        '''''Bouton pour injecter le fichier Excel
        Panneau_Commande()
    End Sub
    Private Sub Boutonchoix_Click(sender As Object, e As RoutedEventArgs) Handles Boutonchoix.Click
        Dim nOFD As New Microsoft.Win32.OpenFileDialog()
        Dim nResultOFD As Nullable(Of Boolean) = nOFD.ShowDialog()
        If nResultOFD = True Then
            textBox1.Text = nOFD.FileName
            readExcelFile()
        End If
        MsgBox(nC3Array.Count)
    End Sub
    Private Sub readExcelFile()
        Dim nApp As Excel.Application
        Dim nWorkbook As Excel.Workbook
        Dim nWorksheet As Excel.Worksheet
        nApp = New Excel.Application
        nWorkbook = nApp.Workbooks.Open(textBox1.Text)
        nWorksheet = nWorkbook.Worksheets("P D")
        Dim nRange As Excel.Range = nWorksheet.UsedRange
        Dim nArray(,) As Object = nRange.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize
            nO1Array.Add(nArray(i, 15))
            nT5Array.Add(nArray(i, 16))
            nC3Array.Add(nArray(i, 17))
            nF7Array.Add(nArray(i, 18))
            nFp1Array.Add(nArray(i, 19))
            nCzArray.Add(nArray(i, 20))
            nO2array.Add(nArray(i, 21))
            nT6Array.Add(nArray(i, 22))
            nC4Array.Add(nArray(i, 23))
            nF8Array.Add(nArray(i, 24))
            nFp2Array.Add(nArray(i, 25))
        Next
        arrayList.Add(nFp2Array)
        arrayList.Add(nF8Array)
        arrayList.Add(nC4Array)
        arrayList.Add(nT6Array)
        arrayList.Add(nO2array)
        arrayList.Add(nCzArray)
        arrayList.Add(nFp1Array)
        arrayList.Add(nC3Array)
        arrayList.Add(nF7Array)
        arrayList.Add(nT5Array)
        arrayList.Add(nO1Array)

        nWorksheet = nWorkbook.Worksheets("P T")
        Dim nRange1 As Excel.Range = nWorksheet.UsedRange
        Dim nArray1(,) As Object = nRange1.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize1 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize1
            nO1Array1.Add(nArray1(i, 15))
            nT5Array1.Add(nArray1(i, 16))
            nC3Array1.Add(nArray1(i, 17))
            nF7Array1.Add(nArray1(i, 18))
            nFp1Array1.Add(nArray1(i, 19))
            nCzArray1.Add(nArray1(i, 20))
            nO2array1.Add(nArray1(i, 21))
            nT6Array1.Add(nArray1(i, 22))
            nC4Array1.Add(nArray1(i, 23))
            nF8Array1.Add(nArray1(i, 24))
            nFp2Array1.Add(nArray1(i, 25))
        Next
        arrayList1.Add(nFp2Array1)
        arrayList1.Add(nF8Array1)
        arrayList1.Add(nC4Array1)
        arrayList1.Add(nT6Array1)
        arrayList1.Add(nO2array1)
        arrayList1.Add(nCzArray1)
        arrayList1.Add(nFp1Array1)
        arrayList1.Add(nC3Array1)
        arrayList1.Add(nF7Array1)
        arrayList1.Add(nT5Array1)
        arrayList1.Add(nO1Array1)

        nWorksheet = nWorkbook.Worksheets("P A")
        Dim nRange2 As Excel.Range = nWorksheet.UsedRange
        Dim nArray2(,) As Object = nRange2.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize2 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize2
            nO1Array2.Add(nArray2(i, 15))
            nT5Array2.Add(nArray2(i, 16))
            nC3Array2.Add(nArray2(i, 17))
            nF7Array2.Add(nArray2(i, 18))
            nFp1Array2.Add(nArray2(i, 19))
            nCzArray2.Add(nArray2(i, 20))
            nO2array2.Add(nArray2(i, 21))
            nT6Array2.Add(nArray2(i, 22))
            nC4Array2.Add(nArray2(i, 23))
            nF8Array2.Add(nArray2(i, 24))
            nFp2Array2.Add(nArray2(i, 25))
        Next
        arraylist2.Add(nFp2Array2)
        arraylist2.Add(nF8Array2)
        arraylist2.Add(nC4Array2)
        arraylist2.Add(nT6Array2)
        arraylist2.Add(nO2array2)
        arraylist2.Add(nCzArray2)
        arraylist2.Add(nFp1Array2)
        arraylist2.Add(nC3Array2)
        arraylist2.Add(nF7Array2)
        arraylist2.Add(nT5Array2)
        arraylist2.Add(nO1Array2)

        nWorksheet = nWorkbook.Worksheets("P B")
        Dim nRange3 As Excel.Range = nWorksheet.UsedRange
        Dim nArray3(,) As Object = nRange3.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize3 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize3
            nO1Array3.Add(nArray3(i, 15))
            nT5Array3.Add(nArray3(i, 16))
            nC3Array3.Add(nArray3(i, 17))
            nF7Array3.Add(nArray3(i, 18))
            nFp1Array3.Add(nArray3(i, 19))
            nCzArray3.Add(nArray3(i, 20))
            nO2array3.Add(nArray3(i, 21))
            nT6Array3.Add(nArray3(i, 22))
            nC4Array3.Add(nArray3(i, 23))
            nF8Array3.Add(nArray3(i, 24))
            nFp2Array3.Add(nArray3(i, 25))
        Next
        arraylist3.Add(nFp2Array3)
        arraylist3.Add(nF8Array3)
        arraylist3.Add(nC4Array3)
        arraylist3.Add(nT6Array3)
        arraylist3.Add(nO2array3)
        arraylist3.Add(nCzArray3)
        arraylist3.Add(nFp1Array3)
        arraylist3.Add(nC3Array3)
        arraylist3.Add(nF7Array3)
        arraylist3.Add(nT5Array3)
        arraylist3.Add(nO1Array3)

        nWorksheet = nWorkbook.Worksheets("P G")
        Dim nRange4 As Excel.Range = nWorksheet.UsedRange
        Dim narray4(,) As Object = nRange4.Value(Excel.XlRangeValueDataType.xlRangeValueDefault)
        Dim nSize4 As Integer = nArray.GetUpperBound(0)
        For i As Integer = 2 To nSize4
            nO1Array4.Add(narray4(i, 15))
            nT5Array4.Add(narray4(i, 16))
            nC3Array4.Add(narray4(i, 17))
            nF7Array4.Add(narray4(i, 18))
            nFp1Array4.Add(narray4(i, 19))
            nCzArray4.Add(narray4(i, 20))
            nO2array4.Add(narray4(i, 21))
            nT6Array4.Add(narray4(i, 22))
            nC4Array4.Add(narray4(i, 23))
            nF8Array4.Add(narray4(i, 24))
            nFp2Array4.Add(narray4(i, 25))
        Next
        arraylist4.Add(nFp2Array4)
        arraylist4.Add(nF8Array4)
        arraylist4.Add(nC4Array4)
        arraylist4.Add(nT6Array4)
        arraylist4.Add(nO2array4)
        arraylist4.Add(nCzArray4)
        arraylist4.Add(nFp1Array4)
        arraylist4.Add(nC3Array4)
        arraylist4.Add(nF7Array4)
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
        If textBox1.Text = "Fichier Excel de travail" Then
            MsgBox("Merci de choisir un fichier Excel avant de demander le chronogramme")
            Exit Sub
        End If
        Dim Itembande As Integer
        Itembande = comboBox1.SelectedIndex
        Dim Nbinterval As Integer = 150
        For iVoie = 1 To 11
            If liste_voie(iVoie - 1).Interval.Count > 0 Then
                For itemps As Integer = 1 To Nbinterval
                    Canvas1.Children.Remove(liste_voie(iVoie - 1).Interval(itemps - 1))
                Next
                liste_voie(iVoie - 1).Interval.Clear()
            End If

            For itemps As Integer = 1 To Nbinterval
                Dim Intervall = New Rectangle()
                liste_voie(iVoie - 1).Interval.Add(Intervall)
                liste_voie(iVoie - 1).Interval(itemps - 1).Height = ListofArray(Itembande)(iVoie - 1)(itemps)
                liste_voie(iVoie - 1).Interval(itemps - 1).Width = (Lfenetre * 15 / Nbinterval) - 1.5
                liste_voie(iVoie - 1).Interval(itemps - 1).Stroke = liste_voie(iVoie - 1).Color
                liste_voie(iVoie - 1).Interval(itemps - 1).StrokeThickness = 2
                liste_voie(iVoie - 1).Interval(itemps - 1).Fill = liste_voie(iVoie - 1).Color
                Canvas1.Children.Add(liste_voie(iVoie - 1).Interval(itemps - 1))
                Canvas.SetLeft(Intervall, 70 + Lfenetre * 15 / Nbinterval * (itemps - 1))
                Canvas.SetTop(Intervall, liste_voie(iVoie - 1).Vert_pos + liste_voie(2).Label1.ActualHeight / 2 - (liste_voie(iVoie - 1).Interval(itemps - 1).Height) / 2)
            Next
        Next
    End Sub
    Private Sub Panneau_Commande()
        Boutonchoix.Content = "Choisir un fichier excel"
        Boutonchoix.Height = 30
        Boutonchoix.Width = 150
        Canvas.SetTop(Boutonchoix, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        Canvas.SetLeft(Boutonchoix, 20)
        Canvas.SetTop(Boutonchrono, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        Canvas.SetLeft(Boutonchrono, Boutonchoix.ActualWidth + comboBox1.ActualWidth + 60)
        Canvas.SetTop(textBox1, (((Hauteurfen - marge) / 11) * 11 + 25 + 75))
        Canvas.SetLeft(textBox1, 20)
        comboBox1.Items.Add("Bande Delta")
        comboBox1.Items.Add("Bande Theta")
        comboBox1.Items.Add("Bande Alpha")
        comboBox1.Items.Add("Bande Beta")
        comboBox1.Text = "Bande Delta"
        Canvas.SetLeft(comboBox1, 20 + Boutonchoix.ActualWidth + 20)
        Canvas.SetTop(comboBox1, (((Hauteurfen - marge) / 11) * 11 + 25 + 30))
        textBox1.Text = "Fichier Excel de travail"
        Canvas.SetLeft(Seuil, 20)
        Canvas.SetTop(Seuil, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75)
        Canvas.SetTop(textBoxSeuil, (((Hauteurfen - marge) / 11) * 11 + 25 + 30) + 75)
        Canvas.SetLeft(textBoxSeuil, 20 + 140 + 20)
    End Sub

    Private Sub comboBox1_DropDownClosed(sender As Object, e As EventArgs) Handles comboBox1.DropDownClosed
        chrono()
    End Sub
End Class
