namespace GymApp.BLL.Contracts;

public interface IMemberService
{
     Task<IEnumerable<MemberViewModel>> GetAllMembersAsync(CancellationToken cancellationToken = default);
     Task<bool> CreateMemberAsync(  MemberCreateViewModel createMemberViewModel,CancellationToken cancellationToken = default);
    Task<MemberDetailsViewModel?> GetMemberDetailsAsync(int id, CancellationToken cancellationToken = default);
    Task<MemberEditViewModel?> GetMemberDetailsForEditAsync(int id, CancellationToken cancellationToken = default);
    Task<HealthRecordViewModel?> GetMemberHealthRecordDetailsAsync(int id, CancellationToken cancellationToken);
    Task<bool> UpdateMemberAsync(int id, MemberEditViewModel memberViewModel, CancellationToken cancellationToken);
    Task<bool> DeleteMemberAsync(int id, CancellationToken cancellationToken);
}

// DTO  is a Data Transfer Object that is used to transfer data between layers of the application.
// It is a simple class that contains properties that represent the data that needs to be transferred.
// In this case, the MemberDTO class is used to transfer member data from the BLL layer to the presentation layer.
//public class MemberDTO
//{
//    public int Id { get; set; }
//    public string Name { get; set; } = string.Empty;
//    public string Email { get; set; } = string.Empty;
//    public DateTime DateOfBirth { get; set; }
//}