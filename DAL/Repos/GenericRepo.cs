using DAL.EF;
using DAL.EF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repos
{
    public class GenericRepo <T> where T : class
    {
        Context db;
        DbSet<T> table;
        public GenericRepo(Context db)
        {
            this.db = db;
            this.table = db.Set<T>();
        }

        public List<T> Get()
        {
            try
            {
                return table.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new List<T>();
            }
        }
        public T Get(int id) {
            try
            {
                return table.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool Add(T entity)
        {
            try
            {
                table.Add(entity);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                table.Update(entity);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool Delete(int id) { 
            try
            {
                var entity = table.Find(id);
                table.Remove(entity);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
