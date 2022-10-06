using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyApis.Data;
using MyApis.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Category1Controller : ControllerBase
    {
        private ApiDBContext _db = new ApiDBContext();
       private readonly IConfiguration _config;
        public Category1Controller(IConfiguration configuration)
        {
            _config = configuration;
        }

        [HttpGet("GetJWTToken")]
        public IActionResult GetJwtToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>();
            claims.Add(new Claim("UserId", "shivamjha"));
            var token = new JwtSecurityToken( issuer: _config["JWT:Issuer"], audience: _config["JWT:Audience"], claims, expires: DateTime.Now.AddHours(2),signingCredentials: credentials);
            var jwttoken = new JwtSecurityTokenHandler().WriteToken(token);
            return Ok(jwttoken);
        }


        [HttpGet("[action]")]
        [ResponseCache(Duration = 60)]
        public IActionResult GetCategoryData()
        {
           return Ok(_db.Categories.ToList());
        }

        [HttpGet("GetAllCategories")]
        public IEnumerable<Category> GetCategoryById()
        {
             return _db.Categories.ToList();
        }

        [HttpPost]
        [Route("AddingCategory")]
        [Authorize]
        public IActionResult AddRecord([FromBody] Category categories)
        {
            if(categories.Name != null)
            {
                _db.Categories.Add(categories);
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

        }
        [HttpPut("UpdateRecord")]
        public int UpdateRecord([FromBody] Category category)
        {

            _db.Categories.Update(category);
            int result = _db.SaveChanges(); 
            return result;

        }

        [HttpDelete("DeleteData/{id}")]
        public IActionResult DeleteData(int id)
        {

            Category category = _db.Categories.Find(id);
            if( category != null)
            {
                _db.Categories.Remove(category);
                int Result = _db.SaveChanges();
                return Ok();
            }
            else
                return NotFound("no record found");
            
        }





        // GET api/<Category1Controller>/5
        [HttpGet("{Id}")]
        public string Get(int Id)
        {
            return "value";
        }

        // POST api/<Category1Controller>
        [HttpPost]
        public void Post([FromBody] Category categories)
        {
        }

        // PUT api/<Category1Controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<Category1Controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("GetToken")]
        public object GetToken()
        {
            string key = "my_secret_key_12345";
            var issuer = "http://mysite.com";
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var permClaims = new List<Claim>();
            permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            permClaims.Add(new Claim("valid", "1"));
            permClaims.Add(new Claim("UserId", "1"));
            permClaims.Add(new Claim("name", "shivam"));

            var token = new JwtSecurityToken(issuer,issuer,permClaims,expires:DateTime.Now.AddDays(1),
                signingCredentials:credentials);
            
            var jwt_token = new JwtSecurityTokenHandler().WriteToken(token);
            return new {data = jwt_token};



        }
        [HttpGet("[action]")]
        public string GetMyDetails()
        {
            if (User.Identity.IsAuthenticated)
                return "I am Authenticated";
            else
                return "Not Authenticated";

        }
        [HttpGet("[action]")]
        [Authorize]
        public string ValidAuthorization()
        {
            return "Valid_Authorization";
        }

        
    }
}
