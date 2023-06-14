using AppNoOne.MiddelWere;
using AppNoOne.MiddelWere.MailSender;
using AppNoOne.Models;
using AppNoOne.TagHelper;
using AppNoOne.ViewComponents;
using AppNoOne.ViewService.IviewService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppNoOne.Controllers
{
    [Route("api/{Controller}/{Action}")]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUsers> _signInManager;
        private readonly UserManager<AppUsers> _userManager;
        private readonly ILogger<UserViewModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        private readonly IUserViewService _userViewService;

        public AccountController(UserManager<AppUsers> userManager,
            SignInManager<AppUsers> signInManager,
            ILogger<UserViewModel> logger,
            IEmailSender emailSender,
            IJwtAuthenticationManager jwtAuthenticationManager,
            IUserViewService userViewService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _userViewService = userViewService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult RegisterAcount()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                // Tạo AppUser sau đó tạo User mới (cập nhật vào db)
                var user = new AppUsers { UserName = userViewModel.UserName, Email = userViewModel.Email };
                var result = await _userManager.CreateAsync(user, userViewModel.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Vừa tạo mới tài khoản thành công.");

                    // phát sinh token theo thông tin user để xác nhận email
                    // mỗi user dựa vào thông tin sẽ có một mã riêng, mã này nhúng vào link
                    // trong email gửi đi để người dùng xác nhận
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

                    // callbackUrl = /Account/ConfirmEmail?userId=useridxx&code=codexxxx
                    // Link trong email người dùng bấm vào, nó sẽ gọi Page: /Acount/ConfirmEmail để xác nhận
                    var callbackUrl = Url.ActionLink(
                        "ConfirmEmail",
                        "Account",
                        values: new { userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    // Gửi email    
                    await _emailSender.SendEmailAsync(userViewModel.Email, "Xác nhận địa chỉ email",
                        $"Hãy xác nhận địa chỉ email bằng cách <a href='{callbackUrl}'>Bấm vào đây</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedEmail)
                    {
                        // Nếu cấu hình phải xác thực email mới được đăng nhập thì chuyển hướng đến trang
                        // RegisterConfirmation - chỉ để hiện thông báo cho biết người dùng cần mở email xác nhận
                        ViewBag.UrlContinue = Url.ActionLink("Index", "Dashboard");
                        return View("RegisterConfirmation", new { email = userViewModel.Email });
                    }
                    else
                    {
                        // Không cần xác thực - đăng nhập luôn
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect("/Dashboard/Index");
                    }
                }
                // Có lỗi, đưa các lỗi thêm user vào ModelState để hiện thị ở html heleper: asp-validation-summary
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("RegisterAcount");
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("RegisterAcount");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Không tồn tại User - '{userId}'.");
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            // Xác thực email
            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (result.Succeeded)
            {

                // Đăng nhập luôn nếu xác thực email thành công
                await _signInManager.SignInAsync(user, false);

                return ViewComponent(MessagePage.COMPONENTNAME,
                    new MessagePage.Message()
                    {
                        title = "Xác thực email",
                        htmlcontent = "Đã xác thực thành công, đang chuyển hướng",
                        urlredirect = Url.ActionLink("Index", "Dashboard")
                    }
                );
            }
            else
            {
                ViewBag.StatusMessage = "Lỗi xác nhận email";
            }
            return View();
        }

        public async Task<IActionResult> RegisterConfirmation(string email)
        {
            if (email == null)
            {
                return RedirectToAction("RegisterAcount");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Không có user với email: '{email}'.");
            }

            if (user.EmailConfirmed)
            {
                // Tài khoản đã xác thực email
                return ViewComponent(MessagePage.COMPONENTNAME,
                        new MessagePage.Message()
                        {
                            title = "Thông báo",
                            htmlcontent = "Tài khoản đã xác thực, chờ chuyển hướng",
                            urlredirect = Url.ActionLink("Index", "Dashboard")
                        }
                );
            }
            else
            {
                return RedirectToAction("ConfirmedEmail",new { email = email });
            }
        }
        public async Task<IActionResult> ConfirmedEmail(string email)
        {
            if (email == null)
            {
                return RedirectToAction("RegisterAcount");
            }

            // phát sinh token theo thông tin user để xác nhận email
            // mỗi user dựa vào thông tin sẽ có một mã riêng, mã này nhúng vào link
            // trong email gửi đi để người dùng xác nhận
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Không có user với email: '{email}'.");
            }
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            // callbackUrl = /Account/ConfirmEmail?userId=useridxx&code=codexxxx
            // Link trong email người dùng bấm vào, nó sẽ gọi Page: /Acount/ConfirmEmail để xác nhận
            var callbackUrl = Url.ActionLink(
                "ConfirmEmail",
                "Account",
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);

            // Gửi email    
            await _emailSender.SendEmailAsync(email, "Xác nhận địa chỉ email",
                $"Hãy xác nhận địa chỉ email bằng cách <a href='{callbackUrl}'>Bấm vào đây</a>.");
            return View("RegisterConfirmation", new { email = email });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (!_signInManager.IsSignedIn(User)) return RedirectToPage("/Index");

            await _signInManager.SignOutAsync();
            Utility.SetCookie(Response, Common.TokenKey, "");
            _logger.LogInformation("Người dùng đăng xuất");

            return View("Index");
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ResponseData> GetAllUsers(int pageIndex,int pageSize)
        {
            var rd = new ResponseData();
            var totalUsers = await _userManager.Users.CountAsync();
            // Tính toán tổng số trang
            var totalPages = (int)Math.Ceiling(totalUsers / (double)pageSize);
            var users = await _userManager.Users
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            rd.Data = users;
            rd.TotalPage = totalPages;
            return rd;
        }
          
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SingIn(UserViewModel model)
        {
            if (_signInManager.IsSignedIn(User)) return Redirect("api/Dashboard/Index");
            if (!ModelState.IsValid && ModelState.ContainsKey("Email"))
            {
                ModelState.Remove("Email");
                ModelState.Remove("ConfirmPassword");
            }
            if (ModelState.IsValid)
            {
                // Thử login bằng username/password
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName,
                    model.Password,
                    model.RememberMe,
                    true
                );
                if (!result.Succeeded)
                {
                    // Thất bại username/password -> tìm user theo email, nếu thấy thì thử đăng nhập
                    // bằng user tìm được
                    var user = await _userManager.FindByEmailAsync(model.UserName);
                    if (user != null)
                    {
                        result = await _signInManager.PasswordSignInAsync(
                            user,
                            model.Password,
                            model.RememberMe,
                            true
                        );
                    }
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("User đã đăng nhập");
                    var userDto = await _userViewService.GetUserByUserName(model.UserName);
                    if(userDto != null)
                    {
                        var token = _jwtAuthenticationManager.CreateToken(userDto);
                        Utility.SetCookie(Response, Common.TokenKey, token);
                    }
                    return ViewComponent(MessagePage.COMPONENTNAME, new MessagePage.Message()
                    {
                        title = "Đã đăng nhập",
                        htmlcontent = "Đăng nhập thành công",
                        urlredirect = "/api/Dashboard/Index"
                    });
                }
                if (result.RequiresTwoFactor)
                {
                    // Nếu cấu hình đăng nhập hai yếu tố thì chuyển hướng đến LoginWith2fa
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = "", RememberMe = model.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Tài khoản bí tạm khóa.");
                    // Chuyển hướng đến trang Lockout - hiện thị thông báo
                    return RedirectToAction("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Không đăng nhập được. Kiểm tra lại tài khoản");
                    return View("Index", model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View("Index",model);
        }

        [Authorize]
        [HttpGet]
        public async Task<ResponseData> GetUserLogin()
        {
            var rd = new ResponseData();
            string username = HttpContext.User.Identity.Name;
            rd.Data = await _userViewService.GetUserByUserName(username);
            rd.Success = true;
            return rd; 
        }
    }
}
