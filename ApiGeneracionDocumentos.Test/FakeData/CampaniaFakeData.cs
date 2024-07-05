using System.Data;

namespace ApiGeneracionDocumentos.Test.FakeData
{
    public class CampaniaFakeData
    {
        public static DataSet StoreProcedureConsultaNombreCampania(string CampaignName)
        {
            DataSet ds = new();
            DataTable dt = new();
            DataColumn dataColumn = new("Descripcion", typeof(string));
            dt.Columns.Add(dataColumn);

            switch (CampaignName)
            {
                case CAMPAIGN_NAME:
                    dt.Rows.Add(CAMPAIGN_CODE);
                    break;
                case OTHER_CAMPAIGN_NAME:
                    dt.Rows.Add(OTHER_CAMPAIGN_CODE);
                    break;
            }           

            ds.Tables.Add(dt);
            return ds;
        }

        public const string CAMPAIGN_NAME = "RealCampaign";
        public const string OTHER_CAMPAIGN_NAME = "FakeCampaign";
        public const string NON_EXISTENT_CAMPAIGN_NAME = "Non-existent campaing name";

        public const string CAMPAIGN_CODE = "RealCampaign";
        public const string OTHER_CAMPAIGN_CODE = "FakeCampaign";

        public const string EXCEPTION_MESSAGE = "Exception message";
    }
}
