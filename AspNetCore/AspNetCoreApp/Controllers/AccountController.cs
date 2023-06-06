using AspNetCoreApp.Controllers;
using AspNetCoreApp.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ASPNetCoreApp.Controllers
{
    [Produces("application/json")]
    public class AccountController : Controller
    {
        private readonly ILogger _logger;                                 // переменная журнала логированния
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        /// <summary>
        /// Конструктор контроллера AccountController
        /// </summary>
        /// <param name="userManager">Менеджер пользователя</param>
        /// <param name="signInManager">Менеджер входа</param>
        /// <param name="logger">Переменная журнала логированния</param>
        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        /// Функция регистрации
        /// </summary>
        /// <param name="model">Модель для регистрации, включающая: логин, пароль и пароль подверждение</param>
        /// <returns> В случае успешного выполнения: message = "Добавлен новый пользователь: " + user.UserName, userName = model.Email
        ///           В случае неуспешного выполнения: errorMsg
        /// </returns>

        [HttpPost]
        [Route("api/account/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterViewModel model)
        {
            _logger.LogInformation("AccountController is Invoked with the function Register()");
            try
            {

                if (ModelState.IsValid)
                {
                    User user = new() { Email = model.Email, UserName = model.Email };
                    // Добавление нового пользователя
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        // Установка роли User
                        await _userManager.AddToRoleAsync(user, "user");
                        // Установка куки
                        await _signInManager.SignInAsync(user, false);
                        _logger.LogInformation("Request POST completed successfully");
                        return Ok(new { message = "Добавлен новый пользователь: " + user.UserName, userName = model.Email });
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        var errorMsg = new
                        {
                            message = "Пользователь не добавлен",
                            error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                        };
                        _logger.LogInformation("Request POST completed successfully");
                        return Created("", errorMsg);
                    }
                }
                else
                {
                    var errorMsg = new
                    {
                        message = "Неверные входные данные",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    _logger.LogInformation("Request POST completed successfully");
                    return Created("", errorMsg);
                }
            }
            catch (Exception ex)
            {
                // Log error/exception message
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// Функция выполнения аутентификации и авторизации
        /// </summary>
        /// <param name="model">Модель для аутентификации, включающая: логин, пароль и подверждение на запоминание</param>
        /// <returns>В случае успешного выполнения: message = "Выполнен вход", userName = model.Email, userRole
        ///           В случае неуспешного выполнения: errorMsg
        /// </returns>

        [HttpPost]
        [Route("api/account/login")]
        //[AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            _logger.LogInformation("AccountController is Invoked with the function Login()");
            try
            {
                if (ModelState.IsValid)
            {
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    IList<string>? roles = await _userManager.GetRolesAsync(user);
                    string? userRole = roles.FirstOrDefault();
                    _logger.LogInformation("Request POST completed successfully");
                    return Ok(new { message = "Выполнен вход", userName = model.Email, userRole });
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                    var errorMsg = new
                    {
                        message = "Вход не выполнен",
                        error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                    };
                    _logger.LogInformation("Request POST completed successfully");
                    return Created("", errorMsg);
                }
            }
            else
            {
                var errorMsg = new
                {
                    message = "Вход не выполнен",
                    error = ModelState.Values.SelectMany(e => e.Errors.Select(er => er.ErrorMessage))
                };
                _logger.LogInformation("Request POST completed successfully");
                return Created("", errorMsg);
            }
        }
            catch (Exception ex)
            {
                // Log error/exception message
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
}


        /// <summary>
        /// Функция деавторизации(выхода из аккаунта)
        /// </summary>
        /// <returns> В случае успешного выполнения: message = "Выполнен выход", userName = ""; если же выход был не выполнен: message = "Сначала выполните вход" </returns>

        [HttpPost]
        [Route("api/account/logoff")]
        public async Task<IActionResult> LogOff()
        {
            _logger.LogInformation("AccountController is Invoked with the function LogOff()");
            try
            {
                User usr = await GetCurrentUserAsync();
            if (usr == null)
            {
                    _logger.LogInformation("Request POST completed successfully");
                    return Unauthorized(new { message = "Сначала выполните вход" });
            }
            // Удаление куки
            await _signInManager.SignOutAsync();
                _logger.LogInformation("Request POST completed successfully");
                return Ok(new { message = "Выполнен выход", userName = "" });
            }
            catch (Exception ex)
            {
                // Log error/exception message
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        /// <summary>
        /// Функция проверки аутентификации
        /// </summary>
        /// <returns>В случае успешного выполнения: message = "Сессия активна", userName = usr.UserName, userRole; 
        /// если же выход был не выполнен: message = "Вы Гость. Пожалуйста, выполните вход"
        /// </returns>

        [HttpGet]
        [Route("api/account/isauthenticated")]
        public async Task<IActionResult> IsAuthenticated()
        {
            _logger.LogInformation("AccountController is Invoked with the function IsAuthenticated()");
            try
            {
                User usr = await GetCurrentUserAsync();
            if (usr == null)
            {
                    _logger.LogInformation("Request GET completed successfully");
                    return Unauthorized(new { message = "Вы Гость. Пожалуйста, выполните вход" });
            }
            IList<string> roles = await _userManager.GetRolesAsync(usr);
            string? userRole = roles.FirstOrDefault();
                _logger.LogInformation("Request GET completed successfully");
                return Ok(new { message = "Сессия активна", userName = usr.UserName, userRole });
            }
            catch (Exception ex)
            {
                // Log error/exception message
                _logger.LogError("Exception thrown" + ex);
                throw;
            }
        }

        private Task<User> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
    }
}