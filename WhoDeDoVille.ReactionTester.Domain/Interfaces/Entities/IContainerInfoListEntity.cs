namespace WhoDeDoVille.ReactionTester.Domain.Interfaces.Entities;

public interface IContainerInfoListEntity
{
    public Dictionary<string, ContainerInfoEntity> ContainerList { get; set; }
}
