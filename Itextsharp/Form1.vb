
Imports System.IO
Imports Itextsharp.text
Imports Itextsharp.text.pdf.parser
Imports Itextsharp.text.pdf


Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim WorkingFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)

        ''//The file that we are creating
        Dim WorkingFile = System.IO.Path.Combine(WorkingFolder, "Output.pdf")
        Debug.Print("Current Directory:" + WorkingFolder)
        Dim arial As BaseFont = BaseFont.CreateFont("c:\windows\fonts\KAIU.TTF", BaseFont.IDENTITY_H, BaseFont.EMBEDDED)
        Dim Font As New Font(arial, 36)
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
                    Doc.Add(New Paragraph("Hello World", Font))
                    Doc.Add(New Paragraph("這是測試", Font))

                    ''//Close our document
                    Doc.Close()
                End Using
            End Using
        End Using

        Me.Close()


    End Sub

    Public Sub ReplacePDFText(ByVal strSearch As String, ByVal scCase As StringComparison, ByVal strSource As String, ByVal strDest As String)

        Dim psStamp As PdfStamper = Nothing 'PDF Stamper Object
        Dim pcbContent As PdfContentByte = Nothing 'Read PDF Content

        If File.Exists(strSource) Then 'Check If File Exists

            Dim pdfFileReader As New PdfReader(strSource) 'Read Our File

            psStamp = New PdfStamper(pdfFileReader, New FileStream(strDest, FileMode.Create)) 'Read Underlying Content of PDF File

            pbProgress.Value = 0 'Set Progressbar Minimum Value
            pbProgress.Maximum = pdfFileReader.NumberOfPages 'Set Progressbar Maximum Value

            For intCurrPage As Integer = 1 To pdfFileReader.NumberOfPages 'Loop Through All Pages

                Dim lteStrategy As LocTextExtractionStrategy = New LocTextExtractionStrategy 'Read PDF File Content Blocks

                pcbContent = psStamp.GetUnderContent(intCurrPage) 'Look At Current Block

                'Determine Spacing of Block To See If It Matches Our Search String
                lteStrategy.UndercontentCharacterSpacing = pcbContent.CharacterSpacing
                lteStrategy.UndercontentHorizontalScaling = pcbContent.HorizontalScaling

                'Trigger The Block Reading Process
                Dim currentText As String = PdfTextExtractor.GetTextFromPage(pdfFileReader, intCurrPage, lteStrategy)

                'Determine Match(es)
                Dim lstMatches As List(Of Itextsharp.text.Rectangle) = lteStrategy.GetTextLocations(strSearch, scCase)

                Dim pdLayer As PdfLayer 'Create New Layer
                pdLayer = New PdfLayer("Overrite", psStamp.Writer) 'Enable Overwriting Capabilities

                'Set Fill Colour Of Replacing Layer
                pcbContent.SetColorFill(BaseColor.BLACK)

                For Each rctRect As Rectangle In lstMatches 'Loop Through Each Match

                    pcbContent.Rectangle(rctRect.Left, rctRect.Bottom, rctRect.Width, rctRect.Height) 'Create New Rectangle For Replacing Layer

                    pcbContent.Fill() 'Fill With Colour Specified

                    pcbContent.BeginLayer(pdLayer) 'Create Layer

                    pcbContent.SetColorFill(BaseColor.BLACK) 'Fill aLyer

                    pcbContent.Fill() 'Fill Underlying Content

                    Dim pgState As PdfGState 'Create GState Object
                    pgState = New PdfGState()

                    pcbContent.SetGState(pgState) 'Set Current State

                    pcbContent.SetColorFill(BaseColor.WHITE) 'Fill Letters

                    pcbContent.BeginText() 'Start Text Replace Procedure

                    pcbContent.SetTextMatrix(rctRect.Left, rctRect.Bottom) 'Get Text Location

                    'Set New Font And Size
                    pcbContent.SetFontAndSize(BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED), 9)

                    pcbContent.ShowText("AMAZING!!!!") 'Replacing Text

                    pcbContent.EndText() 'Stop Text Replace Procedure

                    pcbContent.EndLayer() 'Stop Layer replace Procedure

                Next

                pbProgress.Value = pbProgress.Value + 1 'Increase Progressbar Value

                pdfFileReader.Close() 'Close File

            Next

            psStamp.Close() 'Close Stamp Object

        End If

        'Add Watermark
        AddPDFWatermark("C:\test_words_replaced.pdf", "C:\test_Watermarked_and_Replaced.pdf", Application.StartupPath & "\Anuba.jpg")

    End Sub


End Class
