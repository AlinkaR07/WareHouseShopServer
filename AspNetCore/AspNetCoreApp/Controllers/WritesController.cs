using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreApp.DAL.Models;
using DAL.Repository;
using AspNetCoreApp.FoodShopModels;
using Write = AspNetCoreApp.DAL.Models.Write;
using AspNetCoreApp.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class WritesController : ControllerBase
    {
        IDbCrud dbo;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор контроллера WritesController
        /// </summary>
        /// <param name="newDbRepos">Переменная репозитория</param>
        /// <param name="logger">Перемнная журнала логирования</param>
        public WritesController(IDbCrud dbCrud, ILogger<WritesController> logger)
        {
            _logger = logger;
            dbo = dbCrud;
        }

        /// <summary>
        /// GET запрос получения списка актов списаний(Write)
        /// </summary>
        /// <returns>_writeRepository.WriteRepository.Get().ToList() - список актов списаний</returns>

        // GET:
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Write>>> GetWrite()
        {
            // Информационное сообщение журнала
            _logger.LogInformation("WriteController is Invoked with the function GetWrite()");
            try
            {
                _logger.LogInformation("Request GET completed successfully");
                  return dbo.AllGetWrites().ToList();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// GET запрос получения списка актов списаний(Write) по номеру
        /// </summary>
        /// <param name="number">Номер акта списания</param>
        /// <returns>write - найденный по номеру акта списания</returns>

        // GET:
        [HttpGet("{number}")]
        public async Task<ActionResult<Write>> GetWrite(int number)
        {
            _logger.LogInformation("WriteController is Invoked with the function GetWrite(int number)");
            try
            {
                var write = dbo.GetWrites(number);

            if (write == null)
            {
                return NotFound();
            }
                _logger.LogInformation("Request GET completed successfully");
                return write;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// PUT запрос изменения акта списания(Write) по номеру
        /// </summary>
        /// <param name="number">номер акта списания</param>
        /// <param name="write">объект Акт списания, который будет изменяться</param>
        /// <returns>Измененный акт списания</returns>

        // PUT: 
        [HttpPut("{number}")]
        public async Task<IActionResult> PutWrite(int number, Write write)
        {
            _logger.LogInformation("WriteController is Invoked with the function PutWrite(int number, Write write)");

            try
            {
                if (number != write.NumberAct)
            {
                return BadRequest();
            }
                dbo.UpdateWrite(write);

            try
            {
                    dbo.Save();
                    _logger.LogInformation("Request PUT completed successfully");
                }
            catch (DbUpdateConcurrencyException)
            {
                if (!WriteExists(number))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// POST запрос добавления нового акта списания(Write) в БД
        /// </summary>
        /// <param name="write">Объект Акт списания, который будет добавлен</param>
        /// <returns>"GetWrite", new { number = write.Number }, write - новый акт списания</returns>

        // POST:
        [HttpPost]
        public async Task<ActionResult<Write>> PostWrite(Write write)
        {
            _logger.LogInformation("WriteController is Invoked with the function PostWrite(Write write)");

            try
            {
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
              dbo.CreateWrite(write);
                dbo.Save();

                _logger.LogInformation("Request POST completed successfully");

                return CreatedAtAction("GetWrite", new { number = write.NumberAct }, write);
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// DELETE запрос удаления акта списания(Write) из БД по номеру акта списания
        /// </summary>
        /// <param name="number">номер акта списания</param>
        /// <returns></returns>

        // DELETE: 
        [HttpDelete("{number}")]
        public async Task<IActionResult> DeleteWrite(int number)
        {
            _logger.LogInformation("WriteController is Invoked with the function DeleteOrder(int number)");

            try
            {
                  var write = dbo.GetWrites(number);

                if (write == null)
            {
                return NotFound();
            }
            dbo.DeleteWrite(number); 
            dbo.Save();

                _logger.LogInformation("Request DELETE completed successfully");

                return NoContent();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// Функция проверки существования акта списания(Write) по номеру
        /// </summary>
        /// <param name="number"> номер акта списания</param>
        /// <returns>Write - акт списания, если он существует</returns>

        private bool WriteExists(int number)
        {
            _logger.LogInformation("WriteController is Invoked");
            try
            {
                _logger.LogInformation("Exists completed successfully");
                return dbo.GetWrites(number) != null;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }
     }
}
