using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WinformsLinqSQL.Models
{
    [Table(Name = "customers")]
    public class Customer
    {
        [Column(IsPrimaryKey = true,IsDbGenerated =true, Name = "cutomer_id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is a requierd field!")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="First name must be between 2-50 charcters")]
        [Column(Name = "first_name")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is a requierd field!")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Last name must be between 2-50 charcters")]
        [Column(Name = "last_name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Address is a requierd field!")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Address must be between 5-50 charcters")]
        [Column(Name = "address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Phone number is a requierd field!")]
        [StringLength(12, MinimumLength = 7, ErrorMessage = "Last name must be between 7-12 charcters")]
        [RegularExpression(@"^[0-9]*$",ErrorMessage ="Phone number must contain only numbers!")]
        [Column (Name = "phone_number")]
        public string PhoneNumber { get; set; }

        public Customer()
        {
            
        }

        public Customer(string firstName, string lastName, string address, string phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
        }

        public Customer(int id, string firstName, string lastName, string address, string phoneNumber)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Address = address;
            PhoneNumber = phoneNumber;
        }
    }
}
