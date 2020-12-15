using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleMap.Libs.Entity;
using GoogleMap.Libs.Setting;

namespace GoogleMap.Models
{
    public class MapModel
    {
        public List<MarkerView> MarkerViews { get; set; } = new List<MarkerView>();
        public SiteConfig SiteConfig { get; set; }

        public string GetIcon(MarkerView marker)
        {
            if (marker.RoleType == Libs.Enums.RoleType.Member || marker.RoleType == Libs.Enums.RoleType.Admin)
            {
                return "http://maps.google.com/mapfiles/ms/icons/red-dot.png";
            }
            else
            {
                return "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png";
            }
        }
    }
}
