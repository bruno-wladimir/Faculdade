using AutenticacaO.Views;
using Prism.Navigation;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutenticacaO
{

    public class App : PrismApplication
    {

        public  Action HideLoginView
        {
            get
            {
                return new Action(() => NavigationService.GoBackAsync());
            }
        }   

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("MainPage");

        
        }
      


    protected override void RegisterTypes()
    {
        Container.RegisterTypeForNavigation<MainPage>();
        Container.RegisterTypeForNavigation<LoginFacebookPage>();
        Container.RegisterTypeForNavigation<SucessLoginPage>();
    }

    public async Task NavigateToProfile(string v)
    {

       
        await NavigationService.NavigateAsync("SucessLoginPage");
          
            

    }


}
}