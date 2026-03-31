using System.Linq;
using Content.Client.Lobby.UI.Roles;
using Content.Client.Stylesheets;
using Content.Shared.Traits;
using Robust.Client.UserInterface.Controls;
using Robust.Shared.Utility;

namespace Content.Client.Lobby.UI;

public sealed partial class HumanoidProfileEditor
{

    /// <summary>
    /// Refreshes traits selector
    /// </summary>
    public void RefreshTraits()
    {
        foreach (var child in TraitsTabContainer.Children.ToList())
        {
            child.Orphan();
            child.Dispose();
        }
        TraitsTabContainer.RemoveAllChildren();

        if (Profile == null) return;

        int totalPointsBalance = 7;
        foreach (var traitId in Profile.TraitPreferences)
        {
            if (_prototypeManager.TryIndex<TraitPrototype>(traitId, out var trait))
            {
                totalPointsBalance -= trait.Cost;
            }
        }

        TotalTraitPointsLabel.Text = Loc.GetString("humanoid-profile-editor-traits-header", ("points", totalPointsBalance));
        TotalTraitPointsLabel.FontColorOverride = totalPointsBalance < 0 ? Color.Red : Color.Cyan;

        var traitGroups = new Dictionary<string, List<TraitPrototype>>();
        var allTraits = _prototypeManager.EnumeratePrototypes<TraitPrototype>().OrderBy(t => Loc.GetString(t.Name));

        foreach (var trait in allTraits)
        {
            if (Profile.Species is { } selectedSpecies &&
                (trait.ExcludedSpecies.Contains(selectedSpecies) ||
                trait.IncludedSpecies.Count > 0 && !trait.IncludedSpecies.Contains(selectedSpecies)))
                continue;

            var catId = trait.Category?.ToString() ?? "Default";
            if (!traitGroups.ContainsKey(catId)) traitGroups[catId] = new List<TraitPrototype>();
            traitGroups[catId].Add(trait);
        }

        foreach (var (categoryId, traits) in traitGroups)
        {
            _prototypeManager.TryIndex<TraitCategoryPrototype>(categoryId, out var categoryProto);
            var categoryName = categoryProto != null ? Loc.GetString(categoryProto.Name) : Loc.GetString("traits-category-default");

            var listContainer = new BoxContainer { Orientation = LayoutOrientation.Vertical, Margin = new Thickness(5) };
            var scroll = new ScrollContainer { VerticalExpand = true };
            scroll.AddChild(listContainer);

            var tabPage = new BoxContainer { Orientation = LayoutOrientation.Vertical, Visible = true };
            tabPage.AddChild(scroll);

            TraitsTabContainer.AddChild(tabPage);
            var tabIndex = TraitsTabContainer.ChildCount - 1;
            TraitsTabContainer.SetTabTitle(tabIndex, categoryName);

            foreach (var trait in traits)
            {
                if (!Profile.TraitPreferences.Contains(trait.ID) && !IsTraitCompatible(trait))
                {
                    continue;
                }

                var selector = new TraitPreferenceSelector(trait);
                selector.Preference = Profile.TraitPreferences.Contains(trait.ID);

                selector.PreferenceChanged += preference =>
                {
                    if (preference)
                        Profile = Profile.WithTraitPreference(trait.ID, _prototypeManager);
                    else
                        Profile = Profile.WithoutTraitPreference(trait.ID, _prototypeManager);

                    SetDirty();
                    RefreshTraits();
                };
                listContainer.AddChild(selector);
            }
        }

        if (TraitsTabContainer.Parent?.Parent is TabContainer mainTabs)
        {
            for (var i = 0; i < mainTabs.ChildCount; i++)
            {
                if (mainTabs.GetChild(i) == TraitsTabContainer.Parent)
                {
                    mainTabs.SetTabTitle(i, Loc.GetString("humanoid-profile-editor-traits-tab"));
                    break;
                }
            }
        }
        UpdateSaveButton();
    }

    public bool IsTraitsBalanceValid()
    {
        if (Profile == null) return true;
        int points = 0;
        foreach (var traitId in Profile.TraitPreferences)
        {
            if (_prototypeManager.TryIndex<TraitPrototype>(traitId, out var trait))
                points -= trait.Cost;
        }
        return points >= 0;
    }

    private bool IsTraitCompatible(TraitPrototype trait)
    {
        if (Profile == null) return true;

        if (trait.Blacklist.Contains(Profile.Species))
        {
            return false;
        }

        foreach (var selectedId in Profile.TraitPreferences)
        {
            if (selectedId == trait.ID) continue;

            if (!_prototypeManager.TryIndex<TraitPrototype>(selectedId, out var selected))
                continue;

            if (trait.Blacklist.Contains(selectedId) || selected.Blacklist.Contains(trait.ID))
                return false;
        }

        return true;
    }
}
