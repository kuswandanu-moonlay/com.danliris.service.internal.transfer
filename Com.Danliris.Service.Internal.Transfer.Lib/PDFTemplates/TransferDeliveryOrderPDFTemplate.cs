using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferDeliveryOrderViewModel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates
{
    public class TransferDeliveryOrderPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(TransferDeliveryOrderViewModel viewModel, TransferDeliveryOrderService transferDeliveryOrderService)
        {
            //UnitViewModel unit = transferDeliveryOrderService.GetUnitFromInternalTransferOrderByInternalTransferOrderId(viewModel.ExternalTransferOrderItems[0].ITOId);

            BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            BaseFont bf_bold = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
            var normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            var bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            int margin = 40;
            int width = 600;

            Document document = new Document(PageSize.A4, 40, 40, 40, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.CloseStream = false;
            document.Open();

            PdfContentByte cb = writer.DirectContent;

            cb.BeginText();

            cb.SetFontAndSize(bf_bold, 16);
            string[] headerLeft = new string[] {
                "PT.DAN LIRIS",

            };
            int headerLeftPosition = 820;
            foreach (var item in headerLeft)
            {
                cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, item, margin, headerLeftPosition -= 10, 0);
            }
            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, "Sukoharjo, " + viewModel.DODate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), width - margin, 810, 0);
            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "No. " + viewModel.DONo, 41, 730, 0);
            cb.SetFontAndSize(bf_bold, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "FM-PJ-00-03-005/R1", 41, 695, 0);

            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Kepada :", 454, 780, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Yth. Kepala Gudang", 454, 763, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, viewModel.Supplier.name, 454, 748, 0);

            cb.SetFontAndSize(bf_bold, 11);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "DO. PENJUALAN :", 454, 714, 0);
            cb.SetFontAndSize(bf, 10);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Order dari : ...............", 454, 695, 0);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "                   ...............", 454, 680, 0);

            //cb.SetFontAndSize(bf_bold, 9);
            //cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "Nomor : " + viewModel.DONo, width - margin, 200, 0);


            cb.EndText();

            string paragraphContent = $"Harap dikeluarkan barang tersebut di bawah ini :";
            Paragraph paragraph = new Paragraph(paragraphContent, normal_font) { Alignment = Element.ALIGN_JUSTIFIED };

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };

            PdfPTable tableContent = new PdfPTable(4);
            tableContent.SetWidths(new float[] { 2f, 4f, 10f, 4f });

            cellCenter.Phrase = new Phrase("NO", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("KODE BARANG", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("NAMA BARANG", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("JUMLAH", bold_font);
            tableContent.AddCell(cellCenter);

            double total = 0;
            int index = 1;
            string unitname = "";
            string unitnameTemp = "";
            for (int indexItem = 0; indexItem < viewModel.items.Count; indexItem++)
            {
                TransferDeliveryOrderItemViewModel transferDeliveryOrderItem = viewModel.items[indexItem];
                for (int indexDetail = 0; indexDetail < transferDeliveryOrderItem.details.Count; indexDetail++)
                {
                    TransferDeliveryOrderDetailViewModel transferDeliveryOrderDetail = transferDeliveryOrderItem.details[indexDetail];

                    string NamaDanJenisBarang = transferDeliveryOrderDetail.ProductName;
                    if (transferDeliveryOrderDetail.Grade != null)
                        NamaDanJenisBarang += transferDeliveryOrderDetail.Grade.Replace(" ", "").Equals("") ? "" : $" - Grade {transferDeliveryOrderDetail.Grade}";
                    if (transferDeliveryOrderDetail.ProductRemark != null)
                        NamaDanJenisBarang += transferDeliveryOrderDetail.ProductRemark.Replace(" ", "").Equals("") ? "" : $" - {transferDeliveryOrderDetail.ProductRemark}";

                    cellCenter.Phrase = new Phrase(index.ToString(), normal_font);
                    tableContent.AddCell(cellCenter);
                    cellLeft.Phrase = new Phrase($"{transferDeliveryOrderDetail.ProductCode}", normal_font);
                    tableContent.AddCell(cellLeft);
                    cellLeft.Phrase = new Phrase($"{NamaDanJenisBarang}", normal_font);
                    tableContent.AddCell(cellLeft);
                    cellRight.Phrase = new Phrase($"{transferDeliveryOrderDetail.DOQuantity.ToString("N", new CultureInfo("id-ID"))}  {transferDeliveryOrderDetail.UomUnit}", normal_font);
                    tableContent.AddCell(cellRight);
                    total += transferDeliveryOrderDetail.DOQuantity;
                    
                    if(unitname == "")
                    {
                        unitname = transferDeliveryOrderDetail.UnitName;
                        unitnameTemp = unitname;
                    } else if (unitnameTemp != transferDeliveryOrderDetail.UnitName)
                    {
                        unitnameTemp = transferDeliveryOrderDetail.UnitName;
                        unitname = $"{unitname}, {transferDeliveryOrderDetail.UnitName}";
                    }

                }
                index++;
            }
            cellCenter.Phrase = new Phrase("", normal_font);
            tableContent.AddCell(cellCenter);
            cellRight.Colspan = 2;
            cellRight.Phrase = new Phrase("Total", bold_font);
            tableContent.AddCell(cellRight);
            cellLeft.Colspan = 1;
            cellLeft.Phrase = new Phrase($"{total.ToString("N", new CultureInfo("id-ID"))}", normal_font);
            tableContent.AddCell(cellLeft);

            PdfPTable tableFooter = new PdfPTable(3);
            tableFooter.SetWidths(new float[] { 4f, 4f, 4f });

            PdfPCell cellFooterContent = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            cellFooterContent.Phrase = new Phrase("Disp : ....................", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase("Op : ....................", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Phrase = new Phrase("Sc : ....................", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Colspan = 3;
            cellFooterContent.Phrase = new Phrase($"Untuk bagian dikirim kepada : {unitname}", normal_font);
            tableFooter.AddCell(cellFooterContent);
            cellFooterContent.Colspan = 3;
            cellFooterContent.Phrase = new Phrase("Keterangan : ............................................................................................................................................", normal_font);
            tableFooter.AddCell(cellFooterContent);

            PdfPTable tableSignature = new PdfPTable(3);
            tableFooter.SetWidths(new float[] { 4f, 4f, 4f });
            PdfPCell cellSignatureContent = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            cellSignatureContent.Colspan = 2;
            cellSignatureContent.Phrase = new Phrase("");
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Colspan = 1;
            cellSignatureContent.Phrase = new Phrase("Terima kasih :", normal_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Phrase = new Phrase("AdmPenjualan\n\n\n\n\n\n\n(                                      )", bold_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Phrase = new Phrase("Gudang\n\n\n\n\n\n\n(                                      )", bold_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Phrase = new Phrase("Bagian Penjualan\n\n\n\n\n\n\n(                                      )", bold_font);
            tableSignature.AddCell(cellSignatureContent);

            PdfPCell cellContent = new PdfPCell(tableContent);
            PdfPCell cellFooter = new PdfPCell(tableFooter);
            PdfPCell cellSignature = new PdfPCell(tableSignature);

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