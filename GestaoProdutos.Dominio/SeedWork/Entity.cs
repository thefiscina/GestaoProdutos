using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoProduto.Dominio
{
    public class Entity
    {        
        [Key]
        [Column("id")]
        public int Id { get; set; }

    }
}
