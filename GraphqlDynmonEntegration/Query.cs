using Dynmon;
using System.Text.Json.Nodes;

namespace GraphqlDynmonEntegration
{
    public class Query
    {
        public async Task<JsonNode?> DynmonExecuteQuery(MongoQueryRequest mongoQueryRequest, string connectionString, string cluster, string collection)
        {
            QueryExecuter queryExecuter = new QueryExecuter();
            return await queryExecuter.ExecuteQueryAsync(mongoQueryRequest, connectionString, cluster, collection);
        }
    }
}
