Imports Microsoft.VisualBasic
Imports System
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Media
Imports System.Windows.Shapes
Class MainWindow

    Private Sub Bouton1_Click(sender As Object, e As RoutedEventArgs) Handles Bouton1.Click
        'recupération de l'image
        Dim Mygrid2 As New Grid
        Dim ImgTemp As New BitmapImage
        ImgTemp.BeginInit()
        ImgTemp.UriSource = New Uri("C:\Users\mathieu\Documents\Visual Studio 2013\Projects\MPSI\MPSI\Resources\scalp.jpg", UriKind.RelativeOrAbsolute)
        ImgTemp.EndInit()
        ' Draw the Image
        Dim myImage2 As New Image()
        Dim myBrush As New ImageBrush()
        myImage2.Source = ImgTemp
        myBrush.ImageSource = myImage2.Source
        myGrid.Background = myBrush

        ' Add a Line Element
        Dim myLine As New Line()

        myLine.Stroke = Brushes.LightSteelBlue
        myLine.X1 = 1
        myLine.X2 = 1
        myLine.Y1 = 500
        myLine.Y2 = 500
        myLine.HorizontalAlignment = HorizontalAlignment.Left
        myLine.VerticalAlignment = VerticalAlignment.Center
        myLine.StrokeThickness = 2
        GridEllipse.Children.Add(myLine)
        ' add a circle
        ' Create a StackPanel to contain the shape.


        ' Create a red Ellipse.
        Dim myEllipse As New Ellipse()

        ' Create a SolidColorBrush with a red color to fill the 
        ' Ellipse with.
        Dim mySolidColorBrush As New SolidColorBrush()

        ' Describes the brush's color using RGB values. 
        ' Each value has a range of 0-255.
        mySolidColorBrush.Color = Color.FromArgb(255, 255, 255, 0)
        myEllipse.Fill = mySolidColorBrush
        myEllipse.StrokeThickness = 5

        ' Set the width and height of the Ellipse.
        myEllipse.Width = 20
        myEllipse.Height = 20
        '


        ' Add the Ellipse to the StackPanel.
        myGrid.Children.Add(myEllipse)
        myEllipse.Margin = New Thickness(200, 200, 0, 0)
        MsgBox("OK")
    End Sub
End Class
