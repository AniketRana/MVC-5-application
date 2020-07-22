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
                    LastName= model.LastName,
                    Email= model.Email,
                    Code = model.Code
                    
                };
                if (model.Address != null)
                {
                    emp.tblAddress = new tblAddress()
                    {
                        Details = model.Address.Details,
                        Country = model.Address.Country,
                        State= model.Address.State,
                        City= model.Address.City,
                    };

                }
                context.tblEmp.Add(emp);
                context.SaveChanges();
                return emp.Id;
            }
        }
    }
}
