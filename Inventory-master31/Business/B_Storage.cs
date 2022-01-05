using DataAccess;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business
{
    public class B_Storage
    {
        public static List<StorageEntity> StorageList()
        {
            using (var db = new InventaryContext())
            {
                return db.Storages
                    .Include(s => s.Product)
                    .Include(s => s.Warehouse)
                    .ToList();
            }
        }

        public static StorageEntity CreateStorage(StorageEntity oStorage)
        {
            using (var db = new InventaryContext())
            {
                db.Storages.Add(oStorage);
                db.SaveChanges();

                return GetStorageById(oStorage.StorageId);
            }
        }

        /* NUEVO MÉTODO */
        public static StorageEntity GetStorageById(string id)
        {
            using (var db = new InventaryContext())
            {
                return db.Storages
                    .Include(s => s.Product)
                    .Include(s => s.Warehouse)
                    .ToList()
                    .LastOrDefault(s => s.StorageId == id);
            }
        }

        /* MÉTODO SI EXISTE ESE PRODUCTO EN LA STORAGE */
        public static bool IsProductInWarehouse(string idStorage)
        {
            using (var db = new InventaryContext())
            {
                var product = db.Storages.ToList()
                    .Where(p => p.StorageId == idStorage);

                return product.Any();
            }
        }

        /*  MÉTODO PARA LA INFORMACION ASOCIADO A LOS PRODUCTOS */
        public static List<StorageEntity> StorageProductsByWarehouse(string idWarehouse)
        {
            using (var db = new InventaryContext())
            {
                return db.Storages
                    .Include(s => s.Product)
                    .Include(s => s.Warehouse)
                    .Where(s => s.WarehouseId == idWarehouse)
                    .ToList();
            }
        }

        public static void UpdateStorage(StorageEntity oStorage)
        {
            oStorage.LastUpdate = DateTime.Now;

            using (var db = new InventaryContext())
            {
                db.Storages.Update(oStorage);
                db.SaveChanges();
            }
        }
    }
}
