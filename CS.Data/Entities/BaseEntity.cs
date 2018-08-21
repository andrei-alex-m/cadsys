using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CS.Data.Entities
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id
        {
            get;
            set;
        }

        public int Index
        {
            get;
            set;
        }

        public int ExcelRow
        {
            get;
            set;
        }

        public bool CompareBy(BaseEntity other, params Func<BaseEntity, object>[] props)
        {
            for (var i = 0; i < props.Length; i++)
            {
                if (props[i](this) != props[i](other))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
