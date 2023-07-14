using eStat.BLL.Core;
using eStat.BLL.Interfaces;
using eStat.DAL.Core.Context.Interfaces;
using eStat.Library.Converters;
using eStat.Library.Models;

namespace eStat.BLL.Implementations
{
    public class PurchasesBL : BusinessObject, IPurchases
    {
        public PurchasesBL(IDALContext dalContext) : base(dalContext)
        {
        }

        public Purchase Add(Purchase purchase)
        {
            return PurchaseConverter.ToDTO(_dalContext.Purchases.Add(PurchaseConverter.ToEntity(purchase)));
        }

        public Purchase AddPurchase(Guid shoppingCartUid)
        {
            DAL.Entities.ShoppingCart shoppingCart = _dalContext.ShoppingCarts.GetByUid(shoppingCartUid);
            if (shoppingCart == null) return null;

            DAL.Entities.Purchase purchase = new();
            purchase.PurchaseGUID = Guid.Empty;
            purchase.UserGUID = shoppingCart.UserGUID;
            purchase.Date = DateTime.Now;
            purchase.Products = new List<DAL.Entities.PurchaseProduct>();
            foreach(DAL.Entities.ShoppingCartProduct shoppingCartProduct in shoppingCart.Products)
            {
                DAL.Entities.PurchaseProduct purchaseProduct = new();
                purchaseProduct.PurchaseProductGUID = Guid.Empty;
                purchaseProduct.Quantity = shoppingCartProduct.Quantity;
                purchaseProduct.UserProductGUID = shoppingCartProduct.UserProductGUID;
                purchaseProduct.Price = shoppingCartProduct.UserProduct.Price;
                purchase.Products.Add(purchaseProduct);

                DAL.Entities.UserProduct oldUserProduct = _dalContext.UserProducts.GetByUid(shoppingCartProduct.UserProductGUID);
                if (oldUserProduct == null) continue;

                DAL.Entities.UserProduct newUserProduct = new();
                newUserProduct.UserProductGUID = oldUserProduct.UserProductGUID;
                newUserProduct.Quantity = oldUserProduct.Quantity - shoppingCartProduct.Quantity;
                newUserProduct.UserGUID = oldUserProduct.UserGUID;
                newUserProduct.ProductGUID = oldUserProduct.ProductGUID;
                newUserProduct.Price = oldUserProduct.Price;

                _dalContext.UserProducts.Update(newUserProduct);
            }

            for (int index = shoppingCart.Products.Count - 1; index >= 0; index--)
            {
                _dalContext.ShoppingCartProducts.Delete(shoppingCart.Products[index].ShoppingCartProductGUID);
            }



            //shoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //DAL.Entities.ShoppingCart newShoppingCart = new();
            //newShoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //_dalContext.ShoppingCarts.Update(newShoppingCart);

            return PurchaseConverter.ToDTO(_dalContext.Purchases.Add(purchase));
        }

        public Purchase AddPurchaseWithAddress(Guid shoppingCartUid, string address)
        {
            DAL.Entities.ShoppingCart shoppingCart = _dalContext.ShoppingCarts.GetByUid(shoppingCartUid);
            if (shoppingCart == null) return null;

            DAL.Entities.Purchase purchase = new();
            purchase.PurchaseGUID = Guid.Empty;
            purchase.UserGUID = shoppingCart.UserGUID;
            purchase.Date = DateTime.Now;
            purchase.Products = new List<DAL.Entities.PurchaseProduct>();
            purchase.Address = address;
            foreach (DAL.Entities.ShoppingCartProduct shoppingCartProduct in shoppingCart.Products)
            {
                DAL.Entities.PurchaseProduct purchaseProduct = new();
                purchaseProduct.PurchaseProductGUID = Guid.Empty;
                purchaseProduct.Quantity = shoppingCartProduct.Quantity;
                purchaseProduct.UserProductGUID = shoppingCartProduct.UserProductGUID;
                purchaseProduct.Price = shoppingCartProduct.UserProduct.Price;
                purchase.Products.Add(purchaseProduct);

                DAL.Entities.UserProduct oldUserProduct = _dalContext.UserProducts.GetByUid(shoppingCartProduct.UserProductGUID);
                if (oldUserProduct == null) continue;

                DAL.Entities.UserProduct newUserProduct = new();
                newUserProduct.UserProductGUID = oldUserProduct.UserProductGUID;
                newUserProduct.Quantity = oldUserProduct.Quantity - shoppingCartProduct.Quantity;
                newUserProduct.UserGUID = oldUserProduct.UserGUID;
                newUserProduct.ProductGUID = oldUserProduct.ProductGUID;
                newUserProduct.Price = oldUserProduct.Price;

                _dalContext.UserProducts.Update(newUserProduct);
            }

            for (int index = shoppingCart.Products.Count - 1; index >= 0; index--)
            {
                _dalContext.ShoppingCartProducts.Delete(shoppingCart.Products[index].ShoppingCartProductGUID);
            }



            //shoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //DAL.Entities.ShoppingCart newShoppingCart = new();
            //newShoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //_dalContext.ShoppingCarts.Update(newShoppingCart);

            return PurchaseConverter.ToDTO(_dalContext.Purchases.Add(purchase));
        }

