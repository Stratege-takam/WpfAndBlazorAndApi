using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Brewery.BL.Client.Business.Beers;
using Brewery.BL.Client.Business.Breweries;
using Brewery.BL.Client.Contracts.Inputs.Beers;
using Brewery.VM.Enums;
using Elia.Core.Utils;
using Elia.Share.WPF.BaseClasses;
using Elia.Share.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Brewery.VM.ViewModels.Breweries;

public class BeerCreateViewModel: DialogViewModelCommon
{
    
    #region Private properties

    private BeerService _bl;
    private BreweryService _breweryBl;

    #endregion
    
      #region Properties Notify
    
        private string _errorServer;
        public string ErrorServer
        {
            get => _errorServer;
            set => SetProperty(ref _errorServer, value); 
        }
        
        private string _successServer;
        public string SuccessServer
        {
            get => _successServer;
            set => SetProperty(ref _successServer, value); 
        }
        
    

        #region Information for beer
        
        private Guid? _id ;
        public Guid? Id
        {
            get => _id;
            set
            {
                SetProperty(ref _id, value, () => Validate()) ; 
            }
        }
        
        
        private ObservableCollection<BreweryCreateViewModel> _owners ;
        public ObservableCollection<BreweryCreateViewModel> Owners
        {
            get => _owners;
            set => SetProperty(ref _owners, value) ; 
        }

        private Guid _idOfBeer ;
        public Guid IdOfBeer
        {
            get => _idOfBeer;
            set => SetProperty(ref _idOfBeer, value, () => Validate()) ; 
        }

        private string _nameOfBeer;
        public string NameOfBeer
        {
            get => _nameOfBeer;
            set => SetProperty(ref _nameOfBeer, value , () => Validate()); 
        }
        
        private double? _degreeOfBeer;
        public double? DegreeOfBeer
        {
            get => _degreeOfBeer;
            set => SetProperty(ref _degreeOfBeer, value , () => Validate()); 
        }
        
        private double? _priceOfBeer;
        public double? PriceOfBeer
        {
            get => _priceOfBeer;
            set => SetProperty(ref _priceOfBeer, value , () => Validate()); 
        }
        
        private string _descriptionOfBeer;
        public string DescriptionOfBeer
        {
            get => _descriptionOfBeer;
            set => SetProperty(ref _descriptionOfBeer, value , () => Validate()); 
        }
        #endregion
     
        #endregion
        
        #region Commands

        public  ICommand OnSubmitCommand { get; set; }

        #endregion

        #region Constructor

            public  BeerCreateViewModel(): base()
            {
                _bl = ServiceProvider.GetRequiredService<BeerService>();
                _breweryBl = ServiceProvider.GetRequiredService<BreweryService>();

                NotifyColleagues(MessageEnum.MsgLoadBeer);
                
                Register( MessageEnum.MsgLoadBeer, async () =>
                {
                    OnRefresh();
                });

                OnSubmitCommand = new RelayCommand(async () =>
                {
                    Loading = DefaultTextLoad;
                    SuccessServer = null;
                    ErrorServer = null;
                    var response = await  _bl.CreateBeerAsync(new CreateBeerInput()
                    {
                       Degree = DegreeOfBeer.GetValueOrDefault(),
                       Name = NameOfBeer,
                       Price = PriceOfBeer.GetValueOrDefault(),
                       OwnerId = Id.GetValueOrDefault(),
                       Description = DescriptionOfBeer
                    });
               
                    // Login success. Set token
                    if (response.ResultStatus == BaseResultStatus.Success)
                    {
                        ResetForm();
                        Loading = null;
                        SuccessServer = $"Create beer {response.Data.Name} successfull !";
                        return;
                    }

                    Loading = null;
                    ErrorServer = response.Reason;
                });
            }
        #endregion


        #region Reset method

        public async Task OnRefresh()
        {
            var response = await _breweryBl.GetBreweriesAsync(null, int.MaxValue, 0 );
            if (response.ResultStatus == BaseResultStatus.Success)
            {
                Owners = new ObservableCollection<BreweryCreateViewModel>(response.Data.Results.Select(b => new BreweryCreateViewModel()
                {
                    Id = b.Id,
                    Name = b.Name,
                }));
            }
        }
        public void ResetForm()
        {
            NameOfBeer = null;
            DescriptionOfBeer = null;
            DegreeOfBeer = null;
            PriceOfBeer = null;
            Id = null;
            ClearErrors();
        }
        public override bool Validate()
        {
            ClearErrors();
        
            if ( string.IsNullOrEmpty(NameOfBeer))
                AddError(nameof(NameOfBeer), "The name is required");
            else if (NameOfBeer.Length <2)
                AddError(nameof(NameOfBeer), "The name must be least that 2 characters");
            
            if (PriceOfBeer == null || PriceOfBeer <=0)
                AddError(nameof(PriceOfBeer), "The price must be greater than 0");
            
            if (DegreeOfBeer == null || DegreeOfBeer <0)
                AddError(nameof(DegreeOfBeer), "The degree must be positive");
            
            if (Id == null)
                AddError(nameof(Id), "The owner is required");
            
            if ( string.IsNullOrEmpty(DescriptionOfBeer))
                AddError(nameof(DescriptionOfBeer), "The description is required");
            else if (DescriptionOfBeer.Length <12)
                AddError(nameof(DescriptionOfBeer), "The description must be least that 1 characters");

            return !HasErrors;
        }


        #endregion
}