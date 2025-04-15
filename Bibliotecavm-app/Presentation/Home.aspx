<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Presentation.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.5.1.slim.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/bootstrap@4.6.0/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1 class="welcome-title">Bienvenido </h1>
    
    <!-- Carrusel Principal con imágenes locales -->
    <div id="mainCarousel" class="carousel slide carousel-fade" data-ride="carousel">
        <div class="carousel-inner">
            <!-- Slide 1 - Imagen local -->
            <div class="carousel-item active">
                <img src="Recursos/Materiales/Cartilla1.webp" class="d-block w-100" alt="Cultura Misak" style="height:500px;object-fit:cover;">
                <div class="mask">
                    <div class="carousel-content">
                        <h4 class="animated fadeInLeft">Explora Nuestra Colección</h4>
                        <p class="animated slideInRight">Descubre materiales educativos que preservan el conocimiento ancestral del pueblo Misak.</p>
                        <a href="WFPresentationMaterial.aspx" class="animated fadeInUp">Ver Materiales</a>
                    </div>
                </div>
            </div>
            
            <!-- Slide 2 - Imagen local -->
            <div class="carousel-item">
                <img src="Recursos/Materiales/Cartilla2.webp" class="d-block w-100" alt="Aprendizaje" style="height:500px;object-fit:cover;">
                <div class="mask">
                    <div class="carousel-content">
                        <h4 class="animated fadeInLeft">Conoce Nuestra Cultura</h4>
                        <p class="animated slideInRight">Sumérgete en la riqueza cultural a través de nuestros recursos educativos.</p>
                        <a href="WFPresentationMaterial.aspx" class="animated fadeInUp">Explorar Ahora</a>
                    </div>
                </div>
            </div>
            
            <!-- Slide 3 - Imagen local -->
            <div class="carousel-item">
                <img src="Recursos/Materiales/Cartilla3.webp" class="d-block w-100" alt="Comunidad" style="height:500px;object-fit:cover;">
                <div class="mask">
                    <div class="carousel-content">
                        <h4 class="animated fadeInLeft">Únete a la Comunidad</h4>
                        <p class="animated slideInRight">Regístrate para acceder a todos nuestros recursos y actividades culturales.</p>
                        <a href="WFUserRegistration.aspx" class="animated fadeInUp">Registrarse</a>
                    </div>
                </div>
            </div>
        </div>
        
        <!-- Controles del carrusel -->
        <a class="carousel-control-prev" href="#mainCarousel" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only"><</span>
        </a>
        <a class="carousel-control-next" href="#mainCarousel" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">></span>
        </a>
    </div>
    
    <!-- Slider de materiales -->
    <div class="slider-container">
        <h3 class="slider-title">Nuestros Materiales Destacados</h3>
        <div class="slider-track">
             <!-- Primera serie de imágenes locales -->
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla1.webp" alt="Cartilla educativa 1">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla2.webp" alt="Cartilla educativa 2">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla3.webp" alt="Cartilla educativa 3">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla4.webp" alt="Cartilla educativa 4">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla5.webp" alt="Cartilla educativa 5">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla6.webp" alt="Cartilla educativa 6">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla7.webp" alt="Cartilla educativa 7">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla8.webp" alt="Cartilla educativa 8">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        
        <!-- Segunda serie (para efecto continuo) -->
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla1.webp" alt="Cartilla educativa 1">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla2.webp" alt="Cartilla educativa 2">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla3.webp" alt="Cartilla educativa 3">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla4.webp" alt="Cartilla educativa 4">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla5.webp" alt="Cartilla educativa 5">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla6.webp" alt="Cartilla educativa 6">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla7.webp" alt="Cartilla educativa 7">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
        <div class="slider-item">
            <div class="image-container">
                <img src="Recursos/Materiales/Cartilla8.webp" alt="Cartilla educativa 8">
                <a href="WFPresentationMaterial.aspx" class="view-button">Ver</a>
            </div>
        </div>
    </div>
