using api_safezone_cs.Controllers;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.Domain.Enums;
using api_safezone_cs.DTOs.Ocorrencia;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api_safezone_cs.Tests.Controllers
{
    public class OcorrenciaControllerTests
    {
        private readonly Mock<IOcorrenciaService> _ocorrenciaServiceMock;
        private readonly Mock<LinkGenerator> _linkGeneratorMock;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly OcorrenciaController _controller;

        public OcorrenciaControllerTests()
        {
            _ocorrenciaServiceMock = new Mock<IOcorrenciaService>();
            _linkGeneratorMock = new Mock<LinkGenerator>();
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

            var httpContext = new DefaultHttpContext();
            _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            _controller = new OcorrenciaController(
                _ocorrenciaServiceMock.Object,
                _linkGeneratorMock.Object,
                _httpContextAccessorMock.Object
            );
        }

        private Ocorrencia CreateOcorrencia(int id = 1) =>
            new()
            {
                Id = id,
                Localizacao = new Localizacao { Latitude = "-23.55", Longitude = "-46.63" },
                Tipo = TipoOcorrencia.Incendio,
                Status = Status.Aberta,
                Prioridade = Prioridade.Alta,
                DataHora = DateTime.UtcNow
            };

        private OcorrenciaRequest CreateRequest() =>
            new(
                new Localizacao { Latitude = "-23.55", Longitude = "-46.63" },
                TipoOcorrencia.Incendio,
                Status.Aberta,
                Prioridade.Alta,
                DateTime.UtcNow
            );

        [Fact]
        public async Task GetOcorrencias_ReturnsOk()
        {
            var ocorrencias = new List<Ocorrencia> { CreateOcorrencia() };
            _ocorrenciaServiceMock.Setup(s => s.GetAllOcorrenciasAsync()).ReturnsAsync(ocorrencias);

            var result = await _controller.GetOcorrencias();

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetOcorrenciaById_ReturnsOk_WhenExists()
        {
            var ocorrencia = CreateOcorrencia();
            _ocorrenciaServiceMock.Setup(s => s.GetOcorrenciaByIdAsync(1)).ReturnsAsync(ocorrencia);

            var result = await _controller.GetOcorrenciaById(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task GetOcorrenciaById_ReturnsNotFound_WhenNotExists()
        {
            _ocorrenciaServiceMock.Setup(s => s.GetOcorrenciaByIdAsync(1)).ReturnsAsync((Ocorrencia?)null);

            var result = await _controller.GetOcorrenciaById(1);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateOcorrencia_ReturnsCreatedAtRoute()
        {
            var request = CreateRequest();
            var ocorrencia = CreateOcorrencia(10);

            _ocorrenciaServiceMock.Setup(s => s.CreateOcorrenciaAsync(request)).ReturnsAsync(ocorrencia);

            var result = await _controller.CreteOcorrencia(request);

            var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetOcorrenciaById", createdResult.RouteName);
            Assert.Equal(10, createdResult.RouteValues?["id"]);
        }

        [Fact]
        public async Task UpdateOcorrencia_ReturnsOk_WhenUpdated()
        {
            var request = CreateRequest();
            var ocorrencia = CreateOcorrencia();

            _ocorrenciaServiceMock.Setup(s => s.UpdateOcorrenciaByAsync(1, request)).ReturnsAsync(ocorrencia);

            var result = await _controller.UpdateOcorrencia(1, request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task UpdateOcorrencia_ReturnsNotFound_WhenNotExists()
        {
            var request = CreateRequest();
            _ocorrenciaServiceMock.Setup(s => s.UpdateOcorrenciaByAsync(1, request)).ReturnsAsync((Ocorrencia?)null);

            var result = await _controller.UpdateOcorrencia(1, request);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task DeleteOcorrencia_ReturnsNoContent_WhenSuccess()
        {
            _ocorrenciaServiceMock.Setup(s => s.DeleteOcorrenciaByAsync(1)).ReturnsAsync(true);

            var result = await _controller.DeleteOcorrencia(1);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteOcorrencia_ReturnsNotFound_WhenFails()
        {
            _ocorrenciaServiceMock.Setup(s => s.DeleteOcorrenciaByAsync(1)).ReturnsAsync(false);

            var result = await _controller.DeleteOcorrencia(1);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
