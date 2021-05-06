using System;
//using System.ComponentModel.DataAnnotations.Schema;

namespace Explora.Domain
{
    public class Entity<Key>
    {
        public Key Id { get; set; }

        private DateTime? _createdDate;

        /// <summary>
        /// Creation date
        /// </summary>
        //[NotMapped]
        public DateTime CreatedDate
        {
            get { return _createdDate ?? DateTime.UtcNow; }
            set { _createdDate = value; }
        }

        /// <summary>
        /// Last modification date
        /// </summary>
        //[NotMapped]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// TimeStamp for create/update db contexts
        /// </summary>
        //[NotMapped]
        //[Column(TypeName = "timestamp")]
        public DateTime LastTimeStamp { get; set; }
    }
}
