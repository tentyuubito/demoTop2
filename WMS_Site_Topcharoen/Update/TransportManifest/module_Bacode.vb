Imports GenCode128
Module module_Bacode
    Public Function GenCodeToByte(ByVal pText As String) As Byte()
        Try

            Dim ResultByte() As Byte


            Dim pic As System.Drawing.Image = Code128Rendering.MakeBarcodeImage(pText, 1, False)
            Dim BitmapConverter As System.ComponentModel.TypeConverter

            'Me.PictureBox1.Image = pic
            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
            Return ResultByte

        Catch ex As Exception

            Return Nothing
        End Try

    End Function
End Module

