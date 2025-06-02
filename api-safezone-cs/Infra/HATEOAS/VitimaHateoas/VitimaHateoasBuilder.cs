using api_safezone_cs.DTOs.Vitima;

namespace api_safezone_cs.Infra.HATEOAS.VitimaHateoas;

public static class VitimaHateoasBuilder
{
    public static object Build(VitimaResponse dto, LinkGenerator linkGenerator, HttpContext httpContext)
    {
        var links = new List<(string rel, string routeName, object? routeValues, string method)>
        {
            ("all", "GetVitimas", null, "GET"),
            ("self", "GetVitimaById", new { id = dto.Id }, "GET"),
            ("create", "CreateVitima", null, "POST"),
            ("update", "UpdateVitima", new { id = dto.Id }, "PUT"),
            ("delete", "DeleteVitima", new { id = dto.Id }, "DELETE")
        };

        return HateoasHelper.AddLinks(dto, links, linkGenerator, httpContext);
    }
}
