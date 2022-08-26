using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace mbaspnetcore6.CustomTagHelper
{
    /// <summary>
    /// Start and End-Tag
    /// <list-generator></list-generator>
    /// </summary>
    public class ListGenerator : TagHelper
    {

        // Lets define a Data Source over which the List
        // will be geberated
        // <list-generator departments=""></list-generator>
        public List<Department> Departments { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Lets define a Custom Tag for the Tag-Helper
            output.TagName = "List";
            // Lets make sure that the Tag Will have stant and End Tag
            output.TagMode = TagMode.StartTagAndEndTag;

            // Lets write a HTML
            var table = "<table class='table table-bordered table-striped'>";

            foreach (var dept in Departments)
            {
                table += $"<tr><td>{dept.DeptNo}</td><td>{dept.DeptName}</td><td>{dept.Location}</td><td>{dept.Capacity}</td></tr>";
            }
            table += "</table>";

            // Set the Output HTML
            output.PreContent.SetHtmlContent(table);

        }
    }
}

