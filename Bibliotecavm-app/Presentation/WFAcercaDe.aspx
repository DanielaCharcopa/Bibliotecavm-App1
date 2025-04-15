<%@ Page Title="Acerca de" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFAcercaDe.aspx.cs" Inherits="Presentation.WFAcercaDe" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        /* Estilos generales */
        .about-container {
            max-width: 1200px;
            margin: 0 auto;
            padding: 30px 20px;
            color: #333;
            line-height: 1.6;
        }
        
        .about-container h1 {
            color: #1a237e;
            font-size: 2.5rem;
            margin-bottom: 30px;
            text-align: center;
            font-weight: 600;
            border-bottom: 2px solid #1a237e;
            padding-bottom: 10px;
        }
        
        .about-container h2 {
            color: #303f9f;
            font-size: 1.8rem;
            margin: 30px 0 15px 0;
            font-weight: 500;
        }
        
        .about-container p {
            font-size: 1.1rem;
            margin-bottom: 20px;
        }
        
        .about-container ul {
            padding-left: 20px;
            margin-bottom: 25px;
        }
        
        .about-container li {
            margin-bottom: 8px;
            font-size: 1.1rem;
        }
        
        /* Estilos para las tarjetas de colaboradores (mantenidos como estaban) */
        .thumbnail {
            border-radius: 8px;
            transition: transform 0.3s ease;
            border: 1px solid #e0e0e0;
        }
        
        .thumbnail:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0,0,0,0.1);
        }
        
        .thumbnail img {
            border-radius: 8px 8px 0 0;
            width: 100%;
            height: 200px;
            object-fit: cover;
        }
        
        .caption {
            padding: 15px;
        }
        
        blockquote {
            padding: 0;
            margin: 0 0 10px 0;
            border-left: none;
        }
        
        blockquote small {
            display: block;
            margin-top: 5px;
            color: #666;
        }
        
        /* Estilos responsivos */
        @media (max-width: 768px) {
            .about-container {
                padding: 20px 15px;
            }
            
            .about-container h1 {
                font-size: 2rem;
            }
            
            .about-container h2 {
                font-size: 1.5rem;
            }
            
            .about-container p,
            .about-container li {
                font-size: 1rem;
            }
            
            .col-sm-6 {
                margin-bottom: 20px;
            }
        }
        
        /* Estilo para el enlace de contacto */
        .contact-link {
            color: #1a237e;
            font-weight: 500;
            text-decoration: none;
            transition: color 0.3s;
        }
        
        .contact-link:hover {
            color: #303f9f;
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="about-container">
        <h1>Acerca de</h1>
        
        <div class="mission-section">
            <h2>Misión</h2>
            <p>La Biblioteca Virtual Misak tiene como misión preservar y difundir la riqueza cultural y educativa de la comunidad Misak, proporcionando acceso a materiales educativos y culturales de manera accesible y sostenible.</p>
        </div>
        
        <div class="vision-section">
            <h2>Visión</h2>
            <p>Ser un referente en la preservación y promoción de la cultura indígena, facilitando el acceso a la información y el conocimiento para las futuras generaciones de la comunidad Misak.</p>
        </div>
        
        <div class="objectives-section">
            <h2>Objetivos</h2>
            <ul>
                <li>Fortalecer el sistema educativo indígena propio.</li>
                <li>Preservar y difundir los conocimientos ancestrales y la cultura Misak.</li>
                <li>Proporcionar acceso a materiales educativos y culturales de alta calidad.</li>
                <li>Fomentar la inclusión y accesibilidad para todos los miembros de la comunidad.</li>
            </ul>
        </div>
        
        <div class="history-section">
            <h2>Historia</h2>
            <p>La Biblioteca Virtual Misak fue creada en 2024 como una iniciativa para preservar y promover la cultura y los conocimientos ancestrales de la comunidad Misak. Este proyecto nació de la necesidad de salvaguardar los materiales educativos propios y fortalecer el sistema educativo indígena.</p>
        </div>
        
        <div class="team-section">
            <h2>Equipo</h2>
            <p>La biblioteca es gestionada por un equipo de profesionales comprometidos con la educación y la preservación cultural, incluyendo ingenieros de sistemas, educadores y miembros de la comunidad Misak.</p>
        </div>
        
        <div class="collaborators-section">
            <h2>Colaboradores</h2>
            <div class="container">
                <div class="row">
                    <!-- Colaborador 3 - Álvaro Javier Morales -->
                    <div class="col-sm-6 col-md-3">
                        <div class="thumbnail">
                            <img src="https://via.placeholder.com/150x150?text=Álvaro+Morales" alt="Álvaro Javier Morales" class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Álvaro Javier Morales</p> 
                                    <small><cite title="Source Title">Desarrollador <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <p> 
                                    <i class="glyphicon glyphicon-envelope"></i> alvaro.morales@ejemplo.com
                                    <br />
                                    <i class="glyphicon glyphicon-education"></i> Ingeniero de Sistemas
                                </p>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Colaborador 4 - Karen Daniela Charcopa -->
                    <div class="col-sm-6 col-md-3">
                        <div class="thumbnail">
                            <img src="https://via.placeholder.com/150x150?text=Karen+Charcopa" alt="Karen Daniela Charcopa" class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Karen Daniela Charcopa</p> 
                                    <small><cite title="Source Title">Desarrolladora <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <p> 
                                    <i class="glyphicon glyphicon-envelope"></i> karen.charcopa@ejemplo.com
                                    <br />
                                    <i class="glyphicon glyphicon-education"></i> Ingeniera de Software
                                </p>
                            </div>
                        </div>
                    </div>

                    <!-- Colaborador 1 - Fundación Universitaria de Popayán -->
                    <div class="col-sm-6 col-md-3">
                        <div class="thumbnail">
                            <img src="Recursos/Images/SedeFup.webp" alt="Fundación Universitaria de Popayán" class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Fundación Universitaria de Popayán</p> 
                                    <small><cite title="Source Title">Popayán, Colombia <i class="glyphicon glyphicon-map-marker"></i></cite></small>
                                </blockquote>
                                <p> 
                                    <i class="glyphicon glyphicon-envelope"></i> contacto@fup.edu.co
                                    <br />
                                    <i class="glyphicon glyphicon-globe"></i> www.fup.edu.co
                                </p>
                            </div>
                        </div>
                    </div>
                    
                    <!-- Colaborador 2 - Cabildo Indígena de Guambía -->
                    <div class="col-sm-6 col-md-3">
                        <div class="thumbnail">
                            <img src="Recursos/Images/Cabildo.webp" alt="Cabildo Indígena de Guambía" class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Cabildo Indígena de Guambía</p> 
                                    <small><cite title="Source Title">Silvia, Cauca, Colombia <i class="glyphicon glyphicon-map-marker"></i></cite></small>
                                </blockquote>
                                <p> 
                                    <i class="glyphicon glyphicon-envelope"></i> cabildo@guambia.org.co
                                    <br />
                                    <i class="glyphicon glyphicon-globe"></i> www.guambia.org.co
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        
        <div class="services-section">
            <h2>Servicios</h2>
            <ul>
                <li>Acceso a libros, artículos, videos y otros materiales educativos.</li>
                <li>Recursos multimedia sobre la cultura y tradiciones Misak.</li>
                <li>Herramientas de accesibilidad para usuarios con discapacidades.</li>
                <li>Servico de ventas.</li>
            </ul>
        </div>
        
        <div class="contact-section">
            <h2>Contacto</h2>
            <p>Para más información, puedes contactarnos a través de nuestro correo electrónico: <a href="mailto:contacto@bibliotecamisak.org" class="contact-link">contacto@bibliotecamisak.org</a> o visitar nuestras oficinas en el resguardo indígena de Guambía.</p>
        </div>
    </div>
</asp:Content>