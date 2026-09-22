using CrosswiredStudios.VideoGame.AgentKit;
using CSweet.Agent.SDK;
using Microsoft.Extensions.AI;

namespace CSweet.Agent.UiUxAccessibilityDesigner.VideoGame;

public sealed class SpecialistAgent : VideoGameSpecialistAgentBase
{
    internal const int DefaultContextWindowTokens = 128_000;
    internal const int DefaultOutputTokens = 16_000;
    private const int MinimumOutputTokens = 1_000;
    public override string AgentId => "com.csweet.video-game-ui-ux-accessibility-designer";
    public override string Version => "2.3.2";
    protected override AgentConfigurationBuilder Configure(AgentConfigurationBuilder builder) =>
        base.Configure(builder)
            .Number("maxContextWindowTokens", "Maximum context-window tokens", required: true,
                description: "Planning ceiling for UI UX Accessibility Designer model requests; set this no higher than the selected model's real context window.",
                minimum: 16_000, step: 1_000,
                defaultValue: DefaultContextWindowTokens)
            .Number("maxOutputTokens", "Maximum output tokens", required: true,
                description: "Budget for each UI UX Accessibility Designer model response, including reasoning. Set this within the selected model and provider's supported limits.",
                minimum: MinimumOutputTokens, step: 1_000,
                defaultValue: DefaultOutputTokens,
                lessThanFieldKey: "maxContextWindowTokens");

    protected override ChatOptions? ResponseOptions() =>
        new() { MaxOutputTokens = ResolveOutputTokens(Settings) };

    internal static int ResolveOutputTokens(AgentSettings settings)
    {
        var contextWindow = Math.Max(settings.GetInt32("maxContextWindowTokens", DefaultContextWindowTokens),
            MinimumOutputTokens + 1);
        var output = Math.Max(settings.GetInt32("maxOutputTokens", DefaultOutputTokens),
            MinimumOutputTokens);
        return Math.Min(output, contextWindow - 1);
    }
    protected override string RoleKey => "game-ui-ux-accessibility-designer";
    protected override string ArtifactTypeKey => "video-game.ux-accessibility.v1";
    protected override string RolePrompt => "Own player flows, HUD, menus, controls, input alternatives, readability, usability evidence, and accessibility acceptance. Define measurable criteria across supported input and display modes.";
    protected override IReadOnlyList<string> RequiredSections => ["Player Flows", "HUD and Menus", "Controls", "Input Alternatives", "Readability", "Usability Evidence", "Accessibility Acceptance"];
}
