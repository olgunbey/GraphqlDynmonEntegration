using Dynmon;
using GraphqlDynmonEntegration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddGraphQLServer().AddQueryType<Query>().AddType<JsonNodeType>()
    .AddType<MongoQueryRequest>()
    .AddType<Lookup>()
    .AddInputObjectType<Filter>(u =>
    {
        u.Field(y => y.Value).Type<AnyType>();
    })
    .AddEnumType<Enums.MatchOperators>(d =>
    {
        d.BindValuesExplicitly();
        d.Value(Enums.MatchOperators.None).Name("None");
        d.Value(Enums.MatchOperators.And).Name("And");
        d.Value(Enums.MatchOperators.Or).Name("And");
    })
    .AddEnumType<Enums.RelationTypes>(d =>
    {
        d.BindValuesExplicitly();
        d.Value(Enums.RelationTypes.One).Name("One");
        d.Value(Enums.RelationTypes.Many).Name("Many");
    })
    .AddEnumType<Enums.FilterOperators>(d =>
    {
        d.BindValuesExplicitly();
        d.Value(Enums.FilterOperators.Equals).Name("Equals");
        d.Value(Enums.FilterOperators.NotEquals).Name("NotEquals");
        d.Value(Enums.FilterOperators.Contains).Name("Contains");
        d.Value(Enums.FilterOperators.GreaterThan).Name("GreaterThan");
        d.Value(Enums.FilterOperators.LessThan).Name("LessThan");
        d.Value(Enums.FilterOperators.GreaterThanOrEqual).Name("GreaterThanOrEqual");
        d.Value(Enums.FilterOperators.LessThanOrEqual).Name("LessThanOrEqual");
        d.Value(Enums.FilterOperators.In).Name("In");
        d.Value(Enums.FilterOperators.NotIn).Name("NotIn");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.MapGraphQL();
app.MapControllers();

app.Run();
