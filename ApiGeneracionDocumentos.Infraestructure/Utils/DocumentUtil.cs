using ApiGeneracionDocumentos.Entity.Dto;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.Data;
using System.Drawing;

namespace ApiGeneracionDocumentos.Infraestructure.Utils
{
    public class DocumentUtil
    {
        public void GenerarTablaAmortizacionCasasComerciales(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 6:
                                tbl.BeginUpdate();
                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;

                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        tbl.Rows[indice].Delete();
                                        row = tbl.Rows[indice - 1];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;

                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["CuotaNumero"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FechaPago"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["Pago"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["Interes"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["Capital"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["Saldo"].ToString());
                                    }
                                }
                                tbl.EndUpdate();
                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    if (tbl1.Rows.Count > 1)
                                    {
                                        TableRow rowInicial1 = tbl1.Rows[1];
                                        switch (rowInicial1.Cells.Count)
                                        {
                                            case 6:
                                                tbl1.BeginUpdate();
                                                foreach (TableRow filaTabla in tbl1.Rows)
                                                {
                                                    filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                }
                                                tbl1.EndUpdate();
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarTablaReferenciasPersonalesCCOWEB(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 7:
                                tbl.BeginUpdate();

                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row;

                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        row = tbl.Rows[indice];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;

                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["PRIMERAPELLIDO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["SEGUNDOAPELLIDO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["NOMBRES"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PARENTESCOREF"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CELULARREF"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["TELEFONOREF"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["DIRECCION"].ToString());
                                    }
                                }
                                tbl.EndUpdate();
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarCartaDeInstruccionNoCliente(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            try
            {
                if (servidor.Document.Tables.Count > 0)
                {
                    foreach (Table tbl in servidor.Document.Tables)
                    {
                        if (tbl.Rows.Count > 1)
                        {
                            TableRow rowInicial = tbl.Rows[1];

                            switch (rowInicial.Cells.Count)
                            {
                                case 2:
                                    tbl.BeginUpdate();
                                    DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                    for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                    {
                                        TableRow? row = null;
                                        if (i == 0)
                                        {
                                            int indice = tbl.Rows.Count - 1;
                                            tbl.Rows[indice].Delete();
                                        }
                                        row = tbl.Rows.Append();
                                        TableCell cell = row.FirstCell;
                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i][0].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i][1].ToString());
                                    }

                                    tbl.EndUpdate();
                                    break;
                            }
                        }
                    }
                }
                servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void GenerarProformaUnificadaRapiditoNoCliente(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            try
            {
                if (servidor.Document.Tables.Count > 0)
                {
                    foreach (Table tbl in servidor.Document.Tables)
                    {
                        if (tbl.Rows.Count > 1)
                        {
                            TableRow rowInicial = tbl.Rows[1];
                            switch (rowInicial.Cells.Count)
                            {
                                case 9:
                                    tbl.BeginUpdate();
                                    DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                    for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                    {
                                        TableRow? row = null;
                                        if (i == 0)
                                        {
                                            int indice = tbl.Rows.Count - 1;
                                            tbl.Rows[indice].Delete();
                                            row = tbl.Rows[indice - 1];
                                        }
                                        else
                                        {
                                            row = tbl.Rows.Append();
                                        }

                                        if (row != null)
                                        {
                                            TableCell cell = row.FirstCell;
                                            servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["NUMCUOTA"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FECHAPAGO"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["SALDOCAPITAL"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CAPITAL"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["INTERES"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGVOL"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTACAPINT"].ToString());
                                        }
                                    }

                                    tbl.EndUpdate();

                                    foreach (Table tbl1 in servidor.Document.Tables)
                                    {
                                        if (tbl1.Rows.Count > 1)
                                        {
                                            TableRow rowInicial1 = tbl1.Rows[1];
                                            switch (rowInicial1.Cells.Count)
                                            {
                                                case 8:
                                                    tbl1.BeginUpdate();
                                                    foreach (TableRow filaTabla in tbl1.Rows)
                                                    {
                                                        filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                    }
                                                    tbl1.EndUpdate();
                                                    break;
                                            }
                                        }
                                    }
                                    break;

                                case 2:

                                    tbl.BeginUpdate();

                                    DataTable tablaInformacion1 = dtoDocumento.InformacionSP.Tables[2];

                                    for (int i = 0; i < tablaInformacion1.Rows.Count; i++)
                                    {
                                        TableRow? row = null;
                                        if (i == 0)
                                        {
                                            int indice = tbl.Rows.Count - 1;
                                            row = tbl.Rows[indice];
                                        }
                                        else
                                        {
                                            row = tbl.Rows.Append();
                                        }

                                        if (row != null)
                                        {
                                            TableCell cell = row.FirstCell;
                                            if (cell.Next.Next.Next != null)
                                            {
                                                servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion1.Rows[i]["NROOPERACION"].ToString());
                                                servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion1.Rows[i]["VALOR"].ToString());
                                            }
                                        }
                                    }
                                    tbl.EndUpdate();
                                    break;
                            }
                        }
                    }
                }
                servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public void GenerarRecordatoriodePagoRapiditoNoCliente(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            try
            {
                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[0];
                ReemplazarCodigoDeBarras(servidor, "OPERACIONCODB", (string)tablaInformacion.Rows[0]["OperacionCodB"]);

                if (servidor.Document.Tables.Count > 0)
                {
                    tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                    foreach (Table tbl in servidor.Document.Tables)
                    {
                        if (tbl.Rows.Count > 1)
                        {
                            TableRow rowInicial = tbl.Rows[1];
                            switch (rowInicial.Cells.Count)
                            {
                                case 6:
                                    tbl.BeginUpdate();
                                    for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                    {
                                        TableRow? row = null;
                                        if (i == 0)
                                        {
                                            int indice = tbl.Rows.Count - 1;
                                            tbl.Rows[indice].Delete();
                                            row = tbl.Rows[indice - 1];
                                        }
                                        else
                                        {
                                            row = tbl.Rows.Append();
                                        }

                                        if (row != null)
                                        {
                                            TableCell cell = row.FirstCell;
                                            servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["NUMCUOTA"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FECHAPAGO"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTACAPINT"].ToString());
                                        }
                                    }
                                    tbl.EndUpdate();

                                    foreach (Table tbl1 in servidor.Document.Tables)
                                    {
                                        tbl1.BeginUpdate();
                                        foreach (TableRow filaTabla in tbl1.Rows)
                                        {
                                            filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                        }
                                        tbl1.EndUpdate();
                                    }
                                    break;
                            }
                        }
                    }
                }
                servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
       
        public void ReemplazarCodigoDeBarras(RichEditDocumentServer server, string campo, string reemplazo = "null")
        {
            DevExpress.BarCodes.BarCode barCode = new();
            barCode.Symbology = DevExpress.BarCodes.Symbology.Code128;
            barCode.CodeText = reemplazo;
            barCode.CodeBinaryData = System.Text.Encoding.Default.GetBytes(barCode.CodeText);
            barCode.Unit = GraphicsUnit.Pixel;
            barCode.Module = 4;
            barCode.AutoSize = true;
            barCode.BarHeight = 160;
            barCode.CodeTextFont = new Font(barCode.CodeTextFont.FontFamily, 11);
            barCode.ImageHeight = 8;
            barCode.ImageWidth = 180;

            DocumentRange[] ranges = server.Document.FindAll(campo, SearchOptions.None, server.Document.Range);
            for (int i = 0; i < ranges.Length; i++)
            {
                if (reemplazo == "null")
                    reemplazo = string.Empty;

                server.Document.Replace(ranges[i], string.Empty);
                CharacterProperties cp = server.Document.BeginUpdateCharacters(ranges[i]);
                server.Document.EndUpdateCharacters(cp);
                server.Document.BeginUpdate();
                server.Document.InsertImage(ranges[i].Start, DocumentImageSource.FromImage(barCode.BarCodeImage));
                server.Document.EndUpdate();
            };
        }

        public void GenerarRecordatoriodePago(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[0];
            ReemplazarCodigoDeBarras(servidor, "OPERACIONCODB", (string)tablaInformacion.Rows[0]["OperacionCodB"]);

            if (servidor.Document.Tables.Count > 0)
            {
                tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 8:
                            case 9:
                                tbl.BeginUpdate();

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        tbl.Rows[indice].Delete();
                                        row = tbl.Rows[indice - 1];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;
                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["NUMCUOTA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FECHAPAGO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTACAPINT"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGVOL"].ToString());
                                        if (rowInicial.Cells.Count == 8)
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                        }
                                        else
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGINCMEN"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                        }
                                    }
                                }

                                tbl.EndUpdate();

                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    tbl1.BeginUpdate();

                                    foreach (TableRow filaTabla in tbl1.Rows)
                                    {
                                        filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                    }
                                    tbl1.EndUpdate();
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarLiquidacionConsumoOro(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[0];
            ReemplazarCodigoDeBarras(servidor, "OPERACIONCODB", (string)tablaInformacion.Rows[0]["OperacionCodB"]);
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarProformaUnificadaOro(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 9:
                                tbl.BeginUpdate();

                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;

                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        row = tbl.Rows[indice];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;
                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["NROCUOTA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FECHAPAGO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["SALDOCAPITAL"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CAPITAL"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["INTERES"].ToString());
                                        if (servidor.Document.GetText(tbl.Rows[0].Range).Split("\r\n")[5].Contains("CUOTA"))
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                        }
                                        else
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                        }
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGVOL"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["TOTALCUOTA"].ToString());
                                    }
                                }

                                tbl.EndUpdate();

                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    if (tbl1.Rows.Count > 1)
                                    {
                                        TableRow rowInicial1 = tbl1.Rows[1];

                                        switch (rowInicial1.Cells.Count)
                                        {
                                            case 8:
                                                tbl1.BeginUpdate();

                                                foreach (TableRow filaTabla in tbl1.Rows)
                                                {
                                                    filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                }
                                                tbl1.EndUpdate();
                                                break;
                                        }
                                    }
                                }

