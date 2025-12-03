using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OLC.Web.UI.Models;
using OLC.Web.UI.Services;

namespace OLC.Web.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotyfService _notyfService;
        private readonly IAuthenticateService _authenticateService;
        private readonly IUserKycService _userKycService;
        private readonly IUserKycDocumentService _userKycDocumentService;
        public UserController(IUserService userService,
            INotyfService notyfService,
            IAuthenticateService authenticateService,
            IUserKycService userKycService, IUserKycDocumentService userKycDocumentService)
        {
            _userService = userService;
            _notyfService = notyfService;
            _authenticateService = authenticateService;
            _userKycService = userKycService;
            _userKycDocumentService = userKycDocumentService;
        }

        [Authorize(Roles = ("Administrator"))]
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = ("Administrator,Executive"))]
        public IActionResult ManageUser(long userId, bool isReadOnly)
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> GetUserAccounts()
        {
            try
            {
                var response = await _userService.GetUserAccountsAsync();
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpPost]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> CreatePortalUser([FromBody] UserRegistration userRegistration)
        {
            try
            {
                var response = await _authenticateService.RegisterUserAsync(userRegistration);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetUserKycByUSerId(long userId)
        {
            try
            {
                var response = await _userKycService.GetUserKycByUserIdAsync(userId);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUsersKyc()
        {
            try
            {
                var response = await _userKycService.GetAllUsersKycAsync();
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> InsertAndUpdateUserKyc([FromBody] UserKyc userKyc)
        {
            try
            {
                bool isSaved = false;

                if (userKyc != null)
                {
                    if (userKyc.Id > 0)
                        isSaved = await _userKycService.ProcessUserKycAsync(userKyc);
                    else
                        isSaved = await _userKycService.InsertUserKycAsync(userKyc);

                    _notyfService.Success("Successfully Inserted User Kyc");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to Insert User Kyc");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Authorize(Roles = ("Administrator,Executive"))]
        public async Task<IActionResult> GetAllUsersKycDocuments(UserKyc userKyc)
        {
            try
            {
                var response = await _userKycDocumentService.GetAllUsersKycDocumentsAsync();
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Executive")]
        public async Task<IActionResult> UploadAndUpdateUserKyc([FromBody] UserKycDocument userKycDocument)
        {
            try
            {
                bool isSaved = false;

                if (userKycDocument != null)
                {
                    if (userKycDocument.Id > 0)
                        isSaved = await _userKycDocumentService.UpdateUserKycDocumentAsync(userKycDocument);
                    else
                        isSaved = await _userKycDocumentService.UploadeUserKycDocumentAsync(userKycDocument);

                    _notyfService.Success("Successfully uploaded user kyc document");

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to upload user kyc document");
                return Json(isSaved);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpPost]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> UploadAndUpdateUserKycDocument([FromBody] KycDocument kycDocument)
        {
            try
            {
                bool isSaved = false;

                UserKycDocument userKycDocument = new UserKycDocument();



                if (userKycDocument != null && !string.IsNullOrEmpty(kycDocument.DocumentFileData))
                {


                    // Convert base64 string to byte array
                    byte[] documentBytes = Convert.FromBase64String(kycDocument.DocumentFileData);

                    userKycDocument.DocumentNumber = kycDocument.DocumentNumber;
                    userKycDocument.DocumentType = kycDocument.DocumentType;
                    userKycDocument.DocumentFileData = documentBytes;
                    userKycDocument.ExpiryDate = DateTime.Now.AddDays(360);
                    userKycDocument.VerificationStatus = "Pending";
                    userKycDocument.CreatedBy = kycDocument.UserId;
                    userKycDocument.CreatedOn = DateTime.Now;
                    userKycDocument.ModifiedBy = kycDocument.UserId;
                    userKycDocument.ModifiedOn = DateTime.Now;
                    userKycDocument.IsActive = true;
                    userKycDocument.RejectionReason = "";
                    userKycDocument.UserId = kycDocument.UserId.Value;
                    isSaved = await _userKycDocumentService.UploadeUserKycDocumentAsync(userKycDocument);

                    if (isSaved)
                    {
                        _notyfService.Success("Successfully uploaded user KYC document");
                        var applicationUser = await _userService.GetUserAccountAsync(kycDocument.UserId.Value);
                        return Json(new { data = applicationUser });
                    }

                    return Json(isSaved);
                }

                _notyfService.Error("Unable to upload user KYC document - invalid data");
                return Json(isSaved);
            }
            catch (FormatException ex)
            {
                _notyfService.Error("Invalid base64 format for document");
                return StatusCode(StatusCodes.Status400BadRequest, "Invalid document format");
            }
            catch (Exception ex)
            {
                _notyfService.Error("Error uploading KYC document");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        [HttpGet]
        public async Task<IActionResult> PreviewUserKycDocument(long userId)
        {
            try
            {
                var response = await _userService.PreviewUserKycDocumentAsync(userId);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> VerifyUserKycDocument([FromBody] VerifyUserKyc verifyUserKyc)
        {
            try
            {
                var response = await _userKycService.VerifyUserKycAsync(verifyUserKyc);
                return Json(new { data = response });

            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }
        [HttpGet]
        [Authorize(Roles = "Administrator,Executive,User")]
        public IActionResult Profile()
        {
            return View("~/Views/Shared/_userProfileSettingsPartial.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Administrator,Executive,User")]
        public async Task<IActionResult> UpdateUserPersonalInformation([FromBody] UserPersonalInformation userPersonalInformation)
        {
            try
            {
                var response = await _userService.UpdateUserPersonalInformationAsync(userPersonalInformation);
                return Json(new { data = response });
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
                throw ex;
            }
        }   
    }
}
