
using Npgsql;
using Dapper;
using System.Text;
using Book_manager.src.BookManager.Domain.entities;
using Book_manager.src.BookManager.Domain.Interfaces;
using System.Data;

namespace Book_manager.src.BookManager.Infrastructure.repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly IDbConnection DbConnection;

        public BookRepository(IDbConnection dbConnection) => DbConnection = dbConnection;

        public async Task<IEnumerable<Book>> GetAll()
        {
            const string query = @"SELECT * FROM ""Books""";

            return await DbConnection.QueryAsync<Book>(query);
        }

        public async Task<bool> SaveAsync(Book item)
        {
            const string query = @"INSERT INTO ""Books"" (""Name"", ""Author"", ""ImageUrl"", ""Rating"", ""Status"", ""Description"") 
                      VALUES (@Name, @Author, @ImageUrl, @Rating, @Status, @Description)";

            var result = await DbConnection.ExecuteAsync(query, item);

            return result > 0;
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            const string query = @"SELECT * FROM ""Books"" WHERE ""Id"" = @Id";

            return await DbConnection.QueryFirstAsync<Book>(query);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            const string query = @"DELETE from ""Books"" WHERE ""Id"" = @Id";

            var result = await DbConnection.ExecuteAsync(query, id);

            return result > 0;
        }

        public async Task<IEnumerable<Book>> SearchAsync(IBookSearchQuery query)
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

            var result = await DbConnection.QueryAsync<Book>(sqlBuilder.ToString(), parameters);

            return result;
        }

    }
}
