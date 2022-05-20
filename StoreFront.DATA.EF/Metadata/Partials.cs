using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.DATA.EF.Models
{
    //internal class Partials
    //{
    //}

    #region Game
    [ModelMetadataType(typeof(GameMetadata))]

    public partial class Game { }
    #endregion

    #region GameType
    [ModelMetadataType(typeof(GameMetadata))]

    public partial class GameType { }
    #endregion

    #region Genre
    [ModelMetadataType(typeof(GenreMetadata))]

    public partial class Genre { }
    #endregion

    #region Order
    [ModelMetadataType(typeof(OrderMetadata))]

    public partial class Order { }
    #endregion

    #region Product
    [ModelMetadataType(typeof(ProductMetadata))]

    public partial class Product 
    {
        [NotMapped]
        public IFormFile? Image { get; set; }
    }
    #endregion

    #region ProductType
    [ModelMetadataType(typeof(ProductTypeMetadata))]

    public partial class ProductType { }
    #endregion

    #region StockStatus
    [ModelMetadataType(typeof(StockStatusMetadata))]

    public partial class StockStatus { }
    #endregion

    #region Supplier
    [ModelMetadataType(typeof(SupplierMetadata))]

    public partial class Supplier { }
    #endregion

    #region UserDetail
    [ModelMetadataType(typeof(UserDetailMetadata))]

    public partial class UserDetail
    {
        public string FullName { get { return $"{FirstName} {LastName}"; } }
    }

    #endregion

}
