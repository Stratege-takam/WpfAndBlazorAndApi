using System.Globalization;
using Brewery.Web.ViewModels;
using Elia.Core.Attributes;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Brewery.Web.States;

[Injectable(serviceLifetime: ServiceLifetime.Singleton)]
public class CultureState: NotifyPropertyChanged
{
    // [Inject] private NavigationManager NavManager { get; set; }
    private NavigationManager _navManager;
    private IJSRuntime _jSRuntime;

   
    
    private string _search;
    public string Search
    {
      get => _search;
      set => SetProperty(ref _search, value );
    }

      private int _increment;
      public int Increment
      {
        get => _increment ;
        set => SetProperty(ref _increment, value );
      } 
    
     private List<KeyValue> Languages = new List<KeyValue>()
      {
        new KeyValue()
        {
          Key = "fr",
          Value = "HeaderFr"
        },
        new KeyValue(){
          Key = "en",
          Value = "HeaderEn"
        },
        new KeyValue(){
          Key = "nl",
          Value = "HeaderNl"
        },
        new KeyValue() {
          Key = "ar",
          Value = "HeaderAr"
        }
      };
      
      public  void SetCurrentCulture()
      {
        CurrentCulture = Languages.FirstOrDefault(c => c.Key == CultureInfo.CurrentCulture?.Name) ??  new KeyValue()
        {
          Key = "fr",
          Value = "HeaderFr"
        };
      }
    
      public KeyValue CurrentCulture { get; set; }


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

      public List<KeyValue> GetDisplayLanguage()
      {
         var languages =  this.CurrentCulture != null ? Languages.Where(c => c.Key != CurrentCulture.Key).ToList() : Languages;

         return languages;
      }

      public void SetIncrement()
      {
        Increment++;
      }
      
      
  
}