/// <summary>
/// Implementing classes can check whether or not an IAsset instance meets a criterion.
/// </summary>
public interface IAssetMatcher 
{
    bool MatchesCriterion(IAsset asset);
}
