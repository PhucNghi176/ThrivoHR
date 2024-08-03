using AutoMapper;

namespace EXE201_BE_ThrivoHR.Application.Common.Mappings
{
    internal interface IMapFrom<T>
    {
        void Mapping(Profile profile);
    }
}
