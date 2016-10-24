using System;
using AutenticacaO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Net;
using Prism.Navigation;
using System.Collections.ObjectModel;
using AutenticacaO.Model;

namespace AutenticacaO
{
    public class API
    {
        public const string WepApiUrl = "";
        //INavigationService _navigation;

        public string GetAsync(string nome, string senha)
        {
            
               Users us = new Users();
            var httpclient = new HttpClient();
            
             
           HttpResponseMessage res =  httpclient.GetAsync(WepApiUrl + "?nome=" + nome + "&senha=" + senha).Result;
       

            if (res.IsSuccessStatusCode)

            {
                return "Logado";
              

            }
  
            else
            {
                return "Erro AO LOGAR";

            }
         
        }


       public async Task<ObservableCollection<Users>> GetAllUser()
        {
            ObservableCollection<Users> listauser = new ObservableCollection<Model.Users>();
            var client = new HttpClient();

            var response = await client.GetAsync(WepApiUrl);
                if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

              listauser =  JsonConvert.DeserializeObject <ObservableCollection<Users>>(json);
                return listauser;
                    
                    }
            return null;
         }
       

        public API()
        {


        }
    }
}

