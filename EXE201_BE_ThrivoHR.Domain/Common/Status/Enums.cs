namespace EXE201_BE_ThrivoHR.Domain.Common.Status;
public enum TrainingHistory
{
    Completed = 0,
    InProgress = 1,
    Failed = 2,
    NotStarted = 3,

}
public enum FormStatus
{
    Pending = 0,
    Approved = 1,
    Rejected = 2,
}

public enum TaskStatus
{
    NotStarted = 0,
    InProgress = 1,
    Completed = 2,
    Canceled = 3,
}

public enum FormOfAction
{
    Warning = 0,
    Dismissal = 1,
    Promotion = 2,
    Demotion = 3,
    Suspension = 4,
    Bonus = 5,
    Fine = 6,
    SalaryIncrease = 7,

}