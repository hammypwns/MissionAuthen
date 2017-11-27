using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MissionAuthen.Models
{
    [Table("Mission")]
    public class Mission
    {
        [Key]
        public int MissionId { get; set; }
        [Required, DisplayName("Mission Name"), StringLength(50, MinimumLength =3 , ErrorMessage = "Mission name must be between 3 and 50 characters long."),
            RegularExpression(@"^[A-Z][a-zA-Z.\-\/\\()#\d'"" ]*$", ErrorMessage = "Capitalize mission name. -().#/\\'\" symbols allowed only.")]
        public string MissionName { get; set; } //ensures mission name is capitalized and doesn't have ridiculous symbols
        [Required, DisplayName("Mission President"), StringLength(50, MinimumLength = 3, ErrorMessage = "Mission President must be between 3 and 50 characters long."),
            RegularExpression(@"^[A-Z][a-zA-Z.\- ]*$", ErrorMessage = "Capitalize Mission President. Only alphabetic characters, spaces, periods, and hyphens allowed.")]
        public string MissionPresident { get; set; } //ensures Mission President (name) is capitalized and doesn't have ridiculous symbols
        [DisplayName("Mission Address"), StringLength(50, MinimumLength = 3, ErrorMessage = "Mission address must be between 3 and 50 characters long."),
            RegularExpression(@"^[a-zA-Z.\-\/\\()#\d'"" ]*$", ErrorMessage = "-().#/\\'\" symbols allowed only.")]
        public string MissionAddress { get; set; } //ensures mission address is reasonable length of string and doesn't have ridiculous symbols
        [Required, DisplayName("Mission Language"), StringLength(50, MinimumLength = 3, ErrorMessage = " Language must be between 3 and 50 characters long."),
            RegularExpression(@"^[A-Z][a-zA-Z.\- ]*$", ErrorMessage = "Capitalize language. Only alphabetic characters, spaces, periods, and hyphens allowed.")]
        public string MissionPrimaryLanguage { get; set; } //ensures language is capitalized and doesn't have ridiculous symbols
        [DisplayName("Mission Climate"), StringLength(50, MinimumLength = 3, ErrorMessage = "Climate must be between 3 and 50 characters long."),
            RegularExpression(@"^[A-Z][a-zA-Z.\- ]*$", ErrorMessage = "Capitalize climate. Only alphabetic characters, spaces, periods, and hyphens allowed.")]
        public string MissionClimate { get; set; } //ensures climate is capitalized and doesn't have ridiculous symbols
        [DisplayName("Mission Dominant Religion"), StringLength(50, MinimumLength = 3, ErrorMessage = "Dominant religion must be between 3 and 50 characters long."),
            RegularExpression(@"^[A-Z][a-zA-Z.\- ]*$", ErrorMessage = "Capitalize dominant religion. Only alphabetic characters, spaces, periods, and hyphens allowed.")]
        public string MissionDominantReligion { get; set; } //ensures dominant religion is capitalized and doesn't have ridiculous symbols
        [DisplayName("Mission Flag URL"), StringLength(500, MinimumLength = 5, ErrorMessage = "Flag URL must be between 5 and 500 characters long.")]
        public string MissionFlagURL { get; set; } //url can be 500 characters long
    }
}