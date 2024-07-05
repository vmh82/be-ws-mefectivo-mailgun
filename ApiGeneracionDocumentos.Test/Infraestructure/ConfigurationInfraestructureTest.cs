using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Infraestructure.Services;
using ApiGeneracionDocumentos.Repository.Context;
using ApiGeneracionDocumentos.Repository.Services;
using ApiGeneracionDocumentos.Test.FakeData;
using Microsoft.EntityFrameworkCore;
using MockQueryable.Moq;
using Moq;

namespace ApiGeneracionDocumentos.Test.Infraestructure
{
    [TestFixture]
    public class ConfigurationInfraestructureTest
    {
        private ConfiguracionRepository configurationRepository;

        private ConfiguracionInfraestructure configurationInfraestructure;

        private readonly Mock<IDbContextFactory<ConfiguracionContext>> parametroContextMock = new();

        [SetUp]
        public void Setup()
        {
            configurationRepository = new ConfiguracionRepository(parametroContextMock.Object);
            configurationInfraestructure = new ConfiguracionInfraestructure(configurationRepository);
        }

        [Test]
        public async Task ShouldReturnParametroByNombre()
        {
            var mock = ConfiguracionFakeData.GetFakeParametrosList().BuildMock().BuildMockDbSet();
            parametroContextMock.Setup(x => x.CreateDbContext().Parametro).Returns(mock.Object);

            var parametro = await configurationInfraestructure.GetParametroByNombre("RUTADOCDIGITAL");

            Assert.That(parametro, Is.Not.EqualTo(null));
            Assert.That(parametro.Nombre, Is.EqualTo("RUTADOCDIGITAL"));
        }

        [Test]
        public async Task ShouldReturnDefaultParametro()
        {
            var mock = ConfiguracionFakeData.GetFakeParametrosList().BuildMock().BuildMockDbSet();
            parametroContextMock.Setup(x => x.CreateDbContext().Parametro).Returns(mock.Object);

            var parametro = await configurationInfraestructure.GetParametroByNombre("Not_Exists");

            Assert.That(parametro.IdParametro, Is.EqualTo(0));
        }
    }
}