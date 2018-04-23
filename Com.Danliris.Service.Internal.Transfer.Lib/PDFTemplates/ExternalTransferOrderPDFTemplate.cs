using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates
{
    public class ExternalTransferOrderPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(ExternalTransferOrderViewModel viewModel, ExternalTransferOrderService externalTransferOrderService)
        {
            UnitViewModel unit = externalTransferOrderService.GetUnitFromInternalTransferOrderByInternalTransferOrderId(viewModel.ExternalTransferOrderItems[0].InternalTransferOrderId);

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            var normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            var bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            int margin = 40;
            int width = 600;

            Document document = new Document(PageSize.A4, 40, 40, 40, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            //writer.CloseStream = false;
            document.Open();

            PdfContentByte cb = writer.DirectContent;

            cb.BeginText();

            cb.SetFontAndSize(bf_bold, 8);
            string[] headerLeft = new string[] {
                "PT.DAN LIRIS",
                "Head Office: Kelurahan Banaran",
                "Kecamatan Grogol",
                "Sukoharjo 57193 - INDONESIA",
                "PO.BOX 166 Solo 57100",
                "Telp. (0271) 740888, 714400",
                "Fax. (0271) 735222, 740777",
            };
            int headerLeftPosition = 820;
            foreach (var item in headerLeft)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, item, margin, headerLeftPosition -= 10, 0);
            }

            cb.SetFontAndSize(bf_bold, 9);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Nomor : " + viewModel.ExternalTransferOrderNo, width - margin, 800, 0);

            cb.SetFontAndSize(bf_bold, 12);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "TRANSFER ORDER", 300, 725, 0);

            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Sukoharjo, " + viewModel.OrderDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), width - margin, 700, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Kepada :", margin, 700, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Division.name, margin + 45, 700, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Attn.", margin + 45, 685, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Telp.", margin + 45, 670, 0);

            cb.EndText();

            string paragraphContent = $"Dengan hormat, Yang bertanda tangan di bawah ini, Unit {unit.name} (selanjutnya disebut sebagai pihak Pemesan) dan Unit {viewModel.Division.name} (selanjutnya disebut sebagai pihak Pengirim) saling menyetujui untuk mengadakan transfer dengan ketentuan sebagai berikut:";
            Paragraph paragraph = new Paragraph(paragraphContent, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };

            PdfPTable tableContent = new PdfPTable(4);
            tableContent.SetWidths(new float[] { 2f, 1f, 1f, 1f });

            cellCenter.Phrase = new Phrase("NAMA DAN JENIS BARANG", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("JUMLAH", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("HARGA SATUAN", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("SUB TOTAL", bold_font);
            tableContent.AddCell(cellCenter);

            double total = 0;
            //for (int a = 0; a < 3; a++) // coba kalau banyak baris ^_^
            for (int indexItem = 0; indexItem < viewModel.ExternalTransferOrderItems.Count; indexItem++)
            {
                ExternalTransferOrderItemViewModel externalTransferOrderItem = viewModel.ExternalTransferOrderItems[indexItem];
                for (int indexDetail = 0; indexDetail < externalTransferOrderItem.ExternalTransferOrderDetails.Count; indexDetail++)
                {
                    ExternalTransferOrderDetailViewModel externalTransferOrderDetail = externalTransferOrderItem.ExternalTransferOrderDetails[indexDetail];
                    cellLeft.Phrase = new Phrase($"{externalTransferOrderDetail.Product.code}\n{externalTransferOrderItem.TransferRequestNo}", normal_font);
                    tableContent.AddCell(cellLeft);
                    cellRight.Phrase = new Phrase($"{externalTransferOrderDetail.DealQuantity} {externalTransferOrderDetail.DealUom.unit}", normal_font);
                    tableContent.AddCell(cellRight);
                    cellRight.Phrase = new Phrase($"{externalTransferOrderDetail.Price.ToString("N", new CultureInfo("id-ID"))}", normal_font);
                    tableContent.AddCell(cellRight);
                    cellRight.Phrase = new Phrase($"{(externalTransferOrderDetail.DealQuantity * externalTransferOrderDetail.Price).ToString("N", new CultureInfo("id-ID"))}", normal_font);
                    tableContent.AddCell(cellRight);
                    total += externalTransferOrderDetail.DealQuantity * externalTransferOrderDetail.Price;
                }
            }

            cellRight.Colspan = 3;
            cellRight.Phrase = new Phrase("Total", bold_font);
            tableContent.AddCell(cellRight);
            cellRight.Colspan = 1;
            cellRight.Phrase = new Phrase($"{total.ToString("N", new CultureInfo("id-ID"))}", normal_font);
            tableContent.AddCell(cellRight);

            PdfPTable tableFooter = new PdfPTable(3);
            tableFooter.SetWidths(new float[] { 8f, 1f, 45f });

            PdfPCell cellFooterContent = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            cellFooterContent.Phrase = new Phrase("Diminta Datang", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase(":", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase($"{viewModel.DeliveryDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase("Keterangan", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase(":", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase($"{viewModel.Remark}", normal_font);
            tableFooter.AddCell(cellFooterContent);

            PdfPTable tableSignature = new PdfPTable(2);

            PdfPCell cellSignatureContent = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            cellSignatureContent.Phrase = new Phrase("Pemesan", bold_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Phrase = new Phrase("Pengirim", bold_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.PaddingTop = 50f;
            cellSignatureContent.Phrase = new Phrase(unit.name, bold_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Phrase = new Phrase($"{viewModel.Division.name}", bold_font);
            tableSignature.AddCell(cellSignatureContent);

            // --------- kalo dihapus tabel malah jadi ada margin kanan dan kiri
            PdfPCell cellContent = new PdfPCell(tableContent);
            PdfPCell cellFooter = new PdfPCell(tableFooter);
            PdfPCell cellSignature = new PdfPCell(tableSignature);
            // --------- kalo dihapus tabel malah jadi ada margin kanan dan kiri

            LineSeparator lineSeparator = new LineSeparator(1f, 100f, BaseColor.White, Element.ALIGN_LEFT, 1);
            document.Add(lineSeparator);

            paragraph.SpacingBefore = 150f;
            paragraph.SpacingAfter = 10f;
            document.Add(paragraph);

            tableContent.ExtendLastRow = false;
            document.Add(tableContent);

            tableFooter.SpacingBefore = 10f;
            tableFooter.ExtendLastRow = false;
            document.Add(tableFooter);

            tableSignature.SpacingBefore = 50f;
            tableSignature.ExtendLastRow = false;
            document.Add(tableSignature);

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
