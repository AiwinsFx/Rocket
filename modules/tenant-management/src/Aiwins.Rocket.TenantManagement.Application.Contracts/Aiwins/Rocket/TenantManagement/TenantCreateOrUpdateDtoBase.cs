using System.ComponentModel.DataAnnotations;
using Aiwins.Rocket.ObjectExtending;

namespace Aiwins.Rocket.TenantManagement {
    public abstract class TenantCreateOrUpdateDtoBase : ExtensibleObject {
        [Display (Name = "租户名称")]
        [Required (ErrorMessage = "租户名称为必填字段。")]
        [StringLength (TenantConsts.MaxNameLength, ErrorMessage = "{0}的长度不能超过{1}个字符。")]
        public string Name { get; set; }
    }
}