using Microsoft.AspNetCore.Http;
using System;
using System.Security.Cryptography;
using System.Text;

namespace AppNoOne.TagHelper
{
    public static class Utility
    {
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    stringBuilder.Append(data[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }

        public static void SetCookie(HttpResponse Response, string key, string value)
        {
            CookieOptions options = new CookieOptions
            {
                // Cấu hình các thuộc tính của cookie
                Expires = DateTime.Now.AddHours(8), // Thời gian hết hạn của cookie (7 ngày)
                Secure = true, // Sử dụng kết nối an toàn (HTTPS)
                HttpOnly = false // Chỉ cho phép truy cập thông qua HTTP
            };
            Response.Cookies.Append(key, value, options);
        }
        
        public static string GetCookie(HttpContext context, string key)
        {
            // Lấy giá trị của cookie có tên "MyCookie"
            string cookieValue = context.Request.Cookies[key];
            // Kiểm tra xem cookie có tồn tại hay không
            if (cookieValue != null)
                return cookieValue;
            return string.Empty;
        }
    }

    public class CurrentUser
    {

    }
}
