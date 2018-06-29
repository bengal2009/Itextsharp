
Imports System.IO
Imports Itextsharp.text
Imports Itextsharp.text.pdf.parser
Imports Itextsharp.text.pdf






Public Class Form2


    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim FONT1 As BaseFont = BaseFont.CreateFont("c:\windows\fonts\KAIU.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED,)
        Dim smallfont = FontFactory.GetFont("Arial", 12)
        Dim WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
        Dim WorkingFile = System.IO.Path.Combine(WorkingFolder, "Output-2.pdf")
        Debug.Print("Current Directory:" + WorkingFolder)
        Dim arial As BaseFont = BaseFont.CreateFont("c:\windows\fonts\KAIU.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        Dim Font1 As New Font(arial, 24, 1, BaseColor.BLUE)
        Dim c1 As New Chunk("A chunk represents an isolated string. 測試")
        Dim c3 As New Chunk("A chunk represents an isolated string. ")
        Dim phrase As New Phrase()
        Dim p2 As New Phrase()
        Dim Listest As New List(List.UNORDERED)
        Dim Anchor = New Anchor("www.google.com", Font1)
        Dim basicTable = New PdfPTable(2)
        Dim png = Itextsharp.text.Image.GetInstance("C:\\Users\\jackiechen\\Pictures\\DK-3965.jpg")
        Dim barcodetest = New Barcode128()
        'Dim cb As PdfContentByte = doc.DirectContent

        ''//Create our file with an exclusive writer lock
        Dim cell1 = New PdfPCell(png, True)
        barcodetest.CodeType = Barcode.CODE128_UCC
        barcodetest.Code = "This is a world!"
        Dim barocdeimg As Itextsharp.text.Image

        Using FS As New FileStream(WorkingFile, FileMode.Create, FileAccess.Write, FileShare.None)
            ''//Create our PDF document
            Using Doc As New Document(PageSize.LETTER)
                ''//Bind our PDF object to the physical file using a PdfWriter
                Using Writer = PdfWriter.GetInstance(Doc, FS)
                    ''//Open our document for writing
                    Doc.Open()
                    barocdeimg = barcodetest.CreateImageWithBarcode(Writer.DirectContent, BaseColor.YELLOW, BaseColor.BLACK)
                    barocdeimg.ScalePercent(88.0F)
                    barocdeimg.Alignment = Element.ALIGN_CENTER
                    Doc.NewPage()
                    Anchor.Reference = "http://www.google.com"

                    Doc.Add(Anchor)

                    ''//Insert a blank page

                    Doc.AddTitle("Hello World example")
                    Doc.Add(New Paragraph("這是測試", Font1))
                    png.ScalePercent(50.0F)
                    'png.Alignment = Element.ALIGN_CENTER
                    Doc.Add(png)

                    basicTable.AddCell(cell1)
                    basicTable.AddCell(New PdfPCell(New Phrase("line2,cell2")))
                    Doc.Add(basicTable)
                    Doc.Add(barocdeimg)

                    'Paragraph pa = New Paragraph();            
                    'Barcode128 Barcode = New Barcode128();
                    'Barcode.CodeType = Barcode.CODE128_UCC;
                    'Barcode.Code = "hello barcode";
                    'Itextsharp.text.Image barcodeImage = Barcode.CreateImageWithBarcode(cb, null, null);
                    'barcodeImage.ScalePercent(88.0F);
                    'barcodeImage.Alignment = Element.ALIGN_CENTER;
                    'pa.Add(barcodeImage);
                    Doc.Close()
                End Using
            End Using
        End Using

        Me.Close()

    End Sub
End Class