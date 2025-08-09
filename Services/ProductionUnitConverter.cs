using FactoryManagementCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SatisfactoryProductionManager;

public class ProductionUnitConverter : JsonConverter<SatisfactoryProductionUnit>
{
    public override SatisfactoryProductionUnit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;

        var request = root.GetProperty("ProductionRequest")
            .Deserialize<ResourceStream>(options);

        var recipe = App.RecipeProvider
            .Get(root.GetProperty("Recipe").GetString());

        var unit = new SatisfactoryProductionUnit(request, recipe)
        {
            IsSomersloopUsed = root.GetProperty("IsSomersloopUsed").GetBoolean(),
            Overclock = root.GetProperty("Overclock").GetDouble()
        };

        return unit;
    }

    public override void Write(Utf8JsonWriter writer, SatisfactoryProductionUnit value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName(nameof(value.ProductionRequest));
        JsonSerializer.Serialize(writer, value.ProductionRequest, options);

        writer.WriteString(nameof(value.Recipe), value.Recipe.Name);
        writer.WriteBoolean(nameof(value.IsSomersloopUsed), value.IsSomersloopUsed);
        writer.WriteNumber(nameof(value.Overclock), value.Overclock);

        writer.WriteEndObject();
    }
}
