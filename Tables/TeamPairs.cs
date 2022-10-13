#nullable enable
using System;
using System.ComponentModel.DataAnnotations;


namespace ServiceWithHangfire
{
    public class TeamPairs
    {
        [Key]
        public int id { get; set; }
        public int? first_team_id { get; set; }
        public int? second_team_id { get; set; }
    }
}
