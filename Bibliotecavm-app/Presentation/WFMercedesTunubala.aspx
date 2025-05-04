<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFMercedesTunubala.aspx.cs" Inherits="Presentation.WFMercedesTunubala" %>
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
            text-align: justify;
        }
        .firma {
            font-style: italic;
            text-align: right;
            margin-top: 30px;
        }
        .nivel {
            font-weight: bold;
            margin-top: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contenido">
        <div class="titulo-principal">MERCEDES TUNUBALÁ VELASCO</div>
        
        <div class="contenido-texto">
            <p><strong>Indígena Misak</strong>, nacida el <span class="fecha">17 de noviembre de 1974</span> en la vereda La Campana.</p>
        </div>

        <div class="subtitulo">TRAYECTORIA PERSONAL Y CULTURAL</div>
        <div class="contenido-texto">
            <p>Desde su infancia, rodeada de la historia, el trabajo comunitario y los valores transmitidos por sus padres Mama Julia y Shur Manuel, Mercedes ha encarnado el respeto y el amor por su cultura, destacándose por su honestidad, responsabilidad y vocación de servicio.</p>
        </div>

        <div class="subtitulo">ESTUDIOS REALIZADOS</div>
        <ul class="lista">
            <li><span class="fecha">2023</span> - Magíster en Gerencia de Proyectos, Universidad del Valle</li>
            <li><span class="fecha">2015</span> - Especialista en Gestión de Proyectos de Inversión, Universidad Libre de Cali</li>
            <li><span class="fecha">2011</span> - Economista, Universidad del Valle</li>
            <li>Bachiller Normalista, Almaguer Cauca</li>
        </ul>

        <div class="subtitulo">CARGOS COMUNITARIOS</div>
        <ul class="lista">
            <li>Secretaria y Alcaldesa del Cabildo Nupachik chak Cali</li>
            <li>Secretaria y Gobernadora del Cabildo Indígena de la Universidad del Valle</li>
            <li>Secretaria General y Gobernadora del Cabildo Wampia Silvia Cauca (<span class="fecha">2012, 2024</span>)</li>
        </ul>

        <div class="subtitulo">CARGOS CON OTRAS ENTIDADES</div>
        <ul class="lista">
            <li>Oficina Financiera, IPS-I Mama Dominga</li>
            <li>OIM, convenio con Cabildo de Guambia</li>
        </ul>

        <div class="subtitulo">EXPERIENCIA EN ENTIDADES GUBERNAMENTALES</div>
        <div class="nivel">Municipal:</div>
        <ul class="lista">
            <li>Secretaria del Concejo Municipal de Silvia Cauca</li>
            <li>Jefe Oficina de Planeación, Silvia Cauca (<span class="fecha">2013-2015</span>)</li>
            <li>Alcaldesa encargada en 20 ocasiones (<span class="fecha">2013-2015</span>)</li>
            <li>Contratista del Plan de Desarrollo Municipal de Totoró (<span class="fecha">2016</span>)</li>
            <li>Alcaldesa de Silvia Cauca (<span class="fecha">2020-2023</span>)</li>
        </ul>
        <div class="nivel">Departamental:</div>
        <ul class="lista">
            <li>Contratista, Secretaría de Gobierno del Cauca</li>
        </ul>
        <div class="nivel">Nacional:</div>
        <ul class="lista">
            <li>Asistente en el Senado (Shur Lorenzo Muelas y Marceliano Jamioy)</li>
            <li>Contratista con el DANE, Secretaría de Educación de Bogotá, DNP</li>
        </ul>

        <div class="subtitulo">RESULTADOS COMO ALCALDESA DE SILVIA CAUCA (<span class="fecha">2020-2023</span>)</div>
        <ul class="lista">
            <li>Pavimentación y estudios de vías principales</li>
            <li>Construcción de la Casa de la Mujer y de la Juventud</li>
            <li>Dotación en salud y educación</li>
            <li>Construcción de placa huellas y acueductos</li>
            <li>Gestión de $6.500 millones en recursos del fondo de pensiones</li>
            <li>Calificación sobresaliente (93,92%) en <span class="fecha">2023</span></li>
            <li>Fortalecimiento de AMCOC</li>
        </ul>

        <div class="subtitulo">RESULTADOS COMO GOBERNADORA CON 159 AUTORIDADES (<span class="fecha">2024</span>)</div>
        <ul class="lista">
            <li>Reconocimiento a mayores y participación en eventos internacionales</li>
            <li>Ampliación del Resguardo de Guambia</li>
            <li>Hospital verde Mama Dominga</li>
            <li>Consolidación del SISPI y avances en educación propia</li>
            <li>Adquisición de fincas y sede de AISO</li>
            <li>Participación en la COP 16 y diversas movilizaciones</li>
        </ul>

        <div class="subtitulo">RECONOCIMIENTOS</div>
        <ul class="lista">
            <li>Vicepresidencia internacional, Instituto de Mejores Gobernantes de Iberoamérica (<span class="fecha">2021</span>)</li>
            <li>Plan de Desarrollo destacado por el DNP</li>
            <li>Premios por Buen Gobierno y liderazgo femenino en Cauca</li>
        </ul>

        <div class="firma">Por: Gerardo Tunubalá</div>
    </div>
</asp:Content>