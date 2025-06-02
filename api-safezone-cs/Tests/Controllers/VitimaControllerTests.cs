using api_safezone_cs.Controllers;
using api_safezone_cs.Domain.Entities;
using api_safezone_cs.Domain.Enums;
using api_safezone_cs.DTOs.Vitima;
using api_safezone_cs.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace api_safezone_cs.Tests.Controllers;

public class VitimaControllerTests
{
    private readonly Mock<IVitimaService> _vitimaServiceMock;
    private readonly Mock<LinkGenerator> _linkGeneratorMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly VitimaController _controller;

    public VitimaControllerTests()
    {
        _vitimaServiceMock = new Mock<IVitimaService>();
        _linkGeneratorMock = new Mock<LinkGenerator>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var httpContext = new DefaultHttpContext();
        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        _controller = new VitimaController(
            _vitimaServiceMock.Object,
            _linkGeneratorMock.Object,
            _httpContextAccessorMock.Object
        );
    }

    private VitimaRequest CreateVitimaRequest() =>
        new(
            Nome: "Maria",
            Idade: 35,
            Condicao: Condicao.Estavel,
            Localizacao: new Localizacao { Latitude = "-23.56", Longitude = "-46.63" },
            OcorrenciaId: 1
        );

    private Vitima CreateVitimaEntity(int id = 1) =>
        new()
        {
            Id = id,
            Nome = "Maria",
            Idade = 35,
            Condicao = Condicao.Estavel,
            Localizacao = new Localizacao { Latitude = "-23.56", Longitude = "-46.63" },
            OcorrenciaId = 1
        };

    [Fact]
    public async Task GetVitimaById_ReturnsOk_WhenVitimaExists()
    {
        var vitima = CreateVitimaEntity();
        _vitimaServiceMock.Setup(x => x.GetVitimaByIdAsync(1)).ReturnsAsync(vitima);

        var result = await _controller.GetVitimaById(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        dynamic response = okResult.Value!;
        var data = (VitimaResponse)response.Data;

        Assert.Equal(1, data.Id);
        Assert.Equal("Maria", data.Nome);
    }

    [Fact]
    public async Task GetVitimaById_ReturnsNotFound_WhenVitimaNotExists()
    {
        _vitimaServiceMock.Setup(x => x.GetVitimaByIdAsync(1)).ReturnsAsync((Vitima?)null);

        var result = await _controller.GetVitimaById(1);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task CreateVitima_ReturnsCreatedAtAction()
    {
        var request = CreateVitimaRequest();
        var created = CreateVitimaEntity(10);

        _vitimaServiceMock.Setup(x => x.CreateVitimaAsync(request)).ReturnsAsync(created);

        var result = await _controller.CreteVitima(request);

        var createdResult = Assert.IsType<CreatedAtRouteResult>(result);
        Assert.Equal(nameof(_controller.GetVitimaById), createdResult.RouteName);

        dynamic response = createdResult.Value!;
        var data = (VitimaResponse)response.Data;

        Assert.Equal(10, data.Id);
        Assert.Equal("Maria", data.Nome);
    }

    [Fact]
    public async Task UpdateVitima_ReturnsOk_WhenVitimaExists()
    {
        var request = CreateVitimaRequest();
        var updated = CreateVitimaEntity();

        _vitimaServiceMock.Setup(x => x.UpdateVitimaByAsync(1, request)).ReturnsAsync(updated);

        var result = await _controller.UpdateVitima(1, request);

        var okResult = Assert.IsType<OkObjectResult>(result);
        dynamic response = okResult.Value!;
        var data = (VitimaResponse)response.Data;

        Assert.Equal("Maria", data.Nome);
        Assert.Equal(35, data.Idade);
    }

    [Fact]
    public async Task UpdateVitima_ReturnsNotFound_WhenVitimaNotExists()
    {
        var request = CreateVitimaRequest();
        _vitimaServiceMock.Setup(x => x.UpdateVitimaByAsync(1, request)).ReturnsAsync((Vitima?)null);

        var result = await _controller.UpdateVitima(1, request);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task DeleteVitima_ReturnsNoContent_WhenDeleted()
    {
        _vitimaServiceMock.Setup(x => x.DeleteVitimaByAsync(1)).ReturnsAsync(true);

        var result = await _controller.DeleteVitima(1);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteVitima_ReturnsNotFound_WhenNotDeleted()
    {
        _vitimaServiceMock.Setup(x => x.DeleteVitimaByAsync(1)).ReturnsAsync(false);

        var result = await _controller.DeleteVitima(1);

        Assert.IsType<NotFoundResult>(result);
    }
}
