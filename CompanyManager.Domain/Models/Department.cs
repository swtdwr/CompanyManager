using CompanyManager.Domain.Extensions;
using CompanyManager.Domain.Models.Abstractions;

namespace CompanyManager.Domain.Models
{
    public sealed class Department : Entity
    {
        private readonly List<Division> _divisions;
        private string _name;
    
        public Department(string name, IEnumerable<Division> divisions)
        {
            Name = name;
            _divisions = [..divisions];
        }
    
        /// <summary>
        /// Описывает название департамента
        /// </summary>
        public string Name
        {
            get => _name;
            set  
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);
                _name = value;
            }
        }

        /// <summary>
        /// Добавляет новое отделение в департамент.
        /// </summary>
        /// <param name="division">Отделение, который необходимо добавить.</param>
        /// <exception cref="ArgumentNullException">Если отделение равно null.</exception>
        /// <exception cref="InvalidOperationException">Если такое отделение уже существует в департаменте.</exception>
        public void Append(Division division)
        {
            ArgumentNullException.ThrowIfNull(division);
            if (_divisions.Contains(division))
            {
                throw new InvalidOperationException();
            }
            _divisions.Add(division);
        }

        /// <summary>
        /// Удаляет отделение из департамента.
        /// </summary>
        /// <param name="division">Отделение, которое необходимо удалить.</param>
        /// <exception cref="ArgumentNullException">Если департамент равен null.</exception>
        public void Remove(Division division)
        {
            ArgumentNullException.ThrowIfNull(division);
            _divisions.Remove(division);
        }

        /// <summary>
        /// Перемещает отделение вверх в списке отделений департамента.
        /// </summary>
        /// <param name="division">Отделение, который необходимо переместить вверх.</param>
        /// <exception cref="ArgumentNullException">Если отделение равно null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если отделение нельзя переместить выше</exception>
        public void MoveUp(Division division)
        {
            ArgumentNullException.ThrowIfNull(division);
            _divisions.MoveUp(division);
        }

        /// <summary>
        /// Перемещает отделение вниз в списке отделений департамента.
        /// </summary>
        /// <param name="division">Отделение, который необходимо переместить вниз.</param>
        /// <exception cref="ArgumentNullException">Если отделение равно null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если отделение нельзя переместить ниже</exception>
        public void MoveDown(Division division)
        {
            ArgumentNullException.ThrowIfNull(division);
            _divisions.MoveDown(division);
        }

        /// <summary>
        /// Возвращает отделение по его идентификатору.
        /// Если отделение не найдено, будет возвращен null
        /// </summary>
        /// <param name="divisionId">Идентификатор отделения.</param>
        public Division? GetDivisionById(Guid divisionId)
        {
            return _divisions.FirstOrDefault(x => x.Id == divisionId);
        }

        /// <summary>
        /// Возвращает все отделения департамента
        /// </summary>
        public IReadOnlyCollection<Division> GetDivisions()
        {
            return _divisions.AsReadOnly();
        }
    }
}