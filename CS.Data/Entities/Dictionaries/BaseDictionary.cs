using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class BaseDictionary
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id
        {
            get;
            set;
        }

        [Required]
        public string Denumire
        {
            get;
            set;
        }

        public string Descriere
        {
            get;
            set;
        }

    }
}
