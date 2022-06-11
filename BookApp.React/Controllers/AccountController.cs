using BookApp.Shared;
using Microsoft.AspNetCore.Mvc;

using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System.Security.Cryptography;

namespace BookApp.React.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IHaruUserRepository _repository;
        private readonly ILogger _logger;

        public AccountController(IHaruUserRepository repository, ILoggerFactory loggerFactory)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(AccountController));
            this._logger = loggerFactory.CreateLogger(nameof(AccountController));
        }

        //#region 출력
        //[HttpGet] // [HttpGet("[action]")]
        //public async Task<IActionResult> GetAll()
        //{
        //    try
        //    {
        //        var models = await _repository.GetAllAsync();
        //        if (!models.Any())
        //        {
        //            return new NoContentResult();
        //        }
        //        return Ok(models);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest();
        //    }
        //}
        //#endregion

        //#region 상세
        //// Get api/Books/123
        //[HttpGet("{id:int}", Name = "GetUserById")] // Name 속성을 RouteName 설정
        //public async Task<IActionResult> GetById([FromRoute] int id) // fromroute : postmanㅇ에서 url에 넣은 데이터
        //{
        //    try
        //    {
        //        var model = await _repository.GetByIdAsync(id);
        //        if (model == null)
        //        {
        //            return NotFound();
        //        }
        //        return Ok(model);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest();
        //    }
        //}
        //#endregion

        #region 로그인
        // Get api/account/123
        [HttpPut] // [HttpGet("[action]")]
        public async Task<IActionResult> GetHaruUser([FromBody] HaruUser dto)
        {
            try
            {
                var model = await _repository.GetHaruUser(dto.UserID);

                // id 존재x
                if (model == null)
                {
                    return NotFound();
                }

                #region sha1 + salt + iteration
                Rfc2898DeriveBytes key = new($"{dto.UserID} + {dto.Pass}", Convert.FromBase64String(model.Salt), model.Iteration, HashAlgorithmName.SHA256);

                var result = key.GetBytes(128).SequenceEqual(Convert.FromBase64String(model.Pass));

                if (result == true)
                {
                    HttpContext.Session.SetString("userid", model.UserID);
                }

                return new JsonResult(result);

                #endregion

                #region .net sha256 + salt
                //var salt = Convert.FromBase64String(model.Salt);

                //// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
                //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                //    password: dto.Pass,
                //    salt: salt,
                //    prf: KeyDerivationPrf.HMACSHA256,
                //    iterationCount: 100000,
                //   numBytesRequested: 256 / 8)); 

                //var result = "";

                //if (hashed.Equals(model.Pass))
                //{
                //    HttpContext.Session.SetString("userid", model.UserID);
                //    result = "1";
                //}
                //else
                //{
                //    result = "-1";
                //}
                #endregion


                //return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion

        #region 등록
        // POST api/Account
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] HaruUser dto) // data trans object + formbody : postman에서 body에 넣은 데이터
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var haruUser = await _repository.GetHaruUser(dto.UserID);

            // id 중복
            if (haruUser != null)
            {
                return new JsonResult(false);
            }

            #region sha1 + salt + iterations
            // i choose a salt with the length of 32
            byte[] salt = new byte[32];

            // now we fill salt with random bytes.
            // this function take care of that.
            RandomNumberGenerator.Fill(salt);

            // the algorithm which generates the key (hash) calculations the key in server1 rounds.
            // the higher the number, to longer it takes. i choose between 1000 and 5000 rounds for this.
            int iterations = RandomNumberGenerator.GetInt32(1000, 5000);

            // this class does all the magic.
            // we give this class a combination of username and password
            // along with the random-generated salt and a random number of rounds.
            // to generate the key (hash)
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes($"{dto.UserID} + {dto.Pass}", salt, iterations, HashAlgorithmName.SHA256);
            #endregion


            #region .net 6의 sha256 + salt 
            //// generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            //byte[] salt = new byte[128 / 8];
            //using (var rngCsp = new RNGCryptoServiceProvider())
            //{
            //    rngCsp.GetBytes(salt);
            //}
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            //// derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            //string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            //    password: dto.Pass,
            //    salt: salt,
            //    prf: KeyDerivationPrf.HMACSHA256,
            //    iterationCount: 100000,
            //   numBytesRequested: 256 / 8));

            //Console.WriteLine($"Hashed: {hashed}");

            //var temp = new HaruUser();
            //temp.UserID = dto.UserID;
            //temp.Pass = hashed;
            //temp.Salt = Convert.ToBase64String(salt);
            //temp.Created = DateTime.Now;
            #endregion


            var temp = new HaruUser();
            temp.UserID = dto.UserID;
            temp.Pass = Convert.ToBase64String(key.GetBytes(128));
            temp.Salt = Convert.ToBase64String(salt);
            temp.Iteration = iterations;
            temp.Created = DateTime.Now;

            try
            {
                var model = await _repository.AddAsync(temp);
                if (model == null)
                {
                    return new JsonResult(false);
                }

                return new JsonResult(true);
                //return Ok(model); // 200 ok

                //if (DateTime.Now.Second % 60 == 0)
                //{
                //    return Ok(model); // 200 ok
                //}
                //else if (DateTime.Now.Second % 3 == 0)
                //{
                //    return CreatedAtRoute("GetBookById", new { id = model.Id }, model);
                //}
                //else if (DateTime.Now.Second % 2 == 0)
                //{
                //    var uri = Url.Link("GetBookById", new { id = model.Id });
                //    return Created(uri, model); // 201 created
                //}
                //else
                //{
                //    // GetById 액션 이름을 사용해서 입력된 데이터 반환
                //    return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
                //}

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion


        //#region 수정
        //// Put api/Books/123
        //[HttpPut("{id}")] // Name 속성을 RouteName 설정
        //public async Task<IActionResult> UpdateAsync(string userID, [FromBody] HaruUser dto) // fromroute : postmanㅇ에서 url에 넣은 데이터
        //{
        //    if (dto == null) return BadRequest();
        //    if (!ModelState.IsValid) return BadRequest();

        //    try
        //    {
        //        dto.UserID = userID;
        //        var status = await _repository.UpdateAsync(dto);

        //        if (!status)
        //        {
        //            return BadRequest();
        //        }

        //        // 204 No Content
        //        return NoContent(); // 이미 전송된 정보에 모든 값을 가지고 있기에..(?)

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest();
        //    }
        //}
        //#endregion

        //#region 삭제
        //// Delete api/Books/1
        //[HttpDelete("{id:int}")] // Name 속성을 RouteName 설정
        //public async Task<IActionResult> DeleteAsync(int id) // fromroute : postmanㅇ에서 url에 넣은 데이터
        //{
        //    try
        //    {
        //        var status = await _repository.DeleteAsync(id);

        //        if (!status)
        //        {
        //            return BadRequest();
        //        }

        //        // 204 No Content
        //        return NoContent(); // 이미 전송된 정보에 모든 값을 가지고 있기에..(?)

        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest("삭제할 수 없습니다.");
        //    }
        //}
        //#endregion
    }
}
