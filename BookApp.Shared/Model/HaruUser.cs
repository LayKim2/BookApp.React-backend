using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookApp.Shared
{
    /// <summary>
    /// 기본 클래스: 공통 속성들을 모두 모아 놓은 모델 클래스
    /// </summary>
    public class HaruUserBase
    {
        /// <summary>
        /// UserID
        /// </summary>
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Input UserID")]
        [Display(Name = "UserID")]
        public string UserID { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        //[MaxLength(255)]
        [Required(ErrorMessage = "Input Password")]
        [Display(Name = "Pass")]
        public string Pass { get; set; }

        public string? Salt { get; set; }

        public int Iteration { get; set; }

        public DateTime? Created { get; set; }
    }

    // DataAnnotations pkg
    [Table("HaruUser")]
    public class HaruUser : HaruUserBase
    {

    }
}
