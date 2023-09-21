namespace Elia.Core.BaseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


  /// <summary>
    /// Base table to detecte and save all entries
    /// </summary>
    [Table("Tracks")]
    public abstract partial class Track: ITrack, IValidatableObject
    {
        #region Properties

        /// <summary>
        /// Key id
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual Guid Id { get; set; }
        
        /// <summary>
        ///modification date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? UpdateAt { get; set; }
        
        
        /// <summary>
        /// creation date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? CreateAt { get; set; }
        
        /// <summary>
        /// delation date
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime? DeleteAt { get; set; }

        #endregion


        #region Validators

        public virtual List<ValidationResult> ValidationResults { get; set; } = null;
        public  IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
          /*  if (UpdateAt == CreateAt)
            {
                yield return new ValidationResult(
                    "Blog Title cannot match Blogger Name",
                    new[] { nameof(Title), nameof(BloggerName) });
            } */

          if (ValidationResults != null)
          {
              foreach (var validationResult in ValidationResults)
              {
                  yield return validationResult;
              }
          }
        }


        #endregion
       
    }