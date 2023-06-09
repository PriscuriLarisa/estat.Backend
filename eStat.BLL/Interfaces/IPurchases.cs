﻿using eStat.Library.Models;

namespace eStat.BLL.Interfaces
{
    public interface IPurchases
    {
        List<Purchase> GetAll();
        Purchase? GetByUid(Guid uid);
        Purchase Add(Purchase purchase);
        Purchase AddPurchase(Guid shoppingCartUid);
        void Update(Purchase purchase);
        void Delete(Guid uid);
        List<Purchase> GetByUser(Guid userUid);
    }
}