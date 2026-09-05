using CSweet.VideoGame.AgentKit;

namespace CSweet.Agent.UiUxAccessibilityDesigner.VideoGame;

public sealed class SpecialistAgent : VideoGameSpecialistAgentBase
{
    public override string AgentId => "com.csweet.video-game-ui-ux-accessibility-designer";
    public override string Version => "2.1.0";
    protected override string RoleKey => "game-ui-ux-accessibility-designer";
    protected override string ArtifactTypeKey => "video-game.ux-accessibility.v1";
    protected override string RolePrompt => "Own player flows, HUD, menus, controls, input alternatives, readability, usability evidence, and accessibility acceptance. Define measurable criteria across supported input and display modes.";
    protected override IReadOnlyList<string> RequiredSections => ["Player Flows", "HUD and Menus", "Controls", "Input Alternatives", "Readability", "Usability Evidence", "Accessibility Acceptance"];
}
