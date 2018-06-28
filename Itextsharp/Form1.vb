﻿
Imports System.IO
Imports Itextsharp.text
Imports Itextsharp.text.pdf.parser
Imports Itextsharp.text.pdf


Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

        ''//The file that we are creating
        Dim WorkingFile = Path.Combine(WorkingFolder, "Output.pdf")
        Debug.Print("Current Directory:" + WorkingFolder)

        ''//Create our file with an exclusive writer lock
        Using FS As New FileStream(WorkingFile, FileMode.Create, FileAccess.Write, FileShare.None)
            ''//Create our PDF document
            Using Doc As New Document(PageSize.LETTER)
                ''//Bind our PDF object to the physical file using a PdfWriter
                Using Writer = PdfWriter.GetInstance(Doc, FS)
                    ''//Open our document for writing
                    Doc.Open()

                    ''//Insert a blank page
                    Doc.NewPage()

                    ''//Add a simple paragraph with text
                    Doc.Add(New Paragraph("Hello World"))
                    Doc.Add(New Paragraph("這是測試"))

                    ''//Close our document
                    Doc.Close()
                End Using
            End Using
        End Using

        Me.Close()


    End Sub
End Class