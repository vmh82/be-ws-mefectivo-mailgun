namespace ApiGeneracionDocumentos.Domain.Interfaces
{
    public interface IReconocimientoFacialInfraestructure
    {
        string GetIdFirmaByNumeroTramite(string numeroTramite);
    }
}