namespace ApiGeneracionDocumentos.Entity
{
    public partial class Cliente
    {
        public int IdCliente { get; set; }
        public string? TipoIdentificacion { get; set; }
        public string? Identificacion { get; set; }
        public string? Naturaleza { get; set; }
        public string? Estado { get; set; }
        public bool Actualizado { get; set; } = false;
        public bool? EsExcento { get; set; }
        public string? EstadoRiesgo { get; set; }
        public DateTime? FechaUltimaTransaccion { get; set; }
        public int? IdSectorOrganismoControl { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public int? IdUsuarioCreacion { get; set; }
        public short? IdOficinaCreacion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
        public int? IdUsuarioActualizacion { get; set; }
        public short? IdOficinaActualizacion { get; set; }
        public int? IdClienteBP { get; set; }

    }
}
