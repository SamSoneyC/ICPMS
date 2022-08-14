using RailwayMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace RailwayMVC.Controllers
{
    public class RailwayController : Controller
    {
        // Database Context property
        private MVCContext db = new MVCContext();

        // get the connection sting from web.config file
        string constr = ConfigurationManager.ConnectionStrings["RailwayMVCEntitiestest"].ToString();
        // open the login page
        public ActionResult Login()
        {
            return View();
        }

        // in login page if user click login button this function will call with Users parameter
        // Use that parameret to check user credentials in user table
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Users objUser)
        {
            if (ModelState.IsValid)
            {
                using (MVCContext db = new MVCContext())
                {
                    var obj = db.Users.Where(a => a.User_name.Equals(objUser.User_name) && a.Pass.Equals(objUser.Pass)).FirstOrDefault();
                    if (obj != null)
                    {
                        Session["UserID"] = obj.User_id.ToString();
                        Session["UserName"] = obj.User_name.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(objUser);
        }

        //once login sucessfull redirect to index page
        //This page display all station details and sales details so index page load time we ill get station and sales data from data base
        public ActionResult Index()
        {
            
            SqlConnection _con = new SqlConnection(constr);
            SqlDataAdapter _da = new SqlDataAdapter("Select * From Station", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            
            ViewBag.Station = ToSelectList(_dt, "CRS_Code", "CRS_Code");

            return View();
        }
        
        //Here get all station data this method call from index viwe javaScript function
        [HttpPost]
        public JsonResult getAllStation()
        {
            SqlDataAdapter _da = new SqlDataAdapter("Select * From Station", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            List<Station> stations = new List<Station>();
            stations = ConvertDataTable<Station>(_dt);
          
                return Json(stations);

            
        }

        //Here get station target value this method call from index viwe javaScript function
        // user select the station this function will call
        [HttpPost]
        public ActionResult TargetVal(string val){

            SqlDataAdapter _da = new SqlDataAdapter("Select * From Station", constr);
            DataTable _dt = new DataTable();
            _da.Fill(_dt);
            List<Station> stations = new List<Station>();
            stations = ConvertDataTable<Station>(_dt);
            var target = stations.Where(c => c.CRS_Code == val).FirstOrDefault().Target;
            return Json(target);
    
        }

        //Here store sales data to the database 
        public ActionResult AddSale(Sale sale)
        {

            using (MVCContext entities = new MVCContext())
            {
               
                entities.Sales.Add(sale);
                entities.SaveChanges();
                var id = sale.CRS_Code;
            }
            return RedirectToAction("Index");
        }

        // Here get all sales data from the database
        [HttpPost]
        public JsonResult Sales()
        {
            
            using (MVCContext entities = new MVCContext())
            {
                List<Saledata> saledatas = new List<Saledata>();
               
                List<Sale> sale = (from Sale in entities.Sales select Sale).ToList();
                foreach (var s in sale) {
                    Saledata sd = new Saledata();
                    int? act = s.Actual_val;
                    
                    int? target = s.Target;
             
                    int? variance = target - act;
                    string date = s.Date.ToString("yyyy-MM-dd");
                    string crs = s.CRS_Code;
                    sd.Actual_val = act;
                    sd.Target = target;
                    sd.Variance = variance<0 ? variance*-1 : variance;
                    sd.Date = date;
                    sd.CRS_Code = crs;

                    saledatas.Add(sd);
                }
                return Json(saledatas);

            }
        }

        //purpose of this method convert data table to 'List'
        private static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        //purpose of this method read the data row and return the item 
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        //purpose of this method convert data table to 'SelectList'
        [NonAction]
        public SelectList ToSelectList(DataTable table, string valueField, string textField)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new SelectListItem()
                {
                    Text = row[textField].ToString(),
                    Value = row[valueField].ToString()
                });
            }

            return new SelectList(list, "Value", "Text");
        }



    }
}