using System.ComponentModel.DataAnnotations;

namespace GatewayService.Models
{
    public class GlobalConfigurationEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string GatewayName { get; set; }

        [StringLength(100)]
        public string RequestIdKey { get; set; }

        [StringLength(100)]
        public string BaseUrl { get; set; }

        [StringLength(50)]
        public string DownstreamScheme { get; set; }

        [StringLength(500)]
        public string ServiceDiscoveryProvider { get; set; }

        [StringLength(500)]
        public string LoadBalancerOptions { get; set; }

        [StringLength(500)]
        public string HttpHandlerOptions { get; set; }

        [StringLength(200)]
        public string QoSOptions { get; set; }

        public int IsDefault { get; set; }

        public int InfoStatus { get; set; }
    }
}
