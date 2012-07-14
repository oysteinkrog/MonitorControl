using System;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows;
using System.Windows.Markup;

[assembly: AssemblyTitle("Elysium: Theme assembly")]
[assembly: AssemblyDescription("WPF Metro-style theme")]
[assembly: AssemblyProduct("Elysium")]
[assembly: AssemblyCopyright("Copyright © Alex F. Sherman & Codeplex Community 2011-2012")]

[assembly: SecurityRules(SecurityRuleSet.Level2)]
[assembly: AllowPartiallyTrustedCallers]
[assembly: InternalsVisibleTo("Elysium.Notifications.Server, PublicKey=0024000004800000940000000602000000240000525341310004000001000100cd87cb3804debcaa" +
                                                                      "7edc1def98e42610bdbb17e423711a88429dc54feb574284165edd131e30a88193284d73db2720b3" +
                                                                      "7c080b59e27ff3fae0ba2f05b7828a51625b20946f37e260a4c42fc437927550f0237f56b8050104" +
                                                                      "025dfe07d52cb1f0ff6281f3a06b096a3c8b2923726880c5f029ae1e42f8abbff578e516d8b549f9")]
[assembly: InternalsVisibleTo(       "Elysium.Notifications, PublicKey=00240000048000009400000006020000002400005253413100040000010001003ba9b43a7c6d34cd" +
                                                                      "2f1b869efd90a1e8a8b54cc1de82b33ac493b17e4cc4b3fd783fab6dd00d810388ee0c4b13039c1c" +
                                                                      "23bfe5a340395a1ae40205e04a5459299b6c919ac058ae379b1a9fa06e40ca82a321f78e7c297dd1" +
                                                                      "7eddba4e1fa0edaafac51bc146f9c92da3c561197091f9bf84df4d912d8b332dbfaa6c54fe6945ba")]

[assembly: ComVisible(false)]
[assembly: CLSCompliant(true)]

[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Controls")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme", "Elysium.Converters")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme/design", "Elysium.Design")]
[assembly: XmlnsDefinition("http://schemas.codeplex.com/elysium/theme/params", "Elysium.Parameters")]
[assembly: XmlnsPrefix("http://schemas.codeplex.com/elysium/theme", "metro")]
[assembly: XmlnsPrefix("http://schemas.codeplex.com/elysium/theme/design", "design")]
[assembly: XmlnsPrefix("http://schemas.codeplex.com/elysium/theme/params", "params")]

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,
    ResourceDictionaryLocation.SourceAssembly
)]

[assembly: NeutralResourcesLanguage("en-us")]

[assembly: AssemblyVersion("1.5.491.0")]
[assembly: AssemblyFileVersion("1.5.491.0")]