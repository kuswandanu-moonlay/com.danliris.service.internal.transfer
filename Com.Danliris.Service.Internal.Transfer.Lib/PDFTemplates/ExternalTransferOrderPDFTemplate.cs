using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates
{
    public class ExternalTransferOrderPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(ExternalTransferOrderViewModel viewModel)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            var normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            var bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            int margin = 30;
            int width = 600;

            Document document = new Document(PageSize.A4, 25, 25, 30, 30);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.CloseStream = false;
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
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Supplier.name, margin + 45, 700, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Attn.", margin + 45, 685, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Telp.", margin + 45, 670, 0);

            cb.EndText();

            PdfPTable content = new PdfPTable(1);
            PdfPCell cellContent;
            content.TotalWidth = width - 2 * margin;
            cellContent = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_JUSTIFIED };
            string par = $"Dengan hormat, Yang bertanda tangan di bawah ini, Unit Finishing (selanjutnya disebut sebagai pihak Pemesan) dan Unit {viewModel.Supplier.name} (selanjutnya disebut sebagai pihak Pengirim) saling menyetujui untuk mengadakan transfer dengan ketentuan sebagai berikut:";
            cellContent.Phrase = new Phrase(1.5f, par, normal_font);
            content.AddCell(cellContent);
            content.WriteSelectedRows(0, -1, margin, 660, cb);

            PdfPTable table = new PdfPTable(4);
            table.TotalWidth = width - 2 * margin;
            float[] widths = new float[] { 2f, 1f, 1f, 1f };
            table.SetWidths(widths);

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };

            cellCenter.Phrase = new Phrase("NAMA DAN JENIS BARANG", bold_font);
            table.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("JUMLAH", bold_font);
            table.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("HARGA SATUAN", bold_font);
            table.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("SUB TOTAL", bold_font);
            table.AddCell(cellCenter);

            double total = 0;
            //for (int a = 0; a < 50; a++) // coba kalau banyak baris ^_^
            for (int indexItem = 0; indexItem < viewModel.ExternalTransferOrderItems.Count; indexItem++)
            {
                ExternalTransferOrderItemViewModel externalTransferOrderItem = viewModel.ExternalTransferOrderItems[indexItem];
                for (int indexDetail = 0; indexDetail < externalTransferOrderItem.ExternalTransferOrderDetails.Count; indexDetail++)
                {
                    ExternalTransferOrderDetailViewModel externalTransferOrderDetail = externalTransferOrderItem.ExternalTransferOrderDetails[indexDetail];
                    cellLeft.Phrase = new Phrase($"{externalTransferOrderDetail.Product.code}\n{externalTransferOrderItem.TransferRequestNo}", normal_font);
                    table.AddCell(cellLeft);
                    cellRight.Phrase = new Phrase($"{externalTransferOrderDetail.DealQuantity} {externalTransferOrderDetail.DealUom.unit}", normal_font);
                    table.AddCell(cellRight);
                    cellRight.Phrase = new Phrase($"{externalTransferOrderDetail.Price.ToString("N", new CultureInfo("id-ID"))}", normal_font);
                    table.AddCell(cellRight);
                    cellRight.Phrase = new Phrase($"{(externalTransferOrderDetail.DealQuantity * externalTransferOrderDetail.Price).ToString("N", new CultureInfo("id-ID"))}", normal_font);
                    table.AddCell(cellRight);
                    total += externalTransferOrderDetail.DealQuantity * externalTransferOrderDetail.Price;
                }
            }

            cellRight.Colspan = 3;
            cellRight.Phrase = new Phrase("Total", bold_font);
            table.AddCell(cellRight);
            cellRight.Colspan = 1;
            cellRight.Phrase = new Phrase($"{total.ToString("N", new CultureInfo("id-ID"))}", normal_font);
            table.AddCell(cellRight);

            PdfPCell cellFooter = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            cellFooter.Colspan = 4;
            cellFooter.Phrase = new Phrase($"Diminta Datang : {viewModel.DeliveryDate.ToString("dd MMMM yyyy", new CultureInfo("id-ID"))}", normal_font);
            table.AddCell(cellFooter);
            cellFooter.Phrase = new Phrase($"Keterangan : {viewModel.Remark}", normal_font);
            table.AddCell(cellFooter);

            table.WriteSelectedRows(0, -1, margin, 620, cb);

            cb.BeginText();
            cb.SetTextMatrix(15, 23);
            cb.SetFontAndSize(bf_bold, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Pemesan,", 130, 95, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Unit", 130, 30, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "Pengirim,", 460, 95, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_CENTER, viewModel.Supplier.name, 460, 30, 0);
            cb.EndText();

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}
