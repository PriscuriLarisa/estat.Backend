using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eStat.BLL.Implementations
{
    public class ShoppingCartsBL : BusinessObject, IShoppingCarts
    {
        public ShoppingCartsBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public ShoppingCart Add(ShoppingCartCreate shoppingCart)
        {
            ShoppingCart shoppingCartModel = new ShoppingCart();
            shoppingCartModel.ShoppingCartGUID = Guid.Empty;
            shoppingCartModel.UserGUID = shoppingCart.UserGUID;
            shoppingCartModel.Products = new List<ShoppingCartProduct>();
            shoppingCartModel.User = null;


            DAL.Entities.ShoppingCart createdShoppingCart = _dalContext.ShoppingCarts.Add(ShoppingCartConverter.ToEntity(shoppingCartModel));
            return ShoppingCartConverter.ToDTO(createdShoppingCart);
        }


        public void AddItemToCart(ShoppingCartProductAdd shoppingCartProduct)
        {
            ShoppingCartProduct shoppingCartProductModel = new();
            shoppingCartProductModel.UserProduct = null;
            shoppingCartProductModel.ShoppingCart = null;
            shoppingCartProductModel.ShoppingCartProductGUID = Guid.Empty;
            shoppingCartProductModel.ShoppingCartGUID = shoppingCartProduct.ShoppingCartGUID;
            shoppingCartProductModel.UserProductGUID = shoppingCartProduct.UserProductGUID;
            shoppingCartProductModel.Quantity = shoppingCartProduct.Quantity;
            //ShoppingCart shoppingCart = ShoppingCartConverter.ToDTO(_dalContext.ShoppingCarts.GetByUid(shoppingCartProduct.ShoppingCartGUID ?? Guid.Empty));
            //if(shoppingCart == null)
                //return;
            
            //shoppingCart.Products.Add(shoppingCartProductModel);
            _dalContext.ShoppingCarts.AddProduct(ShoppingCartProductConverter.ToEntity(shoppingCartProductModel));
        }

        public List<ShoppingCart> GetAll()
        {
            return _dalContext.ShoppingCarts.GetAll().Select(s => ShoppingCartConverter.ToDTO(s)).ToList();
        }

        public ShoppingCart GetByUid(Guid uid)
        {
            return ShoppingCartConverter.ToDTO(_dalContext.ShoppingCarts.GetByUid(uid));
        }

        public ShoppingCart GetshoppingCartByUser(Guid userUid)
        {
           return ShoppingCartConverter.ToDTO(_dalContext.ShoppingCarts.GetShoppingCartByUser(userUid));
        }
    }
}
