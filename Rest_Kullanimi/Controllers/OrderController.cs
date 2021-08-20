using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;


namespace Rest_Kullanimi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class OrderController : ControllerBase
    {
        //katmanlı mimari
        //dapper e bakacagım liste yada tek dönüyoruz
        //xml ile baglantı cümlesi var
        //datalogic de yapıyoruz crud işlemlerini 
        // 3 get olacak 1 post

        [HttpGet]
        public List<Orders> GetAllOrders()
        {

            //Orders ord = new Orders();
            //burdan sınıfa erişiyorum ama bu sınıf anayerde

            SqlConnection bgl = new SqlConnection(@"Data Source=DESKTOP-E9UTSVL;Initial Catalog=KahveSiparis;Integrated Security=True");
            bgl.Open();
            SqlCommand komut = new SqlCommand("select [SipId],[SipAdSoyad],[SipTelNo],[SipAdres],[SipTutar] from [orders]", bgl);
            DataTable tablo = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(komut);
            da.Fill(tablo);

            List<Orders> order = new List<Orders>();

            order = tablo.AsEnumerable().Select(s => new Orders
            {
                SipId = s.Field<int>("SipId"),
                SipAdSoyad = s.Field<string>("SipAdSoyad"),
                SipTelNo = s.Field<string>("SipTelNo"),
                SipAdres = s.Field<string>("SipAdres"),
                SipTutar = s.Field<decimal>("SipTutar")
            }).ToList();

            return order;     
        }


    }
}
