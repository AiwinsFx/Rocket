using Aiwins.Rocket.AutoMapper;
using AutoMapper;

namespace Aiwins.Rocket.BackgroundJobs {
    public class BackgroundJobsDomainAutoMapperProfile : Profile {
        public BackgroundJobsDomainAutoMapperProfile () {
            CreateMap<BackgroundJobInfo, BackgroundJobRecord> ()
                .ConstructUsing (x => new BackgroundJobRecord (x.Id))
                .Ignore (record => record.ConcurrencyStamp)
                .Ignore (record => record.ExtraProperties);

            CreateMap<BackgroundJobRecord, BackgroundJobInfo> ();
        }
    }
}