                                break;

                            case 2:

                                if (tbl.Rows.Count == 3)
                                {

                                    tbl.BeginUpdate();

                                    DataTable tablaInformacion1 = dtoDocumento.InformacionSP.Tables[2];

                                    for (int i = 0; i < tablaInformacion1.Rows.Count; i++)
                                    {
                                        TableRow row;
                                        if (i == 0)
                                        {
                                            int indice = tbl.Rows.Count - 1;
                                            row = tbl.Rows[indice];
                                        }
                                        else
                                        {
                                            row = tbl.Rows.Append();
                                        }

                                        if (row != null)
                                        {
                                            TableCell cell = row.FirstCell;
                                            servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion1.Rows[i]["NROOPERACION"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion1.Rows[i]["VALOR"].ToString());
                                        }
                                    }

                                    tbl.EndUpdate();
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarRecordatoriodePagoOro(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[0];
            ReemplazarCodigoDeBarras(servidor, "OPERACIONCODB", (string)tablaInformacion.Rows[0]["OperacionCodB"]);

            if (servidor.Document.Tables.Count > 0)
            {
                tablaInformacion = dtoDocumento.InformacionSP.Tables[1];
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 8:
                                tbl.BeginUpdate();
                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        tbl.Rows[indice].Delete();
                                        row = tbl.Rows[indice - 1];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;
                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["NUMCUOTA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FECHAVENCIMIENTO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGVOL"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["VALOR"].ToString());
                                    }
                                }
                                tbl.EndUpdate();
                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    tbl1.BeginUpdate();

                                    foreach (TableRow filaTabla in tbl1.Rows)
                                    {
                                        filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                    }
                                    tbl1.EndUpdate();
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarLiquidacionConsumo(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[0];
            ReemplazarCodigoDeBarras(servidor, "OPERACIONCODB", (string)tablaInformacion.Rows[0]["NUMOPERCODBARRA"]);
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }
        
        public void GenerarLogoCasaComercial(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[0];
            ReemplazarLogoCCOWEB(servidor, "LOGOCCOWEB", (byte[])tablaInformacion.Rows[0]["IMAGEN"]);
            if (dtoDocumento.Codigo == "CCOWEBPAGARE")
            {
                ReemplazarLogoCCOWEB(servidor, "ICONO2", (byte[])tablaInformacion.Rows[0]["IMAGEN"]);
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void ReemplazarLogoCCOWEB(RichEditDocumentServer server, string campo, byte[] reemplazo)
        {
            Bitmap imagen;
            using (MemoryStream memoryStream = new MemoryStream(reemplazo))
            using (Image newImage = Image.FromStream(memoryStream))
                imagen = new Bitmap(newImage);


            DocumentRange[] ranges = server.Document.FindAll(campo, SearchOptions.None, server.Document.Range);

            for (int i = 0; i < ranges.Length; i++)
            {
                server.Document.Replace(ranges[0], string.Empty);
                CharacterProperties cp = server.Document.BeginUpdateCharacters(ranges[0]);
                server.Document.EndUpdateCharacters(cp);

                server.Document.BeginUpdate();
                server.Document.InsertImage(ranges[0].Start, DocumentImageSource.FromImage(imagen));
                server.Document.EndUpdate();
            };
        }

        public void GenerarCartaDeInstruccion(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 2:
                                tbl.BeginUpdate();
                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        tbl.Rows[indice].Delete();
                                    }

                                    TableRow? row = tbl.Rows.Append();

                                    TableCell cell = row.FirstCell;
                                    servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i][0].ToString());
                                    servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i][1].ToString());
                                }
                                tbl.EndUpdate();
                                break;
                        }
                    }
                }
            }

            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarProformaUnificada(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 9:
                            case 10:
                                tbl.BeginUpdate();

                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];
                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        tbl.Rows[indice].Delete();
                                        row = tbl.Rows[indice - 1];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;
                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["NUMCUOTA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["FECHAPAGO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["SALDOCAPITAL"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CAPITAL"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["INTERES"].ToString());
                                        if (servidor.Document.GetText(tbl.Rows[0].Range).Split("\r\n")[5].Contains("CUOTA"))
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTACAPINT"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                        }
                                        else
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["OTROSRUBROS"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTACAPINT"].ToString());
                                        }
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGVOL"].ToString());
                                        if (rowInicial.Cells.Count == 10)
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGINCMEN"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                        }
                                        else
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["CUOTA"].ToString());
                                        }
                                    }
                                }

                                tbl.EndUpdate();

                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    if (tbl1.Rows.Count > 1)
                                    {
                                        TableRow rowInicial1 = tbl1.Rows[1];
                                        switch (rowInicial1.Cells.Count)
                                        {
                                            case 8:
                                                tbl1.BeginUpdate();

                                                foreach (TableRow filaTabla in tbl1.Rows)
                                                {
                                                    filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                }
                                                tbl1.EndUpdate();
                                                break;
                                        }
                                    }
                                }

                                break;

                            case 4:
                            case 5:

                                tbl.BeginUpdate();

                                DataTable tablaInformacion1 = dtoDocumento.InformacionSP.Tables[2];

                                for (int i = 0; i < tablaInformacion1.Rows.Count; i++)
                                {
                                    TableRow? row;
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        row = tbl.Rows[indice];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;
                                        if (cell.Next.Next.Next != null)
                                        {
                                            servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion1.Rows[i]["NROOPERACION"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion1.Rows[i]["VALOR"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion1.Rows[i]["SEGNODEVDES"].ToString());
                                            servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion1.Rows[i]["SEGNODEVGAR"].ToString());
                                            if (cell.Next.Next.Next.Next != null)
                                            {
                                                servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion1.Rows[i]["SEGNODEVINC"].ToString());
                                            }
                                        }
                                    }
                                }
                                tbl.EndUpdate();
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GenerarCartaMicroWeb(DtoDocumento dtoDocumento, RichEditDocumentServer servidor, string nombreCampania)
        {
            ReemplazarCampos(servidor, "CODCAMPANIA", nombreCampania);

            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 7:
                                tbl.BeginUpdate();

                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        row = tbl.Rows[indice];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;

                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["PRIMERAPELLIDO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["SEGAPELLIDO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["PRIMERNOMBRE"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGNOMBRE"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PARENTESCO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["DIRECCION"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["TELEFONO"].ToString());
                                    }
                                }

                                tbl.EndUpdate();

                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    if (tbl1.Rows.Count > 1)
                                    {
                                        TableRow rowInicial1 = tbl1.Rows[1];

                                        switch (rowInicial1.Cells.Count)
                                        {
                                            case 7:
                                                tbl1.BeginUpdate();

                                                foreach (TableRow filaTabla in tbl1.Rows)
                                                {
                                                    filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                }
                                                tbl1.EndUpdate();
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void ReemplazarCampos(RichEditDocumentServer server, string campo, string reemplazo)
        {
            if (!string.IsNullOrEmpty(campo))
            {
                DocumentRange[] ranges = server.Document.FindAll(campo, SearchOptions.None, server.Document.Range);

                for (int i = 0; i < ranges.Length; i++)
                {
                    if (reemplazo == "null")
                        reemplazo = string.Empty;

                    server.Document.Replace(ranges[i], reemplazo);
                    CharacterProperties cp = server.Document.BeginUpdateCharacters(ranges[i]);
                    server.Document.EndUpdateCharacters(cp);
                }
            }
        }

        public void GeneraActaCancelacion(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 8:
                                tbl.BeginUpdate();

                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;
                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        row = tbl.Rows[indice];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;

                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["CANT"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["TIPOJOYA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["DESCRIPCION"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PESOBRUTO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PESOPIEDRA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PESONETO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["QUILATAJE"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["AVALUO"].ToString());
                                    }
                                }
                                TableRow rowFinal = tbl.Rows.Append();
                                TableCell cellFinal = rowFinal.FirstCell;
                                DataTable tablaInformacion_campos = dtoDocumento.InformacionSP.Tables[0];

                                tbl.MergeCells(tbl[tbl.Rows.Count - 1, 0], tbl[tbl.Rows.Count - 1, 2]);
                                servidor.Document.InsertSingleLineText(cellFinal.Range.Start, "TOTALES:");
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALPESOBRUTO"].ToString());
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALPESOOTROS"].ToString());
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Next.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALPESONETO"].ToString());
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Next.Next.Next.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALAVALUO"].ToString());

                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    if (tbl1.Rows.Count > 1)
                                    {
                                        TableRow rowInicial1 = tbl1.Rows[1];

                                        switch (rowInicial1.Cells.Count)
                                        {
                                            case 8:
                                                tbl1.BeginUpdate();

                                                foreach (TableRow filaTabla in tbl1.Rows)
                                                {
                                                    filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                }
                                                tbl1.EndUpdate();
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }

        public void GeneraContratoPrendaCustodia(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 8:
                                tbl.BeginUpdate();
                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                {
                                    TableRow? row = null;

                                    if (i == 0)
                                    {
                                        int indice = tbl.Rows.Count - 1;
                                        row = tbl.Rows[indice];
                                    }
                                    else
                                    {
                                        row = tbl.Rows.Append();
                                    }

                                    if (row != null)
                                    {
                                        TableCell cell = row.FirstCell;

                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["CANT"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["TIPOJOYA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["DESCRIPCION"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PESOBRUTO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PESOPIEDRA"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["PESONETO"].ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["QUILATAJE"]?.ToString());
                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Next.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["AVALUO"].ToString());
                                    }
                                }
                                TableRow rowFinal = tbl.Rows.Append();
                                TableCell cellFinal = rowFinal.FirstCell;
                                DataTable tablaInformacion_campos = dtoDocumento.InformacionSP.Tables[0];

                                tbl.MergeCells(tbl[tbl.Rows.Count - 1, 0], tbl[tbl.Rows.Count - 1, 2]);
                                servidor.Document.InsertSingleLineText(cellFinal.Range.Start, "TOTALES:");
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALPESOBRUTO"].ToString());
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALPESOOTROS"].ToString());
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Next.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALPESONETO"].ToString());
                                servidor.Document.InsertSingleLineText(cellFinal.Next.Next.Next.Next.Next.Range.Start, tablaInformacion_campos.Rows[0]["TOTALAVALUO"].ToString());

                                tbl.EndUpdate();

                                foreach (Table tbl1 in servidor.Document.Tables)
                                {
                                    if (tbl1.Rows.Count > 1)
                                    {
                                        TableRow rowInicial1 = tbl1.Rows[1];

                                        switch (rowInicial1.Cells.Count)
                                        {
                                            case 8:
                                                tbl1.BeginUpdate();

                                                foreach (TableRow filaTabla in tbl1.Rows)
                                                {
                                                    filaTabla.TableRowAlignment = TableRowAlignment.Center;
                                                }
                                                tbl1.EndUpdate();
                                                break;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);

        }

        public void GenerarSolicitudOro(DtoDocumento dtoDocumento, RichEditDocumentServer servidor)
        {
            if (servidor.Document.Tables.Count > 0)
            {
                foreach (Table tbl in servidor.Document.Tables)
                {
                    if (tbl.Rows.Count > 1)
                    {
                        TableRow rowInicial = tbl.Rows[1];

                        switch (rowInicial.Cells.Count)
                        {
                            case 4:
                                tbl.BeginUpdate();

                                DataTable tablaInformacion = dtoDocumento.InformacionSP.Tables[1];

                                TableRow? rowTemporal = tbl.Rows[0];
                                var texto = string.Empty;

                                if (rowTemporal != null)
                                {
                                    TableCell cellTemporal = rowTemporal.FirstCell;
                                    texto = servidor.Document.GetText(cellTemporal.Range);
                                }

                                if (!string.IsNullOrEmpty(texto))
                                {
                                    if (texto.ToUpper().Contains("PRIMER APELLIDO") || texto.ToUpper().Contains("RELACIÓN"))
                                    {
                                        for (int i = 0; i < tablaInformacion.Rows.Count; i++)
                                        {
                                            TableRow? row;
                                            if (i == 0)
                                            {
                                                int indice = tbl.Rows.Count - 1;
                                                row = tbl.Rows[indice];
                                                rowTemporal = tbl.Rows[0];
                                            }
                                            else
                                            {
                                                row = tbl.Rows.Append();
                                            }

                                            if (row != null)
                                            {
                                                TableCell cell = row.FirstCell;
                                                texto = string.Empty;
                                                if (rowTemporal != null)
                                                {
                                                    TableCell cellTemporal = rowTemporal.FirstCell;
                                                    texto = servidor.Document.GetText(cellTemporal.Range);
                                                }

                                                if (!string.IsNullOrEmpty(texto))
                                                {
                                                    if (texto.ToUpper().Contains("PRIMER APELLIDO"))
                                                    {
                                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["PRIMERAPELLIDO"].ToString());
                                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["SEGAPELLIDO"].ToString());
                                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["PRIMERNOMBRE"].ToString());
                                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Next.Range.Start, tablaInformacion.Rows[i]["SEGNOMBRE"].ToString());
                                                    }
                                                    if (texto.ToUpper().Contains("RELACIÓN"))
                                                    {
                                                        servidor.Document.InsertSingleLineText(cell.Range.Start, tablaInformacion.Rows[i]["PARENTESCO"].ToString());
                                                        servidor.Document.InsertSingleLineText(cell.Next.Range.Start, tablaInformacion.Rows[i]["DIRECCION"].ToString());
                                                        servidor.Document.InsertSingleLineText(cell.Next.Next.Range.Start, tablaInformacion.Rows[i]["TELEFONO"].ToString());
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }
                                tbl.EndUpdate();
                                break;
                        }
                    }
                }
            }
            servidor.SaveDocument(dtoDocumento.PathRtf, DocumentFormat.Rtf);
        }
    }
}
