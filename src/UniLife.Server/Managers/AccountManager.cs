﻿using UniLife.Server.Middleware.Wrappers;
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
        private readonly IConfiguration _configuration;
        private readonly SignInManager<ApplicationUser> _signInManager;

        private static readonly UserInfoDto LoggedOutUser = new UserInfoDto { IsAuthenticated = false, Roles = new List<string>() };

        public AccountManager(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AccountManager> logger,
            RoleManager<IdentityRole<Guid>> roleManager,
            IEmailManager emailManager,
            IUserProfileStore userProfileStore,
            IConfiguration configuration,
            IOgrenciStore ogrenciStore,
            IAkademisyenStore akademisyenStore)
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
                _logger.LogInformation("Accountmanager _ogrenciStore.Update güncelleme hatsı: {0} inner {1} stacktrace:{2}", ex.Message,ex.InnerException,ex.StackTrace);
                return new ApiResponse(Status400BadRequest, "Öğrenci kullanıcı güncellemesi hatası oluştu: {0}", ex.Message);
            }


            return new ApiResponse(Status200OK, "Kullanıcı bilgileri güncellendi");
        }

        public async Task<ApiResponse> UpdateAkademisyenUser(AkademisyenDto akademisyenDto)
        {
            var user = await _userManager.FindByIdAsync(akademisyenDto.ApplicationUserId.ToString());

            if (user == null)
            {
                _logger.LogInformation("Bu kullanıcı mevcut değil: {0}", akademisyenDto.OgrtNo);
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

        public async Task<ApiResponse> CreateOgrenci(OgrenciDto ogrenciDto)
        {
            try
            {
                Guid possibleUserId = Guid.NewGuid();



                var user = new ApplicationUser
                {
                    UserName = ogrenciDto.OgrNo,
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
                        new Claim(JwtClaimTypes.Name, ogrenciDto.OgrNo),
                        new Claim(JwtClaimTypes.Email, ogrenciDto.Email),
                        new Claim(JwtClaimTypes.EmailVerified, "false", ClaimValueTypes.Boolean)
                    }).Result;
                }


                try
                {
                    //Öğrenci Kaydı ekleme
                    ogrenciDto.ApplicationUserId = possibleUserId;
                    Ogrenci ogrCreateResult = await _ogrenciStore.Create(ogrenciDto);
                }
                catch (Exception ex)
                {
                    _logger.LogInformation("Kullanıcının öğrenci kaydı başarısız oldu: {0} inner:{1} stacktrace:{2}", ex.Message, ex.InnerException, ex.StackTrace);
                    return new ApiResponse(Status400BadRequest, "Kullanıcının öğrenci kaydı başarısız oldu: " + ex.Message);
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
                    ApplicationUserId = user.Id,
                    IsAuthenticated = false,
                    OgrNo = user.UserName,
                    Email = user.Email,
                    Ad = user.FirstName,
                    Soyad = user.LastName,
                    TCKN = user.TCKN,
                    //ExposedClaims = user.Claims.ToDictionary(c => c.Type, c => c.Value),
                    Roles = new List<string> { "User" }
                };

                return new ApiResponse(Status200OK, "Yeni öğrenci kullanıcı oluşturuldu", ogrenciDto1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Yeni öğrenci kullanıcısı oluşturulamadı: {ex.Message}");
                return new ApiResponse(Status400BadRequest, "Yeni öğrenci kullanıcısı oluşturulamadı.");
            }
        }

        public async Task<ApiResponse> CreateAkademisyen(AkademisyenDto akademisyenDto)
        {
            try
            {
                Guid possibleUserId = Guid.NewGuid();



                var user = new ApplicationUser
                {
                    UserName = akademisyenDto.OgrtNo,
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
                        new Claim(JwtClaimTypes.Name, akademisyenDto.OgrtNo),
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
                    OgrtNo = user.UserName,
                    Email = user.Email,
                    Ad = user.FirstName,
                    Soyad = user.LastName,
                    TCKN = user.TCKN,
                    //ExposedClaims = user.Claims.ToDictionary(c => c.Type, c => c.Value),
                    Roles = new List<string> { "User" }
                };

                return new ApiResponse(Status200OK, "Yeni akademisyen kullanıcı oluşturuldu", akademisyenDto1);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Yeni akademisyen kullanıcısı oluşturulamadı: {ex.Message}");
                return new ApiResponse(Status400BadRequest, "Yeni akademisyen kullanıcısı oluşturulamadı.");
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

        
    }
}