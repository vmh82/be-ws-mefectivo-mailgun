using ApiGeneracionDocumentos.Domain.Interfaces;
using System.Data;
namespace ApiGeneracionDocumentos.Infraestructure.Services
{
    public class CampaniaInfraestructure : ICampaniaInfraestructure
    {
        private readonly ICampaniaRepository _campaniaRepository;
        public CampaniaInfraestructure(ICampaniaRepository campaniaRepository)
        {
            _campaniaRepository = campaniaRepository;
        }

        public string GetNombreCampaniaByCodigoCampania(string codigoCampania)
        {
            try
            {
                string nombreCampania = string.Empty;
                DataSet dataSet = _campaniaRepository.GetNombreCampaniaByCodigoCampania(codigoCampania);
                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                    {
                        nombreCampania = dataSet.Tables[0].Rows[i]["Descripcion"].ToString()!;
                    }
                }
                return nombreCampania;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
