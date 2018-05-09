using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferShippingOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Globalization;
using System.IO;

namespace Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates
{
    public class TransferShippingOrderPDFTemplate
    {
        public MemoryStream GeneratePdfTemplate(TransferShippingOrderViewModel viewModel, TransferShippingOrderService externalTransferOrderService)
        {
            Font normal_font = FontFactory.GetFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);
            Font bold_font = FontFactory.GetFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED, 10);

            Document document = new Document(PageSize.A4, 40, 40, 40, 40);
            MemoryStream stream = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            //writer.CloseStream = false;
            document.Open();

            #region Header

            string companyNameString = "PT. DANLIRIS";
            Paragraph companyName = new Paragraph(companyNameString, bold_font) { Alignment = Element.ALIGN_CENTER };
            document.Add(companyName);

            string companyAddressString = "BANARAN, GROGOL, SUKOHARJO";
            Paragraph companyAddress = new Paragraph(companyAddressString, normal_font) { Alignment = Element.ALIGN_CENTER };
            companyAddress.SpacingAfter = 10f;
            document.Add(companyAddress);

            LineSeparator lineSeparator = new LineSeparator(1f, 100f, BaseColor.Black, Element.ALIGN_CENTER, 1);
            document.Add(lineSeparator);

            string titleString = "SURAT JALAN";
            Paragraph title = new Paragraph(titleString, bold_font) { Alignment = Element.ALIGN_CENTER };
            title.SpacingBefore = 10f;
            title.SpacingAfter = 20f;
            document.Add(title);

            #endregion

            #region Identity

            PdfPTable tableIdentity = new PdfPTable(3);
            tableIdentity.SetWidths(new float[] { 1f, 3f, 4f });
            PdfPCell cellIdentityContentLeft = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_LEFT };
            PdfPCell cellIdentityContentRight = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT };
            cellIdentityContentLeft.Phrase = new Phrase("Nomor SJ", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.SONo, normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("Sukoharjo, " + viewModel.SODate.ToString("dd MMMM yyyy", new CultureInfo("id-ID")), normal_font);
            tableIdentity.AddCell(cellIdentityContentRight);
            cellIdentityContentLeft.Phrase = new Phrase("Dari", normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentLeft.Phrase = new Phrase(": " + viewModel.Supplier.name, normal_font);
            tableIdentity.AddCell(cellIdentityContentLeft);
            cellIdentityContentRight.Phrase = new Phrase("");
            tableIdentity.AddCell(cellIdentityContentRight);
            PdfPCell cellIdentity = new PdfPCell(tableIdentity); // dont remove
            tableIdentity.ExtendLastRow = false;
            document.Add(tableIdentity);

            #endregion

            #region TableContent

            PdfPCell cellCenter = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_CENTER, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellRight = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_RIGHT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };
            PdfPCell cellLeft = new PdfPCell() { Border = Rectangle.TOP_BORDER | Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, HorizontalAlignment = Element.ALIGN_LEFT, VerticalAlignment = Element.ALIGN_MIDDLE, Padding = 5 };

            PdfPTable tableContent = new PdfPTable(5);
            tableContent.SetWidths(new float[] { 1f, 4f, 7f, 3f, 4f });

            cellCenter.Phrase = new Phrase("NO", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("KODE BARANG", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("NAMA BARANG", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("JUMLAH", bold_font);
            tableContent.AddCell(cellCenter);
            cellCenter.Phrase = new Phrase("KETERANGAN", bold_font);
            tableContent.AddCell(cellCenter);

            double total = 0;
            //for (int a = 0; a < 20; a++) // coba kalau banyak baris ^_^
            for (int indexItem = 0; indexItem < viewModel.TransferShippingOrderItems.Count; indexItem++)
            {
                TransferShippingOrderItemViewModel transferShippingOrderItem = viewModel.TransferShippingOrderItems[indexItem];
                for (int indexDetail = 0; indexDetail < transferShippingOrderItem.TransferShippingOrderDetails.Count; indexDetail++)
                {
                    TransferShippingOrderDetailViewModel externalTransferOrderDetail = transferShippingOrderItem.TransferShippingOrderDetails[indexDetail];

                    cellLeft.Phrase = new Phrase((indexDetail + 1).ToString(), normal_font);
                    tableContent.AddCell(cellLeft);

                    cellLeft.Phrase = new Phrase(externalTransferOrderDetail.Product.code, normal_font);
                    tableContent.AddCell(cellLeft);

                    string NamaBarang = externalTransferOrderDetail.Product.name;
                    if (externalTransferOrderDetail.Grade != null)
                        NamaBarang += externalTransferOrderDetail.Grade.Replace(" ", "").Equals("") ? "" : $"\nGRADE {externalTransferOrderDetail.Grade}";

                    cellLeft.Phrase = new Phrase(NamaBarang, normal_font);
                    tableContent.AddCell(cellLeft);

                    cellLeft.Phrase = new Phrase($"{externalTransferOrderDetail.DeliveryQuantity} {externalTransferOrderDetail.Uom.unit}", normal_font);
                    tableContent.AddCell(cellLeft);

                    cellLeft.Phrase = new Phrase(externalTransferOrderDetail.ProductRemark, normal_font);
                    tableContent.AddCell(cellLeft);

                    total += externalTransferOrderDetail.DeliveryQuantity;
                }
            }

            cellRight.Colspan = 3;
            cellRight.Phrase = new Phrase("TOTAL", bold_font);
            tableContent.AddCell(cellRight);
            cellLeft.Phrase = new Phrase(total.ToString(), normal_font);
            tableContent.AddCell(cellLeft);
            cellLeft.Phrase = new Phrase(string.Empty, normal_font);
            tableContent.AddCell(cellLeft);

            PdfPCell cellContent = new PdfPCell(tableContent); // dont remove
            tableContent.ExtendLastRow = false;
            tableContent.SpacingBefore = 20f;
            tableContent.SpacingAfter = 20f;
            document.Add(tableContent);

            #endregion

            #region Signature

            PdfPTable tableSignature = new PdfPTable(2);
            PdfPCell cellSignatureContent = new PdfPCell() { Border = Rectangle.NO_BORDER, HorizontalAlignment = Element.ALIGN_CENTER };
            cellSignatureContent.Phrase = new Phrase("Diterima oleh :\n\n\n\n\n\n\n(                              )", normal_font);
            tableSignature.AddCell(cellSignatureContent);
            cellSignatureContent.Phrase = new Phrase("Diserahkan oleh :\n\n\n\n\n\n\n(                              )", normal_font);
            tableSignature.AddCell(cellSignatureContent);
            PdfPCell cellSignature = new PdfPCell(tableSignature); // dont remove
            tableSignature.ExtendLastRow = false;
            document.Add(tableSignature);

            #endregion

            document.Close();
            byte[] byteInfo = stream.ToArray();
            stream.Write(byteInfo, 0, byteInfo.Length);
            stream.Position = 0;

            return stream;
        }
    }
}