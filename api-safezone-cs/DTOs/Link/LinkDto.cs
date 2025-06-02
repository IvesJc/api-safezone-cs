namespace api_safezone_cs.DTOs.Link;

public record LinkDto(
    string Href = "",
    string Rel = "",
    string Method = "GET"
);