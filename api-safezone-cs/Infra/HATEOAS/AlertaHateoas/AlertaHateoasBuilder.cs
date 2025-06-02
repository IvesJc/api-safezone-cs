using api_safezone_cs.DTOs.Alerta;

namespace api_safezone_cs.Infra.HATEOAS.AlertaHateoas;

public static class AlertaHateoasBuilder
{
    public static object Build(AlertaResponse dto, LinkGenerator linkGenerator, HttpContext httpContext)
    {
        var links = new List<(string rel, string routeName, object? routeValues, string method)>
        {
            ("all", "GetAlertas", null, "GET"),
            ("self", "GetAlertaById", new { id = dto.Id }, "GET"),
            ("create", "CreateAlerta", null, "POST"),
            ("update", "UpdateAlerta", new { id = dto.Id }, "PUT"),
            ("delete", "DeleteAlerta", new { id = dto.Id }, "DELETE")
        };

        return HateoasHelper.AddLinks(dto, links, linkGenerator, httpContext);
    }
}