using System;
using System.ComponentModel.DataAnnotations;

namespace SanctionScanner.DAL.Model.Entity
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        private bool _isActive = true;
        public bool IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }

    }
}
