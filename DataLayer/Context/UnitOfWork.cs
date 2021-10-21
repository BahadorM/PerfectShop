using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class UnitOfWork : IDisposable
    {
        DbShopContext db = new DbShopContext();

        IUserRepository _userRepository;
        public IUserRepository userRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(db);
                }
                return _userRepository;
            }
        }

        private GenericRepository<Products> _productRepositoryBase;
        public GenericRepository<Products> ProductRepositoryBase
        {
            get
            {
                if (_productRepositoryBase == null)
                {
                    _productRepositoryBase = new GenericRepository<Products>(db);
                }
                return _productRepositoryBase;
            }
        }

        private GenericRepository<ProductGroup> _productGroupsRepositoryBase;
        public GenericRepository<ProductGroup> ProductGroupsRepositoryBase
        {
            get
            {
                if (_productGroupsRepositoryBase == null)
                {
                    _productGroupsRepositoryBase = new GenericRepository<ProductGroup>(db);
                }
                return _productGroupsRepositoryBase;
            }
        }

        private GenericRepository<ProductSelectGroup> _selectedGroupRepositoryBase;
        public GenericRepository<ProductSelectGroup> SelectedGroupRepositoryBase
        {
            get
            {
                if (_selectedGroupRepositoryBase == null)
                {
                    _selectedGroupRepositoryBase = new GenericRepository<ProductSelectGroup>(db);
                }
                return _selectedGroupRepositoryBase;
            }
        }

        private GenericRepository<ProductTag> _productTagRepositoryBase;
        public GenericRepository<ProductTag> productTagRepositoryBase
        {
            get
            {
                if (_productTagRepositoryBase == null)
                {
                    _productTagRepositoryBase = new GenericRepository<ProductTag>(db);
                }
                return _productTagRepositoryBase;
            }
        }

        private GenericRepository<Orders> _ordersRepositoryBase;
        public GenericRepository<Orders> OrdersRepositoryBase
        {
            get
            {
                if (_ordersRepositoryBase == null)
                {
                    _ordersRepositoryBase = new GenericRepository<Orders>(db);
                }
                return _ordersRepositoryBase;
            }
        }

        private GenericRepository<OrderDetails> _orderDetailsRepositoryBase;
        public GenericRepository<OrderDetails> OrderDetailsRepositoryBase
        {
            get
            {
                if (_orderDetailsRepositoryBase == null)
                {
                    _orderDetailsRepositoryBase = new GenericRepository<OrderDetails>(db);
                }
                return _orderDetailsRepositoryBase;
            }
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}
