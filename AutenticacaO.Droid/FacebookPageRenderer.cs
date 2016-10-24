using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using AutenticacaO.Views;
using Prism.Navigation;
using Xamarin.Auth;
using Newtonsoft.Json.Linq;

[assembly: ExportRenderer(typeof(LoginFacebookPage), typeof(AutenticacaO.Droid.FacebookPageRenderer))]

namespace AutenticacaO.Droid
{
   public  class FacebookPageRenderer : PageRenderer
    {


        INavigationService _navegar;
        public FacebookPageRenderer()
        {
            var activity = this.Context as Activity;
            var auth = new OAuth2Authenticator(
    clientId: "476292725915235",
    scope: "",
    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
    redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));

            auth.Completed += async (sender, eventArgs) => {
                if (eventArgs.IsAuthenticated)
                {
                    var accessToken = eventArgs.Account.Properties["access_token"].ToString();
                    var expiresIn = Convert.ToDouble(eventArgs.Account.Properties["expires_in"]);
                    var expiryDate = DateTime.Now + TimeSpan.FromSeconds(expiresIn);

                    var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me"), null, eventArgs.Account);
                    var response = await request.GetResponseAsync();
                    var obj = JObject.Parse(response.GetResponseText());

                    var id = obj["id"].ToString().Replace("\"", "");
                    var name = obj["name"].ToString().Replace("\"", "");

                    App app = new App();
                   await app.NavigateToProfile(string.Format("Olá {0}", name));

                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "lOGIN CANCELADO", " OK");


                }
            };
            activity.StartActivity(auth.GetUI(activity));
        }
    }
}