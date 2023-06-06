using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using AspNetCoreApp.DAL.Models.DTO;
using AspNetCoreApp.DAL.Models;
using DAL.Repository;
using AspNetCoreApp.BLL;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinesOrderController : ControllerBase
    {
        IDbCrud dbo;
        private readonly ILogger _logger;


        /// <summary>
        /// Конструктор контроллера OrdersController
        /// </summary>
        /// <param name="newDbRepos">Переменная репозитория</param>
        /// <param name="logger">Перемнная журнала логирования</param>
        public LinesOrderController(IDbCrud dbCrud, ILogger<LinesOrderController> logger)
        {
            _logger = logger;
             dbo = dbCrud;

        }

        /// <summary>
        /// GET запрос получения списка строк строки заказа(LineOrder)
        /// </summary>
        /// <returns>_lineorderRepository.LineOrderRepository.Get().ToList() - список строк строки заказа</returns>

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LineOrder>>> GetLineOrder()
        {
            // Информационное сообщение журнала
            _logger.LogInformation("LinesOrderController is Invoked with the function GetLineOrder()");
            try
            {
                _logger.LogInformation("Request GET completed successfully");
                return dbo.AllGetLineOrders().ToList();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }


        /// <summary>
        /// GET запрос получения списка строки заказа(LineOrder) по номеру
        /// </summary>
        /// <param name="ID">Номер строки заказа</param>
        /// <returns>lineorder - найденная по номеру строки заказа</returns>

        // GET: 
        [HttpGet("{ID}")]
        public async Task<ActionResult<LineOrder>> GetLineOrder(int ID)
        {
            _logger.LogInformation("LinesOrderController is Invoked with the function GetLineOrder(int ID)");
            try
            {
                var lineorder = dbo.GetLineOrders(ID);

            if (lineorder == null)
            {
                return NotFound();
            }
                _logger.LogInformation("Request GET completed successfully");
                return lineorder;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// PUT запрос изменения строки заказа(LineOrder) по номеру
        /// </summary>
        /// <param name="ID">номер строки заказа</param>
        /// <param name="lineorder">объект Строка заказа, которая будет изменяться</param>
        /// <returns>Измененная строка заказа</returns>

        // PUT: 
        [HttpPut("{ID}")]
        public async Task<ActionResult<IEnumerable<LineOrder>>> Putlineorder(int ID, LineOrder lineorder)
        {
            _logger.LogInformation("LinesOrderController is Invoked with the function PutLineOrder(int ID, Order order)");

            try
            {
                if (ID != lineorder.ID)
            {
                return BadRequest();
            }
            dbo.UpdateLineOrder(lineorder);

            try
            {
                 dbo.Save();
                _logger.LogInformation("Request PUT completed successfully");
                    return dbo.AllGetLineOrders().ToList();
                }
            catch (DbUpdateConcurrencyException)
            {
                if (!lineorderExists(ID))
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
        /// POST запрос добавления новой строки заказа(LineOrder) в БД
        /// </summary>
        /// <param name="lineorderDTO">Объект Строка заказа, которая будет добавлена</param>
        /// <returns>"GetOrder", new { ID = order.ID }, order - новый заказ</returns>

        // POST: 
        [HttpPost]
        public async Task<ActionResult<LineOrder>> PostLineorder(LineOrder lineorder)
        {
            _logger.LogInformation("LinesOrderController is Invoked with the function PostLineOrder(Order order)");

            try
            {
                dbo.CreateLineOrder(lineorder);
                dbo.Save();

            _logger.LogInformation("Request POST completed successfully");

                return CreatedAtAction("GetLineOrder", new { iD  = lineorder.ID }, lineorder);
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// DELETE запрос удаления строки заказа(LineOrder) из БД по номеру строки заказа
        /// </summary>
        /// <param name="ID">номер строки заказа</param>
        /// <returns></returns>

        // DELETE: 
        [HttpDelete("{ID}")]
        public async Task<IActionResult> Deletelineorder(int ID)
        {
            _logger.LogInformation("LinesOrderController is Invoked with the function DeleteLineOrder(int ID)");

            try
            {
                var lineorder = dbo.GetLineOrders(ID);

                if (lineorder == null)
            {
                return NotFound();
            }
                dbo.DeleteLineOrder(ID);
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
        /// Функция проверки существования строки заказа(LineOrder) по номеру
        /// </summary>
        /// <param name="ID"> номер строки заказа</param>
        /// <returns>LineOrder - строка заказа, если она существует</returns>

        private bool lineorderExists(int ID)
        {
            _logger.LogInformation("OrderController is Invoked");
            try
            {
                _logger.LogInformation("Exists completed successfully");
                return dbo.GetLineOrders(ID) != null;
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
