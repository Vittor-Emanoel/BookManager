
using Npgsql;
using Dapper;
using Book_manager.Domain.Entities;
using System.Text;

namespace Book_manager.Repository
{
  public class BooksRepository : IBooksRepository
  {
    private readonly string _connectionString;

    public BooksRepository(IConfiguration configuration) => _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";

    public async Task<IEnumerable<Book>> GetAll()
    {
      using (var dbConnection = new NpgsqlConnection(_connectionString))
      {
        var query = @"SELECT * FROM ""Books""";

        return await dbConnection.QueryAsync<Book>(query);
      }
    }

    public async Task<bool> SaveAsync(Book item)
    {
      using (var dbConnection = new NpgsqlConnection(_connectionString))
      {
        var query = @"INSERT INTO ""Books"" (""Name"", ""Author"", ""ImageUrl"", ""Rating"", ""Status"", ""Description"") 
                      VALUES (@Name, @Author, @ImageUrl, @Rating, @Status, @Description)";

        var result = await dbConnection.ExecuteAsync(query, item);

        return result > 0;
      }
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
      using (var dbConnection = new NpgsqlConnection(_connectionString))
      {
        var query = @"SELECT * FROM ""Books"" WHERE ""Id"" = @Id";

        return await dbConnection.QueryFirstAsync<Book>(query);
      }
    }
    public async Task<bool> DeleteAsync(int id)
    {
      using (var dbConnection = new NpgsqlConnection(_connectionString))
      {
        var query = @"DELETE from ""Books"" WHERE ""Id"" = @Id";

        var result = await dbConnection.ExecuteAsync(query, id);

        return result > 0;
      }
    }

    public async Task<IEnumerable<Book>> SearchAsync(IBookSearchQuery query)
    {
      using (var dbConnection = new NpgsqlConnection(_connectionString))
      {
        var sqlBuilder = new StringBuilder();
        sqlBuilder.Append(@"SELECT * FROM ""Books"" WHERE 1 = 1 ");

        var parameters = new DynamicParameters();

        if (!string.IsNullOrWhiteSpace(query.Name))
        {
          sqlBuilder.Append(@" AND ""Name"" ILIKE @NameFilter ");
          parameters.Add("@NameFilter", $"%{query.Name}%");
        }

        if (!string.IsNullOrWhiteSpace(query.Author))
        {
          sqlBuilder.Append(@" AND ""Author"" ILIKE @AuthorFilter ");
          parameters.Add("@AuthorFilter", $"%{query.Author}%");
        }

        if (query.MinRating.HasValue && query.MinRating.Value >= 0 && query.MinRating.Value <= 5)
        {
          sqlBuilder.Append(@" AND ""Rating"" >= @MinRating ");
          parameters.Add("@MinRating", query.MinRating.Value);
        }

        if (query.Status.HasValue)
        {
          sqlBuilder.Append(@" AND ""Status"" = @StatusFilter ");
          parameters.Add("@StatusFilter", query.Status.Value);
        }

        sqlBuilder.Append(@"ORDER BY ""Id"" LIMIT @PageSize OFFSET @Offset ");


        // Calculo do offset => numero de paginas - 1 * o numero de items por pagina
        int offset = (query.PageNumber - 1) * query.PageSize;

        parameters.Add("@PageSize", query.PageSize);
        parameters.Add("@Offset", offset);

        var result = await dbConnection.QueryAsync<Book>(sqlBuilder.ToString(), parameters);

        return result;
      }

    }

  }
}
