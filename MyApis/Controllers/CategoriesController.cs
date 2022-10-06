using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyApis.Model;

namespace MyApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
       
        private static List<Category> cat = new List<Category>() { 
        new Category()
        {
            Id= 1,
            Name="Shivam jha",
            ImageUrl="asdaf"
        },
        new Category() 
        {
            Id = 2,
            Name="Sourabh",
            ImageUrl="nbwfmn"
        },
        new Category()
        {
            Id=3,
            Name="Category",
            ImageUrl="jhfdjdfsdf"
        }
        
        
        };
        [HttpGet("{i}")]
        public IEnumerable<Category> Getdata(int i)
        {
            return cat;
        }
        [HttpPost]
        public IEnumerable<Category> Add( [FromBody] Category data)
        {
            cat.Add(data);
            return cat;
        }
        [HttpPut("{Id}")]
        public Category UpadateRecord(int Id, [FromBody] Category cdata)
        {
            cat[Id] = cdata;
            return cat[Id];
        }
        [HttpDelete("{Id}")]
        public string DeleteData(int Id)
        {
            cat.RemoveAt(Id);   
            return "Successfully Deleted";
        }

    }
}
