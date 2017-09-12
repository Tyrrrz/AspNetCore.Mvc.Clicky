using System;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Tyrrrz.AspNetCore.Mvc.Clicky.TagHelpers
{
    /// <summary>
    /// Tag helper used to render Clicky tracker
    /// </summary>
    public partial class ClickyTagHelper : TagHelper
    {
        /// <summary>
        /// Whether the tag helper is enabled
        /// </summary>
        [HtmlAttributeName("enabled")]
        public bool Enabled { get; set; } = true;

        /// <summary>
        /// Site short name
        /// </summary>
        [HtmlAttributeName("site-id")]
        public string SiteId { get; set; }

        /// <inheritdoc />
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (output == null)
                throw new ArgumentNullException(nameof(output));

            // Validate attributes
            if (string.IsNullOrWhiteSpace(SiteId))
                throw new ArgumentException("SiteId attribute must be set");

            // Return if not enabled
            if (!Enabled)
            {
                output.TagName = null;
                return;
            }

            // Format the content
            var content = TemplateHtml;
            content = content.Replace("__SiteId__", SiteId);

            // Output
            output.TagName = null;
            output.Content.SetHtmlContent(content);

            base.Process(context, output);
        }
    }

    public partial class ClickyTagHelper
    {
        private static readonly string TemplateHtml;

        static ClickyTagHelper()
        {
            TemplateHtml = GetTemplateHtml();
        }

        private static string GetTemplateHtml()
        {
            const string resourcePath = "Tyrrrz.AspNetCore.Mvc.Clicky.Resources.Template.html";

            var assembly = typeof(ClickyTagHelper).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(resourcePath);
            if (stream == null)
                throw new MissingManifestResourceException("Could not find template resource");

            using (stream)
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}