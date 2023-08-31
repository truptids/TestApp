using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TestApplication.Model.Database.Entities
{
    public class UserRole 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserID { get; set; }
        public int RoleId { get; set; }
     
       


    }
}
