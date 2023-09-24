using System;
using System.Collections.ObjectModel;

namespace Brewery.VM.ViewModels.Breweries;


public class BreweryCreateViewModel: ViewModelCommon
{
    #region Properties Notify
    #region Information for brewery

    private Guid? _id ;
    public Guid? Id
    {
        get => _id;
        set => SetProperty(ref _id, value, () => Validate()) ; 
    }
    
    private string _email;
    public string Email
    {
        get => _email;
        set => SetProperty(ref _email, value , () => Validate()); 
    }
        
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value , () => Validate()); 
    }
        
    private string _vat;
    public string Vat
    {
        get => _vat;
        set => SetProperty(ref _vat, value , () => Validate()); 
    }
        
    private string _phone;
    public string Phone
    {
        get => _phone;
        set => SetProperty(ref _phone, value , () => Validate()); 
    }
        
    #endregion

    #endregion
}

public class BreweryDetailViewModel: BeerCreateViewModel
    {
        #region Properties Notify
        #region Information for brewery

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value , () => Validate()); 
        }
        
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value , () => Validate()); 
        }
        
        private string _vat;
        public string Vat
        {
            get => _vat;
            set => SetProperty(ref _vat, value , () => Validate()); 
        }
        
        private string _phone;
        public string Phone
        {
            get => _phone;
            set => SetProperty(ref _phone, value , () => Validate()); 
        }

        private ObservableCollection<BeerCreateViewModel> _beers;
        public ObservableCollection<BeerCreateViewModel> Beers
        {
            get => _beers;
            set => SetProperty(ref _beers, value , () => Validate()); 
        }
        
        #endregion

        #endregion
        
    }