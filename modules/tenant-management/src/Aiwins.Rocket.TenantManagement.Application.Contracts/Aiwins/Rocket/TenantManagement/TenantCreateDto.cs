using System.ComponentModel.DataAnnotations;

namespace Aiwins.Rocket.TenantManagement {
    public class TenantCreateDto : TenantCreateOrUpdateDtoBase {
        [Required (ErrorMessage = "用户机号为必填字段。")]
        [RegularExpression (@"^1[3456789][0-9]{9}$", ErrorMessage = "用户手机号输入不合法。")]
        [MaxLength (64)]
        public virtual string AdminPhoneNumber { get; set; }

        [Required (ErrorMessage = "密码为必填字段。")]
        [MaxLength (128)]
        public virtual string AdminPassword { get; set; }
    }
}