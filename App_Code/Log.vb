'░ LogBeta 1.0  Octavio Gutiérrez ░

Imports System.Xml

Public Class LogXML
    Public Shared ruta As String = System.Web.HttpContext.Current.Server.MapPath(Nothing)

    Private Shared Sub LogCreate(ByVal ComandoVar As String, ByVal DescriVar As String, ByVal Elemento As String)
        Dim comando As String = ComandoVar
        Dim Descri As String = DescriVar
        Dim Fecha As String = Date.Now.Date
        Dim Hora As String = Date.Now.TimeOfDay.ToString

        Dim settings As New XmlWriterSettings()
        settings.Indent = True
        settings.NewLineOnAttributes = True
        Using writer As XmlWriter = XmlWriter.Create(ruta & "\App_Data\log.xml", settings)
            writer.WriteStartDocument()

            writer.WriteStartElement("Log")
            writer.WriteStartElement(Elemento)
            writer.WriteStartAttribute("Comando")
            writer.WriteValue(comando)
            writer.WriteEndAttribute()

            writer.WriteStartAttribute("Descripción")
            writer.WriteValue(Descri)
            writer.WriteEndAttribute()

            writer.WriteStartAttribute("Fecha")
            writer.WriteValue(Fecha)
            writer.WriteEndAttribute()

            writer.WriteStartAttribute("Hora")
            writer.WriteValue(Hora)
            writer.WriteEndAttribute()

            writer.WriteEndElement()
            writer.WriteEndDocument()
            writer.Flush()
        End Using

    End Sub

    Private Shared Sub LogAdd(ByVal ComandoVar As String, ByVal DescriVar As String, ByVal Elemento As String)
        Dim xml_Document As New XmlDocument()
        xml_Document.Load(ruta & "\App_Data\log.xml")

        Dim Evento As XmlElement = xml_Document.CreateElement(Elemento)
        Dim Comando As XmlAttribute = Evento.SetAttributeNode("Comando", Nothing)
        Dim Descri As XmlAttribute = Evento.SetAttributeNode("Descripción", Nothing)
        Dim Fecha As XmlAttribute = Evento.SetAttributeNode("Fecha", Nothing)
        Dim Hora As XmlAttribute = Evento.SetAttributeNode("Hora", Nothing)

        Comando.Value = ComandoVar
        Descri.Value = DescriVar
        Fecha.Value = Date.Now.Date
        Hora.Value = Date.Now.TimeOfDay.ToString

        xml_Document.DocumentElement.AppendChild(Evento)
        xml_Document.Save(ruta & "\App_Data\log.xml")

    End Sub

    Public Shared Sub Log(ByVal Comando As String, ByVal Descri As String)
        Try
            If System.IO.File.Exists(ruta & "\App_Data\log.xml") Then
                LogAdd(Comando, Descri, "Evento")
            Else
                LogCreate(Comando, Descri, "Evento")
            End If
        Catch ex As Exception
            'ni pex
        End Try
    End Sub

    Public Shared Sub LogEx(ByVal Comando As String, ByVal Descri As String)
        Try
            If System.IO.File.Exists(ruta & "\App_Data\log.xml") Then
                LogAdd(Comando, Descri, "Excepcion")
            Else
                LogCreate(Comando, Descri, "Excepcion")
            End If
        Catch ex As Exception
            'ni pex
        End Try
    End Sub
End Class

