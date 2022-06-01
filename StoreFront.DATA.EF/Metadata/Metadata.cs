using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace StoreFront.DATA.EF.Models
{
    //internal class Metadata
    //{
    //}

    #region Game

    public partial class GameMetadata
    {
        //PK
        public int GameId { get; set; }
        //FK
        public int ProductId { get; set; }
        //FK
        public int GameTypeId { get; set; }
        //FK
        public int GenreId { get; set; }
        [Required(ErrorMessage = "* Minimum Players is required.")]
        [Display(Name = "Minimum Players")]
        public int MinPlayers { get; set; }
        [Required(ErrorMessage = "* Maximum Players is required.")]
        [Display(Name = "Maximum Players")]
        public int MaxPlayers { get; set; }

    }
    #endregion

    #region GameType

    public partial class GameTypeMetadata
    {
        //PK
        public int GameTypeId { get; set; }
        [Required(ErrorMessage = "* Name is required.")]
        [Display(Name = "Game Type")]
        public string Name { get; set; } = null!;

    }
    #endregion

    #region Genre

    public partial class GenreMetadata
    {
        //PK
        public int GenreId { get; set; }
        [Required(ErrorMessage = "* Genre Name is required")]
        [Display(Name = "Genre Name")]
        public string Genre1 { get; set; } = null!;

    }
    #endregion

    #region Order

    public partial class OrderMetadata
    {
        //PK
        public int OrderId { get; set; }
        //FK
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = "* Order Date is Required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }
        [Required(ErrorMessage = "* Ship To Name is required")]
        [StringLength(100)]
        [Display(Name = "Shpi To Name")]
        public string ShipToName { get; set; } = null!;
        [Required(ErrorMessage = "* City is required")]
        [StringLength(50)]
        [Display(Name = "City")]
        public string ShipCity { get; set; } = null!;
        [StringLength(2)]
        [Display(Name = "State")]
        public string? ShipState { get; set; }
        [Required(ErrorMessage = "* Zip Code is required.")]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        public string ShipZip { get; set; } = null!;

    }
    #endregion

    #region Product

    public partial class ProductMetadata
    {
        //PK
        public int ProductId { get; set; }
        [Required(ErrorMessage = "* Name is required")]
        [StringLength(200)]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; } = null!;
        [Required(ErrorMessage = "* Price is required")]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:c}")]
        [Display(Name = "Price")]
        public decimal Price { get; set; }
        [StringLength(300)]
        public string? Description { get; set; }
        [Display(Name = "In Stock")]
        public int? UnitsInStock { get; set; }
        [Display(Name = "On Order")]
        public int? UnitsOnOrder { get; set; }
        [Display(Name = "Available")]
        public bool? IsActive { get; set; }
        //FK
        public int SupplierId { get; set; }
        [StringLength(50)]
        public string? PhotoUrl { get; set; }
        //FK
        public int StockStatusId { get; set; }
        //FK
        public int ProductTypeId { get; set; }

    }
    #endregion

    #region ProductType

    public partial class ProductTypeMetadata
    {
        //PK
        public int ProductTypeId { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [Display(Name = "Category")]
        public string TypeName { get; set; } = null!;

    }
    #endregion

    #region StockStatus

    public partial class StockStatusMetadata
    {
        //PK
        public int StockStatusId { get; set; }
        [Required(ErrorMessage = "Stock Status Name is required")]
        [Display(Name = "Availability")]
        public string StatusName { get; set; } = null!;

    }
    #endregion

    #region Supplier

    public partial class SupplierMetadata
    {
        //PK
        public int SupplierId { get; set; }
        [Required(ErrorMessage = "Supplier Name is required")]
        [StringLength(150)]
        [Display(Name = "Supplier Name")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "Address is required")]
        [StringLength(150)]

        public string Address { get; set; } = null!;
        [Required(ErrorMessage = "City is required")]
        [StringLength(50)]

        public string City { get; set; } = null!;
        [Required(ErrorMessage = "State is required")]
        [StringLength(2)]

        public string State { get; set; } = null!;
        [Required(ErrorMessage = "Zip Code is required")]
        [StringLength(5)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Zip Code")]
        public string Zip { get; set; } = null!;
        [Required(ErrorMessage = "Phone Number is required")]
        [StringLength(24)]
        [Display(Name = "Phone Number")]
        public string Phone { get; set; } = null!;

    }
    #endregion

    #region UserDetail

    public partial class UserDetailMetadata
    {
        //PK
        public string UserId { get; set; } = null!;
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [StringLength(150)]
        public string? Address { get; set; }
        [StringLength(50)]
        public string? City { get; set; }
        [StringLength(2)]
        public string? State { get; set; }
        [StringLength(2)]
        [DataType(DataType.PostalCode)]
        public string? Zip { get; set; }
        [StringLength(24)]
        [DataType(DataType.PhoneNumber)]
        public string? Phone { get; set; }

    }
    #endregion

}
