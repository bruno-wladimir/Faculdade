using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutenticacaO.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        //HttpClient client;

        private readonly INavigationService _navigationService;
        IPageDialogService _dialogservice;
        public DelegateCommand CommandLogar { get; set; }
        public DelegateCommand CommandLogarFacebook { get; set; }


        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _User;

        public string User
        {
            get { return _User; }
            set { SetProperty(ref _User, value); }
        }

        private string _Senha;

        public string Senha
        {
            get { return _Senha; }
            set { SetProperty(ref _Senha, value); }
        }



        public MainPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
        {
            CommandLogarFacebook = new DelegateCommand(LogarFacebook);
            CommandLogar = new DelegateCommand(Autenticar);
            _navigationService = navigationService;
            _dialogservice = dialogService;
        }

        private async void LogarFacebook()
        {

            await _navigationService.NavigateAsync("LoginFacebookPage");

        }

        private async void Autenticar()
        {
            API api = new API();

            string resposta;


            //    ObservableCollection<Users> x =await api.GetAllUser();


            resposta = api.GetAsync(User, Senha);
            if (resposta == "Logado")
            {
                await _dialogservice.DisplayAlertAsync("Sucess", "Loading...", "ok,");

                await _navigationService.NavigateAsync("InicialPage");
            }

            else
            {
                await _dialogservice.DisplayAlertAsync("Erro", "Senha e/ou Usuario incorreto", "ok,");

            }

        

        }




        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
    }
}
