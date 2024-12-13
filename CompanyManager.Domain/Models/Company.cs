using CompanyManager.Domain.Extensions;
using CompanyManager.Domain.Models.Abstractions;

namespace CompanyManager.Domain.Models
{
    public sealed class Company : Entity
    {
        private readonly List<Department> _departments;
        private string _name;

        public Company(Guid id, string name, IEnumerable<Department> departments) : base(id)
        {
            Name = name;
            _departments = [..departments];
        }
        
        public Company(string name, IEnumerable<Department> departments) : this(Guid.Empty, name, departments)
        {
        }

        /// <summary>
        /// Описывает название организации
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
        /// Добавляет новый департамент в компанию.
        /// </summary>
        /// <param name="department">Департамент, который необходимо добавить.</param>
        /// <exception cref="ArgumentNullException">Если департамент равен null.</exception>
        /// <exception cref="InvalidOperationException">Если такой департамент уже существует в компании.</exception>
        public void Append(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);
            if (_departments.Contains(department))
            {
                throw new InvalidOperationException();
            }

            _departments.Add(department);
        }

        /// <summary>
        /// Удаляет департамент из компании.
        /// </summary>
        /// <param name="department">Департамент, который необходимо удалить.</param>
        /// <exception cref="ArgumentNullException">Если департамент равен null.</exception>
        public void Remove(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);
            _departments.Remove(department);
        }

        /// <summary>
        /// Перемещает департамент вверх в списке департаментов компании.
        /// </summary>
        /// <param name="department">Департамент, который необходимо переместить вверх.</param>
        /// <exception cref="ArgumentNullException">Если департамент равен null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если департамент нельзя переместить выше.</exception>
        public void MoveUp(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);
            _departments.MoveUp(department);
        }

        /// <summary>
        /// Перемещает департамент вниз в списке департаментов компании.
        /// </summary>
        /// <param name="department">Департамент, который необходимо переместить вниз.</param>
        /// <exception cref="ArgumentNullException">Если департамент равен null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Если департамент нельзя переместить ниже</exception>
        public void MoveDown(Department department)
        {
            ArgumentNullException.ThrowIfNull(department);
            _departments.MoveDown(department);
        }

        /// <summary>
        /// Возвращает департамент по его идентификатору.
        /// Если департамент не найден, будет возвращен null
        /// </summary>
        /// <param name="departmentId">Идентификатор департамента.</param>
        public Department? GetDepartmentById(Guid departmentId)
        {
            return _departments.FirstOrDefault(x => x.Id == departmentId);
        }

        /// <summary>
        /// Возвращает все департаменты компании
        /// </summary>
        public IReadOnlyCollection<Department> GetDepartments()
        {
            return _departments.AsReadOnly();
        }
    }
}