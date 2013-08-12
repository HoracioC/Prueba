Imports System.Security.Cryptography
Imports System.Net.NetworkInformation
Imports System.Net

Public Class Tools
    Shared Sub Mensaje_OK(ByVal Pagina As System.Web.UI.Control)
        ScriptManager.RegisterStartupScript(Pagina, GetType(System.String), "r_msg1()", "alert('La operación se realizó correctamente.');", True)
    End Sub

    Shared Sub Mensaje_NO(ByVal Pagina As System.Web.UI.Control)
        ScriptManager.RegisterStartupScript(Pagina, GetType(System.String), "r_msg2()", "alert('No se logró realizar la operación.');", True)
    End Sub

    Shared Sub Mensaje_N(ByVal Pagina As System.Web.UI.Control, ByVal msg As String)
        ScriptManager.RegisterStartupScript(Pagina, GetType(System.String), "r_msg3()", "alert('" & msg & "');", True)
    End Sub

    Public Shared Function Encrypt(ByVal Texto As String) As String
        Texto = My.User.Name & "xNWe@1q" & Texto & Date.Now.DayOfWeek & Date.Now.Day
        'Creamos un objeto de codificación Unicode que
        'representa una codificación UTF-16 de caracteres Unicode. 
        Dim Codificar As New UnicodeEncoding()
        'Declaramos una matriz (array) de tipo Byte para recuperar dentro los bytes del texto
        'utilizando el objeto Codificar
        Dim ByteTexto() As Byte = Codificar.GetBytes(Texto)
        'Instanciamos el objeto MD5 
        Dim Md5 As New MD5CryptoServiceProvider()
        'Se calcula el Hash del Texto en bytes 
        Dim ByteHash() As Byte = Md5.ComputeHash(ByteTexto)
        'convertimos el texto en bytes en texto legible(cadena) 
        Return Convert.ToBase64String(ByteHash)
        'Eliminamos los objetos usados con Nothing
        Codificar = Nothing
        ByteTexto = Nothing
    End Function

    Public Shared Function CleanKey(ByVal K As String) As String
        Try 'Normalizar el string, Leer el valor manualmente porque mediante el querystring omite el caracter "+"            
            K = K.Replace("%3d%3d", "==")
            K = K.Replace("%2f", "/")
            K = K.Substring(K.IndexOf("="), K.Length - K.IndexOf("="))
            K = K.Substring(1, K.IndexOf("==") + 1)
            Return K
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function keyIsEqual(ByVal keySinEncriptar As String, ByVal keyEncriptada As String) As Boolean
        If Encrypt(keySinEncriptar) = CleanKey(keyEncriptada) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function NumeroToString(ByVal StringValor As String) As String
        If StringValor = 10.0 Then
            Return "DIEZ"
        End If

        Dim ReturnValue As String = ""

        Dim Numero1 As String = StringValor.Substring(0, 1)
        If Numero1 = "6" Then
            ReturnValue = "SEIS"
        End If
        If Numero1 = "7" Then
            ReturnValue = "SIETE"
        End If
        If Numero1 = "8" Then
            ReturnValue = "OCHO"
        End If
        If Numero1 = "9" Then
            ReturnValue = "NUEVE"
        End If
        If Numero1 = "0" Then
            ReturnValue = "CERO"
        End If

        ReturnValue = ReturnValue & " PUNTO "

        Try
            Dim Numero2 As String = StringValor.Substring(2, 1)

            If Numero2 = "1" Then
                ReturnValue = ReturnValue & "UNO"
            End If
            If Numero2 = "2" Then
                ReturnValue = ReturnValue & "DOS"
            End If
            If Numero2 = "3" Then
                ReturnValue = ReturnValue & "TRES"
            End If
            If Numero2 = "4" Then
                ReturnValue = ReturnValue & "CUATRO"
            End If
            If Numero2 = "5" Then
                ReturnValue = ReturnValue & "CINCO"
            End If
            If Numero2 = "6" Then
                ReturnValue = ReturnValue & "SEIS"
            End If
            If Numero2 = "7" Then
                ReturnValue = ReturnValue & "SIETE"
            End If
            If Numero2 = "8" Then
                ReturnValue = ReturnValue & "OCHO"
            End If
            If Numero2 = "9" Then
                ReturnValue = ReturnValue & "NUEVE"
            End If
        Catch
            ReturnValue = ReturnValue & "CERO"
        End Try

        Return ReturnValue
    End Function

    Public Shared Function EvaluarError(ByVal NumError As Integer, Mensaje As String) As String

        Return Mensaje

    End Function

    Public Shared Function FormatoFecha(ByVal Fecha As String) As String
        Try
            Return Fecha.Substring(6, 4) & "-" & Fecha.Substring(3, 2) & "-" & Fecha.Substring(0, 2)
        Catch ex As Exception

        End Try
        Return Nothing
    End Function


    Public Class GridDecorator
        Public Shared Sub MergeRows(gridView As GridView, margerow1 As Integer)
            For rowIndex As Integer = gridView.Rows.Count - 2 To 0 Step -1
                Dim row As GridViewRow = gridView.Rows(rowIndex)
                Dim previousRow As GridViewRow = gridView.Rows(rowIndex + 1)

                For i As Integer = margerow1 To margerow1
                    If row.Cells(i).Text = previousRow.Cells(i).Text Then
                        row.Cells(i).RowSpan = If(previousRow.Cells(i).RowSpan < 2, 2, previousRow.Cells(i).RowSpan + 1)
                        previousRow.Cells(i).Visible = False
                    End If
                Next
            Next
        End Sub

        Public Shared Sub MergeRowsAnte(gridView As GridView, margerow1 As Integer)
            For rowIndex As Integer = gridView.Rows.Count - 2 To 0 Step -1
                Dim row As GridViewRow = gridView.Rows(rowIndex)
                Dim previousRow As GridViewRow = gridView.Rows(rowIndex + 1)

                For i As Integer = margerow1 To margerow1
                    If row.Cells(i).Text = previousRow.Cells(i).Text Then
                        row.Cells(i).RowSpan = If(previousRow.Cells(i).RowSpan < 2, 2, previousRow.Cells(i).RowSpan + 1)
                        previousRow.Cells(i + 1).Visible = False
                    End If
                Next
            Next
        End Sub
    End Class


    Public Shared Function CalcularAnios(FechaInicio As Date, FechaActual As Date) As Integer

        Dim diaActual As Integer = DatePart("d", FechaActual)
        Dim mesActual As Integer = DatePart("m", FechaActual)
        Dim anioActual As Integer = DatePart("yyyy", FechaActual)

        Dim diaInicio As Integer = DatePart("d", FechaInicio)
        Dim mesInicio As Integer = DatePart("m", FechaInicio)
        Dim anioInicio As Integer = DatePart("yyyy", FechaInicio)

        Dim B As Integer = 0
        Dim Mes As Integer = mesInicio - 1

        ' si el mes es febrero
        If (Mes = 2) Then   ' *
            If ((anioActual / 4 = 0 And anioActual / 100.0! = 0) Or anioActual / 400 = 0) Then
                B = 29
            Else
                B = 28
            End If
        ElseIf (Mes <= 7) Then  '*
            If (Mes = 0) Then
                B = 31
            ElseIf (Mes / 2 = 0) Then
                B = 30
            Else
                B = 31
            End If

        ElseIf (Mes > 7) Then
            If (Mes / 2 = 0) Then
                B = 31
            Else
                'Cambio a la rutina Original
                B = 31
            End If
        End If

        Dim Anios As Integer
        Dim Meses As Integer
        Dim Dias As Integer

        If ((anioInicio > anioActual) Or (anioInicio = anioActual And mesInicio > mesActual) Or (anioInicio = anioActual And mesInicio = mesActual And diaInicio > diaActual)) Then
        Else
            If (mesInicio <= mesActual) Then
                Anios = anioActual - anioInicio
                If (diaInicio <= diaActual) Then
                    Meses = mesActual - mesInicio
                    Dias = diaActual - diaInicio
                Else
                    If (mesActual = mesInicio) Then
                        Anios = Anios - 1
                    End If
                    Meses = (mesActual - mesInicio - 1 + 12) / 12
                    Dias = B - (diaInicio - diaActual)
                End If
            Else
                Anios = anioActual - anioInicio - 1

                If (diaInicio > diaActual) Then
                    Meses = mesActual - mesInicio - 1 + 12
                    Dias = B - (diaInicio - diaActual)
                Else
                    Meses = mesActual - mesInicio + 12
                    Dias = diaActual - diaInicio
                End If
            End If

        End If '*
        Return Anios

    End Function

    Public Shared Function EstadoCenxion() As Boolean
        Dim isNetworkAvailable1 = IsNetworkAvailable(500)
        If Not isNetworkAvailable1 Then
            Return False
        End If
        Dim ncsiRequest = IsNCSIConnected()
        If ncsiRequest = False Then
            Return False
        End If
        'Dim dnsLookupNCSIResolved = IsDnsLookupResolved()

        'If ncsiRequest OrElse (Not ncsiRequest AndAlso dnsLookupNCSIResolved) Then
        '    Return True
        'End If
        Return True
    End Function

    Public Shared Function IsNetworkAvailable(ByVal minimumSpeed As Long) As Boolean
        If Not NetworkInterface.GetIsNetworkAvailable() Then
            Return False
        End If

        For Each ni As NetworkInterface In NetworkInterface.GetAllNetworkInterfaces()
            If ni.OperationalStatus <> OperationalStatus.Up Or ni.NetworkInterfaceType = NetworkInterfaceType.Loopback Or ni.NetworkInterfaceType = NetworkInterfaceType.Tunnel Then
                Continue For
            End If
            If ni.Speed < minimumSpeed Then
                Continue For
            End If
            If ((ni.Description.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0) Or (ni.Name.IndexOf("virtual", StringComparison.OrdinalIgnoreCase) >= 0)) Then
                Continue For
            End If

            Return True
        Next
        Return False
    End Function

    Public Shared Function IsNCSIConnected() As Boolean
        Try
            Dim request As HttpWebRequest = DirectCast(WebRequest.Create("http://www.msftncsi.com/ncsi.txt"), HttpWebRequest)
            Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
            Return response.StatusCode = HttpStatusCode.OK
        Catch generatedExceptionName As Exception
            Return False
        End Try
    End Function

End Class

