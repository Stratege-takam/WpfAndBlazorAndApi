using Brewery.BO.Entities;
using Elia.Core.BaseModel;
using Microsoft.EntityFrameworkCore;

namespace Brewery.Context;


    /// <summary>
    /// Context of Brewery manage apps
    /// </summary>
    public class BreweryContext: DbContext
    {



        #region Constructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public BreweryContext(DbContextOptions<BreweryContext> options) : base(options)
        { }

        #endregion

        #region Override methods

        /// <summary>
        /// On normalize model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<BeerEntity>()
                .HasIndex(u => u.Name)
                .IsUnique();
            
            modelBuilder.Entity<CompanyEntity>()
                .HasIndex(u => u.Email)
                .IsUnique();
            
            modelBuilder.Entity<CompanyEntity>()
                .HasIndex(u => u.Phone)
                .IsUnique();
        }

        #endregion

        #region Db set

        /// <summary>
        /// Track dbset
        /// </summary>
        public virtual DbSet<Track> Tracks { get; set; }
        
        
        /// <summary>
        /// Beer dbset
        /// </summary>
        public virtual DbSet<BeerEntity> Beers { get; set; }
        
        /// <summary>
        /// Brewery dbset
        /// </summary>
        public virtual DbSet<BreweryEntity> Breweries { get; set; }
        
        /// <summary>
        /// Client dbset
        /// </summary>
        public virtual DbSet<ClientEntity> Clients { get; set; }
        
        
        /// <summary>
        /// Company dbset
        /// </summary>
        public virtual DbSet<CompanyEntity> Companies { get; set; }
        
        
        /// <summary>
        /// Order beer dbset
        /// </summary>
        public virtual DbSet<OrderBeerEntity> OrderBeers { get; set; }
        
        /// <summary>
        /// Order dbset
        /// </summary>
        public virtual DbSet<OrderEntity> Orders { get; set; }
     
        
        /// <summary>
        /// Stock beer wholesaler dbset
        /// </summary>
        public virtual DbSet<StockBeerWholesalerEntity> StockBeerWholesalers { get; set; }
        
        /// <summary>
        /// Wholesaler dbset
        /// </summary>
        public virtual DbSet<WholesalerEntity> Wholesalers { get; set; }
        
        /// <summary>
        /// Users dbset
        /// </summary>
        public virtual DbSet<UserEntity> Users { get; set; }
        
        /// <summary>
        /// Users dbset
        /// </summary>
        public virtual DbSet<MediaEntity> Medias { get; set; }

        #endregion
    }