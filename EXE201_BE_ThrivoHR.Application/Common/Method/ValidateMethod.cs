namespace EXE201_BE_ThrivoHR.Application.Common.Method;

public static class ValidateMethod
{
    public static async Task<bool> DepartmentExsis(int departmentID,IDepartmentRepository departmentRepository)
    {
        var Exsis = await departmentRepository.FindAsync(x => x.Id == departmentID);
        return Exsis != null;
    }
    //validate position
    public static async Task<bool> PositionExsis(int positionID, IPositionRepository positionRepository)
    {
        var Exsis = await positionRepository.FindAsync(x => x.Id == positionID);
        return Exsis != null;
    }
}
