using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Swappler.Database;
using Swappler.Models;
using Swappler.Models.Status;
using Swappler.Services.Interfaces;
using Swappler.Utilities;

namespace Swappler.Services
{
    public class AddressService : Service<Address, SwapplerSqliteContext>, IAddressService
    {
        public AddressStatus Add(Address address)
        {
            try
            {
                Context.Entry(address).State = EntityState.Added;
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                Logger.Write(LogType.Exception, Logger.ExceptionMessage(exception));

                return AddressStatus.Error;
            }

            return AddressStatus.Added;
        }

        public AddressStatus Remove(Address address)
        {
            try
            {
                Context.Addresses.Attach(address);
                Context.Addresses.Remove(address);
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return AddressStatus.Error;
            }

            return AddressStatus.Removed;
        }

        public AddressStatus Update(Address address, params string[] updateFields)
        {
            try
            {
                if (updateFields == null || updateFields.Length == 0)
                {
                    Context.Addresses.Attach(address);
                    Context.Entry(address).State = EntityState.Modified;
                }
                else
                {
                    Context.Addresses.Attach(address);
                    foreach (string updateField in updateFields)
                    {
                        Context.Entry(address).Property(updateField).IsModified = true;
                    }
                }
                Context.SaveChanges();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return AddressStatus.Error;
            }

            return AddressStatus.Updated;
        }

        public List<Address> FindWhere(Func<Address, bool> wherePredicate)
        {
            try
            {
                var addresses = Context.Addresses.Where(wherePredicate);
                return addresses.ToList();
            }
            catch (Exception e)
            {
                Logger.Write(LogType.Exception, e.Message + "\n" + e.StackTrace);
                return null;
            }
        }
    }
}