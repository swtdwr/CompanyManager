using CompanyManager.Domain.Models;

namespace CompanyManager.Domain.Repositories
{
    public interface ICompaniesRepository
    {
        /// <summary>
        /// Добавляет новую компанию.
        /// </summary>
        /// <param name="company">Компания, которую необходимо добавить.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        Task AddAsync(Company company, CancellationToken cancellationToken = default);
        
        
        /// <summary>
        /// Добавляет набор компаний.
        /// </summary>
        /// <param name="companies">Коллекция компаний, которую необходимо добавить.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        Task AddManyAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default);

        /// <summary>
        /// Обновляет существующую компанию.
        /// </summary>
        /// <param name="company">Компания с обновленными данными.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        Task UpdateAsync(Company company, CancellationToken cancellationToken = default);

        /// <summary>
        /// Удаляет компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании, которую необходимо удалить.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает компанию по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор компании, которую необходимо получить.</param>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Получает все компании.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены для асинхронной операции.</param>
        IAsyncEnumerable<Company> GetAllAsync(CancellationToken cancellationToken = default);
    }
}