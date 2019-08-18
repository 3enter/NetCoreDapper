using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreDapper.Models;

namespace NetCoreDapper.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person person)
        {
            DynamicParameters dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("@Code", person.Code);
            dynamicParameters.Add("@PersonCodeTypeID", person.PersonCodeTypeID);
            dynamicParameters.Add("@FirstName", person.FirstName);
            dynamicParameters.Add("@LastName", person.LastName);
            dynamicParameters.Add("@Email", person.Email);
            dynamicParameters.Add("@BirthDate", person.BirthDate);
            dynamicParameters.Add("@MaritalStatusID", person.MaritalStatusID);
            dynamicParameters.Add("@GenderID", person.GenderID);
            dynamicParameters.Add("@PrefixID", person.PrefixID);
            dynamicParameters.Add("@CUserID", person.CUserID);
            dynamicParameters.Add("@Culture", "en-US");            
            using (IDbConnection conn = new SqlConnection(" server=MAINSERVER;initial catalog=AlyatimDB;persist security info=True;user id=sa;password=admin@123;multipleactiveresultsets=True"))
            {
                var result = await conn.ExecuteAsync("[profile].ins_Person", dynamicParameters, commandType: CommandType.StoredProcedure);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}