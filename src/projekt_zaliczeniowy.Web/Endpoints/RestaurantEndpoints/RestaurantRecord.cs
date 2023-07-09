namespace projekt_zaliczeniowy.Web.Endpoints.RestaurantEndpoints;

public record RestaurantRecord(int Id, string Name, string? Type, string? City, bool AdultOnly, decimal Rating);
