using System;
using System.Collections.Generic;

namespace Project_API.Data
{
    public partial class labelstatus
    {
        public string? ID { get; set; }
        public string MAC { get; set; } = null!;
        public string? GROUP { get; set; }
        public string? DESCRIPTION { get; set; }
        public string? IMAGE_FILE { get; set; }
        public int? POLL_INTERVAL { get; set; }
        public int? POLL_TIMEOUT { get; set; }
        public int? SCAN_INTERVAL { get; set; }
        public int? BATTERY_STATUS { get; set; }
        public decimal? BATTERY_VOLTAGE { get; set; }
        public string? VARIANT { get; set; }
        public string? FIRMWARE_VERSION { get; set; }
        public string? FIRMWARE_SUBVERSION { get; set; }
        public int? IMAGE_ID { get; set; }
        public int? IMAGE_ID_LOCAL { get; set; }
        public int? DISPLAY_OPTIONS { get; set; }
        public int? LQI { get; set; }
        public int? LQI_RX { get; set; }
        public DateTime? LAST_POLL { get; set; }
        public DateTime? LAST_INFO { get; set; }
        public DateTime? LAST_IMAGE { get; set; }
        public string? BASE_STATION { get; set; }
        public int? SCAN_CHANNELS { get; set; }
        public int? STATUS { get; set; }
        public int? FIRMWARE_STATUS { get; set; }
        public int? BOOT_COUNT { get; set; }
        public int? TEMPERATURE { get; set; }
        public string? LANID { get; set; }
        public string? PANID { get; set; }
    }
}
