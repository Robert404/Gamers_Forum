#pragma checksum "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "26be6c952d2057e2c4d35f0cc4a0af8c1f379dcb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Profile_Index), @"mvc.1.0.view", @"/Views/Profile/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\_ViewImports.cshtml"
using Games_Forum;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\_ViewImports.cshtml"
using Games_Forum.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"26be6c952d2057e2c4d35f0cc4a0af8c1f379dcb", @"/Views/Profile/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a0f8688ff3c5cd1458388e4c8d81707acf7eedf", @"/Views/_ViewImports.cshtml")]
    public class Views_Profile_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Games_Forum.Models.Profile.ProfileListModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
 if (User.IsInRole("Admin"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <div class=""container body-content"">
        <div class=""row sectionHeader"">
            <div class=""sectionHeading"">All Users</div>
        </div>
        <div class=""row"" id=""forumIndexContent"">
            <table class=""table table-hover"" id=""userIndexTable"">
                <thead>
                    <tr>
                        <th>Username</th>
                        <th>Email</th>
                        <th>Id</th>
                    </tr>
                </thead>
                <tbody>
");
#nullable restore
#line 19 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
                     foreach (var profile in Model.Profiles)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr class=\"userRow\">\r\n                            <td>\r\n                                ");
#nullable restore
#line 23 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
                           Write(profile.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 26 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
                           Write(profile.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 29 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
                           Write(profile.UserId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 32 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\r\n            </table>\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 37 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
}
else 
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h3>Only Admin allowed to see all Users on our Forum</h3>\r\n");
#nullable restore
#line 41 "C:\Games_Forum_New\Games_Forum\Games_Forum\Games_Forum\Views\Profile\Index.cshtml"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Games_Forum.Models.Profile.ProfileListModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
