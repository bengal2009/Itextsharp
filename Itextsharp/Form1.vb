
Imports System.IO
Imports Itextsharp.text
Imports Itextsharp.text.pdf.parser
Imports Itextsharp.text.pdf


Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        'Dim brown As New Font(Font.COURIER, 9.0F, Font.NORMAL, New Color(163, 21, 21));
        'Dim brown2 As New Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Bold)
        'Dim lightblue As New Font(courier, 9.0F, Font.NORMAL, New Color(43, 145, 175));

        'Dim courier As New Font(Font.COURIER, 9.0F);

        ''//The file that we are creating
        Dim WorkingFile = System.IO.Path.Combine(WorkingFolder, "Output.pdf")
        Debug.Print("Current Directory:" + WorkingFolder)
        Dim arial As BaseFont = BaseFont.CreateFont("c:\windows\fonts\KAIU.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        Dim Font1 As New Font(arial, 24)
        Dim c1 As New Chunk("A chunk represents an isolated string. 測試")
        Dim c3 As New Chunk("A chunk represents an isolated string. ")
        Dim phrase As New Phrase()
        Dim p2 As New Phrase()
        Dim Listest As New List(List.UNORDERED)
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
                    Doc.AddTitle("Hello World example")
                    Doc.AddSubject("This is an Example 4 of Chapter 1 of Book 'iText in Action'")
                    Doc.AddKeywords("Metadata, iTextSharp 5.4.4, Chapter 1, Tutorial")
                    Doc.AddCreator("iTextSharp 5.4.4")
                    Doc.AddAuthor("Debopam Pal")
                    Doc.AddHeader("Nothing", "No Header")
                    ''//Add a simple paragraph with text
                    Doc.Add(New Paragraph("Hello World", Font1))
                    Doc.Add(New Paragraph("這是測試", Font1))
                    c1.SetUnderline(0.5F, -1.5F)

                    For i = 1 To 4
                        Doc.Add(c1)
                    Next
                    For i = 1 To 4
                        Doc.Add(c3)
                    Next
                    ''//Close our document

                    p2.Add(c1)
                    p2.Add("11111")
                    Listest.Add("1")
                    Listest.Add("2")
                    Listest.Add("3")


                    Doc.Add(p2)
                    Doc.Add(Listest)
                    Doc.Close()
                End Using
            End Using
        End Using

        Me.Close()


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub
End Class
