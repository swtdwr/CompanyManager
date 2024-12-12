using CompanyManager.Domain.Models.Abstractions;

namespace CompanyManager.Domain.Models
{
    public sealed class Division : Entity
    {
        private string _name;
    
        public Division(string name)
        {
            Name = name;
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
