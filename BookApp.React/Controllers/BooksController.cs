using BookApp.Shared;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Apis.Controllers
{
    [ApiController] // web api
    [Route("api/[controller]")]
    [Produces("application/json")] // 기본값이라 안넣어도 됨
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _repository;
        private readonly ILogger _logger;

        public BooksController(IBookRepository repository, ILoggerFactory loggerFactory)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(BooksController));
            this._logger = loggerFactory.CreateLogger(nameof(BooksController));
        }

        #region 출력
        // Get api/Books
        [HttpGet] // [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var models = await _repository.GetAllAsync();
                if (!models.Any())
                {
                    return new NoContentResult();
                }
                return Ok(models);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion

        #region 상세
        // Get api/Books/123
        [HttpGet("{id:int}", Name= "GetBookById")] // Name 속성을 RouteName 설정
        public async Task<IActionResult> GetById([FromRoute] int id) // fromroute : postmanㅇ에서 url에 넣은 데이터
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                if (model == null)
                {
                    return NotFound();
                }
                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion

        #region 입력
        // POST api/Books
        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Book dto) // data trans object + formbody : postman에서 body에 넣은 데이터
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var temp = new Book();
            temp.Title = dto.Title;
            temp.Description = dto.Description;
            temp.Created = DateTime.Now;

            try
            {
                var model = await _repository.AddAsync(temp);
                if (model == null)
                {
                    return BadRequest();
                }

                if(DateTime.Now.Second % 60 == 0)
                {
                    return Ok(model); // 200 ok
                } else if (DateTime.Now.Second % 3 == 0)
                {
                    return CreatedAtRoute("GetBookById", new { id = model.Id }, model);
                } else if (DateTime.Now.Second % 2 == 0)
                {
                    var uri = Url.Link("GetBookById", new {id = model.Id});
                    return Created(uri, model); // 201 created
                }
                else
                {
                    // GetById 액션 이름을 사용해서 입력된 데이터 반환
                    return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
                }
                
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion

        #region 수정
        // Put api/Books/123
        [HttpPut("{id}")] // Name 속성을 RouteName 설정
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] Book dto) // fromroute : postmanㅇ에서 url에 넣은 데이터
        {
            if(dto == null) return BadRequest();
            if (!ModelState.IsValid) return BadRequest();
            
            try
            {
                dto.Id = id;
                var status = await _repository.UpdateAsync(dto);

                if (!status)
                {
                    return BadRequest();
                }

                // 204 No Content
                return NoContent(); // 이미 전송된 정보에 모든 값을 가지고 있기에..(?)

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
        #endregion

        #region 삭제
        // Delete api/Books/1
        [HttpDelete("{id:int}")] // Name 속성을 RouteName 설정
        public async Task<IActionResult> DeleteAsync(int id) // fromroute : postmanㅇ에서 url에 넣은 데이터
        {
            try
            {
                var status = await _repository.DeleteAsync(id);

                if (!status)
                {
                    return BadRequest();
                }

                // 204 No Content
                return NoContent(); // 이미 전송된 정보에 모든 값을 가지고 있기에..(?)

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest("삭제할 수 없습니다.");
            }
        }
        #endregion
    }
}
