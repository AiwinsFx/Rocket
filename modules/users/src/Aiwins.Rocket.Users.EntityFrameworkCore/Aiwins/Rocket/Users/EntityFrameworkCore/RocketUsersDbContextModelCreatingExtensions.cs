using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aiwins.Rocket.Users.EntityFrameworkCore {
    public static class RocketUsersDbContextModelCreatingExtensions {
        public static void ConfigureRocketUser<TUser> (this EntityTypeBuilder<TUser> b)
        where TUser : class, IUser {
            b.Property (u => u.TenantId).HasColumnName (nameof (IUser.TenantId));
            b.Property (u => u.UserName).IsRequired ().HasMaxLength (RocketUserConsts.MaxUserNameLength).HasColumnName (nameof (IUser.UserName));
            b.Property (u => u.Name).HasMaxLength (RocketUserConsts.MaxNameLength).HasColumnName (nameof (IUser.Name));
            b.Property (u => u.Surname).HasMaxLength (RocketUserConsts.MaxSurnameLength).HasColumnName (nameof (IUser.Surname));
            b.Property (u => u.Email).IsRequired ().HasMaxLength (RocketUserConsts.MaxEmailLength).HasColumnName (nameof (IUser.Email));
            b.Property (u => u.EmailConfirmed).HasDefaultValue (false).HasColumnName (nameof (IUser.EmailConfirmed));
            b.Property (u => u.PhoneNumber).HasMaxLength (RocketUserConsts.MaxPhoneNumberLength).HasColumnName (nameof (IUser.PhoneNumber));
            b.Property (u => u.PhoneNumberConfirmed).HasDefaultValue (false).HasColumnName (nameof (IUser.PhoneNumberConfirmed));
        }
    }
}