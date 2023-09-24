using System;

namespace Brewery.VM.ViewModels;

public class BreweryDetailViewModel: ViewModelCommon
    {
        #region Properties Notify
    
        private string _errorServer;
        public string ErrorServer
        {
            get => _errorServer;
            set => SetProperty(ref _errorServer, value); 
        }

        #region Information for brewery

        private string _email;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value , () => Validate()); 
        }
    
        private Guid? _id ;
        public Guid? Id
        {
            get => _id;
            set => SetProperty(ref _id, value, () => Validate()) ; 
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

        #region Information for beer

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
        
        private double _degreeOfBeer;
        public double DegreeOfBeer
        {
            get => _degreeOfBeer;
            set => SetProperty(ref _degreeOfBeer, value , () => Validate()); 
        }
        
        private double _priceOfBeer;
        public double PriceOfBeer
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
        
        
        public override bool Validate()
        {
            ClearErrors();
        
            if ( string.IsNullOrEmpty(NameOfBeer))
                AddError(nameof(NameOfBeer), "The name is required");
            else if (NameOfBeer.Length <2)
                AddError(nameof(NameOfBeer), "The name must be least that 2 characters");
            
            if (PriceOfBeer <=0)
                AddError(nameof(PriceOfBeer), "The price must be greater than 0");
            
            if (DegreeOfBeer <0)
                AddError(nameof(DegreeOfBeer), "The degree must be positive");
            
            if (Id == null)
                AddError(nameof(Id), "The owner is required");
            
            if ( string.IsNullOrEmpty(DescriptionOfBeer))
                AddError(nameof(DescriptionOfBeer), "The description is required");
            else if (DescriptionOfBeer.Length <12)
                AddError(nameof(DescriptionOfBeer), "The description must be least that 1 characters");

            return !HasErrors;
        }
    }