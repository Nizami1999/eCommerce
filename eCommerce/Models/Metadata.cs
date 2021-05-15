using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCommerce.Models
{
    public class HomeSliderMetadata
    {
        [Display(Name = "Title Top")]
        [Required(ErrorMessage = "Please, fill the title top")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 50")]
        public string TitleTop { get; set; }

        [Display(Name = "Title Main")]
        [Required(ErrorMessage = "Please, fill the title main")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 50")]
        public string TitleMain { get; set; }

        [Display(Name = "Title Bottom")]
        [Required(ErrorMessage = "Please, fill the title bottom")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string TitleBottom { get; set; }

        [Display(Name = "Title Span")]
        [Required(ErrorMessage = "Please, fill the title span")]
        [StringLength(maximumLength: 250, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 250")]
        public string TitleSpan { get; set; }
    }
    public class HomeCollectionMetadata
    {
        [Display(Name = "Left Image")]
        public string LeftImage { get; set; }

        [Display(Name = "Center Image")]
        public string CenterImage { get; set; }

        [Display(Name = "Right Image")]
        public string RightImage { get; set; }

        [Display(Name = "First Text Main")]
        [Required(ErrorMessage = "Please, fill the First Text Main")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 50")]
        public string FirstTextMain { get; set; }

        [Display(Name = "First Text Info")]
        [Required(ErrorMessage = "Please, fill the First Text Info")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 200")]
        public string FirstTextInfo { get; set; }

        [Display(Name = "Second Text Main")]
        [Required(ErrorMessage = "Please, fill the Second Text Main")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 50")]
        public string SecondTextMain { get; set; }

        [Display(Name = "Second Text Info")]
        [Required(ErrorMessage = "Please, fill the Second Text Info")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 200")]
        public string SecondTextInfo { get; set; }
    }
    public class HomeComingProductMetadata
    {
        [Required(ErrorMessage = "Please, fill the Name")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string Name { get; set; }

        [Display(Name = "New Price")]
        [Required(ErrorMessage = "Please, the New Price")]
        public Nullable<decimal> NewPrice { get; set; }

        [Display(Name = "Previous Price")]
        [Required(ErrorMessage = "Please, fill the Previous Price")]
        public Nullable<decimal> PrevPrice { get; set; }

        [Display(Name = "Information")]
        [Required(ErrorMessage = "Please, fill the Information")]
        [StringLength(maximumLength: 400, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 400")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Please, fill the Brand")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 50")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Please, fill the Color 1")]
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 25")]
        public string Color1 { get; set; }

        [Required(ErrorMessage = "Please, fill the Memory")]
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 25")]
        public string Memory { get; set; }

        [Required(ErrorMessage = "Please, fill the Camera")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string Camera { get; set; }
    }
    public class SmartphoneMetadata
    {
        [Required(ErrorMessage = "Please, fill the Name")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string Name { get; set; }

        [Display(Name = "New Price")]
        [Required(ErrorMessage = "Please, fill the New Price")]
        [Range(0, 9999, ErrorMessage = "Price must be between 0 and 9999.99")]
        public Nullable<decimal> NewPrice { get; set; }

        [Display(Name = "Previous Price")]
        [Range(0, 9999, ErrorMessage = "Price must be between 0 and 9999.99")]
        public Nullable<decimal> PrevPrice { get; set; }

        [Display(Name = "Information")]
        [Required(ErrorMessage = "Please, fill the Information")]
        [StringLength(maximumLength: 400, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 400")]
        public string Info { get; set; }

        [Required(ErrorMessage = "Please, fill the Color1")]
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 25")]
        public string Color1 { get; set; }

        [Required(ErrorMessage = "Please, fill the Memory")]
        [StringLength(maximumLength: 25, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 25")]
        public string Memory { get; set; }

        [Required(ErrorMessage = "Please, fill the Camera")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string Camera { get; set; }
    }
    public class UserMetadata
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Please, fill the full name")]
        [StringLength(maximumLength: 70, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 70")]
        public string FullName { get; set; }

        [Display(Name = "Username")]
        [Required(ErrorMessage = "Please, fill the username")]
        [StringLength(maximumLength: 70, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 70")]
        public string Username { get; set; }

        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Email syntax is not valid")]
        [Required(ErrorMessage = "Please, fill the email")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, fill the password")]
        [StringLength(maximumLength: 255, MinimumLength = 5, ErrorMessage = "Number of letters must be between 5 and 255")]
        public string Password { get; set; }
    }
    public class AboutMetadata
    {
        [Display(Name = "Main title")]
        [Required(ErrorMessage = "Please, fill the Main title")]
        [StringLength(maximumLength: 50, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 50")]
        public string MainTitle { get; set; }

        [Display(Name = "Info")]
        [Required(ErrorMessage = "Please, fill the Info")]
        [StringLength(maximumLength: 500, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 500")]
        public string Info { get; set; }
    }
    public class BlogMetadata
    {
        [Display(Name = "Main title")]
        [Required(ErrorMessage = "Please, fill the Main title")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string MainTitle { get; set; }

        [Display(Name = "Info")]
        [Required(ErrorMessage = "Please, fill the Info")]
        [StringLength(maximumLength: 400, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 400")]
        public string Info { get; set; }
    }
    public class TeamMetadata
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Please, fill the Full Name")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 100")]
        public string FullName { get; set; }

        [Display(Name = "Instagram Link")]
        [Required(ErrorMessage = "Please, fill the Instagram Link")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 200")]
        public string InstagramLink { get; set; }

        [Display(Name = "Linked Link")]
        [Required(ErrorMessage = "Please, fill the Linked Link")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 200")]
        public string LinkedInLink { get; set; }

        [Display(Name = "Facebook Link")]
        [Required(ErrorMessage = "Please, fill the Facebook Link")]
        [StringLength(maximumLength: 200, MinimumLength = 2, ErrorMessage = "Number of letters must be between 2 and 200")]
        public string FacebookLink { get; set; }
    }
}