<%@ Page Title="Acerca de" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="true" CodeBehind="WFAcercaDe.aspx.cs"
    Inherits="Presentation.WFAcercaDe" %>

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

        /* Tarjetas de colaboradores */
        .thumbnail {
            border-radius: 8px;
            transition: transform 0.3s ease;
            border: 1px solid #e0e0e0;
            height: 100%;
            display: flex;
            flex-direction: column;
        }

        .thumbnail:hover {
            transform: translateY(-5px);
            box-shadow: 0 10px 20px rgba(0, 0, 0, 0.1);
        }

        .thumbnail img {
            border-radius: 8px 8px 0 0;
            width: 100%;
            height: auto;          /* NO se corta la imagen */
            max-height: 250px;     /* Opcional: limita altura */
            object-fit: contain;   /* Muestra la imagen completa */
        }

        .caption {
            padding: 15px;
            flex-grow: 1;
            display: flex;
            flex-direction: column;
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

        .email-container {
            word-break: break-all;
            overflow-wrap: break-word;
            hyphens: auto;
            font-size: 0.9rem;
            line-height: 1.4;
            margin-top: auto;
        }

        .education-info {
            margin-top: 8px;
            font-size: 0.9rem;
        }

        /* Responsivo */
        @media (max-width: 768px) {
            .about-container { padding: 20px 15px; }
            .about-container h1 { font-size: 2rem; }
            .about-container h2 { font-size: 1.5rem; }
            .about-container p,
            .about-container li { font-size: 1rem; }
            .col-sm-6 { margin-bottom: 20px; }
            .email-container,
            .education-info { font-size: 0.85rem; }
        }

        /* Enlace de contacto */
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

        <!-- Misión -->
        <section class="mission-section">
            <h2>Misión</h2>
            <p>
                La Biblioteca Virtual Misak tiene como misión preservar y difundir la riqueza cultural
                y educativa de la comunidad Misak, proporcionando acceso a materiales educativos y
                culturales de manera accesible y sostenible.
            </p>
        </section>

        <!-- Visión -->
        <section class="vision-section">
            <h2>Visión</h2>
            <p>
                Ser un referente en la preservación y promoción de la cultura indígena, facilitando
                el acceso a la información y el conocimiento para las futuras generaciones de la
                comunidad Misak.
            </p>
        </section>

        <!-- Objetivos -->
        <section class="objectives-section">
            <h2>Objetivos</h2>
            <ul>
                <li>Fortalecer el sistema educativo indígena propio.</li>
                <li>Preservar y difundir los conocimientos ancestrales y la cultura Misak.</li>
                <li>Proporcionar acceso a materiales educativos y culturales de alta calidad.</li>
                <li>Fomentar la inclusión y accesibilidad para todos los miembros de la comunidad.</li>
            </ul>
        </section>

        <!-- Historia -->
        <section class="history-section">
            <h2>Historia</h2>
            <p>
                La Biblioteca Virtual Misak fue creada en 2024 como una iniciativa para preservar y
                promover la cultura y los conocimientos ancestrales de la comunidad Misak. Este
                proyecto nació de la necesidad de salvaguardar los materiales educativos propios y
                fortalecer el sistema educativo indígena.
            </p>
        </section>

        <!-- Equipo -->
        <section class="team-section">
            <h2>Equipo</h2>
            <p>
                La biblioteca es gestionada por un equipo de profesionales comprometidos con la
                educación y la preservación cultural, incluyendo ingenieros de sistemas, educadores y
                miembros de la comunidad Misak.
            </p>
        </section>

        <!-- Colaboradores -->
        <section class="collaborators-section">
            <h2>Colaboradores</h2>
            <div class="container">
                <div class="row">

                    <!-- Colaborador 1 -->
                    <div class="col-sm-6 col-md-4">
                        <div class="thumbnail">
                            <img src="Recursos/Images/colaboradores/Alvaro.webp"
                                 alt="Alvaro Javier Morales Tombé"
                                 class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Alvaro Javier Morales Tombé</p>
                                    <small><cite>Desarrollador <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <div class="email-container">
                                    <i class="glyphicon glyphicon-envelope"></i> moralestombe19@gmail.com
                                </div>
                                <div class="education-info">
                                    <i class="glyphicon glyphicon-education"></i> Ingeniero de Sistemas
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Colaborador 2 -->
                    <div class="col-sm-6 col-md-4">
                        <div class="thumbnail">
                            <img src="Recursos/Images/colaboradores/Karen.webp"
                                 alt="Karen Daniela Quiñones Charcopa"
                                 class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Karen Daniela Quiñones Charcopa</p>
                                    <small><cite>Desarrolladora <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <div class="email-container">
                                    <i class="glyphicon glyphicon-envelope"></i> charcopadaniela178@gmail.com
                                </div>
                                <div class="education-info">
                                    <i class="glyphicon glyphicon-education"></i> Ingeniera de Sistemas
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Colaborador 3 -->
                    <div class="col-sm-6 col-md-4">
                        <div class="thumbnail">
                            <img src="Recursos/Images/Cabildo.webp"
                                 alt="Cabildo Indígena de Guambía"
                                 class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Cabildo Indígena del Resguardo de Guambía</p>
                                    <small><cite>Silvia, Cauca, Colombia <i class="glyphicon glyphicon-map-marker"></i></cite></small>
                                    <small><cite>Aliado<i class="glyphicon glyphicon-map-marker"></i></cite></small>
                                </blockquote>
                                <div class="email-container">
                                    <i class="glyphicon glyphicon-envelope"></i> eduguambia@yahoo.com
                                </div>
                                <div class="education-info">
                                    <i class="glyphicon glyphicon-globe"></i> www.misak-colombia.org
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Colaborador 4 -->
                    <div class="col-sm-6 col-md-4">
                        <div class="thumbnail">
                            <img src="Recursos/Images/colaboradores/Luis.webp"
                                 alt="Mg. Luis Alfonso Vejarano Sanchez"
                                 class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Mg. Luis Alfonso Vejarano Sanchez</p>
                                    <small><cite>Director <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <div class="email-container">
                                    <i class="glyphicon glyphicon-envelope"></i> luis.vejarano@docente.fup.edu.co
                                </div>
                                <div class="education-info">
                                    <i class="glyphicon glyphicon-education"></i> Maestría en En gestión de Tecnologías de la información
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Colaborador 5 -->
                    <div class="col-sm-6 col-md-4">
                        <div class="thumbnail">
                            <img src="Recursos/Images/colaboradores/Cristian.webp"
                                 alt="Dr. Cristian Mendez Rodriguez"
                                 class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Dr. Cristian Mendez Rodriguez</p>
                                    <small><cite>Codirector <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <div class="email-container">
                                    <i class="glyphicon glyphicon-envelope"></i> cristian.mendez@docente.fup.edu.co
                                </div>
                                <div class="education-info">
                                    <i class="glyphicon glyphicon-education"></i> Doctorado en Ciencias Ambientales
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Colaborador 6 -->
                    <div class="col-sm-6 col-md-4">
                        <div class="thumbnail">
                            <img src="Recursos/Images/colaboradores/Daniela.webp"
                                 alt="Mg. Daniela Iboth Idrobo"
                                 class="img-rounded img-responsive" />
                            <div class="caption">
                                <blockquote>
                                    <p>Mg. Daniela Iboth Idrobo</p>
                                    <small><cite>Coordinadora de Pasantía <i class="glyphicon glyphicon-user"></i></cite></small>
                                </blockquote>
                                <div class="email-container">
                                    <i class="glyphicon glyphicon-envelope"></i> daniela.gutierrez@docente.fup.edu.co
                                </div>
                                <div class="education-info">
                                    <i class="glyphicon glyphicon-education"></i> Maestría en En gestión de Tecnologías de la información
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </section>

        <!-- Servicios -->
        <section class="services-section">
            <h2>Servicios</h2>
            <ul>
                <li>Acceso a libros, artículos, videos y otros materiales educativos.</li>
                <li>Recursos multimedia sobre la cultura y tradiciones Misak.</li>
                <li>Herramientas de accesibilidad para usuarios con discapacidades.</li>
                <li>Servicio de ventas.</li>
            </ul>
        </section>

        <!-- Contacto -->
        <section class="contact-section">
            <h2>Contacto</h2>
            <p>
                Para más información, puedes contactarnos a través de nuestro correo electrónico:
                <a href="mailto:contacto@bibliotecamisak.org" class="contact-link">
                    contacto@bibliotecamisak.org
                </a>
                o visitar nuestras oficinas en el resguardo indígena de Guambía.
            </p>
        </section>
    </div>
</asp:Content>