        public Purchase AddPurchaseWithDate(Guid shoppingCartUid, DateTime date)
        {
            DAL.Entities.ShoppingCart shoppingCart = _dalContext.ShoppingCarts.GetByUid(shoppingCartUid);
            if (shoppingCart == null) return null;

            DAL.Entities.Purchase purchase = new();
            purchase.PurchaseGUID = Guid.Empty;
            purchase.UserGUID = shoppingCart.UserGUID;
            purchase.Date = date;
            purchase.Products = new List<DAL.Entities.PurchaseProduct>();
            foreach (DAL.Entities.ShoppingCartProduct shoppingCartProduct in shoppingCart.Products)
            {
                DAL.Entities.PurchaseProduct purchaseProduct = new();
                purchaseProduct.PurchaseProductGUID = Guid.Empty;
                purchaseProduct.Quantity = shoppingCartProduct.Quantity;
                purchaseProduct.UserProductGUID = shoppingCartProduct.UserProductGUID;
                purchaseProduct.Price = shoppingCartProduct.UserProduct.Price;
                purchase.Products.Add(purchaseProduct);

                DAL.Entities.UserProduct oldUserProduct = _dalContext.UserProducts.GetByUid(shoppingCartProduct.UserProductGUID);
                if (oldUserProduct == null) continue;

                DAL.Entities.UserProduct newUserProduct = new();
                newUserProduct.UserProductGUID = oldUserProduct.UserProductGUID;
                newUserProduct.Quantity = oldUserProduct.Quantity - shoppingCartProduct.Quantity;
                newUserProduct.UserGUID = oldUserProduct.UserGUID;
                newUserProduct.ProductGUID = oldUserProduct.ProductGUID;
                newUserProduct.Price = oldUserProduct.Price;

                _dalContext.UserProducts.Update(newUserProduct);
            }

            for (int index = shoppingCart.Products.Count - 1; index >= 0; index--)
            {
                _dalContext.ShoppingCartProducts.Delete(shoppingCart.Products[index].ShoppingCartProductGUID);
            }



            //shoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //DAL.Entities.ShoppingCart newShoppingCart = new();
            //newShoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //_dalContext.ShoppingCarts.Update(newShoppingCart);

            return PurchaseConverter.ToDTO(_dalContext.Purchases.Add(purchase));
        }

        public Purchase AddPurchaseWithDateAndAddress(Guid shoppingCartUid, DateTime date, string address)
        {
            DAL.Entities.ShoppingCart shoppingCart = _dalContext.ShoppingCarts.GetByUid(shoppingCartUid);
            if (shoppingCart == null) return null;

            DAL.Entities.Purchase purchase = new();
            purchase.PurchaseGUID = Guid.Empty;
            purchase.UserGUID = shoppingCart.UserGUID;
            purchase.Date = date;
            purchase.Products = new List<DAL.Entities.PurchaseProduct>();
            purchase.Address = address;
            foreach (DAL.Entities.ShoppingCartProduct shoppingCartProduct in shoppingCart.Products)
            {
                DAL.Entities.PurchaseProduct purchaseProduct = new();
                purchaseProduct.PurchaseProductGUID = Guid.Empty;
                purchaseProduct.Quantity = shoppingCartProduct.Quantity;
                purchaseProduct.UserProductGUID = shoppingCartProduct.UserProductGUID;
                purchaseProduct.Price = shoppingCartProduct.UserProduct.Price;
                purchase.Products.Add(purchaseProduct);

                DAL.Entities.UserProduct oldUserProduct = _dalContext.UserProducts.GetByUid(shoppingCartProduct.UserProductGUID);
                if (oldUserProduct == null) continue;

                DAL.Entities.UserProduct newUserProduct = new();
                newUserProduct.UserProductGUID = oldUserProduct.UserProductGUID;
                newUserProduct.Quantity = oldUserProduct.Quantity - shoppingCartProduct.Quantity;
                newUserProduct.UserGUID = oldUserProduct.UserGUID;
                newUserProduct.ProductGUID = oldUserProduct.ProductGUID;
                newUserProduct.Price = oldUserProduct.Price;

                _dalContext.UserProducts.Update(newUserProduct);
            }

            for (int index = shoppingCart.Products.Count - 1; index >= 0; index--)
            {
                _dalContext.ShoppingCartProducts.Delete(shoppingCart.Products[index].ShoppingCartProductGUID);
            }



            //shoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //DAL.Entities.ShoppingCart newShoppingCart = new();
            //newShoppingCart.Products = new List<DAL.Entities.ShoppingCartProduct>();
            //_dalContext.ShoppingCarts.Update(newShoppingCart);

            return PurchaseConverter.ToDTO(_dalContext.Purchases.Add(purchase));
        }

        public void Delete(Guid uid)
        {
            _dalContext.Purchases.Delete(uid);
        }

        public List<Purchase> GetAll()
        {
            return _dalContext.Purchases.GetAll().Select(o => PurchaseConverter.ToDTO(o)).ToList();
        }

        public Purchase? GetByUid(Guid uid)
        {
            return PurchaseConverter.ToDTO(_dalContext.Purchases.GetByUid(uid));
        }

        public List<Purchase> GetByUser(Guid userUid)
        {
            return _dalContext.Purchases.GetPurchasesByUser(userUid).Select(o => PurchaseConverter.ToDTO(o)).ToList();
        }

        public void Update(Purchase purchase)
        {
            _dalContext.Purchases.Update(PurchaseConverter.ToEntity(purchase)); ;
        }
    }
}
