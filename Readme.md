# Tyrrrz.AspNetCore.Mvc.Clicky

Tag helper used to render [Clicky](https://clicky.com) activity tracker in ASP.net Core MVC views.

## Download

- Using nuget: `Install-Package Tyrrrz.AspNetCore.Mvc.Clicky`

## Usage

Make the tag helper available with the `addTagHelper` directive either in your view or `_ViewImports.cshtml`.

```
@addTagHelper *, Tyrrrz.AspNetCore.Mvc.Clicky
```

Use the tag helper to render the tracker.
You will need to specify the `site` attribute, which is the site ID that you can find in site preferences on your Clicky dashboard.

```html
<clicky site="075468937" />
```
