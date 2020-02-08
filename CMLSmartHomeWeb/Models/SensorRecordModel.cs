using CMLSmartHomeController.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace CMLSmartHomeWeb.Models
{
    public class SensorRecordModel
    {
        [Required]
        public int CollectorID { get; set; }

        [Required]
        public int SensorID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        public DateTime RecordDatetimeFrom { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy HH:mm}", ApplyFormatInEditMode = false)]
        [DataType(DataType.DateTime)]
        public DateTime? RecordDatetimeTo { get; set; }

        public List<SensorRecord> SensorRecords { get; set; }
    }

}
