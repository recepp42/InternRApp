using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.UniqueConstraint("AK_ApplicationUsers_Email", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ManagerEmails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_ApplicationUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StreetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HouseNumber = table.Column<int>(type: "int", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_ApplicationUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InternShips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    SchoolYear = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MaxStudents = table.Column<byte>(type: "tinyint", nullable: false),
                    CurrentCountOfStudents = table.Column<byte>(type: "tinyint", nullable: false),
                    RequiredTrainingType = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternShips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternShips_ApplicationUsers_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InternShips_Departments_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrefaceTranslations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    UnitId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrefaceTranslations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrefaceTranslations_Departments_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrefaceTranslations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternShipLocation",
                columns: table => new
                {
                    InternShipId = table.Column<int>(type: "int", nullable: false),
                    LocationsId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternShipLocation", x => new { x.InternShipId, x.LocationsId });
                    table.ForeignKey(
                        name: "FK_InternShipLocation_InternShips_InternShipId",
                        column: x => x.InternShipId,
                        principalTable: "InternShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternShipLocation_Locations_LocationsId",
                        column: x => x.LocationsId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Translations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TitleContent = table.Column<string>(type: "nvarchar(170)", maxLength: 170, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    KnowledgeToDevelop = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    NeededKnowledge = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    InternShipId = table.Column<int>(type: "int", nullable: false),
                    LanguageId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Translations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Translations_InternShips_InternShipId",
                        column: x => x.InternShipId,
                        principalTable: "InternShips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Translations_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "Id", "CreatedDate", "Email", "IsDeleted", "LastModifiedDate", "Password", "Role" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(1609), "recep@inetum-realdolmen.world", false, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(1606), "admin123", 0 },
                    { 2, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(1610), "anton@inetum-realdolmen.world", false, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(1610), "admin123", 1 }
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "LastModifiedDate", "ManagerEmails", "Name" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(7732), false, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(7731), "anton.louf@student.ehb.be", "Microsoft Competence Center" },
                    { 2, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(7735), false, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(7735), "anton.louf@student.ehb.be", "Java Competence Center" },
                    { 3, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(7736), false, new DateTime(2023, 6, 5, 11, 9, 56, 199, DateTimeKind.Utc).AddTicks(7736), "anton.louf@student.ehb.be", "Low Code" }
                });

            migrationBuilder.InsertData(
                table: "Languages",
                columns: new[] { "Id", "Code", "CreatedDate", "CreatorId", "IsDeleted", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, "nl", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7554), null, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7553), "Dutch" },
                    { 2, "fr", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7556), null, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7556), "French" },
                    { 3, "en", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7557), null, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7557), "English" },
                    { 4, "de", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7558), null, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(7558), "German" }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "CreatedDate", "CreatorId", "HouseNumber", "IsDeleted", "LastModifiedDate", "StreetName", "ZipCode" },
                values: new object[] { 2, "Gent", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(9499), null, 4, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(9499), "Gaston Crommenlaan", "9050" });

            migrationBuilder.InsertData(
                table: "InternShips",
                columns: new[] { "Id", "CreatedDate", "CreatorId", "CurrentCountOfStudents", "IsDeleted", "LastModifiedDate", "MaxStudents", "RequiredTrainingType", "SchoolYear", "UnitId" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1402), null, (byte)0, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1401), (byte)4, 0, "2023-2024", 1 },
                    { 2, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1404), 2, (byte)0, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1404), (byte)4, 1, "2023-2024", 1 },
                    { 3, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1406), null, (byte)0, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1406), (byte)4, 0, "2023-2024", 2 },
                    { 4, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1407), 2, (byte)0, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(1407), (byte)4, 0, "2023-2024", 3 }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "CreatedDate", "CreatorId", "HouseNumber", "IsDeleted", "LastModifiedDate", "StreetName", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Huizingen", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(9496), 2, 42, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(9495), "Vaucampslaan", "1654" },
                    { 3, "Kontich", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(9501), 2, 26, false, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(9501), "Prins Boudewijnlaan", "2550" }
                });

            migrationBuilder.InsertData(
                table: "PrefaceTranslations",
                columns: new[] { "Id", "Content", "CreatedDate", "IsDeleted", "LanguageId", "LastModifiedDate", "UnitId" },
                values: new object[,]
                {
                    { 1, "<p>Als Inetum-Realdolmen Microsoft Competence Center zijn wij met meer dan 150 medewerkers in Belgi&euml; de grootste Microsoft .NET ontwikkelorganisatie. We zijn o.a. actief in de totaalrealisatie van Microsoft-projecten, zowel in strategie, architectuur, implementatie, integratie van systemen, onderhoud, opleiding als ondersteuning. Onze kennis en ervaring situeert zich vandaag binnen de volgende technologie&euml;n en solution domains</p>\r\n<ul>\r\n	<li>Ontwikkeling van Windows-, web- en smart client-applicaties (ASP.NET Core MVC &amp; WebApi, Azure, Angular, WPF, WinForms,&hellip;)</li>\r\n	<li>Mobility (Universal apps, Xamarin)</li>\r\n	<li>Integration &ndash; EAI (Biztalk &amp; WCF)</li>\r\n	<li>CAD/GIS-integratieprojecten</li>\r\n	<li>Azure Cloud</li>\r\n	<li>Opleiding</li>\r\n</ul>\r\n<p>Binnen onze interne werking dragen we technologie hoog in het vaandel, het is als het ware de grondstof van onze divisie. Daarom lopen er tal van initiatieven om binnen de Microsoft technology stack research te voeren naar al wat nieuw is. En hier komen jullie in the picture! We zijn ervan overtuigd dat jullie ons met al jullie energie, enthousiasme en creativiteit kunnen helpen om deze nieuwe technologie&euml;n te onderzoeken en klaar te stomen voor gebruik in de business-toepassingen van de toekomst. Hieronder vinden jullie een lijst van de technologie&euml;n en/of topics waarrond we volledige opdrachten willen uitwerken in het komende academiejaar. Laat het duidelijk zijn dat de complexiteit van de opdrachten niet van de poes zal zijn, we verwachten dan ook dat je reeds over een degelijke basiskennis beschikt van .NET. Laat dit jullie zeker niet afschrikken maar wij zijn nu eenmaal op zoek naar &ldquo;the best of the best&rdquo;. Ben jij diegene die de business-toepassingen van de toekomst mee vorm wil geven? Aarzel dan niet om een stageplaats bij de Realdolmen Microsoft Divisie aan te vragen.</p> <p> Naast de vermelde opdrachten vanuit onze Applications Microsoft divisie zijn we ook actief binnen Enterprise Solutions met Microsoft (Sharepoint, CRM, Business Intelligence). Ook daar zijn er boeiende opdrachten. Realdolmen hecht een groot belang aan een constante flow van innovatie, optimalisatie en uitbreiding van kennis. De instroom aan creatieve idee&euml;n over het integreren of bestuderen van nieuwe technologie&euml;n is allerminst gering. Dit is het punt waar nieuwe en uitdagende opportuniteiten liggen voor jullie. Via een grote waaier aan stageopdrachten geven we jullie de kans om binnen het Realdolmen Microsoft Competence Center deze nieuwe idee&euml;n uit te werken en ons te overtuigen of dit al dan niet een meerwaarde kan betekenen binnen ons huidig aanbod. Ben je ge&iuml;nteresseerd en ga je geen uitdaging uit de weg, wil je je verdiepen in een van de meest uitgebreide technology stacks op dit moment, en ben je bedreven in Microsoft .NET development? Dan is nu het moment om je kans te grijpen! Overtuig ons van je enthousiasme, technische en analytische skills tijdens het uitwerken van een van de vele projecten.</p> \r\n<p>&nbsp;</p><p><strong>Contacteer ons via</strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>om jouw stageplaats aan te vragen</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(883), false, 1, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(882), 1 },
                    { 2, "<p>En tant que centre de comp&eacute;tence Microsoft d&#39;Inetum-Realdolmen, nous sommes la plus grande organisation de d&eacute;veloppement Microsoft .NET en Belgique, avec plus de 150 employ&eacute;s. Nous sommes actifs dans la r&eacute;alisation compl&egrave;te de projets Microsoft, notamment dans les domaines de la strat&eacute;gie, de l&#39;architecture, de la mise en &oelig;uvre, de l&#39;int&eacute;gration de syst&egrave;mes, de la maintenance, de la formation et du support. Nos connaissances et notre exp&eacute;rience se situent actuellement dans les domaines et technologies suivants :</p><ul><li>D&eacute;veloppement d&#39;applications Windows, web et client intelligent (ASP.NET Core MVC &amp; WebApi, Azure, Angular, WPF, WinForms, ...)</li><li>Mobilit&eacute; (applications universelles, Xamarin)</li><li>Int&eacute;gration - EAI (Biztalk &amp; WCF)</li><li>Projets d&#39;int&eacute;gration CAD/GIS</li><li>Cloud Azure</li><li>Formation</li></ul><p>Au sein de notre organisation interne, nous accordons une grande importance &agrave; la technologie, qui est en quelque sorte la mati&egrave;re premi&egrave;re de notre division. C&#39;est pourquoi de nombreuses initiatives sont en cours pour mener des recherches sur toutes les nouveaut&eacute;s de l&#39;&eacute;cosyst&egrave;me technologique Microsoft. Et c&#39;est l&agrave; que vous intervenez ! Nous sommes convaincus que vous pouvez nous aider, avec toute votre &eacute;nergie, votre enthousiasme et votre cr&eacute;ativit&eacute;, &agrave; explorer ces nouvelles technologies et &agrave; les pr&eacute;parer pour une utilisation dans les applications m&eacute;tier du futur. Ci-dessous, vous trouverez une liste des technologies et/ou sujets sur lesquels nous souhaitons d&eacute;velopper des missions compl&egrave;tes au cours de l&#39;ann&eacute;e acad&eacute;mique &agrave; venir.</p><p>Il est clair que la complexit&eacute; des missions ne sera pas facile, nous nous attendons donc &agrave; ce que vous ayez d&eacute;j&agrave; une solide connaissance de base de .NET. Ne vous laissez pas d&eacute;courager, car nous sommes &agrave; la recherche du &quot;meilleur parmi les meilleurs&quot;. &Ecirc;tes-vous pr&ecirc;t &agrave; contribuer &agrave; fa&ccedil;onner les applications m&eacute;tier de l&#39;avenir ? N&#39;h&eacute;sitez pas &agrave; demander un stage au sein de la division Microsoft de Realdolmen. En plus des missions mentionn&eacute;es par notre division Applications Microsoft, nous sommes &eacute;galement actifs dans les Solutions Entreprise avec Microsoft (Sharepoint, CRM, Business Intelligence). L&agrave; aussi, des missions passionnantes sont disponibles.</p><p>Realdolmen attache une grande importance &agrave; un flux constant d&#39;innovation, d&#39;optimisation et d&#39;expansion des connaissances. Nous ne manquons pas de nouvelles id&eacute;es cr&eacute;atives pour int&eacute;grer ou &eacute;tudier de nouvelles technologies. C&#39;est l&agrave; que r&eacute;sident de nouvelles opportunit&eacute;s stimulantes pour vous. &Agrave; travers une large gamme de missions de stage, nous vous offrons la possibilit&eacute; de d&eacute;velopper ces nouvelles id&eacute;es au sein du Centre de Comp&eacute;tence Microsoft de Realdolmen et de nous convaincre de leur valeur ajout&eacute;e pour notre offre actuelle.</p><p>Si vous &ecirc;tes int&eacute;ress&eacute; et que vous n&#39;avez pas peur des d&eacute;fis, si vous souhaitez vous plonger dans l&#39;une des piles technologiques les plus &eacute;tendues &agrave; l&#39;heure actuelle et si vous ma&icirc;trisez le d&eacute;veloppement Microsoft .NET, c&#39;est le moment de saisir votre chance ! Convainquez-nous de votre enthousiasme, de vos comp&eacute;tences techniques et analytiques en travaillant sur l&#39;un des nombreux projets</p><p>&nbsp;</p><p><strong>Contactez nous a </strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>pour demander votre stage</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(886), false, 2, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(885), 1 },
                    { 3, "<p>As the Inetum-Realdolmen Microsoft Competence Center, we are the largest Microsoft .NET development organization in Belgium, with over 150 employees. We are involved in the complete realization of Microsoft projects, including strategy, architecture, implementation, system integration, maintenance, training, and support. Our knowledge and experience are focused on the following technologies and solution domains:</p><ul><li>Development of Windows, web, and smart client applications (ASP.NET Core MVC &amp; WebApi, Azure, Angular, WPF, WinForms, ...)</li><li>Mobility (Universal apps, Xamarin)</li><li>Integration - EAI (Biztalk &amp; WCF)</li><li>CAD/GIS integration projects</li><li>Azure Cloud</li><li>Training</li></ul><p>Within our internal operations, we place a high emphasis on technology, as it is the cornerstone of our division. That&#39;s why we have numerous initiatives to conduct research on everything new within the Microsoft technology stack. And this is where you come into the picture! We are confident that with all your energy, enthusiasm, and creativity, you can help us explore these new technologies and prepare them for use in future business applications. Below you will find a list of technologies and/or topics for which we want to develop complete assignments in the upcoming academic year.</p><p>Let it be clear that the complexity of the assignments will not be easy. We expect you to already have a solid basic knowledge of .NET. However, don&#39;t let this discourage you, as we are looking for the best of the best. Are you the one who wants to shape the business applications of the future? Don&#39;t hesitate to apply for an internship at the Realdolmen Microsoft Division. In addition to the mentioned assignments from our Applications Microsoft division, we are also active in Enterprise Solutions with Microsoft (SharePoint, CRM, Business Intelligence), where exciting projects are available.</p><p>Realdolmen attaches great importance to a constant flow of innovation, optimization, and expansion of knowledge. The influx of creative ideas for integrating or studying new technologies is substantial. This is where new and challenging opportunities lie for you. Through a wide range of internship assignments, we give you the opportunity to work on these new ideas within the Realdolmen Microsoft Competence Center and convince us whether or not they can add value to our current offerings.</p><p>If you are interested and ready to take on challenges, if you want to delve into one of the most extensive technology stacks at the moment, and if you are proficient in Microsoft .NET development, then seize the opportunity now! Convince us of your enthusiasm, technical skills, and analytical abilities while working on one of the many projects.</p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(887), false, 3, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(887), 1 },
                    { 4, "<p>Het Inetum-Realdolmen Java Competence Center is met meer dan 150 Java consultants het grootste onafhankelijke Java expertisecentrum op het Vlaams/Belgisch grondgebied. Naast zijn jarenlange ervaring in de sector, beschikt het over een technologische knowhow die gerust als uniek beschouwd mag worden. Binnen Inetum-Realdolmen noemen we dit expertisecentrum niet zomaar onze &ldquo;Java Community&rdquo;. Onze gepassioneerde Java professionals geven dagelijks het beste van zichzelf om projecten bij klanten op tijd en binnen het overeengekomen budget op te leveren. Java is een technologie die van meet af aan opgezet is als een open technologie. &ldquo;Open&rdquo; wil zeggen dat technologische evoluties gedreven worden vanuit communities. Hierin zijn zowel mensen uit de industrie als leveranciers vertegenwoordigd. De vele innovaties die gegroeid zijn uit deze communities, hebben voor een groot deel het IT-landschap gevormd tot wat het vandaag is. Een greep uit de expertise van het Java Competence Center omvat technologie&euml;n en methodologie&euml;n zoals:</p><ul><li>Java Enterprise Edition<ul><li>Java Persistence API (JPA) + Hibernate / Eclipselink</li><li>Java Server Faces (JSF) + PrimeFaces o Enterprise Java Beans (EJB</li><li>Context and Dependency Injection (CDI)</li></ul></li><li>Spring (Boot)</li><li>Web services (REST / SOAP) / Service-Oriented Architecture (SOA)</li><li>HTML5 / CSS3</li><li>JavaScript / jQuery / Node.js / TypeScript / Angular / React</li><li>Android / iOS / Cordova / PhoneGap</li><li>Agile / Scrum / Test Driven Development / Behavior Driven Development</li><li>User Experience (UX) / Quality Assurance (QA)</li></ul><p>Inetum-Realdolmen hecht een groot belang aan een constante flow van innovatie, optimalisatie en uitbreiding van kennis. De instroom aan creatieve idee&euml;n over het integreren of bestuderen van nieuwe technologie&euml;n is allerminst gering. Dit is het punt waar nieuwe en uitdagende opportuniteiten liggen voor jullie. Via een grote waaier aan stageopdrachten geven we jullie de kans om binnen het Inetum Realdolmen Java Competence Center deze nieuwe idee&euml;n uit te werken en ons te overtuigen of dit al dan niet een meerwaarde kan betekenen binnen ons huidig aanbod. Ben je ge&iuml;nteresseerd en ga je geen uitdaging uit de weg, wil je je verdiepen in een van de meest uitgebreide technology stacks op dit moment, en ben je bedreven in Java? Dan is nu het moment om je kans te grijpen! Overtuig ons van je enthousiasme, technische en analytische skills tijdens het uitwerken van een van de vele projecten.</p><p><strong>Contacteer ons via</strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>om jouw stageplaats aan te vragen</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(888), false, 1, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(887), 2 },
                    { 5, "<p>Le centre de comp&eacute;tences Java d&#39;Inetum-Realdolmen est le plus grand centre d&#39;expertise Java ind&eacute;pendant en Flandre/Belgique, avec plus de 150 consultants Java. En plus de sa longue exp&eacute;rience dans le secteur, il poss&egrave;de un savoir-faire technologique qui peut &ecirc;tre consid&eacute;r&eacute; comme unique. Chez Inetum-Realdolmen, nous ne d&eacute;signons pas ce centre d&#39;expertise simplement comme notre &quot;Communaut&eacute; Java&quot;. Nos professionnels Java passionn&eacute;s donnent chaque jour le meilleur d&#39;eux-m&ecirc;mes pour livrer des projets chez les clients dans les d&eacute;lais et le budget convenus.</p><p>Java est une technologie qui a &eacute;t&eacute; con&ccedil;ue d&egrave;s le d&eacute;part comme une technologie ouverte. &quot;Ouverte&quot; signifie que les &eacute;volutions technologiques sont pilot&eacute;es par des communaut&eacute;s, comprenant &agrave; la fois des personnes de l&#39;industrie et des fournisseurs. Les nombreuses innovations qui ont &eacute;merg&eacute; de ces communaut&eacute;s ont largement fa&ccedil;onn&eacute; le paysage informatique tel qu&#39;il est aujourd&#39;hui.</p><p>Une s&eacute;lection de l&#39;expertise du Centre de comp&eacute;tences Java comprend des technologies et des m&eacute;thodologies telles que :</p><ul><li>Java Enterprise Edition<ul><li>Java Persistence API (JPA) + Hibernate / Eclipselink</li><li>Java Server Faces (JSF) + PrimeFaces</li><li>Enterprise Java Beans (EJB)</li><li>Context and Dependency Injection (CDI)</li></ul></li><li>Spring (Boot)</li><li>Services Web (REST / SOAP) / Architecture Orient&eacute;e Services (SOA)</li><li>HTML5 / CSS3</li><li>JavaScript / jQuery / Node.js / TypeScript / Angular / React</li><li>Android / iOS / Cordova / PhoneGap</li><li>Agile / Scrum / D&eacute;veloppement Pilot&eacute; par les Tests / D&eacute;veloppement Pilot&eacute; par le Comportement</li><li>Exp&eacute;rience Utilisateur (UX) / Assurance Qualit&eacute; (QA)</li></ul><p>Inetum-Realdolmen attache une grande importance &agrave; un flux constant d&#39;innovation, d&#39;optimisation et d&#39;extension des connaissances. L&#39;afflux d&#39;id&eacute;es cr&eacute;atives pour int&eacute;grer ou &eacute;tudier de nouvelles technologies est loin d&#39;&ecirc;tre n&eacute;gligeable. C&#39;est l&agrave; que se trouvent de nouvelles opportunit&eacute;s stimulantes pour vous. &Agrave; travers une large gamme de stages, nous vous offrons la possibilit&eacute; de d&eacute;velopper ces nouvelles id&eacute;es au sein du Centre de comp&eacute;tences Java d&#39;Inetum-Realdolmen et de nous convaincre de leur valeur ajout&eacute;e pour notre offre actuelle.</p><p>Si vous &ecirc;tes int&eacute;ress&eacute; et que vous n&#39;avez pas peur des d&eacute;fis, si vous souhaitez vous plonger dans l&#39;une des piles technologiques les plus &eacute;tendues du moment et que vous ma&icirc;trisez Java, c&#39;est le moment de saisir votre chance ! Impressionnez-nous avec votre enthousiasme, vos comp&eacute;tences techniques et analytiques en travaillant sur l&#39;un des nombreux projets.</p><p>&nbsp;</p><p><strong>Contactez nous a </strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>pour demander votre stage</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(888), false, 2, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(888), 2 },
                    { 6, "<p>The Java Competence Center of Inetum-Realdolmen is the largest independent Java expertise center in Flanders/Belgium, with over 150 Java consultants. In addition to its extensive experience in the industry, it possesses technological expertise that can be considered unique. At Inetum-Realdolmen, we don&#39;t simply refer to this expertise center as our &quot;Java Community.&quot; Our passionate Java professionals give their best every day to deliver projects to clients on time and within the agreed budget.</p><p>Java is a technology that has been designed from the beginning as an open technology. &quot;Open&quot; means that technological advancements are driven by communities, including both industry professionals and suppliers. The many innovations that have emerged from these communities have greatly shaped the IT landscape as it is today.</p><p>A selection of the expertise of the Java Competence Center includes technologies and methodologies such as:</p><ul><li>Java Enterprise Edition<ul><li>Java Persistence API (JPA) + Hibernate / Eclipselink</li><li>Java Server Faces (JSF) + PrimeFaces</li><li>Enterprise Java Beans (EJB)</li><li>Context and Dependency Injection (CDI)</li></ul></li><li>Spring (Boot)</li><li>Web services (REST / SOAP) / Service-Oriented Architecture (SOA)</li><li>HTML5 / CSS3</li><li>JavaScript / jQuery / Node.js / TypeScript / Angular / React</li><li>Android / iOS / Cordova / PhoneGap</li><li>Agile / Scrum / Test Driven Development / Behavior Driven Development</li><li>User Experience (UX) / Quality Assurance (QA)</li></ul><p>Inetum-Realdolmen places great importance on a constant flow of innovation, optimization, and knowledge expansion. The influx of creative ideas for integrating or studying new technologies is significant. This is where new and challenging opportunities lie for you. Through a wide range of internships, we offer you the opportunity to develop these new ideas within the Inetum-Realdolmen Java Competence Center and convince us of their added value to our current offerings.</p><p>If you are interested and unafraid of challenges, if you want to immerse yourself in one of the most extensive technology stacks at the moment, and if you have proficiency in Java, now is the time to seize your chance! Impress us with your enthusiasm, technical skills, and analytical abilities while working on one of the many projects.</p><p>&nbsp;</p><p><strong>Contact us via</strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>to request an internship</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(889), false, 3, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(889), 2 },
                    { 7, "<p>Naast ontwikkelingen in High Code (.NET, Java, ...) werkt Inetum-Realdolmen ook aan Enterprise-projecten in Low Code en dat in verschillende omgevingen: Microsoft Power-Platform, Salesforce, Mendix, Appian en Outsystems.</p><p><strong>Contacteer ons via</strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>om jouw stageplaats aan te vragen</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(890), false, 1, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(890), 3 },
                    { 8, "<p>En plus des d&eacute;veloppements en High Code (.NET, Java, ...), Inetum-Realdolmen travaille &eacute;galement sur des projets d&#39;entreprise en Low Code et ce, dans divers environnements : Microsoft Power-Platform, Salesforce, Mendix, Appian et Outsystems.</p><p><strong>Contactez nous a </strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>pour demander votre stage</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(891), false, 2, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(890), 3 },
                    { 9, "<p>In addition to developments in High Code (.NET, Java, ...), Inetum-Realdolmen is also working on Enterprise projects in Low Code and that in various environments: Microsoft Power-Platform, Salesforce, Mendix, Appian and Outsystems.</p><p><strong>Contact us via</strong><a href=\"mailto: internship@inetum-realdolmen.world\"> internship@inetum-realdolmen.world</a> <strong>to request an internship</strong></p>", new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(891), false, 3, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(891), 3 }
                });

            migrationBuilder.InsertData(
                table: "InternShipLocation",
                columns: new[] { "InternShipId", "LocationsId", "CreatedDate", "LastModifiedDate" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9888), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9888) },
                    { 1, 2, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9928), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9927) },
                    { 1, 3, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9937), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9937) },
                    { 2, 1, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9944), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9944) },
                    { 2, 2, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9950), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9950) },
                    { 2, 3, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9988), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9988) },
                    { 3, 1, new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9997), new DateTime(2023, 6, 5, 11, 9, 56, 202, DateTimeKind.Utc).AddTicks(9997) },
                    { 3, 2, new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(4), new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(4) },
                    { 3, 3, new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(10), new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(10) },
                    { 4, 1, new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(18), new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(18) },
                    { 4, 2, new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(25), new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(25) },
                    { 4, 3, new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(32), new DateTime(2023, 6, 5, 11, 9, 56, 203, DateTimeKind.Utc).AddTicks(31) }
                });

            migrationBuilder.InsertData(
                table: "Translations",
                columns: new[] { "Id", "Comment", "CreatedDate", "Description", "InternShipId", "IsDeleted", "KnowledgeToDevelop", "LanguageId", "LastModifiedDate", "Location", "NeededKnowledge", "TitleContent" },
                values: new object[,]
                {
                    { 1, "<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3290), "<p>In de presalesfase van een klantproject is d&#39; het belangrijk om de risicos van het softwareproject te detecteren (risicos moeten voor de start van het project worden verminderd). Er zijn ook enkele vereisten met betrekking tot methodologie, gereedschap en technologie voordat een project kan worden uitgevoerd in ons DevOps-fabrieksmodel. Om hier een duidelijk beeld van te krijgen, moet een biedarchitect/-manager in de presalesfase een paar vragen beantwoorden (+/- 50, tegenwoordig in een Excel-bestand) die resulteren in een projecttype (type 1, 2 of 3). Als het resultaat type 3 is, kan het project niet worden ingevoerd in ons DevOps Factory-leveringsmodel. We willen een applicatie bouwen ter vervanging van ons Excel-bestand. Een niet-uitputtende lijst van vereisten om dit te realiseren omvat:</p>", 1, false, "<ul>\r\n<li>Het ontwerpen en ontwikkelen van een schaalbare applicatie</li>\r\n<li>Het ontwerpen van een gebruiksvriendelijke applicatie</li>\r\n<li>Het ontwikkelingsproces van een applicatie\r\n<ul>\r\n    <li>Samenwerken met een Product Owner (coach/management DevOps Factory)</li>\r\n    <li>Functionele analyses</li>\r\n    <li>Technische analyses en API-ontwerp</li>\r\n    <li>Ontwikkeling van .NET Core-webapplicaties</li>\r\n    <li>Testen</li>\r\n    <li>Documentatie</li>", 1, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3290), null, "<ul>\r\n<li>In staat zijn om zelfstandig te werken en problemen op te lossen onder wekelijkse begeleiding.</li>\r\n<li>Kennis van C#</li>\r\n<li>Achtergrondkennis of begrip van de backend (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>\r\n<li>Achtergrondkennis of begrip van de frontend (ASP.NET MVV, Blazor)</li>\r\n<li>Vaardigheden in webontwikkeling (HTML, CSS, JavaScript)</li>\r\n<li>Open-minded en leergierig</li>\r\n<li>Positieve instelling en bereidheid om aan te pakken</li>\r\n</ul>", "Devops Factory – Project evaluation tool" },
                    { 2, "<p>Il est requis de se rendre au bureau une journee par semaine.</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3293), "<p>Dans la phase de pr&eacute;vente d&#39;un projet client, il est important de d&eacute;tecter les risques du projet logiciel (les risques doivent &ecirc;tre att&eacute;nu&eacute;s avant le d&eacute;but du projet). Il existe &eacute;galement des exigences en mati&egrave;re de m&eacute;thodologie, d&#39;outils et de technologie avant qu&#39;un projet puisse &ecirc;tre ex&eacute;cut&eacute; dans notre usine DevOps.<br />\r\nPour avoir une vision claire de cela, dans la phase de pr&eacute;vente, un architecte/gestionnaire d&#39;offres doit r&eacute;pondre &agrave; quelques questions (+/- 50, actuellement dans un fichier Excel) qui donnent lieu &agrave; un &quot;type de projet&quot; (type 1, 2 ou 3). Si le r&eacute;sultat est de type 3, le projet ne peut pas &ecirc;tre int&eacute;gr&eacute; &agrave; notre mod&egrave;le de livraison de l&#39;usine DevOps. Nous souhaitons construire une application pour remplacer notre fichier Excel. Voici une liste non exhaustive des exigences :</p>", 1, false, "<ul>\r\n<li>Conception et d&eacute;veloppement d&#39;une application &eacute;volutive</li>\r\n<li>Conception d&#39;une application conviviale</li>\r\n<li>Processus de d&eacute;veloppement d&#39;une application\r\n<ul>\r\n    <li>Collaboration avec un propri&eacute;taire de produit (coach/gestionnaire DevOps Factory)</li>\r\n    <li>Analyses fonctionnelles</li>\r\n    <li>Analyses techniques et conception d&#39;API</li>\r\n    <li>D&eacute;veloppement d&#39;applications Web avec .NET Core</li>\r\n    <li>Tests</li>\r\n    <li>Documentation</li>", 2, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3293), null, "<ul>\r\n<li>&Ecirc;tre capable de travailler et de r&eacute;soudre des probl&egrave;mes de mani&egrave;re autonome sous une supervision hebdomadaire.</li>\r\n<li>Connaissance du langage C#</li>\r\n<li>Connaissances ou notions en back-end (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>\r\n<li>Connaissances ou notions en front-end (ASP.NET MVV, Blazor) ▪ Comp&eacute;tences en d&eacute;veloppement web (HTML, CSS, JavaScript)</li>\r\n<li>Ouverture d&#39;esprit et d&eacute;sir d&#39;apprendre</li>\r\n<li>Attitude positive et proactive</li>\r\n</ul>", "Devops Factory – Project evaluation tool" },
                    { 3, "<p>It is required to come to office once a week</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3294), "<p>In the presales phase of a customer project, it&rsquo;s important to detect the risks of the software project (risks needs to be mitigated before the start of the project). There are also some requirements about methodology, tooling &amp; technology before a project can be executed in our DevOps factory. To have a clear view on this, in presales phase, a bid architect/manager needs to answer a few questions (+/- 50, nowadays in an Excel file) which results in a &lsquo;project type&rsquo; (type 1, 2 or 3). If the result will be type 3, the project cannot be onboarded in our DevOps Factory delivery model. We want to build an application as a replacement of our Excel file. A non-exhaustive list of requirements:</p>", 1, false, "<ul>\r\n<li>Designing and developing a scalable application</li>\r\n<li>Designing a user-friendly application</li>\r\n<li>Development process of an application \r\n<ul>\r\n    <li>Working together with a Product Owner (coach/management DevOps Factory)</li>\r\n    <li>Functional analyses</li>\r\n    <li>Technical analyses and API Desig</li>\r\n    <li>Development van .NET Core Web applications</li>\r\n    <li>Testing</li>\r\n    <li>Documentation</li>\r\n</ul>\r\n</li>\r\n<li>Cloud computing with Azure (hosting &amp; database)</li>\r\n<li>Entity Framework Core</li>\r\n<li>ASP .NET Core Web API</li>\r\n<li>ASP .NET MVC, Blazor</li>\r\n</ul>", 3, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3294), null, "<ul>\r\n<li>Being able to work and solve problems independently under weekly guidance.</li>\r\n<li>C# knowledge</li>\r\n<li>Back-end knowledge or notions (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture,...)</li>\r\n<li>Front-end knowledge or notions (ASP.NET MVV, Blazor)</li>\r\n<li>Web development skills (HTML, CSS, JavaScript)</li>\r\n<li>Open minded and eager to learn</li>\r\n<li>Can do attitude</li>\r\n</ul>", "Devops Factory – Project evaluation tool" },
                    { 4, "<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3295), "<p>Inetum-Realdolmen is een groot bedrijf, bestaande uit verschillende afdelingen (Microsoft, Java, etc.). Elke afdeling is verantwoordelijk voor het opzetten van haar stageopdrachten en het vinden van de bijbehorende stagebegeleiders. Uiteindelijk moeten deze stageopdrachten worden doorgegeven aan het schoolco&ouml;rdinatieteam, dat verantwoordelijk is voor de samenwerking met verschillende hogescholen/universiteiten en deze stageopdrachten zal verdelen. Op dit moment worden deze stageopdrachten per afdeling ingevuld met behulp van een Word-sjabloon en per e-mail verzonden naar het schoolco&ouml;rdinatieteam. Dit zorgt voor administratieve lasten en we willen een applicatie ontwikkelen ter vervanging van dit Word-document.</p>", 2, false, "<ul>\r\n<li>Het ontwerpen en ontwikkelen van een schaalbare applicatie</li>\r\n<li>Het ontwerpen van een gebruiksvriendelijke applicatie</li>\r\n<li>Het ontwikkelingsproces van een applicatie\r\n<ul>\r\n    <li>Samenwerken met een Product Owner (coach/management DevOps Factory)</li>\r\n    <li>Functionele analyses</li>\r\n    <li>Technische analyses en API-ontwerp</li>\r\n    <li>Ontwikkeling van .NET Core-webapplicaties</li>\r\n    <li>Testen</li>\r\n    <li>Documentatie</li>", 1, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3295), null, "<ul>\r\n<li>In staat zijn om zelfstandig te werken en problemen op te lossen onder wekelijkse begeleiding.</li>\r\n<li>Kennis van C#</li>\r\n<li>Achtergrondkennis of begrip van de backend (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>\r\n<li>Achtergrondkennis of begrip van de frontend (ASP.NET MVV, Blazor)</li>\r\n<li>Vaardigheden in webontwikkeling (HTML, CSS, JavaScript)</li>\r\n<li>Open-minded en leergierig</li>\r\n<li>Positieve instelling en bereidheid om aan te pakken</li>\r\n</ul>", "InternRApp" },
                    { 5, "<p>Il est requis de se rendre au bureau une journee par semaine.</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3296), "<p>Inetum-Realdolmen est une grande entreprise, compos&eacute;e de plusieurs d&eacute;partements (Microsoft, Java, etc.). Chaque d&eacute;partement est responsable de la mise en place de ses missions de stage et de la recherche des superviseurs correspondants. Finalement, ces missions de stage doivent &ecirc;tre transmises &agrave; l&#39;&eacute;quipe de coordination scolaire, qui est charg&eacute;e de la collaboration avec les diff&eacute;rentes &eacute;coles/universit&eacute;s et se chargera de distribuer ces missions de stage. Actuellement, ces missions de stage sont remplies par d&eacute;partement &agrave; l&#39;aide d&#39;un mod&egrave;le Word et envoy&eacute;es par e-mail &agrave; l&#39;&eacute;quipe de coordination scolaire. Cela cr&eacute;e une charge administrative et nous souhaitons d&eacute;velopper une application en remplacement de ce document Word.</p>", 2, false, "<ul>\r\n<li>Conception et d&eacute;veloppement d&#39;une application &eacute;volutive</li>\r\n<li>Conception d&#39;une application conviviale</li>\r\n<li>Processus de d&eacute;veloppement d&#39;une application\r\n<ul>\r\n    <li>Collaboration avec un propri&eacute;taire de produit (coach/gestionnaire DevOps Factory)</li>\r\n    <li>Analyses fonctionnelles</li>\r\n    <li>Analyses techniques et conception d&#39;API</li>\r\n    <li>D&eacute;veloppement d&#39;applications Web avec .NET Core</li>\r\n    <li>Tests</li>\r\n    <li>Documentation</li>", 2, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3296), null, "<ul>\r\n<li>&Ecirc;tre capable de travailler et de r&eacute;soudre des probl&egrave;mes de mani&egrave;re autonome sous une supervision hebdomadaire.</li>\r\n<li>Connaissance du langage C#</li>\r\n<li>Connaissances ou notions en back-end (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>\r\n<li>Connaissances ou notions en front-end (ASP.NET MVV, Blazor) ▪ Comp&eacute;tences en d&eacute;veloppement web (HTML, CSS, JavaScript)</li>\r\n<li>Ouverture d&#39;esprit et d&eacute;sir d&#39;apprendre</li>\r\n<li>Attitude positive et proactive</li>\r\n</ul>", "InternRApp" },
                    { 6, "<p>It is required to come to office once a week</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3297), "<p>Inetum-Realdolmen is a large company, consisting of several departments (Microsoft, \r\nJava, etc.). Each department is responsible for setting up its internship assignments \r\nand finding the corresponding internship supervisors. Eventually, these internship \r\nassignments must be passed on to the school coordination team, which is responsible \r\nfor the collaboration with the various colleges/universities and will distribute these \r\ninternship assignments. At the moment, these internship assignments are filled out \r\nper department using a Word template and sent by email to the school coordination \r\nteam. This creates an administrative burden and we want to build an application as a \r\nreplacement of this Word document.<p>", 2, false, "<ul>\r\n<li>Designing and developing a scalable application</li>\r\n<li>Designing a user-friendly application</li>\r\n<li>Development process of an application \r\n<ul>\r\n    <li>Working together with a Product Owner (coach/management DevOps Factory)</li>\r\n    <li>Functional analyses</li>\r\n    <li>Technical analyses and API Desig</li>\r\n    <li>Development van .NET Core Web applications</li>\r\n    <li>Testing</li>\r\n    <li>Documentation</li>", 3, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3296), null, "<ul>\r\n<li>Being able to work and solve problems independently under weekly guidance.</li>\r\n<li>C# knowledge</li>\r\n<li>Back-end knowledge or notions (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture,...)</li>\r\n<li>Front-end knowledge or notions (ASP.NET MVV, Blazor)</li>\r\n<li>Web development skills (HTML, CSS, JavaScript)</li>\r\n<li>Open minded and eager to learn</li>\r\n<li>Can do attitude</li>\r\n</ul>", "InternRApp" },
                    { 7, "<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3298), "<p>In de Inetum-Realdolmen Project Factory (PF) ontwikkelen developers van Inetum Realdolmen software in opdracht van een klant. Al het development gebeurd in house, met de tooling van Inetum-Realdolmen. De klant krijgt dus zijn deliverable op&nbsp;bepaalde tijdstippen en hoeft geen volledig development team te hebben.&nbsp;</p><p>Om onze PF te ondersteunen, hebben enkele collega&rsquo;s een bibliotheek opgericht. Hier&nbsp;kunnen onze developers boeken uitlenen. Momenteel wordt het uitleen-gedrag&nbsp;bijgehouden op een blad papier. Alsook aanvragen om nieuwe boeken aan te kopen&nbsp;worden op papier bijgehouden. Dit is een bewuste keuze. Als we het uitleen-gedrag&nbsp;digitaliseren op basis van bestaande tools (Google Drive, SharePoint...) zorgt dit voor&nbsp;extra overhead, dit be&iuml;nvloedt de toegankelijkheid van de bibliotheek.</p>", 3, false, "<p><u>Niet technisch</u>&nbsp;</p>\r\n<ul>\r\n	<li>Werken in de SCRUM methodologie</li>\r\n	<li>Samenwerken in teamverband</li>\r\n	<li>Bespreken/inschatten en opleveren van deliverables in afgesproken tijdspanne</li>\r\n	<li>Communiceren met klanten/eindgebruikers</li>\r\n	<li>Verantwoordelijkheid, ownership kunnen nemen over de opdracht</li>\r\n	<li>Zelfstandig kunnen werken</li>\r\n</ul>\r\n<p><u>Technisch</u></p>\r\n<ul>\r\n	<li>Angular</li>\r\n	<li>Ionic / NativeScript / &hellip; (te bepalen in samenspraak)</li>\r\n	<li>REST principes</li>\r\n	<li>AngularFire / Firebase (te bepalen in samenspraak)</li>\r\n	<li>Continuous Integration / Continuous Deployment&nbsp;</li>\r\n</ul>", 1, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3298), null, "<p><u>Niet technisch</u></p>\r\n<ul>\r\n	<li>Verantwoordelijkheid</li>\r\n	<li>Zelfstandig</li>\r\n	<li>Probleemoplossend denken</li>\r\n	<li>Assertief</li>\r\n	<li>Positief ingesteld</li>\r\n</ul>\r\n<p><u>Technisch</u></p>\r\n<ul>\r\n	<li>Basiskennis Angular</li>\r\n	<li>Kennis van REST principes</li>\r\n</ul>\r\n<p><u>Bonus</u></p>\r\n<ul>\r\n	<li>Ervaring met een Angular Mobile framework</li>\r\n	<li>Ervaring Firebase en/of AngularFire</li>\r\n	<li>Ervaring met/kennis van Continious Itegration / Continuous Deployment</li>\r\n</ul>", " R-Library" },
                    { 8, "<p>Il est requis de se rendre au bureau une journee par semaine.</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3300), "<p>Dans l&#39;Inetum-Realdolmen Project Factory (PF), les d&eacute;veloppeurs d&#39;Inetum-Realdolmen d&eacute;veloppent des logiciels sur demande d&#39;un client. Tout le d&eacute;veloppement se fait en interne, avec les outils d&#39;Inetum-Realdolmen. Ainsi, le client re&ccedil;oit ses livrables &agrave; des moments pr&eacute;cis et n&#39;a pas besoin d&#39;avoir une &eacute;quipe de d&eacute;veloppement compl&egrave;te.&nbsp;<br />\r\nPour soutenir notre PF, certains coll&egrave;gues ont cr&eacute;&eacute; une biblioth&egrave;que. Nos d&eacute;veloppeurs peuvent y emprunter des livres. Actuellement, les emprunts sont enregistr&eacute;s sur une feuille de papier. De m&ecirc;me, les demandes d&#39;achat de nouveaux livres sont consign&eacute;es sur papier. C&#39;est un choix d&eacute;lib&eacute;r&eacute;. Si nous num&eacute;risons les emprunts &agrave; l&#39;aide </p>", 3, false, "<p><u>Non technique:</u></p>\r\n<ul>\r\n	<li>Travailler selon la m&eacute;thodologie SCRUM</li>\r\n	<li>Collaborer en &eacute;quipe</li>\r\n	<li>Discuter/&eacute;valuer et livrer des livrables dans les d&eacute;lais convenus</li>\r\n	<li>Communiquer avec les clients/utilisateurs finaux</li>\r\n	<li>Assumer la responsabilit&eacute; et s&#39;approprier la t&acirc;che</li>\r\n	<li>Travailler de mani&egrave;re autonome</li>\r\n</ul>\r\n<p><u>Technique:</u></p>\r\n<ul>\r\n	<li>Angular</li>\r\n	<li>Ionic / NativeScript / ... (&agrave; d&eacute;terminer en collaboration)</li>\r\n	<li>Principes REST</li>\r\n	<li>AngularFire / Firebase (&agrave; d&eacute;terminer en collaboration)</li>\r\n	<li>Int&eacute;gration continue / D&eacute;ploiement continu</li>\r\n</ul>", 2, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3299), null, "<p><u>Non technique:</u></p>\r\n<ul>\r\n	<li>Responsabilit&eacute;</li>\r\n	<li>Autonomie</li>\r\n	<li>Esprit de r&eacute;solution de probl&egrave;mes</li>\r\n	<li>Assertivit&eacute;</li>\r\n	<li>Attitude positive</li>\r\n</ul>\r\n<p><u>Technique:</u></p>\r\n<ul>\r\n	<li>Connaissances de base en Angular</li>\r\n	<li>Connaissance des principes REST</li>\r\n</ul>\r\n<p><u>Bonus:</u></p>\r\n<ul>\r\n	<li>Exp&eacute;rience avec un framework Angular Mobile</li>\r\n	<li>Exp&eacute;rience avec Firebase et/ou AngularFire</li>\r\n	<li>Exp&eacute;rience ou connaissance de l&#39;int&eacute;gration continue / du d&eacute;ploiement continu</li>\r\n</ul>", " R-Library" },
                    { 9, "<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3301), "<p>Low Code ontwikkeling is een groeiende markt. Volgens Gartner zal tegen 2024 65% van alle ontwikkelingen een vorm van low code bevatten. Tijdens deze stage krijg je meer inzicht in Low Code, wat de mogelijkheden zijn en hoe je dit kunt positioneren ten opzichte van traditionele ontwikkelingen in .NET of Java bijvoorbeeld. Je zult ook uitgebreid kennismaken met een low code platform (Mendix) en hiermee een kleine applicatie bouwen.</p>", 4, false, "<ul>\r\n	<li>Kennis van Low Code</li>\r\n	<li>Basis kennis van Mendix, indien er tijd over is, andere low code platforms (Appian, Powerapps)</li>\r\n	<li>Analyse maken</li>\r\n	<li>Ontwikkeling</li>\r\n	<li>Testen</li>\r\n</ul>", 1, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3301), null, "<ul>\r\n	<li>Analytische vaardigheden</li>\r\n	<li>Enige ontwikkelingsvaardigheden</li>\r\n	<li>Gemotiveerd om nieuwe dingen te leren</li>\r\n</ul>", " Building Low Code Applications in Mendix" },
                    { 10, "<p>It is required to come to office once a week</p>", new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3302), "<p>Low Code development is a growing market. According to Gartner, by 2024, 65% of all development will contain some form of low code. During this internship you will gain more insight into Low Code, what the possibilities are and how to position this against traditional developments in .NET or Java eg. You will also get to know a low code platform up close (Mendix) and will build a small application in it.</p>", 4, false, "<ul>\r\n	<li>Knowledge of Low Code</li>\r\n	<li>Basic knowledge of Mendix, if there is time left, other low code platforms (Appian, Powerapps)</li>\r\n	<li>Making analyses</li>\r\n	<li>Development</li>\r\n	<li>Testing</li>\r\n</ul>", 3, new DateTime(2023, 6, 5, 11, 9, 56, 201, DateTimeKind.Utc).AddTicks(3302), null, "<ul>\r\n	<li>Analytical skills</li>\r\n	<li>Any development skills</li>\r\n	<li>Eager to learn new stuf</li>\r\n</ul>", " Building Low Code Applications in Mendix" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Name",
                table: "Departments",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_InternShipLocation_LocationsId",
                table: "InternShipLocation",
                column: "LocationsId");

            migrationBuilder.CreateIndex(
                name: "IX_InternShips_CreatorId",
                table: "InternShips",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_InternShips_SchoolYear",
                table: "InternShips",
                column: "SchoolYear");

            migrationBuilder.CreateIndex(
                name: "IX_InternShips_UnitId",
                table: "InternShips",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_Code",
                table: "Languages",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CreatorId",
                table: "Languages",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_CreatorId",
                table: "Locations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PrefaceTranslations_LanguageId",
                table: "PrefaceTranslations",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PrefaceTranslations_UnitId",
                table: "PrefaceTranslations",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_InternShipId",
                table: "Translations",
                column: "InternShipId");

            migrationBuilder.CreateIndex(
                name: "IX_Translations_LanguageId",
                table: "Translations",
                column: "LanguageId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternShipLocation");

            migrationBuilder.DropTable(
                name: "PrefaceTranslations");

            migrationBuilder.DropTable(
                name: "Translations");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "InternShips");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
