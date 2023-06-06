using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreApp.DAL.Models.DTO;
using AspNetCoreApp.DAL.Models;
using DAL.Repository;
using AspNetCoreApp.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesWriteController : ControllerBase
    {
        IDbCrud dbo;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор контроллера LineWritesController
        /// </summary>
        /// <param name="newDbRepos">Переменная репозитория</param>
        /// <param name="logger">Перемнная журнала логирования</param>
        public LinesWriteController(IDbCrud dbCrud, ILogger<LinesWriteController> logger)
        {
            _logger = logger;
            dbo = dbCrud;
        }

        /// <summary>
        /// GET запрос получения строк актов списания(LineWrite)
        /// </summary>
        /// <returns>_linewriteRepository.LineWriteRepository.Get().ToList() - список строк актов списания</returns>

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineWrite>>> GetLineWrite()
        {
            // Информационное сообщение журнала
            _logger.LogInformation("LinesWriteControlle is Invoked with the function GetLinesWrite()");
            try
            {
                _logger.LogInformation("Request GET completed successfully");
                return dbo.AllGetLinesWrites().ToList();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// GET запрос получения строки акта списания(LineWrite) по номеру
        /// </summary>
        /// <param name="ID">Номер строки акта списания</param>
        /// <returns>linewrite - найденная по номеру строка акта списания</returns>

        // GET: 
        [HttpGet("{ID}")]
        public async Task<ActionResult<LineWrite>> GetLineWrite(int ID)
        {
            _logger.LogInformation("LinesWriteControlle is Invoked with the function GetLinesWrite(int ID)");
            try
            {
                var linewrite = dbo.GetLineWrites(ID);

            if (linewrite == null)
            {
                return NotFound();
            }
                _logger.LogInformation("Request GET completed successfully");
                return  linewrite;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// PUT запрос изменения строки акта списания(LineWrite) по номеру
        /// </summary>
        /// <param name="ID">номер строки акта списания</param>
        /// <param name="linewrite">объект Строка акта списания, которая будет изменяться</param>
        /// <returns>Измененная строка акта списания</returns>

        // PUT: 
        [HttpPut("{ID}")]
        public async Task<ActionResult<IEnumerable<LineWrite>>> Putlinewrite(int ID, LineWrite linewrite)
        {
            _logger.LogInformation("LinesWriteControlle is Invoked with the function PutLinesWrite(int ID, LinesWrite linesWrite)");

            try
            {
                if (ID != linewrite.ID)
            {
                return BadRequest();
            }

            dbo.UpdateLineWrite(linewrite);

            try
            {
                    dbo.Save();
                    _logger.LogInformation("Request PUT completed successfully");
                    return dbo.AllGetLinesWrites().ToList();
                }
            catch (DbUpdateConcurrencyException)
            {
                if (!linewriteExists(ID))
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
        /// POST запрос добавления новой строки акта списания(LineWrite) в БД
        /// </summary>
        /// <param name="linewriteDTO">Объект Строка акта списания, которая будет добавлена</param>
        /// <returns>"GetLineWrite", new { ID = LineWrite.ID }, LineWrite - новая строка акта списания</returns>

        // POST: 
        [HttpPost]
        public async Task<ActionResult<LineWrite>> PostLinewrite(LineWrite linewrite)
        {
            _logger.LogInformation("LinesWriteControlle is Invoked with the function PostLinesWrite(LinesWrite linesWrite)");

            try
            {
                dbo.CreateLineWrite(linewrite);
                dbo.Save();

            _logger.LogInformation("Request POST completed successfully");

                return CreatedAtAction("GetLineWrite", new { iD = linewrite.ID }, linewrite);
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// DELETE запрос удаления строки акта списания(LineWrite) из БД по номеру строки акта списания
        /// </summary>
        /// <param name="ID">номер строки акта списания</param>
        /// <returns></returns>
       
        // DELETE: 
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Deletelinewrite(int ID)
        {
            _logger.LogInformation("LinesWriteControlle is Invoked with the function DeleteLinesWrite(int ID)");

            try
            {
                   var linewrite = dbo.GetLineWrites(ID);

                if (linewrite == null)
            {
                return NotFound();
            }
               dbo.DeleteLineWrite(ID); 
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
        /// Функция проверки существования строки акта списания(LineWrite) по номеру
        /// </summary>
        /// <param name="ID"> номер строки акта списания</param>
        /// <returns>LineWrite - Строка акта списания, если она существует</returns>
        private bool linewriteExists(int ID)
        {
            _logger.LogInformation("LinesWriteControlle is Invoked");
            try
            {
                _logger.LogInformation("Exists completed successfully");
                return dbo.GetLineWrites(ID) != null;
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
