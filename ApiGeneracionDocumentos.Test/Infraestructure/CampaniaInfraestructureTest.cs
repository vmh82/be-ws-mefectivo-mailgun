using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Infraestructure.Services;
using ApiGeneracionDocumentos.Test.FakeData;
using Moq;

namespace ApiGeneracionDocumentos.Test.Infraestructure
{
    [TestFixture]
    public class CampaniaInfraestructureTest
    {
        private Mock<ICampaniaRepository> campaniaRepositoryMock;
        private CampaniaInfraestructure campaniaInfraestructure;

        [SetUp]
        public void Setup()
        {
            campaniaRepositoryMock = new();
            campaniaInfraestructure = new CampaniaInfraestructure(campaniaRepositoryMock.Object);
        }

        [Test]
        public void ShouldReturnRealCampaignNameByCampaignCode()
        {
            campaniaRepositoryMock.Setup(x => x.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.CAMPAIGN_NAME)).Returns(CampaniaFakeData.StoreProcedureConsultaNombreCampania(CampaniaFakeData.CAMPAIGN_NAME));
            campaniaRepositoryMock.Setup(x => x.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.OTHER_CAMPAIGN_NAME)).Returns(CampaniaFakeData.StoreProcedureConsultaNombreCampania(CampaniaFakeData.OTHER_CAMPAIGN_NAME));

            string campaignCode = campaniaInfraestructure.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.CAMPAIGN_NAME);
            string otherCampaignCode = campaniaInfraestructure.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.OTHER_CAMPAIGN_NAME);

            Assert.That(campaignCode, Is.EqualTo(CampaniaFakeData.CAMPAIGN_CODE));
            Assert.That(otherCampaignCode, Is.Not.EqualTo(CampaniaFakeData.CAMPAIGN_CODE));

            Assert.That(otherCampaignCode, Is.EqualTo(CampaniaFakeData.OTHER_CAMPAIGN_CODE));
            Assert.That(campaignCode, Is.Not.EqualTo(CampaniaFakeData.OTHER_CAMPAIGN_CODE));
        }

        [Test]
        public void ShouldReturnEmptyByCampaignCodeThatNotExists()
        {
            campaniaRepositoryMock.Setup(x => x.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.NON_EXISTENT_CAMPAIGN_NAME)).Returns(CampaniaFakeData.StoreProcedureConsultaNombreCampania(CampaniaFakeData.NON_EXISTENT_CAMPAIGN_NAME));

            string campaignCodeEmpty = campaniaInfraestructure.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.NON_EXISTENT_CAMPAIGN_NAME);

            Assert.That(campaignCodeEmpty, Is.EqualTo(string.Empty));
            Assert.That(campaignCodeEmpty, Is.Not.EqualTo(CampaniaFakeData.CAMPAIGN_CODE));
        }

        [Test]
        public void ShouldReturnThrowExceptionWhenTheRepositoryThrowsAnException()
        {
            campaniaRepositoryMock.Setup(x => x.GetNombreCampaniaByCodigoCampania(It.IsAny<string>())).Throws(new Exception(CampaniaFakeData.EXCEPTION_MESSAGE));

            Exception ex = Assert.Throws<Exception>(() => campaniaInfraestructure.GetNombreCampaniaByCodigoCampania(CampaniaFakeData.CAMPAIGN_CODE));

            Assert.That(ex.Message, Is.EqualTo(CampaniaFakeData.EXCEPTION_MESSAGE));
        }
    }
}