using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AspNetCoreApp.DAL.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using AspNetCoreApp.DAL.Models;
using DAL.Repository;
using AspNetCoreApp.BLL;

namespace AspNetCoreApp.Controllers
{
    [Route("api/[controller]")]
    [EnableCors]
    [ApiController]
    public class TovarController : ControllerBase
    {
        IDbCrud dbo;
        private readonly ILogger _logger;

        /// <summary>
        /// Конструктор контроллера TovarController
        /// </summary>
        /// <param name="newDbRepos">Переменная репозитория</param>
        /// <param name="logger">Перемнная журнала логирования</param>
        public TovarController(IDbCrud dbCrud, ILogger<TovarController> logger)
        {
            _logger = logger;
            dbo = dbCrud;
        }

        /// <summary>
        /// GET запрос получения списка товаров(Tovar)
        /// </summary>
        /// <returns>_TovarRepository.TovarRepository.Get().ToList() - список товаров</returns>

        // GET: 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tovar>>> GetTovar()
        {
            // Информационное сообщение журнала
            _logger.LogInformation("TovarController is Invoked with the function GetTovar()");
            try
            {
                _logger.LogInformation("Request GET completed successfully");
                return dbo.AllGetTovars().ToList();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// GET запрос получения списка товара(Tovar) по номеру
        /// </summary>
        /// <param name="cod">Номер товара</param>
        /// <returns>Tovar - найденный по номеру товара</returns>

        // GET: 
        [HttpGet("{cod}")]
        public async Task<ActionResult<Tovar>> GetTovar(int cod)
        {
            _logger.LogInformation("TovarController is Invoked with the function GetTovar(int cod)");
            try
            {
                var tovar = dbo.GetTovars(cod);

            if (tovar == null)
            {
                return NotFound();
            }
                _logger.LogInformation("Request GET completed successfully");
                return tovar;
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// PUT запрос изменения товара(Tovar) по номеру
        /// </summary>
        /// <param name="cod">номер товара</param>
        /// <param name="Tovar">объект Товар, который будет изменяться</param>
        /// <returns>Измененный товар</returns>

        // PUT: 
        [HttpPut("{cod}")]   
        public async Task<ActionResult<IEnumerable<Tovar>>> PutTovar(int cod, Tovar tovar)
        {
            _logger.LogInformation("TovarController is Invoked with the function PutTovar(int cod, Tovar tovar)");

            try
            {
                if (cod != tovar.CodTovara)
            {
                return BadRequest();
            }
                dbo.UpdateTovar(tovar);

            try
            {
                    dbo.Save();
                    _logger.LogInformation("Request PUT completed successfully");
                    return dbo.AllGetTovars().ToList();
                }
            catch (DbUpdateConcurrencyException)
            {
                if (!TovarExists(cod))
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
        /// POST запрос добавления нового товара(Tovar) в БД
        /// </summary>
        /// <param name="Tovar">Объект Товар, который будет добавлен</param>
        /// <returns>"GetTovar", new { cod = Tovar.cod }, Tovar - новый товар</returns>


        // POST: 
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Tovar>>> PostTovar(Tovar tovar)
        {
            _logger.LogInformation("TovarController is Invoked with the function PostTovar(Tovar tovar)");

            try
            {
                if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                dbo.CreateTovar(tovar);
                dbo.Save();

                _logger.LogInformation("Request POST completed successfully");

                
                return dbo.AllGetTovars().ToList();
            }
            catch (Exception ex)
            {
                // Сообщение об ошибке журнала/исключении
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }


        /// <summary>
        /// DELETE запрос удаления товара(Tovar) из БД по номеру товара
        /// </summary>
        /// <param name="cod">номер товара</param>
        /// <returns></returns>

        // DELETE: 
        [HttpDelete("{cod}")]
        public async Task<IActionResult> DeleteTovar(int cod)
        {
            _logger.LogInformation("TovarController is Invoked with the function DeleteTovar(int cod)");

            try
            {
                  var tovar = dbo.GetTovars(cod);
                
                if (tovar == null)
            {
                return NotFound();
            }
            dbo.DeleteTovar(cod);
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
        /// Функция проверки существования товара(Tovar) по номеру
        /// </summary>
        /// <param name="cod"> номер товара</param>
        /// <returns>Tovar - товар, если он существует</returns>
        private bool TovarExists(int cod)
        {
            _logger.LogInformation("TovarController is Invoked");
            try
            {
                _logger.LogInformation("Exists completed successfully");
                return dbo.GetTovars(cod) != null;
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
