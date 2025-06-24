<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="WFAlvaroMorales.aspx.cs" Inherits="Presentation.WFAlvaroMorales" %>
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
        .datos-personales {
            margin-bottom: 30px;
            text-align: center;
            font-style: italic;
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
        .evento {
            margin-bottom: 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="contenido">
        <div class="titulo-principal">SHUR ALVARO MORALES TOMBE Q.E.P.D.</div>
        
        <div class="datos-personales">
            <p>Hijo de Micaela Tombé e Israel Morales</p>
            <p>Nació el <span class="fecha">2 de mayo de 1952</span>.</p>
            <p>Falleció el <span class="fecha">22 de diciembre del 2025</span> a la edad de <span class="fecha">72 años</span>.</p>
        </div>
        
        <div class="subtitulo">ESTUDIOS</div>
        <div class="evento"><p>Primaria: Escuela las Delicias en el año <span class="fecha">1961, 1962 y 1963</span>, hasta el grado tercero.</p></div>
        <div class="evento"><p>A los <span class="fecha">14 años</span> se va para el norte en busca del sustento diario, durante un año.</p></div>
        
        <div class="evento"><p>En <span class="fecha">1970</span>, contraen matrimonio con mama María Antonia Camayo Tróchez, tuvieron 2 hijos: Manuel Jesús Morales y Misael Morales quienes hoy en día ya tienen esposas e hijos.</p></div>
        
        <div class="contenido-texto">
            <p>El tata comenta que hasta el momento ha pasado por distintos cargos, de las cuales, día a día aprendió a liderar y apoyar en todos los procesos comunitarios y por fuera del municipio.</p>
        </div>

        <div class="subtitulo">CARGOS DESEMPEÑADOS</div>
        <div class="evento"><p>En <span class="fecha">1974</span>, lo eligen como alguacil de la vereda Cumbre Nueva quien gestiona materiales para la construcción de tanques para acueductos.</p></div>
        <div class="evento"><p>En <span class="fecha">1980</span>, lo eligen como presidente de la Junta de Acción Comunal y con el apoyo de la comunidad logra gestionar herramientas de trabajo para la construcción del puente en la vereda Cumbre Nueva y con el apoyo del alcalde municipal se logra gestionar la compra de un lote de terreno en la vereda Peña del Corazón para la construcción del acueducto en el punto llamado PALMAR.</p></div>
        <div class="evento"><p>En <span class="fecha">1985</span>, la comunidad lo elige como secretario general quien con ese cargo apoya a todos los taitas en el proceso de recuperación de tierras.</p></div>
        <div class="evento"><p>En <span class="fecha">1989</span>, gobernador Cabildo de Guambia.</p></div>
        <div class="evento"><p>En <span class="fecha">1990</span>, participa en el consejo de taitas logrando apoyar procesos de la comunidad dentro y fuera del municipio y departamento.</p></div>
        <div class="evento"><p>En <span class="fecha">1994</span>, candidato para la alcaldía municipal de Silvia-Cauca, elegido por la comunidad, el cual se pierde.</p></div>
        <div class="evento"><p>En <span class="fecha">1995</span>, nuevamente gobernador Cabildo de Guambia.</p></div>
        <div class="evento"><p>En <span class="fecha">1996-1997</span>, coordinación del plan de Vida del pueblo Guambiano, apoyo para el fortalecimiento institucional.</p></div>
        <div class="evento"><p>En <span class="fecha">1997</span>, nuevamente la comunidad lo eligen como candidato para la alcaldía del municipio de Silvia; el cual con la experiencia y el apoyo de la comunidad logra ganar la alcaldía para el periodo 1997-2000.</p></div>
        <div class="evento"><p>Entre los años <span class="fecha">2001-2003</span> fue el coordinador nacional del AICO SOCIAL.</p></div>
        <div class="evento"><p>Desde el año <span class="fecha">2008</span> hasta el <span class="fecha">2013</span>, la autoridad lo elige para coordinar el programa de Ampliación de Cobertura, programa que enfoca a la población vulnerable, respecto a la formación académica.</p></div>
        <div class="evento"><p>En <span class="fecha">2015</span>, la comunidad lo reeligen como candidato para la alcaldía municipal de Silvia; el cual no se logra alcanzar los objetivos.</p></div>
        <div class="evento"><p>En <span class="fecha">2019</span>, por tercera vez la comunidad mediante asamblea lo eligen como gobernador del resguardo de Guambía.</p></div>

        <div class="subtitulo">LOGROS DURANTE SU GOBERNACIÓN (1989)</div>
        <div class="evento"><p>En <span class="fecha">1989</span>, gobernador Cabildo de Guambia, logra realizar acuerdos con el cabildo de Ambaló para el proceso de recuperación de tierras, recuperando los 5 predios azules con la ayuda de la comunidad y autoridad del periodo. Durante este año, da inicio con el proceso de la reconstrucción de la casa del Cabildo en Santiago.</p></div>

        <div class="subtitulo">LOGROS DURANTE SU GOBERNACIÓN (1995)</div>
        <div class="evento"><p>En <span class="fecha">1995</span>, nuevamente gobernador Cabildo de Guambia; con la gestión durante el periodo, logra la realización de estudios y diseños para la construcción de las escuelas de la Campana, Delicias y Pueblito. También apoyo en la gestión de todo el proceso de nuestro hospital mama Dominga.</p></div>

        <div class="subtitulo">LOGROS DURANTE SU ALCALDÍA (1997-2000)</div>
        <div class="contenido-texto">
            <p>Durante esta alcaldía contó con el apoyo de la comunidad Silviana, en diferentes procesos, la cual fue muy importante para el proceso. La gestión del taita a nivel local, regional y nacional, fue muy importante, porque los llevó a ocupar el tercer mejor alcalde de Colombia dentro de los 1.101 alcaldes municipales.</p>
            <p>A nivel interno logra afianzar acuerdos con Pepe Estela para la compra de los predios del Warankal.</p>
        </div>

        <div class="subtitulo">LOGROS DURANTE SU GOBERNACIÓN (2019-2020)</div>
        <div class="evento"><p>En <span class="fecha">2019</span>, por tercera vez la comunidad mediante asamblea lo eligen como gobernador del resguardo de Guambía, desde su gobierno y con el apoyo de la comunidad se desarrolla diferentes procesos comunitarios para el bienestar de la comunidad, como el mercado Misak.</p></div>
        <div class="contenido-texto">
            <p>En este periodo de gobierno, en común acuerdo de la comunidad y la autoridad, apoya el candidato o representante para la alcaldía del Municipio de Silvia, a mama Mercedes Tunubalá Velasco; consiguiendo buenos resultados, ganando la alcaldía del municipio para el periodo 2020-2022.</p>
            <p>En noviembre y diciembre de <span class="fecha">2019</span>, durante el proceso de selección de los nuevos cabildantes para el periodo 2020, mediante asamblea lo reeligen al gobernador tata Álvaro Morales, al vicegobernador, tata José Elías Pillimue y un secretario general, al tata Luis Carlos Calambás para que continúen durante el periodo 2020; quienes con su nuevo equipo las gestiones no han paralizado en bien de los diferentes programas de plan de vida a pesar de la crisis que ha generado la pandemia del COVI-19.</p>
            <p>Entre el <span class="fecha">2019, 2020 y 2021</span>, logró fortalecer Nu NakChak, con este proceso se lograron convenios de las Autoridades Indígenas del Sur Occidente y así mismo la gestión de 3.567 hectáreas en Santa lucía Barragán y otras partes.</p>
        </div>
        
        <div class="evento"><p>En <span class="fecha">2004</span>, apoyó, gestionó la realización del trueque.</p></div>
        <div class="evento"><p>Desde el año <span class="fecha">2008</span> hasta el <span class="fecha">2013</span>, la autoridad lo elige para coordinar el programa de Ampliación de Cobertura, programa que enfoca a la población vulnerable, respecto a la formación académica. Durante los años de acompañamiento, apoyo y formo jóvenes para sean grandes lideres en la comunidad y sobre todo en el fortalecimiento de la práctica de la economía propia, reviviendo los saberes del shuramera y shurmera, con el objetivo de fortalecer la identidad Misak.</p></div>
    </div>
</asp:Content>