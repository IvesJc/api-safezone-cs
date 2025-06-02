using api_safezone_cs.DTOs.Alerta;
using api_safezone_cs.DTOs.Ocorrencia;

namespace api_safezone_cs.Infra.HATEOAS.OcorrenciaHateoas;

public static class OcorrenciaHateoasBuilder
{
    public static object Build(OcorrenciaResponse dto, LinkGenerator linkGenerator, HttpContext httpContext)
    {
        var links = new List<(string rel, string routeName, object? routeValues, string method)>
        {
            ("all", "GetOcorrencias", null, "GET"),
            ("self", "GetOcorrenciaById", new { id = dto.Id }, "GET"),
            ("create", "CreateOcorrencia", null, "POST"),
            ("update", "UpdateOcorrencia", new { id = dto.Id }, "PUT"),
            ("delete", "DeleteOcorrencia", new { id = dto.Id }, "DELETE")
        };

        return HateoasHelper.AddLinks(dto, links, linkGenerator, httpContext);
    }
}