namespace ApiGeneracionDocumentos.Entity
{
    public partial class Formulario
    {
        public int IdFormulario { get; set; }
        public string ModuloNegocio { get; set; }
        public string CodigoFormulario { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string ImpresoraDefecto { get; set; }
        public bool GuardaImagen { get; set; }
        public short? OrdenImpresion { get; set; }
        public bool ImpresionDirecta { get; set; }
        public short NumeroCopias { get; set; }
        public bool EsReimprimible { get; set; }
        public short? MaxImpresiones { get; set; }
        public bool UsaImpresoraSecundaria { get; set; }
        public bool EsDocumentoValorado { get; set; }
        public bool Orientacion { get; set; }
        public string? EtiquetaAdicional { get; set; }
    }
}
