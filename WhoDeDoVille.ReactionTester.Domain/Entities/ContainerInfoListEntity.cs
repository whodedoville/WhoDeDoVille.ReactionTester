namespace WhoDeDoVille.ReactionTester.Domain.Entities;

public class ContainerInfoListEntity
{
    public Dictionary<string, ContainerInfoEntity> ContainerList { get; set; } = new Dictionary<string, ContainerInfoEntity>();
}
