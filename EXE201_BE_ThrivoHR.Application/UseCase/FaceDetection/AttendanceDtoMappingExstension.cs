using EXE201_BE_ThrivoHR.Domain.Entities;

namespace EXE201_BE_ThrivoHR.Application.UseCase.FaceDetection;

    public static class AttendanceDtoMappingExstension
    {
        public static AttendanceDto MapToAttendanceDto(this Attendance attendance, IMapper mapper)
        {
            return mapper.Map<AttendanceDto>(attendance);
        }
        public static List<AttendanceDto> MapToAttendanceListDto(this IEnumerable<Attendance> attendances, IMapper mapper)
        {
            return attendances.Select(attendance => attendance.MapToAttendanceDto(mapper)).ToList();
        }
    }
