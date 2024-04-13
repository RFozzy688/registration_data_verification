using Microsoft.AspNetCore.Mvc;
using registration_data_verification.Data.Dal;
using registration_data_verification.Models.Home.Signup;
using registration_data_verification.Services.Hash;
using registration_data_verification.Services.Kdf;
using System;

namespace registration_data_verification.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHashService _hashService;
        private readonly IKdfService _kdfService;
        private readonly DataAccessor _dataAccessor;
        public static int x = 0;

        public HomeController(
            IHashService hashService,
            IKdfService kdfService,
            DataAccessor dataAccessor)
        {
            _hashService = hashService;
            _kdfService = kdfService;
            _dataAccessor = dataAccessor;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Signup(SignUpFormModel? formModel)
        {
            SignUpPageModel pageModel = new()
            {
                SignUpFormModel = formModel,
                ValidationErrors = _ValidateSignUpModel(formModel),
                Count = x++
            };

            if (pageModel.Count != 0/*formModel?.UserEmail != null*/)
            {
                if (pageModel.ValidationErrors.Count > 0)
                {
                    pageModel.Message = "Регистрация отклонена";
                    pageModel.IsSuccess = false;
                }
                else
                {
                    _dataAccessor.UserDao.SignupUser(mapUser(formModel));
                    pageModel.Message = "Регистрация успешна";
                    pageModel.IsSuccess = true;
                }
            }

            return View(pageModel);

        }

        private Data.Entites.User mapUser(SignUpFormModel formModel)
        {
            String salt = Guid.NewGuid().ToString();

            return new()
            {
                Id = Guid.NewGuid(),
                Name = formModel.UserName,
                Email = formModel.UserEmail,
                Registered = DateTime.Now,
                Birthdate = formModel.Birthdate,
                Salt = salt,
                DerivedKey = _kdfService.GetDerivedKey(formModel.UserPassword, salt)
            };
        }

        private Dictionary<String, String> _ValidateSignUpModel(SignUpFormModel? formModel)
        {
            Dictionary<String, String> res = new();
            if (formModel == null)
            {
                res[nameof(formModel)] = "Model is null";
            }
            else
            {
                if (String.IsNullOrEmpty(formModel.UserName))
                {
                    res[nameof(formModel.UserName)] = "Name is empty";
                }
                if (String.IsNullOrEmpty(formModel.UserEmail))
                {
                    res[nameof(formModel.UserEmail)] = "Email is empty";
                }
                if (String.IsNullOrEmpty(formModel.UserPassword))
                {
                    res[nameof(formModel.UserPassword)] = "Password is empty";
                }

                if (!_dataAccessor.UserDao.IsEmailFree(formModel.UserEmail))
                {
                    res[nameof(formModel.UserEmail)] = "Email is use";
                }
                if (!formModel.Confirm)
                {
                    res[nameof(formModel.Confirm)] = "Confirm expected";
                }
            }

            return res;
        }
    }
}
