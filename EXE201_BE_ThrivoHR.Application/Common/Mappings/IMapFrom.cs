namespace EXE201_BE_ThrivoHR.Application.Common.Mappings
{
    internal interface IMapFrom<T> where T : class
    {
        void Mapping(Profile profile);
    }
}
