using System.Globalization;
using Brewery.Web.Helpers.ViewModels;
using Elia.Core.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Brewery.Web.Helpers.States;

[Injectable(serviceLifetime: ServiceLifetime.Singleton)]
public class CultureState: NotifyPropertyChanged
{
    // [Inject] private NavigationManager NavManager { get; set; }
    private readonly NavigationManager _navManager;
    private readonly IJSRuntime _jSRuntime;

    
     private List<KeyValueViewModel> Languages = new List<KeyValueViewModel>()
      {
        new KeyValueViewModel()
        {
          Key = "fr",
          Value = "HeaderFr"
        },
        new KeyValueViewModel(){
          Key = "en",
          Value = "HeaderEn"
        },
        new KeyValueViewModel(){
          Key = "nl",
          Value = "HeaderNl"
        },
        new KeyValueViewModel() {
          Key = "ar",
          Value = "HeaderAr"
        }
      };
      
      public  void SetCurrentCulture()
      {
        CurrentCulture = Languages.FirstOrDefault(c => c.Key == CultureInfo.CurrentCulture?.Name) ??  new KeyValueViewModel()
        {
          Key = "fr",
          Value = "HeaderFr"
        };
      }
    
      public KeyValueViewModel CurrentCulture { get; set; }


      /// <summary>
      /// 
      /// </summary>
      /// <param name="jSRuntime"></param>
      /// <param name="navManager"></param>
      public CultureState( IJSRuntime jSRuntime, NavigationManager navManager)
      {
        _jSRuntime = jSRuntime;
        _navManager = navManager;
        SetCurrentCulture();
      }
      public void SwitchLanguage(string lang)
      {
        var culture = new CultureInfo(lang);
        if (CultureInfo.CurrentCulture != culture)
        {
          var js = (IJSInProcessRuntime)_jSRuntime;
          js.InvokeVoid("blazorCulture.set", culture.Name);
          SetCurrentCulture();
          _navManager.NavigateTo(_navManager.Uri, forceLoad: true);
        }
      }

      public List<KeyValueViewModel> GetDisplayLanguage()
      {
         var languages =  this.CurrentCulture != null ? Languages.Where(c => c.Key != CurrentCulture.Key).ToList() : Languages;

         return languages;
      }

}