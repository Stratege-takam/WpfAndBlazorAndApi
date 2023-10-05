using Brewery.Contract.Contracts.Responses.Companies;

namespace Brewery.Web.Helpers.ViewModels;

public class BreweryViewModel: NotifyPropertyChanged
{
    #region Properties change

    private Guid _id;
    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value); 
    }
    
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value); 
    }
    
    public List<BeerDetailViewModel>  Beers { get; set; }

    #endregion End for tabControl
}


public class BeerDetailViewModel: NotifyPropertyChanged
{
    #region Properties change

    private Guid _id;
    public Guid Id
    {
        get => _id;
        set => SetProperty(ref _id, value); 
    }
    
    private string _name;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value); 
    }
    
    private string _description;
    public string Description
    {
        get => _name;
        set => SetProperty(ref _description, value); 
    }
    
    private double _degree;
    public double Degree
    {
        get => _degree;
        set => SetProperty(ref _degree, value); 
    }

    private double _price;
    public double Price
    {
        get => _price;
        set => SetProperty(ref _price, value); 
    }
    
    public GetCompanyResponse Owner { get ; set ; }
  
    #endregion End for tabControl
}