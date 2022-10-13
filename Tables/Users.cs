#nullable enable
using System;
using System.ComponentModel.DataAnnotations;

namespace ServiceWithHangfire
{
    public class Users
    {
        [Key]
        public int id { get; set; }
        public ulong uuid { get; set; }
        public string? personal_id { get; set; }
        public int? role_id { get; set; }
        public string? name { get; set; }
        public string? email { get; set; }
        public string? password { get; set; }
        public string? location { get; set; }
        public int? office_id { get; set; }
        public string? skype { get; set; }
        public int? broadvoice_ext { get; set; }
        public int? by_voice_quality { get; set; }
        public int? verify_by_phone { get; set; }
        public int? verify_online { get; set; }
        public string? equipment { get; set; }
        public TimeSpan? work_schedule_start { get; set; }
        public TimeSpan? work_schedule_end { get; set; }
        public TimeSpan? work_schedule_start_utc { get; set; }
        public TimeSpan? work_schedule_end_utc { get; set; }
        public string? work_schedule_timezone { get; set; }
        public int? expected_working_time { get; set; }
        public int? work_shift_id { get; set; }
        public DateTime? training_materials_complete { get; set; }
        public DateTime? started_date { get; set; }
        public string? remember_token { get; set; }
        public DateTime? deleted_at { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public int? current_team_id { get; set; }
        public int? small_team_id { get; set; }
        public int? collect_debug { get; set; }
    }
}
