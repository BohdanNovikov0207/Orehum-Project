using Robust.Client.UserInterface.RichText;

namespace Content.Goobstation.UIKit.UserInterface.RichText;

public sealed class ExamineBorderTag : IMarkupTagHandler
{
    public const string TagName = "examineborder";
    [Dependency] private readonly IEntitySystemManager _entitySystemManager = default!;

    public string Name => TagName;
}
