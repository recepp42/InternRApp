using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Domain.Entities;
using backend.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace backend.Infrastructure.Persistence.Configurations;
public class InternShipContentTranslationConfiguration : IEntityTypeConfiguration<InternShipContentTranslation>
{
    public void Configure(EntityTypeBuilder<InternShipContentTranslation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.TitleContent).IsRequired().HasMaxLength(170);
        builder.Property(x => x.Description).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.KnowledgeToDevelop).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.NeededKnowledge).IsRequired().HasMaxLength(1000);
        builder.Property(x => x.Comment).IsRequired().HasMaxLength(1000);

        //seeding
        builder.HasData(
        new
        {
            Id = 1,
            TitleContent = "Devops Factory – Project evaluation tool",
            Description = @"<p>In de presalesfase van een klantproject is d&#39; het belangrijk om de risicos van het softwareproject te detecteren (risicos moeten voor de start van het project worden verminderd). Er zijn ook enkele vereisten met betrekking tot methodologie, gereedschap en technologie voordat een project kan worden uitgevoerd in ons DevOps-fabrieksmodel. Om hier een duidelijk beeld van te krijgen, moet een biedarchitect/-manager in de presalesfase een paar vragen beantwoorden (+/- 50, tegenwoordig in een Excel-bestand) die resulteren in een projecttype (type 1, 2 of 3). Als het resultaat type 3 is, kan het project niet worden ingevoerd in ons DevOps Factory-leveringsmodel. We willen een applicatie bouwen ter vervanging van ons Excel-bestand. Een niet-uitputtende lijst van vereisten om dit te realiseren omvat:</p>",

            KnowledgeToDevelop = @"<ul>
<li>Het ontwerpen en ontwikkelen van een schaalbare applicatie</li>
<li>Het ontwerpen van een gebruiksvriendelijke applicatie</li>
<li>Het ontwikkelingsproces van een applicatie
<ul>
    <li>Samenwerken met een Product Owner (coach/management DevOps Factory)</li>
    <li>Functionele analyses</li>
    <li>Technische analyses en API-ontwerp</li>
    <li>Ontwikkeling van .NET Core-webapplicaties</li>
    <li>Testen</li>
    <li>Documentatie</li>",
            NeededKnowledge = @"<ul>
<li>In staat zijn om zelfstandig te werken en problemen op te lossen onder wekelijkse begeleiding.</li>
<li>Kennis van C#</li>
<li>Achtergrondkennis of begrip van de backend (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>
<li>Achtergrondkennis of begrip van de frontend (ASP.NET MVV, Blazor)</li>
<li>Vaardigheden in webontwikkeling (HTML, CSS, JavaScript)</li>
<li>Open-minded en leergierig</li>
<li>Positieve instelling en bereidheid om aan te pakken</li>
</ul>",
            Comment = @"<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>",
            LanguageId = 1,
            InternShipId = 1,
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        },
        new
        {
            Id = 2,
            TitleContent = "Devops Factory – Project evaluation tool",
            Description = @"<p>Dans la phase de pr&eacute;vente d&#39;un projet client, il est important de d&eacute;tecter les risques du projet logiciel (les risques doivent &ecirc;tre att&eacute;nu&eacute;s avant le d&eacute;but du projet). Il existe &eacute;galement des exigences en mati&egrave;re de m&eacute;thodologie, d&#39;outils et de technologie avant qu&#39;un projet puisse &ecirc;tre ex&eacute;cut&eacute; dans notre usine DevOps.<br />
Pour avoir une vision claire de cela, dans la phase de pr&eacute;vente, un architecte/gestionnaire d&#39;offres doit r&eacute;pondre &agrave; quelques questions (+/- 50, actuellement dans un fichier Excel) qui donnent lieu &agrave; un &quot;type de projet&quot; (type 1, 2 ou 3). Si le r&eacute;sultat est de type 3, le projet ne peut pas &ecirc;tre int&eacute;gr&eacute; &agrave; notre mod&egrave;le de livraison de l&#39;usine DevOps. Nous souhaitons construire une application pour remplacer notre fichier Excel. Voici une liste non exhaustive des exigences :</p>",
            KnowledgeToDevelop = @"<ul>
<li>Conception et d&eacute;veloppement d&#39;une application &eacute;volutive</li>
<li>Conception d&#39;une application conviviale</li>
<li>Processus de d&eacute;veloppement d&#39;une application
<ul>
    <li>Collaboration avec un propri&eacute;taire de produit (coach/gestionnaire DevOps Factory)</li>
    <li>Analyses fonctionnelles</li>
    <li>Analyses techniques et conception d&#39;API</li>
    <li>D&eacute;veloppement d&#39;applications Web avec .NET Core</li>
    <li>Tests</li>
    <li>Documentation</li>",
            NeededKnowledge = @"<ul>
<li>&Ecirc;tre capable de travailler et de r&eacute;soudre des probl&egrave;mes de mani&egrave;re autonome sous une supervision hebdomadaire.</li>
<li>Connaissance du langage C#</li>
<li>Connaissances ou notions en back-end (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>
<li>Connaissances ou notions en front-end (ASP.NET MVV, Blazor) ▪ Comp&eacute;tences en d&eacute;veloppement web (HTML, CSS, JavaScript)</li>
<li>Ouverture d&#39;esprit et d&eacute;sir d&#39;apprendre</li>
<li>Attitude positive et proactive</li>
</ul>",
            Comment = $@"<p>Il est requis de se rendre au bureau une journee par semaine.</p>",
            LanguageId = 2,
            InternShipId = 1,
            IsDeleted = false,
            LastModifiedDate = DateTime.UtcNow,
            CreatedDate = DateTime.UtcNow,
        },
    new
    {
        Id = 3,
        TitleContent = "Devops Factory – Project evaluation tool",
        Description = @"<p>In the presales phase of a customer project, it&rsquo;s important to detect the risks of the software project (risks needs to be mitigated before the start of the project). There are also some requirements about methodology, tooling &amp; technology before a project can be executed in our DevOps factory. To have a clear view on this, in presales phase, a bid architect/manager needs to answer a few questions (+/- 50, nowadays in an Excel file) which results in a &lsquo;project type&rsquo; (type 1, 2 or 3). If the result will be type 3, the project cannot be onboarded in our DevOps Factory delivery model. We want to build an application as a replacement of our Excel file. A non-exhaustive list of requirements:</p>",
        KnowledgeToDevelop = @"<ul>
<li>Designing and developing a scalable application</li>
<li>Designing a user-friendly application</li>
<li>Development process of an application 
<ul>
    <li>Working together with a Product Owner (coach/management DevOps Factory)</li>
    <li>Functional analyses</li>
    <li>Technical analyses and API Desig</li>
    <li>Development van .NET Core Web applications</li>
    <li>Testing</li>
    <li>Documentation</li>
</ul>
</li>
<li>Cloud computing with Azure (hosting &amp; database)</li>
<li>Entity Framework Core</li>
<li>ASP .NET Core Web API</li>
<li>ASP .NET MVC, Blazor</li>
</ul>",
        NeededKnowledge = @"<ul>
<li>Being able to work and solve problems independently under weekly guidance.</li>
<li>C# knowledge</li>
<li>Back-end knowledge or notions (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture,...)</li>
<li>Front-end knowledge or notions (ASP.NET MVV, Blazor)</li>
<li>Web development skills (HTML, CSS, JavaScript)</li>
<li>Open minded and eager to learn</li>
<li>Can do attitude</li>
</ul>",
        Comment = $@"<p>It is required to come to office once a week</p>",
        LanguageId = 3,
        InternShipId = 1,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    },
    new
    {
        Id = 4, 
        TitleContent = "InternRApp",
        Description = @"<p>Inetum-Realdolmen is een groot bedrijf, bestaande uit verschillende afdelingen (Microsoft, Java, etc.). Elke afdeling is verantwoordelijk voor het opzetten van haar stageopdrachten en het vinden van de bijbehorende stagebegeleiders. Uiteindelijk moeten deze stageopdrachten worden doorgegeven aan het schoolco&ouml;rdinatieteam, dat verantwoordelijk is voor de samenwerking met verschillende hogescholen/universiteiten en deze stageopdrachten zal verdelen. Op dit moment worden deze stageopdrachten per afdeling ingevuld met behulp van een Word-sjabloon en per e-mail verzonden naar het schoolco&ouml;rdinatieteam. Dit zorgt voor administratieve lasten en we willen een applicatie ontwikkelen ter vervanging van dit Word-document.</p>",
        KnowledgeToDevelop = @"<ul>
<li>Het ontwerpen en ontwikkelen van een schaalbare applicatie</li>
<li>Het ontwerpen van een gebruiksvriendelijke applicatie</li>
<li>Het ontwikkelingsproces van een applicatie
<ul>
    <li>Samenwerken met een Product Owner (coach/management DevOps Factory)</li>
    <li>Functionele analyses</li>
    <li>Technische analyses en API-ontwerp</li>
    <li>Ontwikkeling van .NET Core-webapplicaties</li>
    <li>Testen</li>
    <li>Documentatie</li>",
        NeededKnowledge = @"<ul>
<li>In staat zijn om zelfstandig te werken en problemen op te lossen onder wekelijkse begeleiding.</li>
<li>Kennis van C#</li>
<li>Achtergrondkennis of begrip van de backend (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>
<li>Achtergrondkennis of begrip van de frontend (ASP.NET MVV, Blazor)</li>
<li>Vaardigheden in webontwikkeling (HTML, CSS, JavaScript)</li>
<li>Open-minded en leergierig</li>
<li>Positieve instelling en bereidheid om aan te pakken</li>
</ul>",
        Comment = @"<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>",
        LanguageId = 1,
        InternShipId = 2,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    }, new
    {
        Id = 5,
        TitleContent = "InternRApp",
        Description = @"<p>Inetum-Realdolmen est une grande entreprise, compos&eacute;e de plusieurs d&eacute;partements (Microsoft, Java, etc.). Chaque d&eacute;partement est responsable de la mise en place de ses missions de stage et de la recherche des superviseurs correspondants. Finalement, ces missions de stage doivent &ecirc;tre transmises &agrave; l&#39;&eacute;quipe de coordination scolaire, qui est charg&eacute;e de la collaboration avec les diff&eacute;rentes &eacute;coles/universit&eacute;s et se chargera de distribuer ces missions de stage. Actuellement, ces missions de stage sont remplies par d&eacute;partement &agrave; l&#39;aide d&#39;un mod&egrave;le Word et envoy&eacute;es par e-mail &agrave; l&#39;&eacute;quipe de coordination scolaire. Cela cr&eacute;e une charge administrative et nous souhaitons d&eacute;velopper une application en remplacement de ce document Word.</p>",
        KnowledgeToDevelop = @"<ul>
<li>Conception et d&eacute;veloppement d&#39;une application &eacute;volutive</li>
<li>Conception d&#39;une application conviviale</li>
<li>Processus de d&eacute;veloppement d&#39;une application
<ul>
    <li>Collaboration avec un propri&eacute;taire de produit (coach/gestionnaire DevOps Factory)</li>
    <li>Analyses fonctionnelles</li>
    <li>Analyses techniques et conception d&#39;API</li>
    <li>D&eacute;veloppement d&#39;applications Web avec .NET Core</li>
    <li>Tests</li>
    <li>Documentation</li>",
        NeededKnowledge = @"<ul>
<li>&Ecirc;tre capable de travailler et de r&eacute;soudre des probl&egrave;mes de mani&egrave;re autonome sous une supervision hebdomadaire.</li>
<li>Connaissance du langage C#</li>
<li>Connaissances ou notions en back-end (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture, ...)</li>
<li>Connaissances ou notions en front-end (ASP.NET MVV, Blazor) ▪ Comp&eacute;tences en d&eacute;veloppement web (HTML, CSS, JavaScript)</li>
<li>Ouverture d&#39;esprit et d&eacute;sir d&#39;apprendre</li>
<li>Attitude positive et proactive</li>
</ul>",
        Comment = $@"<p>Il est requis de se rendre au bureau une journee par semaine.</p>",
        LanguageId = 2,
        InternShipId = 2,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    },
    new
    {
        Id = 6,
        TitleContent = "InternRApp",
        Description = @"<p>Inetum-Realdolmen is a large company, consisting of several departments (Microsoft, 
Java, etc.). Each department is responsible for setting up its internship assignments 
and finding the corresponding internship supervisors. Eventually, these internship 
assignments must be passed on to the school coordination team, which is responsible 
for the collaboration with the various colleges/universities and will distribute these 
internship assignments. At the moment, these internship assignments are filled out 
per department using a Word template and sent by email to the school coordination 
team. This creates an administrative burden and we want to build an application as a 
replacement of this Word document.<p>",
        KnowledgeToDevelop = @"<ul>
<li>Designing and developing a scalable application</li>
<li>Designing a user-friendly application</li>
<li>Development process of an application 
<ul>
    <li>Working together with a Product Owner (coach/management DevOps Factory)</li>
    <li>Functional analyses</li>
    <li>Technical analyses and API Desig</li>
    <li>Development van .NET Core Web applications</li>
    <li>Testing</li>
    <li>Documentation</li>",
        NeededKnowledge = @"<ul>
<li>Being able to work and solve problems independently under weekly guidance.</li>
<li>C# knowledge</li>
<li>Back-end knowledge or notions (ASP.NET Core, Entity Framework Core, MediatR, Clean Architecture,...)</li>
<li>Front-end knowledge or notions (ASP.NET MVV, Blazor)</li>
<li>Web development skills (HTML, CSS, JavaScript)</li>
<li>Open minded and eager to learn</li>
<li>Can do attitude</li>
</ul>",
        Comment = $@"<p>It is required to come to office once a week</p>",
        LanguageId = 3,
        InternShipId = 2,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    }, new 
    {
        Id = 7, 
        TitleContent = " R-Library",
        Description = @"<p>In de Inetum-Realdolmen Project Factory (PF) ontwikkelen developers van Inetum Realdolmen software in opdracht van een klant. Al het development gebeurd in house, met de tooling van Inetum-Realdolmen. De klant krijgt dus zijn deliverable op&nbsp;bepaalde tijdstippen en hoeft geen volledig development team te hebben.&nbsp;</p><p>Om onze PF te ondersteunen, hebben enkele collega&rsquo;s een bibliotheek opgericht. Hier&nbsp;kunnen onze developers boeken uitlenen. Momenteel wordt het uitleen-gedrag&nbsp;bijgehouden op een blad papier. Alsook aanvragen om nieuwe boeken aan te kopen&nbsp;worden op papier bijgehouden. Dit is een bewuste keuze. Als we het uitleen-gedrag&nbsp;digitaliseren op basis van bestaande tools (Google Drive, SharePoint...) zorgt dit voor&nbsp;extra overhead, dit be&iuml;nvloedt de toegankelijkheid van de bibliotheek.</p>",
        KnowledgeToDevelop = @"<p><u>Niet technisch</u>&nbsp;</p>
<ul>
	<li>Werken in de SCRUM methodologie</li>
	<li>Samenwerken in teamverband</li>
	<li>Bespreken/inschatten en opleveren van deliverables in afgesproken tijdspanne</li>
	<li>Communiceren met klanten/eindgebruikers</li>
	<li>Verantwoordelijkheid, ownership kunnen nemen over de opdracht</li>
	<li>Zelfstandig kunnen werken</li>
</ul>
<p><u>Technisch</u></p>
<ul>
	<li>Angular</li>
	<li>Ionic / NativeScript / &hellip; (te bepalen in samenspraak)</li>
	<li>REST principes</li>
	<li>AngularFire / Firebase (te bepalen in samenspraak)</li>
	<li>Continuous Integration / Continuous Deployment&nbsp;</li>
</ul>",
        NeededKnowledge = @"<p><u>Niet technisch</u></p>
<ul>
	<li>Verantwoordelijkheid</li>
	<li>Zelfstandig</li>
	<li>Probleemoplossend denken</li>
	<li>Assertief</li>
	<li>Positief ingesteld</li>
</ul>
<p><u>Technisch</u></p>
<ul>
	<li>Basiskennis Angular</li>
	<li>Kennis van REST principes</li>
</ul>
<p><u>Bonus</u></p>
<ul>
	<li>Ervaring met een Angular Mobile framework</li>
	<li>Ervaring Firebase en/of AngularFire</li>
	<li>Ervaring met/kennis van Continious Itegration / Continuous Deployment</li>
</ul>",
        Comment = @"<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>", 
        LanguageId = 1,
        InternShipId = 3,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    }, new
    {
        Id = 8, 
        TitleContent = " R-Library",
        Description = @"<p>Dans l&#39;Inetum-Realdolmen Project Factory (PF), les d&eacute;veloppeurs d&#39;Inetum-Realdolmen d&eacute;veloppent des logiciels sur demande d&#39;un client. Tout le d&eacute;veloppement se fait en interne, avec les outils d&#39;Inetum-Realdolmen. Ainsi, le client re&ccedil;oit ses livrables &agrave; des moments pr&eacute;cis et n&#39;a pas besoin d&#39;avoir une &eacute;quipe de d&eacute;veloppement compl&egrave;te.&nbsp;<br />
Pour soutenir notre PF, certains coll&egrave;gues ont cr&eacute;&eacute; une biblioth&egrave;que. Nos d&eacute;veloppeurs peuvent y emprunter des livres. Actuellement, les emprunts sont enregistr&eacute;s sur une feuille de papier. De m&ecirc;me, les demandes d&#39;achat de nouveaux livres sont consign&eacute;es sur papier. C&#39;est un choix d&eacute;lib&eacute;r&eacute;. Si nous num&eacute;risons les emprunts &agrave; l&#39;aide </p>",
        KnowledgeToDevelop = @"<p><u>Non technique:</u></p>
<ul>
	<li>Travailler selon la m&eacute;thodologie SCRUM</li>
	<li>Collaborer en &eacute;quipe</li>
	<li>Discuter/&eacute;valuer et livrer des livrables dans les d&eacute;lais convenus</li>
	<li>Communiquer avec les clients/utilisateurs finaux</li>
	<li>Assumer la responsabilit&eacute; et s&#39;approprier la t&acirc;che</li>
	<li>Travailler de mani&egrave;re autonome</li>
</ul>
<p><u>Technique:</u></p>
<ul>
	<li>Angular</li>
	<li>Ionic / NativeScript / ... (&agrave; d&eacute;terminer en collaboration)</li>
	<li>Principes REST</li>
	<li>AngularFire / Firebase (&agrave; d&eacute;terminer en collaboration)</li>
	<li>Int&eacute;gration continue / D&eacute;ploiement continu</li>
</ul>",
        NeededKnowledge = @"<p><u>Non technique:</u></p>
<ul>
	<li>Responsabilit&eacute;</li>
	<li>Autonomie</li>
	<li>Esprit de r&eacute;solution de probl&egrave;mes</li>
	<li>Assertivit&eacute;</li>
	<li>Attitude positive</li>
</ul>
<p><u>Technique:</u></p>
<ul>
	<li>Connaissances de base en Angular</li>
	<li>Connaissance des principes REST</li>
</ul>
<p><u>Bonus:</u></p>
<ul>
	<li>Exp&eacute;rience avec un framework Angular Mobile</li>
	<li>Exp&eacute;rience avec Firebase et/ou AngularFire</li>
	<li>Exp&eacute;rience ou connaissance de l&#39;int&eacute;gration continue / du d&eacute;ploiement continu</li>
</ul>",
        Comment = $@"<p>Il est requis de se rendre au bureau une journee par semaine.</p>", 
        LanguageId = 2,
        InternShipId = 3,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    },
    new
    {
        Id = 9, 
        TitleContent = " Building Low Code Applications in Mendix",
        Description = @"<p>Low Code ontwikkeling is een groeiende markt. Volgens Gartner zal tegen 2024 65% van alle ontwikkelingen een vorm van low code bevatten. Tijdens deze stage krijg je meer inzicht in Low Code, wat de mogelijkheden zijn en hoe je dit kunt positioneren ten opzichte van traditionele ontwikkelingen in .NET of Java bijvoorbeeld. Je zult ook uitgebreid kennismaken met een low code platform (Mendix) en hiermee een kleine applicatie bouwen.</p>",
        KnowledgeToDevelop = @"<ul>
	<li>Kennis van Low Code</li>
	<li>Basis kennis van Mendix, indien er tijd over is, andere low code platforms (Appian, Powerapps)</li>
	<li>Analyse maken</li>
	<li>Ontwikkeling</li>
	<li>Testen</li>
</ul>",
        NeededKnowledge = @"<ul>
	<li>Analytische vaardigheden</li>
	<li>Enige ontwikkelingsvaardigheden</li>
	<li>Gemotiveerd om nieuwe dingen te leren</li>
</ul>",
        Comment = $@"<p>Het is vereist om 1 dag in de week naar kantoor te komen</p>", 
        LanguageId = 1,
        InternShipId = 4,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    }, new
    {
        Id = 10,
        TitleContent = " Building Low Code Applications in Mendix",
        Description = @"<p>Low Code development is a growing market. According to Gartner, by 2024, 65% of all development will contain some form of low code. During this internship you will gain more insight into Low Code, what the possibilities are and how to position this against traditional developments in .NET or Java eg. You will also get to know a low code platform up close (Mendix) and will build a small application in it.</p>",
        KnowledgeToDevelop = @"<ul>
	<li>Knowledge of Low Code</li>
	<li>Basic knowledge of Mendix, if there is time left, other low code platforms (Appian, Powerapps)</li>
	<li>Making analyses</li>
	<li>Development</li>
	<li>Testing</li>
</ul>",
        NeededKnowledge = @"<ul>
	<li>Analytical skills</li>
	<li>Any development skills</li>
	<li>Eager to learn new stuf</li>
</ul>",
        Comment = $@"<p>It is required to come to office once a week</p>",
        LanguageId = 3,
        InternShipId = 4,
        IsDeleted = false,
        LastModifiedDate = DateTime.UtcNow,
        CreatedDate = DateTime.UtcNow,
    }

        );

    }


}
