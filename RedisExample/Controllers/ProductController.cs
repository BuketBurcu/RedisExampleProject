using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

[ApiController]
[Route("[controller]")]
public class ProductController : Controller
{
    [HttpGet]
    public string GetProduct([FromQuery] string key)
    {
        var data = RedisIndexer.Intance.GetProduct(key);
        if (data == null)
        {
            //Eğer data rediste bulunmuyorsa Db'ye git al ve Redis'e de ekle..
            RedisIndexer.Intance.SetProduct(key, "value");
        }
        return data;
    }

    [HttpPost]
    public string AddProduct([FromQuery] string key, [FromBody] ProductDto category)
    {
        //Db'ye kaydetme işlemleri yaptıktan sonra Redis'e de json'a çevirip ekliyoruz.
        RedisIndexer.Intance.SetProduct(key, JsonSerializer.Serialize(category));
        return "Data ekleme başarılı.";
    }

    [HttpPut]
    public string UpdateProduct([FromQuery] string key, [FromBody] ProductDto category)
    {
        var data = RedisIndexer.Intance.GetProduct(key);
        //Güncellenmek istenen data redis'te varsa alıp güncelleyip tekrar set ediyoruz.
        RedisIndexer.Intance.SetProduct(key, JsonSerializer.Serialize(data));
        return "Data güncelleme başarılı.";
    }

    [HttpDelete]
    public string DeleteProduct([FromQuery] string key)
    {
        //Bir ürün silindiyse artık anında veriyi dönmek istemiyorsak Redis'ten de silebiliriz.
        RedisIndexer.Intance.DeleteProduct(key);
        return "Data silme işlemi başarılı.";
    }
}
