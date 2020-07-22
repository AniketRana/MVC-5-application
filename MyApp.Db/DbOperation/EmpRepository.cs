using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.Models;

namespace MyApp.Db.DbOperation
{
    public class EmpRepository
    {
        public int AddEmp(EmpModel model)
        {
            using (var context = new AniketEntities())
            {
                tblEmp emp = new tblEmp()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Code = model.Code

                };
                if (model.Address != null)
                {
                    emp.tblAddress = new tblAddress()
                    {
                        Details = model.Address.Details,
                        Country = model.Address.Country,
                        State = model.Address.State,
                        City = model.Address.City,
                    };

                }
                context.tblEmp.Add(emp);
                context.SaveChanges();
                return emp.Id;
            }
        }

        public List<EmpModel> GetAllData()
        {
            using (var context = new AniketEntities())
            {
                var result = context.tblEmp
                    .Select(x => new EmpModel()
                    {
                        Id = x.Id,
                        AddressId = x.AddressId,
                        Code = x.Code,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = new AddressModel()
                        {
                            Id = x.tblAddress.Id,
                            Details = x.tblAddress.Details,
                            Country = x.tblAddress.Country,
                            State = x.tblAddress.State,
                            City = x.tblAddress.City,
                        }
                    }).ToList();
                return result;
            }
        }
        public EmpModel GetSingleData(int id)
        {
            using (var context = new AniketEntities())
            {
                var result = context.tblEmp
                    .Where(x => x.Id == id)
                    .Select(x => new EmpModel()
                    {
                        Id = x.Id,
                        AddressId = x.AddressId,
                        Code = x.Code,
                        Email = x.Email,
                        FirstName = x.FirstName,
                        LastName = x.LastName,
                        Address = new AddressModel()
                        {
                            Id = x.tblAddress.Id,
                            Details = x.tblAddress.Details,
                            Country = x.tblAddress.Country,
                            State = x.tblAddress.State,
                            City = x.tblAddress.City,
                        }
                    }).FirstOrDefault();
                return result;
            }
        }

        public bool UpdateEmp(int id, EmpModel model)
        {
            using (var context = new AniketEntities())
            {
                var emp = new tblEmp();  //context.tblEmp.FirstOrDefault(x => x.Id == id);
                emp.Id = model.Id;
                emp.FirstName = model.FirstName;
                emp.LastName = model.LastName;
                emp.Email = model.Email;
                emp.Code = model.Code;
                emp.AddressId = model.AddressId;

                context.Entry(emp).State = System.Data.Entity.EntityState.Modified; //most imp line

                context.SaveChanges();
                return true;
            };
        }
        public bool DeleteEmp(int id)
        {
            using (var context = new AniketEntities())
            {
                //For double hit database operation in delete method first get record then deleted
                //var emp = context.tblEmp.FirstOrDefault(x => x.Id == id);
                //if (emp != null)
                //{
                //    context.tblEmp.Remove(emp);
                //    context.SaveChanges();
                //    return true;
                //}

                //For single hit database operation in delete method 
                var emp = new tblEmp()
                {
                    Id = id
                };
                context.Entry(emp).State = System.Data.Entity.EntityState.Deleted; //most imp line
                context.SaveChanges();
                return true;
            }
        }
    }
}