</div>
    <style type="text/css">
    /* Estilos generales */
    .welcome-title {
        text-align: center;
        color: #1a237e; /* Azul oscuro */
        margin: 30px 0;
        font-family: 'Montserrat', sans-serif;
        font-weight: 600;
        font-size: 2.5rem;
    }
    
    /* Carrusel principal */
    #mainCarousel {
        position: relative;
        z-index: 1;
        margin: 40px 0;
        background-color: #f5f5f5;
        border-radius: 8px;
        overflow: hidden;
        box-shadow: 0 4px 12px rgba(0,0,0,0.1);
    }
    
    #mainCarousel .carousel-item {
        height: 500px;
        min-height: 500px;
        align-items: center;
        justify-content: center;
    }
    
    #mainCarousel .carousel-item .mask {
        position: absolute;
        top: 0;
        left: 0;
        height: 100%;
        width: 100%;
        display: flex;
        align-items: center;
        background-color: rgba(26, 35, 126, 0.7); /* Azul oscuro con transparencia */
    }
    
    .carousel-content {
        max-width: 1200px;
        padding: 0 15px;
        margin: 0 auto;
    }
    
    #mainCarousel h4 {
        font-size: 2.8rem;
        margin-bottom: 20px;
        color: #FFF;
        font-weight: 600;
        text-shadow: 2px 2px 4px rgba(0,0,0,0.3);
    }
    
    #mainCarousel p {
        font-size: 1.2rem;
        margin-bottom: 30px;
        color: #e0e0e0;
        max-width: 600px;
        line-height: 1.6;
    }
    
    #mainCarousel .carousel-item a {
        background: #1a237e; /* Azul oscuro */
        font-size: 1rem;
        color: #FFF;
        padding: 12px 30px;
        display: inline-block;
        border-radius: 4px;
        font-weight: 600;
        transition: all 0.3s;
        border: 2px solid #1a237e;
    }
    
    #mainCarousel .carousel-item a:hover {
        background: transparent;
        color: #fff;
        text-decoration: none;
        transform: translateY(-3px);
        box-shadow: 0 4px 12px rgba(0,0,0,0.2);
    }
    
    /* Controles del carrusel */
    .carousel-control-prev,
    .carousel-control-next {
        width: 50px;
        height: 50px;
        background-color: #1a237e;
        border-radius: 50%;
        top: 50%;
        transform: translateY(-50%);
        opacity: 0.8;
    }
    
    .carousel-control-prev:hover,
    .carousel-control-next:hover {
        opacity: 1;
    }
    
    /* Animaciones */
    .fadeInLeft {
        animation-name: fadeInLeft;
    }
    
    .slideInRight {
        animation-name: slideInRight;
    }
    
    .fadeInUp {
        animation-name: fadeInUp;
    }
    
    .animated {
        animation-duration: 1s;
        animation-fill-mode: both;
    }
    
    /* Slider de materiales */
    .slider-container {
        width: 100%;
        overflow: hidden;
        position: relative;
        margin: 50px 0;
    }
    
    .slider-title {
        text-align: center;
        color: #1a237e;
        margin-bottom: 20px;
        font-size: 1.8rem;
        font-weight: 600;
    }
    
    .slider-track {
        display: flex;
        animation: scroll 70s linear infinite;
        width: calc(200px * 16);
    }
    
    .slider-item {
        width: 200px;
        height: 150px;
        flex-shrink: 0;
        padding: 0 10px;
        transition: transform 0.3s;
    }
    
    .slider-item:hover {
        transform: translateY(-5px);
    }
    
    .slider-item img {
        width: 100%;
        height: 100%;
        object-fit: cover;
        border-radius: 8px;
        box-shadow: 0 4px 8px rgba(0,0,0,0.2);
        border: 1px solid #ddd;
    }
    
    @keyframes scroll {
        0% { transform: translateX(0); }
        100% { transform: translateX(calc(-200px * 8)); }
    }
    
    /* Keyframes para animaciones */
    @keyframes fadeInLeft {
        from {
            opacity: 0;
            transform: translate3d(-100px, 0, 0);
        }
        to {
            opacity: 1;
            transform: translate3d(0, 0, 0);
        }
    }
    
    @keyframes fadeInUp {
        from {
            opacity: 0;
            transform: translate3d(0, 50px, 0);
        }
        to {
            opacity: 1;
            transform: translate3d(0, 0, 0);
        }
    }
    
    @keyframes slideInRight {
        from {
            opacity: 0;
            transform: translate3d(100px, 0, 0);
        }
        to {
            opacity: 1;
            transform: translate3d(0, 0, 0);
        }
    }
  .image-container {
        position: relative;
        width: 100%;
        height: 100%;
    }
    
    .view-button {
        position: absolute;
        bottom: 10px;
        left: 50%;
        transform: translateX(-50%);
        background: rgba(26, 35, 126, 0.8);
        color: white;
        padding: 5px 15px;
        border-radius: 4px;
        text-decoration: none;
        opacity: 0;
        transition: opacity 0.3s ease;
    }
    
    .slider-item:hover .view-button {
        opacity: 1;
    }
    
    .slider-item img {
        transition: transform 0.3s;
    }
    
    .slider-item:hover img {
        transform: scale(1.05);
    }
</style>     <script type="text/javascript">
                 $(document).ready(function () {
                     // Configuración del carrusel principal
                     $('#mainCarousel').carousel({
                         interval: 3000, // Cambia de slide cada 3 segundos
                         //pause: "hover" // Pausa cuando el mouse está sobre el carrusel
                     });
                 });
</script>
</asp:Content>