using UniLife.Server.Middleware.Wrappers;
using UniLife.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static Microsoft.AspNetCore.Http.StatusCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using UniLife.Server.Helpers;
using UniLife.Shared.AuthorizationDefinitions;
using UniLife.Shared.DataModels;
using UniLife.Shared.Dto.Account;
using UniLife.Shared.Dto.Email;
using IdentityModel;
using Microsoft.EntityFrameworkCore;
using UniLife.Shared.Dto.Definitions;
using UniLife.Shared.DataInterfaces;
using UniLife.Shared.Dto;
using MimeKit.Cryptography;

namespace UniLife.Server.Managers
{
    public class AccountManager : IAccountManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AccountManager> _logger;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly IEmailManager _emailManager;
        private readonly IUserProfileStore _userProfileStore;
        private readonly IOgrenciStore _ogrenciStore;
        private readonly IAkademisyenStore _akademisyenStore;
        private readonly IPersonelStore _personelStore;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly IFakulteStore _fakulteStore;
        private readonly IBolumStore _bolumStore;
        private readonly IDonemStore _donemStore;

        private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        public AccountManager(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountManager> logger,
            RoleManager<IdentityRole<Guid>> roleManager,
            IEmailManager emailManager,
            IUserProfileStore userProfileStore,
            IConfiguration configuration,
            IOgrenciStore ogrenciStore,
            IAkademisyenStore akademisyenStore,
            IPersonelStore personelStore,
            IFakulteStore fakulteStore,
            IBolumStore bolumStore,
            IDonemStore donemStore)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _roleManager = roleManager;
            _emailManager = emailManager;
            _userProfileStore = userProfileStore;
            _configuration = configuration;
            _ogrenciStore = ogrenciStore;
            _akademisyenStore = akademisyenStore;
            _personelStore = personelStore;
            _fakulteStore = fakulteStore;
            _bolumStore = bolumStore;
            _donemStore = donemStore;
        }

        public async Task<ApiResponse> ConfirmEmail(ConfirmEmailDto parameters)
        {
            if (parameters.UserId == null || parameters.Token == null)
            {
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            var user = await _userManager.FindByIdAsync(parameters.UserId);
            if (user == null)
            {
                _logger.LogInformation("User does not exist: {0}", parameters.UserId);
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            var token = parameters.Token;
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                _logger.LogInformation("User Email Confirmation Failed: {0}", string.Join(",", result.Errors.Select(i => i.Description)));
                return new ApiResponse(Status400BadRequest, "User Email Confirmation Failed");
            }

            await _signInManager.SignInAsync(user, true);

            return new ApiResponse(Status200OK, "Success");
        }

        public async Task<ApiResponse> ForgotPassword(ForgotPasswordDto parameters)
        {
            var user = await _userManager.FindByEmailAsync(parameters.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                _logger.LogInformation("Forgot Password with non-existent email / user: {0}", parameters.Email);
                // Don't reveal that the user does not exist or is not confirmed
                return new ApiResponse(Status200OK, "Success");
            }

            // TODO: Break out the email sending here, to a separate class/service etc..
            #region Forgot Password Email

            try
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                string callbackUrl = string.Format("{0}/Account/ResetPassword/{1}?token={2}", _configuration["UniLife:ApplicationUrl"], user.Id, token); //token must be a query string parameter as it is very long

                var email = new EmailMessageDto();
                email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                email.BuildForgotPasswordEmail(user.UserName, callbackUrl, token); //Replace First UserName with Name if you want to add name to Registration Form

                _logger.LogInformation("Forgot Password Email Sent: {0}", user.Email);
                await _emailManager.SendEmailAsync(email);
                return new ApiResponse(Status200OK, "Forgot Password Email Sent");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Forgot Password email failed: {0}", ex.Message);
            }

            #endregion Forgot Password Email

            return new ApiResponse(Status200OK, "Success");
        }

        public async Task<ApiResponse> Login(LoginDto parameters)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(parameters.UserName, parameters.Password, parameters.RememberMe, true);

                // If lock out activated and the max. amounts of attempts is reached.
                if (result.IsLockedOut)
                {
                    _logger.LogInformation("User Locked out: {0}", parameters.UserName);
                    return new ApiResponse(Status401Unauthorized, "User is locked out!");
                }

                // If your email is not confirmed but you require it in the settings for login.
                if (result.IsNotAllowed)
                {
                    _logger.LogInformation("User not allowed to log in: {0}", parameters.UserName);
                    return new ApiResponse(Status401Unauthorized, "Login not allowed!");
                }

                if (result.Succeeded)
                {
                    _logger.LogInformation("Logged In: {0}", parameters.UserName);
                    return new ApiResponse(Status200OK, await _userProfileStore.GetLastPageVisited(parameters.UserName));
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Login Failed: " + ex.Message);
            }

            _logger.LogInformation("Invalid Password for user {0}}", parameters.UserName);
            return new ApiResponse(Status401Unauthorized, "Login Failed");
        }

        public async Task<ApiResponse> Logout()
        {
            await _signInManager.SignOutAsync();
            return new ApiResponse(Status200OK, "Logout Successful");
        }

        public async Task<ApiResponse> Register(RegisterDto parameters)
        {
            try
            {
                var requireConfirmEmail = Convert.ToBoolean(_configuration["UniLife:RequireConfirmedEmail"] ?? "false");

                await this.RegisterNewUserAsync(parameters.UserName, parameters.Email, parameters.Password, requireConfirmEmail);

                if (requireConfirmEmail)
                {
                    return new ApiResponse(Status200OK, "Register User Success");
                }
                else
                {
                    return await Login(new LoginDto
                    {
                        UserName = parameters.UserName,
                        Password = parameters.Password
                    });
                }
            }
            catch (DomainException ex)
            {
                _logger.LogError("Register User Failed: {0}, {1}", ex.Description, ex.Message);
                return new ApiResponse(Status400BadRequest, $"Register User Failed: {ex.Description} ");
            }
            catch (Exception ex)
            {
                _logger.LogError("Register User Failed: {0}", ex.Message);
                return new ApiResponse(Status400BadRequest, "Register User Failed");
            }
        }

        public async Task<ApiResponse> ResetPassword(ResetPasswordDto parameters)
        {
            var user = await _userManager.FindByIdAsync(parameters.UserId);
            if (user == null)
            {
                _logger.LogInformation("User does not exist: {0}", parameters.UserId);
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            // TODO: Break this out into it's own self-contained Email Helper service.

            try
            {
                var result = await _userManager.ResetPasswordAsync(user, parameters.Token, parameters.Password);
                if (result.Succeeded)
                {
                    #region Email Successful Password change

                    var email = new EmailMessageDto();
                    email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                    email.BuildPasswordResetEmail(user.UserName); //Replace First UserName with Name if you want to add name to Registration Form

                    _logger.LogInformation($"Reset Password Successful Email Sent: {user.Email}");
                    await _emailManager.SendEmailAsync(email);

                    #endregion Email Successful Password change

                    return new ApiResponse(Status200OK, $"Reset Password Successful Email Sent: {user.Email}");
                }
                else
                {
                    _logger.LogInformation($"Error while resetting the password!: {user.UserName}");
                    return new ApiResponse(Status400BadRequest, $"Error while resetting the password!: {user.UserName}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Reset Password failed: {ex.Message}");
                return new ApiResponse(Status400BadRequest, $"Error while resetting the password!: {ex.Message}");
            }
        }

        public async Task<ApiResponse> UserInfo(ClaimsPrincipal userClaimsPrincipal)
        {
            var userInfo = await BuildUserInfo(userClaimsPrincipal);
            return new ApiResponse(Status200OK, "Retrieved UserInfo", userInfo);
        }

        public async Task<ApiResponse> UpdateUser(UserInfoDto userInfo)
        {
            var user = await _userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                _logger.LogInformation("User does not exist: {0}", userInfo.Email);
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Email = userInfo.Email;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogInformation("User Update Failed: {0}", string.Join(",", result.Errors.Select(i => i.Description)));
                return new ApiResponse(Status400BadRequest, "User Update Failed");
            }

            return new ApiResponse(Status200OK, "User Updated Successfully");
        }
        public async Task<ApiResponse> UpdateOgrenciUser(OgrenciDto ogrenciDto)
        {
            ogrenciDto.Bolum = null;
            ogrenciDto.Fakulte = null;
            ogrenciDto.Program = null;

            var user = await _userManager.FindByIdAsync(ogrenciDto.ApplicationUserId.ToString());

            if (user == null)
            {
                _logger.LogInformation("Bu kullanıcı mevcut değil: {0}", ogrenciDto.OgrNo);
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            user.FirstName = ogrenciDto.Ad;
            user.LastName = ogrenciDto.Soyad;
            user.Email = ogrenciDto.Email;
            user.TCKN = ogrenciDto.TCKN;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı güncellemesi hatası: {0}", string.Join(",", result.Errors.Select(i => i.Description)));
                return new ApiResponse(Status400BadRequest, "Kullanıcı güncellemesi Hatası!");
            }

            try
            {
                //var existingOgrenci =await _ogrenciStore.GetById(ogrenciDto.Id);
                var ogrenciResult = await _ogrenciStore.Update(ogrenciDto);
                if (ogrenciResult.Id == 0)
                {
                    _logger.LogInformation("Ögrenci Accountmanager _ogrenciStore.Update çalışmadı");
                    return new ApiResponse(Status400BadRequest, "Öğrenci kullanıcı güncellemesi Hatası!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Accountmanager _ogrenciStore.Update güncelleme hatsı: {0} inner {1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                return new ApiResponse(Status400BadRequest, "Öğrenci kullanıcı güncellemesi hatası oluştu: {0}", ex.Message);
            }


            return new ApiResponse(Status200OK, "Kullanıcı bilgileri güncellendi");
        }

        public async Task<ApiResponse> UpdateAkademisyenUser(AkademisyenDto akademisyenDto)
        {
            var user = await _userManager.FindByIdAsync(akademisyenDto.ApplicationUserId.ToString());

            if (user == null)
            {
                _logger.LogInformation("Bu kullanıcı mevcut değil: {0}", akademisyenDto.AkaNo.ToString());
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            user.FirstName = akademisyenDto.Ad;
            user.LastName = akademisyenDto.Soyad;
            user.Email = akademisyenDto.Email;
            user.TCKN = akademisyenDto.TCKN;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı güncellemesi hatası: {0}", string.Join(",", result.Errors.Select(i => i.Description)));
                return new ApiResponse(Status400BadRequest, "Kullanıcı güncellemesi Hatası!");
            }

            try
            {
                var akademisyenResult = await _akademisyenStore.Update(akademisyenDto);
                if (akademisyenResult.Id == 0)
                {
                    _logger.LogInformation("Akademisyen Accountmanager _akademisyenStore.Update çalışmadı");
                    return new ApiResponse(Status400BadRequest, "Akademisyen kullanıcı güncellemesi Hatası!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Accountmanager _akademisyenStore.Update güncelleme hatsı: {0} inner {1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                return new ApiResponse(Status400BadRequest, "Akademisyen kullanıcı güncellemesi hatası oluştu: {0}", ex.Message);
            }


            return new ApiResponse(Status200OK, "Kullanıcı bilgileri güncellendi");
        }

        public async Task<ApiResponse> UpdatePersonelUser(PersonelDto personelDto)
        {
            var user = await _userManager.FindByIdAsync(personelDto.ApplicationUserId.ToString());

            if (user == null)
            {
                _logger.LogInformation("Bu kullanıcı mevcut değil: {0}", personelDto.PersNo);
                return new ApiResponse(Status404NotFound, "User does not exist");
            }

            user.FirstName = personelDto.Ad;
            user.LastName = personelDto.Soyad;
            user.Email = personelDto.Email;
            user.TCKN = personelDto.TCKN;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                _logger.LogInformation("Kullanıcı güncellemesi hatası: {0}", string.Join(",", result.Errors.Select(i => i.Description)));
                return new ApiResponse(Status400BadRequest, "Kullanıcı güncellemesi Hatası!");
            }

            try
            {
                var personelResult = await _personelStore.Update(personelDto);
                if (personelResult.Id == 0)
                {
                    _logger.LogInformation("Personel Accountmanager _personelStore.Update çalışmadı");
                    return new ApiResponse(Status400BadRequest, "Personel kullanıcı güncellemesi Hatası!");
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation("Accountmanager _personelStore.Update güncelleme hatsı: {0} inner {1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                return new ApiResponse(Status400BadRequest, "Personel kullanıcı güncellemesi hatası oluştu: {0}", ex.Message);
            }

            return new ApiResponse(Status200OK, "Kullanıcı bilgileri güncellendi");
        }




        public async Task<ApiResponse> Create(RegisterDto parameters)
        {
            try
            {

                var user = new ApplicationUser
                {
                    UserName = parameters.UserName,
                    Email = parameters.Email
                };

                user.UserName = parameters.UserName;
                var result = await _userManager.CreateAsync(user, parameters.Password);
                if (!result.Succeeded)
                {
                    return new ApiResponse(Status400BadRequest, "Register User Failed: " + string.Join(",", result.Errors.Select(i => i.Description)));
                }
                else
                {
                    var claimsResult = _userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(Policies.IsUser, string.Empty),
                        new Claim(JwtClaimTypes.Name, parameters.UserName),
                        new Claim(JwtClaimTypes.Email, parameters.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                    }).Result;
                }

                //Role - Here we tie the new user to the "User" role
                await _userManager.AddToRoleAsync(user, "User");

                if (Convert.ToBoolean(_configuration["UniLife:RequireConfirmedEmail"] ?? "false"))
                {
                    try
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        string callbackUrl = string.Format("{0}/Account/ConfirmEmail/{1}?token={2}", _configuration["UniLife:ApplicationUrl"], user.Id, token);

                        var email = new EmailMessageDto();
                        email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                        email = EmailTemplates.BuildNewUserConfirmationEmail(email, user.UserName, user.Email, callbackUrl, user.Id.ToString(), token); //Replace First UserName with Name if you want to add name to Registration Form

                        _logger.LogInformation("New user created: {0}", user);
                        await _emailManager.SendEmailAsync(email);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("New user email failed: {0}", ex.Message);
                    }

                    return new ApiResponse(Status200OK, "Create User Success");
                }

                try
                {
                    var email = new EmailMessageDto();
                    email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                    email.BuildNewUserEmail(user.FullName, user.UserName, user.Email, parameters.Password);

                    _logger.LogInformation("New user created: {0}", user);
                    await _emailManager.SendEmailAsync(email);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("New user email failed: {0}", ex.Message);
                }

                var userInfo = new UserInfoDto
                {
                    UserId = user.Id,
                    IsAuthenticated = false,
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    //ExposedClaims = user.Claims.ToDictionary(c => c.Type, c => c.Value),
                    Roles = new List<string> { "User" }
                };

                return new ApiResponse(Status200OK, "Created New User", userInfo);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Create User Failed: {ex.Message}");
                return new ApiResponse(Status400BadRequest, "Create User Failed");
            }
        }

        public async Task<ApiResponse> CreateOgrenci(OgrenciDto ogrenciDto, string ogrNoDesen)
        {
            try
            {
                Guid possibleUserId = Guid.NewGuid();

                //ogrenciDto.OgrNo = "U"+ GenerateTimeStampUserId();
                ogrenciDto.OgrNo = await GenerateOgrNoByDesen(ogrenciDto, ogrNoDesen);

                var user = new ApplicationUser
                {
                    UserName = ogrenciDto.OgrNo.ToString(),
                    FirstName = ogrenciDto.Ad,
                    LastName = ogrenciDto.Soyad,
                    FullName = ogrenciDto.Ad + " " + ogrenciDto.Soyad,
                    UserType = (int)UserType.Ogrenci,
                    Email = ogrenciDto.Email,
                    TCKN = ogrenciDto.TCKN,
                    Id = possibleUserId
                };

                //user.UserName = ogrenciDto.OgrNo;
                var result = await _userManager.CreateAsync(user, ogrenciDto.TCKN); //tckn yi pasword yaptık.
                if (!result.Succeeded)
                {
                    return new ApiResponse(Status400BadRequest, "Öğrenci kullanıcı kaydı başarısız oldu: " + string.Join(",", result.Errors.Select(i => i.Description)));
                }
                else
                {
                    var claimsResult = _userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(Policies.IsUser, string.Empty),
                        new Claim(JwtClaimTypes.Name, ogrenciDto.OgrNo.ToString()),
                        new Claim(JwtClaimTypes.Email, ogrenciDto.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                    }).Result;
                }

                Ogrenci ogrCreateResult;
                try
                {
                    //Öğrenci Kaydı ekleme
                    ogrenciDto.ApplicationUserId = possibleUserId;
                    //ogrenciDto.SinitAtYil = (await _donemStore.GetWhere(x => x.Durum == true)).FirstOrDefault().Yil;
                    ogrCreateResult = await _ogrenciStore.Create(ogrenciDto);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Kullanıcının öğrenci kaydı başarısız oldu: {0} inner:{1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                    return new ApiResponse(Status400BadRequest, "Kullanıcının öğrenci kaydı başarısız oldu: " + ex.Message);
                }




                //Role - Here we tie the new user to the "User" role
                await _userManager.AddToRoleAsync(user, "Ogrenci");





                if (Convert.ToBoolean(_configuration["UniLife:RequireConfirmedEmail"] ?? "false"))
                {
                    try
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        string callbackUrl = string.Format("{0}/Account/ConfirmEmail/{1}?token={2}", _configuration["UniLife:ApplicationUrl"], user.Id, token);

                        var email = new EmailMessageDto();
                        email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                        email = EmailTemplates.BuildNewUserConfirmationEmail(email, user.UserName, user.Email, callbackUrl, user.Id.ToString(), token); //Replace First UserName with Name if you want to add name to Registration Form

                        _logger.LogInformation("Yeni öğrenci kullanıcı oluşturuldu: {0}", user);
                        await _emailManager.SendEmailAsync(email);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Yeni öğrenci kullanıcı emaili gönderilemedi: {0}", ex.Message);
                    }



                    return new ApiResponse(Status200OK, "Yeni öğrenci kullanıcısı oluşturuldu");
                }

                try
                {
                    var email = new EmailMessageDto();
                    email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                    email.BuildNewUserEmail(user.FullName, user.UserName, user.Email, ogrenciDto.TCKN);

                    _logger.LogInformation("Yeni öğrenci kullanıcı oluşturuldu: {0}", user);
                    await _emailManager.SendEmailAsync(email);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Yeni öğrenci kullanıcı emaili gönderilemedi: {0}", ex.Message);
                }

                var ogrenciDto1 = new OgrenciDto
                {
                    Id = ogrCreateResult.Id,
                    ApplicationUserId = user.Id,
                    IsAuthenticated = false,
                    OgrNo = ogrenciDto.OgrNo,//user.UserName,
                    Email = user.Email,
                    Ad = user.FirstName,
                    Soyad = user.LastName,
                    TCKN = user.TCKN,
                    //ExposedClaims = user.Claims.ToDictionary(c => c.Type, c => c.Value),
                    Roles = new List<string> { "Ogrenci" }
                };


                return new ApiResponse(Status200OK, "Yeni öğrenci kullanıcı oluşturuldu", ogrenciDto1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Yeni öğrenci kullanıcısı oluşturulamadı: {ex.Message}");
                return new ApiResponse(Status400BadRequest, "Yeni öğrenci kullanıcısı oluşturulamadı.");
            }
        }

        private static string GenerateTimeStampUserId()
        {
            DateTime dtReturn = DateTime.Now.ToLocalTime();
            DateTime dtEPoch = new DateTime(2020, 7, 27, 0, 0, 0, DateTimeKind.Utc);
            DateTime dtTime = dtReturn.Subtract(new TimeSpan(dtEPoch.Ticks));
            long lngTimeSpan = dtTime.Ticks / 100;
            return lngTimeSpan.ToString();
        }

        private async Task<long> GenerateOgrNoByDesen(OgrenciDto ogrenciDto, string ogrNoDesen)
        {
            int donemYil = (await _donemStore.GetWhere(x => x.Durum == true)).FirstOrDefault().Yil;
            long sonOgrNo = await _ogrenciStore.GetLastOgrNo((int)ogrenciDto.FakulteId, (int)ogrenciDto.BolumId);

            string[] diziDesen = ogrNoDesen.Split(",");
            string virgulsuzDesen = ogrNoDesen.Replace(",", "");
            //yyyy
            int ogrYilFormatCount = virgulsuzDesen.Count(x => x == 'y');
            string yilFormat = "";
            for (int i = 0; i < ogrYilFormatCount; i++)
            {
                yilFormat += "y";
            }
            string ogrYil = donemYil.ToString().Substring(donemYil.ToString().Length - ogrYilFormatCount);// DateTime.Now.ToString(yilFormat);



            if (sonOgrNo != 0 && (ogrYil == sonOgrNo.ToString().Substring(0, ogrYilFormatCount)))
            {
                return sonOgrNo + 1;
            }
            else
            {

                //fff
                int orgFakCount = virgulsuzDesen.Count(x => x == 'f');
                var fakulte = await _fakulteStore.GetById((int)ogrenciDto.FakulteId);
                string orgFakKod = fakulte.Kod.PadLeft(orgFakCount, '0');

                //bbb
                int orgBolCount = virgulsuzDesen.Count(x => x == 'b');
                var bolum = await _bolumStore.GetById((int)ogrenciDto.BolumId);
                string orgBolKod = bolum.Kod.PadLeft(orgBolCount, '0');

                long generatedOgrNo = Convert.ToInt64(ogrYil + orgFakKod + orgBolKod + "001");

                return generatedOgrNo;
            }
            
        }

        public async Task<ApiResponse> CreateAkademisyen(AkademisyenDto akademisyenDto)
        {
            try
            {
                Guid possibleUserId = Guid.NewGuid();

                akademisyenDto.AkaNo = await GenerateAkaNo();

                var user = new ApplicationUser
                {
                    UserName = akademisyenDto.AkaNo.ToString(),
                    FirstName = akademisyenDto.Ad,
                    LastName = akademisyenDto.Soyad,
                    FullName = akademisyenDto.Ad + " " + akademisyenDto.Soyad,
                    UserType = (int)UserType.Akademisyen,
                    Email = akademisyenDto.Email,
                    TCKN = akademisyenDto.TCKN,
                    Id = possibleUserId
                };

                var result = await _userManager.CreateAsync(user, akademisyenDto.TCKN); //tckn yi pasword yaptık.
                if (!result.Succeeded)
                {
                    return new ApiResponse(Status400BadRequest, "Öğrenci kullanıcı kaydı başarısız oldu: " + string.Join(",", result.Errors.Select(i => i.Description)));
                }
                else
                {
                    var claimsResult = _userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(Policies.IsUser, string.Empty),
                        new Claim(JwtClaimTypes.Name, akademisyenDto.AkaNo.ToString()),
                        new Claim(JwtClaimTypes.Email, akademisyenDto.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                    }).Result;
                }


                try
                {
                    //Akademisyen Kaydı ekleme
                    akademisyenDto.ApplicationUserId = possibleUserId;
                    Akademisyen ogrCreateResult = await _akademisyenStore.Create(akademisyenDto);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Kullanıcının akademisyen kaydı başarısız oldu: {0} inner:{1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                    return new ApiResponse(Status400BadRequest, "Kullanıcının akademisyen kaydı başarısız oldu: " + ex.Message);
                }




                //Role - Here we tie the new Akademisyen to the "Akademisyen" role
                await _userManager.AddToRoleAsync(user, "Akademisyen");





                if (Convert.ToBoolean(_configuration["UniLife:RequireConfirmedEmail"] ?? "false"))
                {
                    try
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        string callbackUrl = string.Format("{0}/Account/ConfirmEmail/{1}?token={2}", _configuration["UniLife:ApplicationUrl"], user.Id, token);

                        var email = new EmailMessageDto();
                        email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                        email = EmailTemplates.BuildNewUserConfirmationEmail(email, user.UserName, user.Email, callbackUrl, user.Id.ToString(), token); //Replace First UserName with Name if you want to add name to Registration Form

                        _logger.LogInformation("Yeni öğrenci kullanıcı oluşturuldu: {0}", user);
                        await _emailManager.SendEmailAsync(email);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Yeni öğrenci kullanıcı emaili gönderilemedi: {0}", ex.Message);
                    }



                    return new ApiResponse(Status200OK, "Yeni öğrenci kullanıcısı oluşturuldu");
                }

                try
                {
                    var email = new EmailMessageDto();
                    email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                    email.BuildNewUserEmail(user.FullName, user.UserName, user.Email, akademisyenDto.TCKN);

                    _logger.LogInformation("Yeni öğrenci kullanıcı oluşturuldu: {0}", user);
                    await _emailManager.SendEmailAsync(email);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Yeni öğrenci kullanıcı emaili gönderilemedi: {0}", ex.Message);
                }

                var akademisyenDto1 = new AkademisyenDto
                {
                    ApplicationUserId = user.Id,
                    IsAuthenticated = false,
                    AkaNo = akademisyenDto.AkaNo,// user.UserName,
                    Email = user.Email,
                    Ad = user.FirstName,
                    Soyad = user.LastName,
                    TCKN = user.TCKN,
                    //ExposedClaims = user.Claims.ToDictionary(c => c.Type, c => c.Value),
                    Roles = new List<string> { "Akademisyen" }
                };

                return new ApiResponse(Status200OK, "Yeni akademisyen kullanıcı oluşturuldu", akademisyenDto1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Yeni akademisyen kullanıcısı oluşturulamadı: {ex.Message}");
                return new ApiResponse(Status400BadRequest, "Yeni akademisyen kullanıcısı oluşturulamadı.");
            }
        }

        private async Task<long> GenerateAkaNo()
        {
            var sonAkaNo =await _akademisyenStore.GetLastAkaNo();
            if (sonAkaNo != 0)
            {
                return sonAkaNo + 1;
            }
            else
            {
                throw new DomainException(description: "En son akademisyen bulunamadı.");
            }
        }

        private async Task<long> GeneratePersNo()
        {
            var sonPersNo = await _personelStore.GetLastPersNo();
            if (sonPersNo != 0)
            {
                return sonPersNo + 1;
            }
            else
            {
                throw new DomainException(description: "En son personel bulunamadı.");
            }
        }


        public async Task<ApiResponse> CreatePersonel(PersonelDto personelDto)
        {
            try
            {
                Guid possibleUserId = Guid.NewGuid();

                personelDto.PersNo = await GeneratePersNo();

                var user = new ApplicationUser
                {
                    UserName = personelDto.PersNo.ToString(),
                    FirstName = personelDto.Ad,
                    LastName = personelDto.Soyad,
                    FullName = personelDto.Ad + " " + personelDto.Soyad,
                    UserType = (int)UserType.IdariPersonel,
                    Email = personelDto.Email,
                    TCKN = personelDto.TCKN,
                    Id = possibleUserId
                };

                var result = await _userManager.CreateAsync(user, personelDto.TCKN); //tckn yi pasword yaptık.
                if (!result.Succeeded)
                {
                    return new ApiResponse(Status400BadRequest, "Öğrenci kullanıcı kaydı başarısız oldu: " + string.Join(",", result.Errors.Select(i => i.Description)));
                }
                else
                {
                    var claimsResult = _userManager.AddClaimsAsync(user, new Claim[]{
                        new Claim(Policies.IsUser, string.Empty),
                        new Claim(JwtClaimTypes.Name, personelDto.PersNo.ToString()),
                        new Claim(JwtClaimTypes.Email, personelDto.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                    }).Result;
                }


                try
                {
                    //Personel Kaydı ekleme
                    personelDto.ApplicationUserId = possibleUserId;
                    Personel CreateResult = await _personelStore.Create(personelDto);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Kullanıcının personel kaydı başarısız oldu: {0} inner:{1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                    return new ApiResponse(Status400BadRequest, "Kullanıcının personel kaydı başarısız oldu: " + ex.Message);
                }




                //Role - Here we tie the new user to the "User" role
                await _userManager.AddToRoleAsync(user, "Personel");





                if (Convert.ToBoolean(_configuration["UniLife:RequireConfirmedEmail"] ?? "false"))
                {
                    try
                    {
                        // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        string callbackUrl = string.Format("{0}/Account/ConfirmEmail/{1}?token={2}", _configuration["UniLife:ApplicationUrl"], user.Id, token);

                        var email = new EmailMessageDto();
                        email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                        email = EmailTemplates.BuildNewUserConfirmationEmail(email, user.UserName, user.Email, callbackUrl, user.Id.ToString(), token); //Replace First UserName with Name if you want to add name to Registration Form

                        _logger.LogInformation("Yeni öğrenci kullanıcı oluşturuldu: {0}", user);
                        await _emailManager.SendEmailAsync(email);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogInformation("Yeni öğrenci kullanıcı emaili gönderilemedi: {0}", ex.Message);
                    }



                    return new ApiResponse(Status200OK, "Yeni öğrenci kullanıcısı oluşturuldu");
                }

                try
                {
                    var email = new EmailMessageDto();
                    email.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
                    email.BuildNewUserEmail(user.FullName, user.UserName, user.Email, personelDto.TCKN);

                    _logger.LogInformation("Yeni öğrenci kullanıcı oluşturuldu: {0}", user);
                    await _emailManager.SendEmailAsync(email);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Yeni öğrenci kullanıcı emaili gönderilemedi: {0}", ex.Message);
                }

                var personelDto1 = new PersonelDto
                {
                    ApplicationUserId = user.Id,
                    IsAuthenticated = false,
                    PersNo = personelDto.PersNo,// user.UserName,
                    Email = user.Email,
                    Ad = user.FirstName,
                    Soyad = user.LastName,
                    TCKN = user.TCKN,
                    //ExposedClaims = user.Claims.ToDictionary(c => c.Type, c => c.Value),
                    Roles = new List<string> { "Personel" }
                };

                return new ApiResponse(Status200OK, "Yeni personel kullanıcı oluşturuldu", personelDto1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Yeni personel kullanıcısı oluşturulamadı: {ex.Message}");
                return new ApiResponse(Status400BadRequest, "Yeni personel kullanıcısı oluşturulamadı.");
            }
        }



        public async Task<ApiResponse> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                var asd = new ApiResponse(Status404NotFound, "User does not exist");
                return asd;
            }
            try
            {

                //EF: not a fan this will delete old ApiLogs
                await _userProfileStore.DeleteAllApiLogsForUser(user.Id);

                await _userManager.DeleteAsync(user);
                return new ApiResponse(Status200OK, "User Deletion Successful");
            }
            catch
            {
                return new ApiResponse(Status400BadRequest, "User Deletion Failed");
            }
        }

        public ApiResponse GetUser(ClaimsPrincipal userClaimsPrincipal)
        {
            UserInfoDto userInfo = userClaimsPrincipal != null && userClaimsPrincipal.Identity.IsAuthenticated
                ? new UserInfoDto { UserName = userClaimsPrincipal.Identity.Name, IsAuthenticated = true }
                : LoggedOutUser;
            return new ApiResponse(Status200OK, "Get User Successful", userInfo);
        }

        public async Task<ApiResponse> ListRoles()
        {
            return new ApiResponse(Status200OK, "", await _roleManager.Roles.Select(x => x.Name).ToListAsync());
        }

        public async Task<ApiResponse> Update(UserInfoDto userInfo)
        {
            // retrieve full user object for updating
            var appUser = await _userManager.FindByIdAsync(userInfo.UserId.ToString()).ConfigureAwait(true);

            //update values
            appUser.UserName = userInfo.UserName;
            appUser.FirstName = userInfo.FirstName;
            appUser.LastName = userInfo.LastName;
            appUser.Email = userInfo.Email;

            try
            {
                await _userManager.UpdateAsync(appUser).ConfigureAwait(true);
            }
            catch
            {
                return new ApiResponse(Status500InternalServerError, "Error Updating User");
            }

            if (userInfo.Roles != null)
            {
                try
                {
                    var rolesToAdd = new List<string>();
                    var currentUserRoles = (List<string>)(await _userManager.GetRolesAsync(appUser).ConfigureAwait(true));
                    foreach (var newUserRole in userInfo.Roles)
                    {
                        if (!currentUserRoles.Contains(newUserRole))
                        {
                            rolesToAdd.Add(newUserRole);
                        }
                    }
                    await _userManager.AddToRolesAsync(appUser, rolesToAdd).ConfigureAwait(true);
                    //HACK to switch to claims auth
                    foreach (var role in rolesToAdd)
                    {
                        await _userManager.AddClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }

                    var rolesToRemove = currentUserRoles
                        .Where(role => !userInfo.Roles.Contains(role)).ToList();

                    await _userManager.RemoveFromRolesAsync(appUser, rolesToRemove).ConfigureAwait(true);

                    //HACK to switch to claims auth
                    foreach (var role in rolesToRemove)
                    {
                        await _userManager.RemoveClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }
                }
                catch
                {
                    return new ApiResponse(Status500InternalServerError, "Error Updating Roles");
                }
            }
            return new ApiResponse(Status200OK, "User Updated");
        }


        public async Task<ApiResponse> UpdateRoleFromOgrenciUser(OgrenciDto ogrenciDto)
        {
            // retrieve full user object for updating
            var appUser = await _userManager.FindByIdAsync(ogrenciDto.ApplicationUserId.ToString()).ConfigureAwait(true);


            if (ogrenciDto.Roles != null)
            {
                try
                {
                    var rolesToAdd = new List<string>();
                    var currentUserRoles = (List<string>)(await _userManager.GetRolesAsync(appUser).ConfigureAwait(true));
                    foreach (var newUserRole in ogrenciDto.Roles)
                    {
                        if (!currentUserRoles.Contains(newUserRole))
                        {
                            rolesToAdd.Add(newUserRole);
                        }
                    }
                    await _userManager.AddToRolesAsync(appUser, rolesToAdd).ConfigureAwait(true);
                    //HACK to switch to claims auth
                    foreach (var role in rolesToAdd)
                    {
                        await _userManager.AddClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }

                    var rolesToRemove = currentUserRoles
                        .Where(role => !ogrenciDto.Roles.Contains(role)).ToList();

                    await _userManager.RemoveFromRolesAsync(appUser, rolesToRemove).ConfigureAwait(true);

                    //HACK to switch to claims auth
                    foreach (var role in rolesToRemove)
                    {
                        await _userManager.RemoveClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }
                }
                catch
                {
                    return new ApiResponse(Status500InternalServerError, "Roller güncellenirken hata oluştu!");
                }
            }
            return new ApiResponse(Status200OK, "Roller Güncellendi.");
        }

        public async Task<ApiResponse> UpdateRoleFromAkademisyenUser(AkademisyenDto akademisyenDto)
        {
            // retrieve full user object for updating
            var appUser = await _userManager.FindByIdAsync(akademisyenDto.ApplicationUserId.ToString()).ConfigureAwait(true);


            if (akademisyenDto.Roles != null)
            {
                try
                {
                    var rolesToAdd = new List<string>();
                    var currentUserRoles = (List<string>)(await _userManager.GetRolesAsync(appUser).ConfigureAwait(true));
                    foreach (var newUserRole in akademisyenDto.Roles)
                    {
                        if (!currentUserRoles.Contains(newUserRole))
                        {
                            rolesToAdd.Add(newUserRole);
                        }
                    }
                    await _userManager.AddToRolesAsync(appUser, rolesToAdd).ConfigureAwait(true);
                    //HACK to switch to claims auth
                    foreach (var role in rolesToAdd)
                    {
                        await _userManager.AddClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }

                    var rolesToRemove = currentUserRoles
                        .Where(role => !akademisyenDto.Roles.Contains(role)).ToList();

                    await _userManager.RemoveFromRolesAsync(appUser, rolesToRemove).ConfigureAwait(true);

                    //HACK to switch to claims auth
                    foreach (var role in rolesToRemove)
                    {
                        await _userManager.RemoveClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }
                }
                catch
                {
                    return new ApiResponse(Status500InternalServerError, "Roller güncellenirken hata oluştu!");
                }
            }
            return new ApiResponse(Status200OK, "Roller Güncellendi.");
        }
        public async Task<ApiResponse> UpdateRoleFromPersonelUser(PersonelDto personelDto)
        {
            // retrieve full user object for updating
            var appUser = await _userManager.FindByIdAsync(personelDto.ApplicationUserId.ToString()).ConfigureAwait(true);


            if (personelDto.Roles != null)
            {
                try
                {
                    var rolesToAdd = new List<string>();
                    var currentUserRoles = (List<string>)(await _userManager.GetRolesAsync(appUser).ConfigureAwait(true));
                    foreach (var newUserRole in personelDto.Roles)
                    {
                        if (!currentUserRoles.Contains(newUserRole))
                        {
                            rolesToAdd.Add(newUserRole);
                        }
                    }
                    await _userManager.AddToRolesAsync(appUser, rolesToAdd).ConfigureAwait(true);
                    //HACK to switch to claims auth
                    foreach (var role in rolesToAdd)
                    {
                        await _userManager.AddClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }

                    var rolesToRemove = currentUserRoles
                        .Where(role => !personelDto.Roles.Contains(role)).ToList();

                    await _userManager.RemoveFromRolesAsync(appUser, rolesToRemove).ConfigureAwait(true);

                    //HACK to switch to claims auth
                    foreach (var role in rolesToRemove)
                    {
                        await _userManager.RemoveClaimAsync(appUser, new Claim($"Is{role}", "true")).ConfigureAwait(true);
                    }
                }
                catch
                {
                    return new ApiResponse(Status500InternalServerError, "Roller güncellenirken hata oluştu!");
                }
            }
            return new ApiResponse(Status200OK, "Roller Güncellendi.");
        }


        public async Task<ApiResponse> AdminResetUserPasswordAsync(Guid id, string newPassword, ClaimsPrincipal userClaimsPrincipal)
        {
            ApplicationUser user;

            try
            {
                user = await _userManager.FindByIdAsync(id.ToString());
                if (user == null)
                {
                    throw new KeyNotFoundException();
                }
            }
            catch (KeyNotFoundException ex)
            {
                return new ApiResponse(Status400BadRequest, "Unable to find user" + ex.Message);
            }
            try
            {
                var passToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, passToken, newPassword);
                if (result.Succeeded)
                {
                    _logger.LogInformation(user.UserName + "'s password reset; Requested from Admin interface by:" + userClaimsPrincipal.Identity.Name);
                    return new ApiResponse(Status204NoContent, user.UserName + " password reset");
                }
                else
                {
                    _logger.LogInformation(user.UserName + "'s password reset failed; Requested from Admin interface by:" + userClaimsPrincipal.Identity.Name);

                    // this is going to an authenticated Admin so it should be safe/useful to send back raw error messages
                    if (result.Errors.Any())
                    {
                        return new ApiResponse(Status400BadRequest, string.Join(',', result.Errors.Select(x => x.Description)));
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex) // not sure if failed password reset result will throw an exception
            {
                _logger.LogInformation(user.UserName + "'s password reset failed; Requested from Admin interface by:" + userClaimsPrincipal.Identity.Name);
                return new ApiResponse(Status400BadRequest, ex.Message);
            }
        }

        public async Task<ApplicationUser> RegisterNewUserAsync(string userName, string email, string password, bool requireConfirmEmail)
        {
            var user = new ApplicationUser
            {
                UserName = userName,
                Email = email
            };

            var createUserResult = password == null ?
                await _userManager.CreateAsync(user) :
                await _userManager.CreateAsync(user, password);

            if (!createUserResult.Succeeded)
                throw new DomainException(string.Join(",", createUserResult.Errors.Select(i => i.Description)));

            await _userManager.AddClaimsAsync(user, new Claim[]{
                    new Claim(Policies.IsUser, string.Empty),
                    new Claim(JwtClaimTypes.Name, user.UserName),
                    new Claim(JwtClaimTypes.Email, user.Email),
                    new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                });

            //Role - Here we tie the new user to the "User" role
            await _userManager.AddToRoleAsync(user, "User");

            _logger.LogInformation("New user registered: {0}", user);

            var emailMessage = new EmailMessageDto();

            if (requireConfirmEmail)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = $"{_configuration["UniLife:ApplicationUrl"]}/Account/ConfirmEmail/{user.Id}?token={token}";

                emailMessage.BuildNewUserConfirmationEmail(user.UserName, user.Email, callbackUrl, user.Id.ToString(), token); //Replace First UserName with Name if you want to add name to Registration Form
            }
            else
            {
                emailMessage.BuildNewUserEmail(user.FullName, user.UserName, user.Email, password);
            }

            emailMessage.ToAddresses.Add(new EmailAddressDto(user.Email, user.Email));
            try
            {
                await _emailManager.SendEmailAsync(emailMessage);
            }
            catch (Exception ex)
            {
                _logger.LogInformation("New user email failed: Body: {0}, Error: {1}", emailMessage.Body, ex.Message);
            }

            return user;
        }

        private async Task<UserInfoDto> BuildUserInfo(ClaimsPrincipal userClaimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(userClaimsPrincipal);

            if (user != null)
            {
                try
                {
                    return new UserInfoDto
                    {
                        IsAuthenticated = userClaimsPrincipal.Identity.IsAuthenticated,
                        UserName = user.UserName,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserId = user.Id,
                        //Optionally: filter the claims you want to expose to the client
                        ExposedClaims = userClaimsPrincipal.Claims.Select(c => new KeyValuePair<string, string>(c.Type, c.Value)).ToList(),

                        ////Permissionlardan sadece menü olanları alacak olursak...
                        //ExposedClaims = userClaimsPrincipal.Claims.Where(x=>(x.Type.Contains("permission") ? x.Value.Contains("Menu."):true)).Select(c => new KeyValuePair<string, string>(c.Type, c.Value)).ToList(),
                        Roles = ((ClaimsIdentity)userClaimsPrincipal.Identity).Claims
                                .Where(c => c.Type == "role")
                                .Select(c => c.Value).ToList()
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("Could not build UserInfoDto: " + ex.Message);
                }
            }
            else
            {
                return new UserInfoDto();
            }

            return null;
        }

        //private async Task Deneme(ClaimsPrincipal userClaimsPrincipal)
        //{
        //    _userManager.getrol
        //}


    }
}
