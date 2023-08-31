using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TestApplication.Model.Base;

namespace TestApplication.Model.Database.Entities
{
    public class User : IEntity
    {
        public User()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)] public string Name { get; set; }
        [MaxLength(50)] public string FullName { get; set; }
        

      }

   
}