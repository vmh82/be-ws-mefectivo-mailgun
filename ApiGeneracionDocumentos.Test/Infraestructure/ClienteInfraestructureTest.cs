using ApiGeneracionDocumentos.Domain.Interfaces;
using ApiGeneracionDocumentos.Entity;
using ApiGeneracionDocumentos.Infraestructure.Services;
using ApiGeneracionDocumentos.Test.FakeData;
using Moq;
using System.Data;

namespace ApiGeneracionDocumentos.Test.Infraestructure
{
    [TestFixture]
    public class ClienteInfraestructureTest
    {
        private Mock<IClienteRepository> clienteRepositoryMock;
        private ClienteInfraestructure clienteInfraestructure;

        [SetUp]
        public void Setup()
        {
            clienteRepositoryMock = new();
            clienteInfraestructure = new ClienteInfraestructure(clienteRepositoryMock.Object);
        }

        [Test]
        public void ShouldReturnEconomicActivityByClient()
        {
            clienteRepositoryMock.Setup(x => x.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_VEN)).Returns(ClienteFakeData.StoreProcedurepObtieneActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_VEN));
            clienteRepositoryMock.Setup(x => x.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_SAL)).Returns(ClienteFakeData.StoreProcedurepObtieneActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_SAL));

            string saleEconomicActivity = clienteInfraestructure.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_VEN);
            string salaryEconomicActivity = clienteInfraestructure.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_SAL);

            Assert.That(saleEconomicActivity, Is.EqualTo(ClienteFakeData.VEN_ECONOMIC_ACTIVITY));
            Assert.That(salaryEconomicActivity, Is.EqualTo(ClienteFakeData.SAL_ECONOMIC_ACTIVITY));
        }

        [Test]
        public void ShouldReturnEmptyIfDataTableResponseDoesntHaveRows()
        {

            DataSet ds = new();
            DataTable dt = new();
            ds.Tables.Add(dt);
            clienteRepositoryMock.Setup(x => x.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_EMPTY)).Returns(ds);

            string campaignCodeEmpty = clienteInfraestructure.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_EMPTY);

            Assert.That(campaignCodeEmpty, Is.EqualTo(string.Empty));
        }

        [Test]
        public void ShouldReturnThrowExceptionWhenTheRepositoryThrowsAnException()
        {
            clienteRepositoryMock.Setup(x => x.GetActividadEconomicaCliente(It.IsAny<string>())).Throws(new Exception(ClienteFakeData.EXCEPTION_MESSAGE));

            Exception ex = Assert.Throws<Exception>(() => clienteInfraestructure.GetActividadEconomicaCliente(ClienteFakeData.IDENTIFICATION_SAL));

            Assert.That(ex.Message, Is.EqualTo(ClienteFakeData.EXCEPTION_MESSAGE));
        }

        [Test]
        public void ShouldReturnClientByIdClient()
        {
            clienteRepositoryMock.Setup(x => x.GetClienteByIdCliente(1)).Returns(ClienteFakeData.GetClientByIdClient(1));
            clienteRepositoryMock.Setup(x => x.GetClienteByIdCliente(2)).Returns(ClienteFakeData.GetClientByIdClient(2));

            Cliente clientWithIdOne = clienteInfraestructure.GetClienteByIdCliente(1).Result;
            Cliente clientWithIdTwo = clienteInfraestructure.GetClienteByIdCliente(2).Result;

            Assert.That(clientWithIdOne.IdCliente, Is.EqualTo(1));
            Assert.That(clientWithIdOne.Identificacion, Is.EqualTo(ClienteFakeData.IDENTIFICATION_SAL));
            Assert.That(clientWithIdTwo.IdCliente, Is.EqualTo(2));
            Assert.That(clientWithIdTwo.Identificacion, Is.EqualTo(ClienteFakeData.IDENTIFICATION_VEN));
        }

        [Test]
        public void ShouldReturnClientByIdentification()
        {
            clienteRepositoryMock.Setup(x => x.GetClienteByIdentificacion(ClienteFakeData.IDENTIFICATION_SAL)).Returns(ClienteFakeData.GetClientByIdentification(ClienteFakeData.IDENTIFICATION_SAL));
            clienteRepositoryMock.Setup(x => x.GetClienteByIdentificacion(ClienteFakeData.IDENTIFICATION_VEN)).Returns(ClienteFakeData.GetClientByIdentification(ClienteFakeData.IDENTIFICATION_VEN));

            Cliente clientWithSalaryActivity = clienteInfraestructure.GetClienteByIdentificacion(ClienteFakeData.IDENTIFICATION_SAL).Result;
            Cliente clientWithSalesActivity = clienteInfraestructure.GetClienteByIdentificacion(ClienteFakeData.IDENTIFICATION_VEN).Result;

            Assert.That(clientWithSalaryActivity.IdCliente, Is.EqualTo(1));
            Assert.That(clientWithSalaryActivity.Identificacion, Is.EqualTo(ClienteFakeData.IDENTIFICATION_SAL));
            Assert.That(clientWithSalesActivity.IdCliente, Is.EqualTo(2));
            Assert.That(clientWithSalesActivity.Identificacion, Is.EqualTo(ClienteFakeData.IDENTIFICATION_VEN));
        }
    }
}