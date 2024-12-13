using CompanyManager.Domain.Models.Abstractions;

namespace CompanyManager.Domain.Models
{
    public sealed class Division : Entity
    {
        private string _name;
    
        public Division(Guid id, string name) : base(id)
        {
            Name = name;
        }
        
        public Division(string name) : this(Guid.Empty, name)
        {
        }

        /// <summary>
        /// Описывает название отдела
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
    }
}
