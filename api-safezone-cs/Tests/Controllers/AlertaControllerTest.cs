using api_safezone_cs.Controllers;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.Domain.Enums;
using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api_safezone_cs.Tests.Controllers
{
    public class AlertaControllerTests
    {
        private readonly Mock<IAlertaService> _alertaServiceMock;
        private readonly Mock<LinkGenerator> _linkGeneratorMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly AlertaController _controller;

        public AlertaControllerTests()
        {
            _alertaServiceMock = new Mock<IAlertaService>();
            _linkGeneratorMock = new Mock<LinkGenerator>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            _controller = new AlertaController(
                _alertaServiceMock.Object,
                _linkGeneratorMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        private Alerta CreateAlertaEntity(int id = 1) =>
            new()
            {
                Id = id,
                Tipo = 0,
                AreaAfetada = "Área urbana",
                Severidade = Severidade.Critica,
                Status = Status.Encerrada,
                DataHora = DateTime.UtcNow
            };

        private AlertaRequest CreateAlertaRequest() =>
            new(
                Tipo: 0,
                AreaAfetada: "Área urbana",
                Severidade: Severidade.Critica,
                Status: Status.Encerrada,
                DataHora: DateTime.UtcNow
            );

        [Fact]
        public async Task GetAlertas_ReturnsOk()
        {
            var alertas = new List<Alerta> { CreateAlertaEntity() };
            _alertaServiceMock.Setup(x => x.GetAllAlertasAsync()).ReturnsAsync(alertas);

            var result = await _controller.GetAlertas();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetAlertaById_ReturnsOk_WhenAlertaExists()
        {
            var alerta = CreateAlertaEntity();
            _alertaServiceMock.Setup(x => x.GetAlertaByIdAsync(1)).ReturnsAsync(alerta);

            var result = await _controller.GetAlertaById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetAlertaById_ReturnsNotFound_WhenAlertaNotExists()
        {
            _alertaServiceMock.Setup(x => x.GetAlertaByIdAsync(1)).ReturnsAsync((Alerta?)null);

            var result = await _controller.GetAlertaById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateAlerta_ReturnsCreatedAtRoute()
        {
            var request = CreateAlertaRequest();
            var created = CreateAlertaEntity(10);

            _alertaServiceMock.Setup(x => x.CreateAlertaAsync(request)).ReturnsAsync(created);

            var result = await _controller.CreateAlerta(request);

            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetAlertaById", createdResult.RouteName);
        }

        [Fact]
        public async Task UpdateAlerta_ReturnsOk_WhenAlertaExists()
        {
            var request = CreateAlertaRequest();
            var updated = CreateAlertaEntity();

            _alertaServiceMock.Setup(x => x.UpdateAlertaByAsync(1, request)).ReturnsAsync(updated);

            var result = await _controller.UpdateAlerta(1, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateAlerta_ReturnsNotFound_WhenAlertaNotExists()
        {
            var request = CreateAlertaRequest();
            _alertaServiceMock.Setup(x => x.UpdateAlertaByAsync(1, request)).ReturnsAsync((Alerta?)null);

            var result = await _controller.UpdateAlerta(1, request);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteAlerta_ReturnsNoContent_WhenDeleted()
        {
            _alertaServiceMock.Setup(x => x.DeleteAlertaByAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteAlerta(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAlerta_ReturnsNotFound_WhenNotDeleted()
        {
            _alertaServiceMock.Setup(x => x.DeleteAlertaByAsync(1)).ReturnsAsync(false);

            var result = await _controller.DeleteAlerta(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}