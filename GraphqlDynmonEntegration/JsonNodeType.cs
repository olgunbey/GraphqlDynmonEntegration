using HotChocolate.Language;
using System.Text.Json.Nodes;

namespace GraphqlDynmonEntegration
{
    public class JsonNodeType : ScalarType<JsonNode, StringValueNode>
    {
        public JsonNodeType(string name, BindingBehavior bind = BindingBehavior.Explicit)
     : base(name = "JsonNode", bind)
        {
            Description = "Represents arbitrary JSON data";
        }

        public override StringValueNode ParseResult(object? resultValue)
        {
            if (resultValue is JsonNode node)
            {
                return new StringValueNode(node.ToJsonString());
            }

            throw new Exception($"Unable to serialize '{resultValue.GetType().Name}' to JsonNode.");
        }



        public override bool TryDeserialize(object? resultValue, out object? runtimeValue)
        {
            try
            {
                if (resultValue is string stringValue)
                {
                    runtimeValue = JsonNode.Parse(stringValue);
                    return true;
                }

                if (resultValue is JsonNode jsonNode)
                {
                    runtimeValue = jsonNode;
                    return true;
                }

                runtimeValue = null;
                return false;
            }
            catch
            {
                runtimeValue = null;
                return false;
            }
        }

        public override bool TrySerialize(object? runtimeValue, out object? resultValue)
        {
            try
            {
                if (runtimeValue is JsonNode jsonNode)
                {
                    resultValue = jsonNode.ToJsonString();
                    return true;
                }

                resultValue = null;
                return false;
            }
            catch
            {
                resultValue = null;
                return false;
            }
        }

        protected override JsonNode ParseLiteral(StringValueNode valueSyntax)
        {
            if (valueSyntax is StringValueNode stringValue)
            {
                return JsonNode.Parse(valueSyntax.Value);
            }
            throw new Exception("Hata");
        }

        protected override StringValueNode ParseValue(JsonNode runtimeValue)
        {
            if (runtimeValue is JsonNode jsonNode)
            {
                return new StringValueNode(jsonNode.ToJsonString());
            }
            throw new Exception("StringValueNode tipi dönüştürülemedi.");
        }
    }
}
