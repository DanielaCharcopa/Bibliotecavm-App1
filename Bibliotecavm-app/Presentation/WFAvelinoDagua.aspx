<%@ Page Title="Taita Avelino Dagua Hurtado" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFAvelinoDagua.aspx.cs" Inherits="Presentation.WFAvelinoDagua" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .contenido {
            font-family: Arial, sans-serif;
            line-height: 1.6;
            padding: 20px;
            max-width: 900px;
            margin: 0 auto;
        }
        .titulo-principal {
            font-size: 28px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 20px;
            color: #333;
        }
        .datos-personales {
            text-align: center;
            margin-bottom: 30px;
            font-style: italic;
            font-size: 18px;
        }
        .subtitulo {
            font-size: 20px;
            font-weight: bold;
            margin-top: 25px;
            margin-bottom: 15px;
            color: #444;
            border-bottom: 2px solid #ddd;
            padding-bottom: 5px;
        }
        .lista {
            margin-left: 20px;
            margin-bottom: 20px;
        }
        .lista li {
            margin-bottom: 10px;
        }
        .fecha {
            font-weight: bold;
            color: #0066cc;
        }
        .contenido-texto {
            margin-bottom: 15px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contenido">
        <div class="titulo-principal">TAITA AVELINO DAGUA HURTADO</div>
        
        <div class="datos-personales">
            <p><span class="fecha">27 de mayo de 1938</span> - <span class="fecha">2015</span></p>
        </div>

        <div class="subtitulo">PARENTESCO</div>
        <div class="contenido-texto">
            <p>Padres: José Dagua y Mama Beatriz Hurtado.</p>
            <p>Hermano de Clemencia y Ignacio.</p>
            <p>Estado civil: Casado con Ascensión Yalanda de Dagua.</p>
        </div>

        <div class="subtitulo">HIJOS</div>
        <ul class="lista">
            <li>Rosalía Dagua Yalanda</li>
            <li>José Vicente Dagua Yalanda</li>
            <li>Florencio Dagua Yalanda</li>
            <li>Clemencia Dagua Yalanda</li>
            <li>Isabel Dagua Yalanda</li>
        </ul>

        <div class="subtitulo">CARGOS DESEMPEÑADOS EN LA COMUNIDAD</div>
        <ul class="lista">
            <li><span class="fecha">1973:</span> Presidente de la junta de acción comunal.</li>
            <li><span class="fecha">1980:</span> Elegido por el consejo del cabildo.</li>
            <li><span class="fecha">1981:</span> Participó en el comité de historia y proceso de NAMUI WAM.</li>
            <li><span class="fecha">1982:</span> Gobernador del cabildo de Guambia.</li>
            <li><span class="fecha">1982:</span> Formalizó el diálogo de gobierno a gobierno con el presidente Belisario Betancur Cuartas.</li>
            <li>Realizó investigaciones locales, nacionales e internacionales.</li>
            <li><span class="fecha">1983-1985:</span> Inició investigación con el Instituto Colombiano de Antropología ("MANASRϴ IWAN WETϴRRIA KϴN").</li>
            <li><span class="fecha">1990-1992:</span> Comité de historia y lingüística de NAMUI WAM.</li>
            <li><span class="fecha">1995-2000:</span> Investigación y publicación del libro "LA VOZ DE NUESTROS MAYORES".</li>
            <li>Participó en el "comité de recuperación de la memoria histórica oral".</li>
            <li><span class="fecha">2000-2006:</span> Escribió "NAMUI NUMISAKWAM TAMARA PϴRIK".</li>
            <li>Colaboró en libro sobre la reivindicación de la primera infancia.</li>
            <li><span class="fecha">2006:</span> Segunda edición de "LA VOZ DE LOS MAYORES".</li>
            <li>Participó en "CAMINANDO DESDE EL CORAZÓN DE LA MONTAÑA" (<span class="fecha">2017</span>).</li>
            <li>Participó en "PENSANDO Y EDUCANDO DESDE EL CORAZÓN DE LA MONTAÑA" (<span class="fecha">2017</span>).</li>
            <li><span class="fecha">1998:</span> "Guambianos hijos del aro iris y del agua" por Luis Guillermo Vasco.</li>
            <li>Su legado más importante: la casa de taita Payan en Santiago, con pinturas y jeroglíficos.</li>
            <li>Promotor del idioma Guambiano.</li>
        </ul>
    </div>
</asp:Content>