using api_safezone_cs.DTOs.Link;

namespace api_safezone_cs.Infra;

public static class HateoasHelper
{
    public static object AddLinks<T>(
        T dto,
        List<(string rel, string routeName, object? routeValues, string method)> links,
        LinkGenerator linkGenerator,
        HttpContext httpContext)
    {
        var linkDtos = links.Select(link =>
        {
            var href = linkGenerator.GetPathByName(
                httpContext,
                link.routeName,
                link.routeValues
            );
            return new LinkDto(
                Method: link.method,
                Href: href ?? string.Empty,
                Rel: link.rel
            );
        }).ToList();

        return new
        {
            Data = dto,
            Links = linkDtos
        };
    }

}