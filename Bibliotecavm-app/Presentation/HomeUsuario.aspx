<%@ Page Title="" Language="C#" MasterPageFile="~/MainUsuario.Master" AutoEventWireup="true" CodeBehind="HomeUsuario.aspx.cs" Inherits="Presentation.HomeUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        :root {
            --primary-color: #ffffff;
            --accent-color: #e67e22;
            --dark-bg: #121212;
            --dark-bg-gradient: linear-gradient(145deg, #0c0c0c, #1e1e1e);
            --overlay: rgba(0, 0, 0, 0.65);
            --transition-time: 0.6s;
            --min-contrast-ratio: 4.5; /* WCAG AA */
        }

        body {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: var(--dark-bg-gradient);
            color: var(--primary-color);
            line-height: 1.6;
        }

        .slider-container {
            position: relative;
            height: auto;
            min-height: 90vh;
            overflow: hidden;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0, 0, 0, 0.5);
            margin: 20px;
        }

        .slide {
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-size: cover;
            background-position: center;
            opacity: 0;
            pointer-events: none;
            transition: opacity var(--transition-time) ease-in-out;
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 2rem;
            background-attachment: fixed;
        }

        .slide::before {
            content: '';
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: linear-gradient(to right, rgba(0,0,0,0.75) 0%, rgba(0,0,0,0.4) 100%);
            z-index: 1;
        }

        .slide.active {
            opacity: 1;
            pointer-events: auto;
        }

        .slide-overlay {
            position: relative;
            z-index: 2;
            background-color: var(--overlay);
            padding: 2rem;
            border-radius: 15px;
            max-width: 100%;
            width: 100%;
            transition: transform var(--transition-time) ease-in-out;
            backdrop-filter: blur(5px);
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.3);
            border: 1px solid rgba(255, 255, 255, 0.1);
        }

        @media (min-width: 768px) {
            .slide-overlay {
                max-width: 50%;
            }
            
            .slider-container {
                height: 90vh;
            }
        }

        .slide.active .slide-overlay {
            transform: translateY(0);
        }

        .slide:not(.active) .slide-overlay {
            transform: translateY(20px);
        }

        .slide-title {
            font-size: clamp(2rem, 5vw, 3rem);
            font-weight: bold;
            transition: all var(--transition-time) ease-in-out;
            text-shadow: 2px 2px 4px rgba(0, 0, 0, 0.5);
            margin-bottom: 0.5rem;
            color: #fff;
        }

        .slide-subtitle {
            font-size: clamp(1.2rem, 3vw, 1.5rem);
            margin: 0.5rem 0;
            font-style: italic;
            transition: all var(--transition-time) ease-in-out;
            text-shadow: 1px 1px 3px rgba(0, 0, 0, 0.5);
            color: #f0f0f0;
        }

        .slide-description {
            margin-top: 1rem;
            font-size: clamp(0.9rem, 2vw, 1rem);
            line-height: 1.6;
            transition: all var(--transition-time) ease-in-out;
            color: #e0e0e0;
        }

        .ver-mas-btn {
            margin-top: 1.5rem;
            padding: 0.75rem 1.5rem;
            background-color: var(--accent-color);
            color: white;
            border: none;
            border-radius: 25px;
            font-weight: bold;
            cursor: pointer;
            transition: all var(--transition-time) ease-in-out;
            display: inline-block;
            text-decoration: none;
            box-shadow: 0 4px 15px rgba(230, 126, 34, 0.3);
            font-size: 1rem;
        }

        .ver-mas-btn:hover, .ver-mas-btn:focus {
            background-color: #d35400;
            transform: translateY(-2px);
            box-shadow: 0 6px 20px rgba(230, 126, 34, 0.4);
            outline: 2px solid white;
        }

        .thumbnail-slider {
            position: absolute;
            bottom: 1.5rem;
            right: 1.5rem;
            display: flex;
            align-items: center;
            gap: 0.5rem;
            z-index: 10;
            background-color: rgba(0, 0, 0, 0.6);
            padding: 0.5rem;
            border-radius: 15px;
            backdrop-filter: blur(5px);
            max-width: 90%;
        }

        @media (max-width: 768px) {
            .thumbnail-slider {
                left: 50%;
                transform: translateX(-50%);
                right: auto;
                bottom: 1rem;
            }
        }

        .thumbnail-wrapper {
            display: flex;
            overflow: hidden;
            width: 100%;
            max-width: 330px;
            gap: 0.5rem;
        }

        @media (max-width: 480px) {
            .thumbnail-wrapper {
                max-width: 220px;
            }
        }

        .thumbnail {
            width: 80px;
            height: 100px;
            border-radius: 12px;
            overflow: hidden;
            cursor: pointer;
            border: 2px solid transparent;
            transition: all 0.3s ease;
            flex-shrink: 0;
            display: none;
            opacity: 0.7;
            transform: scale(0.95);
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.3);
            background: none;
            padding: 0;
        }

        @media (min-width: 480px) {
            .thumbnail {
                width: 100px;
                height: 130px;
            }
        }

        .thumbnail.visible {
            display: block;
        }

        .thumbnail:hover, .thumbnail:focus {
            opacity: 1;
            transform: scale(1);
            border-color: var(--accent-color);
            outline: none;
        }

        .thumbnail.active-thumb {
            opacity: 1;
            transform: scale(1);
            border-color: var(--accent-color);
        }

        .thumbnail img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            transition: transform 0.3s ease;
        }

        .thumbnail:hover img, .thumbnail:focus img {
            transform: scale(1.05);
        }

        .thumb-btn {
            background-color: rgba(0, 0, 0, 0.6);
            color: white;
            border: none;
            font-size: 1.5rem;
            padding: 0.3rem 0.7rem;
            border-radius: 50%;
            cursor: pointer;
            transition: background 0.3s;
            z-index: 11;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.3);
            line-height: 1;
        }

        .thumb-btn:hover, .thumb-btn:focus {
            background-color: var(--accent-color);
            outline: 2px solid white;
        }

        /* Mejoras de accesibilidad */
        @media (prefers-reduced-motion) {
            .slide, .slide-overlay, .slide-title, 
            .slide-subtitle, .slide-description,
            .ver-mas-btn, .thumbnail {
                transition: none !important;
            }
            
            .slide {
                background-attachment: scroll !important;
            }
        }

        .sr-only {
            position: absolute;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0, 0, 0, 0);
            white-space: nowrap;
            border-width: 0;
        }

        /* Navegación por teclado */
        .ver-mas-btn:focus, 
        .thumb-btn:focus, 
        .thumbnail:focus {
            outline: 2px solid white;
            outline-offset: 2px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="slider-container" role="region" aria-label="Galería de biografías">
        <div class="slide active" style="background-image: url('Recursos/Biografias/M.MercedesTunubala.webp');" data-index="0" aria-hidden="false">
            <div class="slide-overlay">
                <h1 class="slide-title">Shura:Mercedes Tunubalá V.</h1>
                <p class="slide-subtitle">2023 - Magíster en Gerencia de Proyectos</p>
                <p class="slide-description">Desde su infancia, rodeada de la historia, el trabajo
                    comunitario y los valores transmitidos por sus padres Mama Julia y Shur Manuel, Mercedes ha encarnado
                    el respeto y el amor por su cultura, destacándose por su honestidad, responsabilidad y vocación de servicio.</p>
                <a href="WFMercedesTunubala.aspx" class="ver-mas-btn">Ver más <span class="sr-only">sobre Mercedes Tunubalá</span></a>
            </div>
        </div>

        <div class="slide" style="background-image: url('Recursos/Biografias/T.LuisFelipeC.webp');" data-index="1" aria-hidden="true">
            <div class="slide-overlay">
                <h1 class="slide-title">Shur: Luis Felipe Calambás</h1>
                <p class="slide-subtitle">licenciado en Ciencias Sociales</p>
                <p class="slide-description">Realizó sus estudios de básica primaria en la Escuela Las Delicias,
                    bajo la orientación de las Hermanas Lauritas. Continuó su formación secundaria en el Colegio
                    Agrícola de Silvia hasta noveno grado, y culminó los grados décimo y undécimo en la Institución Educativa INEM Francisco José de Caldas.</p>
                <a href="WFLuisFCalambas.aspx" class="ver-mas-btn">Ver más <span class="sr-only">sobre Luis Felipe Calambas</span></a>
            </div>
        </div>

        <div class="slide" style="background-image: url('Recursos/Biografias/T.AlvaroMorales.webp');" data-index="2" aria-hidden="true">
            <div class="slide-overlay">
                <h1 class="slide-title">SHUR ALVARO MORALES TOMBE Q.E.P.D.</h1>
                <p class="slide-subtitle">Primaria: Escuela las Delicias en el año 1961, 1962 y 1963</p>
                <p class="slide-description">El tata comenta que hasta el momento ha pasado por distintos cargos, de las cuales, día a día aprendió a liderar y apoyar en todos los
                    procesos comunitarios y por fuera del municipio.</p>
                <a href="WFAlvaroMorales.aspx" class="ver-mas-btn">Ver más <span class="sr-only">sobre Shur Alvaro Morales T.</span></a>
            </div>
        </div>

        <div class="slide" style="background-image: url('Recursos/Biografias/T. Avelino Dagua.webp');" data-index="3" aria-hidden="true">
            <div class="slide-overlay">
                <h1 class="slide-title">SHUR AVELINO DAGUA HURTADO</h1>
                <p class="slide-subtitle">27 de mayo de 1938 - 2015 </p>
                <p class="slide-description">Realizó investigaciones locales, nacionales e internacionales. Su legado más importante: la casa de taita Payan en Santiago, con pinturas y jeroglíficos.</p>
                <a href="WFAvelinoDagua.aspx" class="ver-mas-btn">Ver más <span class="sr-only">sobre Shur Avelino Dagua</span></a>
            </div>
        </div>

             <%--Miniaturas con flechas--%> 
    <div class="thumbnail-slider">
        <button class="thumb-btn prev-btn" aria-label="Diapositiva anterior">&lt;</button>
        <div class="thumbnail-wrapper" id="thumbnailWrapper">
            <button class="thumbnail visible active-thumb" data-index="0" aria-label="Mostrar diapositiva 1: Mercedes Tunubala">
                <img src="Recursos/Biografias/M.MercedesTunubala.webp" alt="Miniatura de Mercedes Tunubala Velasco" />
            </button>
            <button class="thumbnail visible" data-index="1" aria-label="Mostrar diapositiva 2: Felipe Calambás">
                <img src="Recursos/Biografias/T.LuisFelipeC.webp" alt="Miniatura de Luis Felipe Calambás" />
            </button>
            <button class="thumbnail visible" data-index="2" aria-label="Mostrar diapositiva 3:Alvaro Morales ">
                <img src="Recursos/Biografias/T.AlvaroMorales.webp" alt="Miniatura Alvaro Morales Tombé" />
            </button>
            <button class="thumbnail" data-index="3" aria-label="Mostrar diapositiva 4:Avelino Dagua">
                <img src="Recursos/Biografias/T. Avelino Dagua.webp" alt="Miniatura de Avelino Dagua" />
            </button>
        </div>
        <button class="thumb-btn next-btn" aria-label="Diapositiva siguiente">&gt;</button>
    </div>
</div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const slides = document.querySelectorAll('.slide');
            const thumbnails = document.querySelectorAll('.thumbnail');
            const prevBtn = document.querySelector('.prev-btn');
            const nextBtn = document.querySelector('.next-btn');
            let currentSlide = 0;
            let thumbStart = 0;
            const thumbsToShow = 3;
            const transitionTime = 600;
            let slideInterval;
            let userActive = true;
            let userActivityTimer;

            function mostrarSlide(index) {
                if (index === currentSlide || index < 0 || index >= slides.length) return;

                // Desactivar slide actual
                slides[currentSlide].classList.remove('active');
                slides[currentSlide].setAttribute('aria-hidden', 'true');
                thumbnails[currentSlide].classList.remove('active-thumb');
                thumbnails[currentSlide].setAttribute('aria-current', 'false');

                // Activar nuevo slide
                currentSlide = index;
                slides[currentSlide].classList.add('active');
                slides[currentSlide].setAttribute('aria-hidden', 'false');
                thumbnails[currentSlide].classList.add('active-thumb');
                thumbnails[currentSlide].setAttribute('aria-current', 'true');

                ensureThumbVisible(currentSlide);
                resetInterval();
            }

            function moverThumbnails(direccion) {
                const maxStart = thumbnails.length - thumbsToShow;
                thumbStart += direccion;

                if (thumbStart < 0) thumbStart = 0;
                if (thumbStart > maxStart) thumbStart = maxStart;

                thumbnails.forEach((thumb, i) => {
                    thumb.classList.remove('visible');
                    if (i >= thumbStart && i < thumbStart + thumbsToShow) {
                        thumb.classList.add('visible');
                    }
                });
            }

            function ensureThumbVisible(index) {
                if (index < thumbStart || index >= thumbStart + thumbsToShow) {
                    thumbStart = Math.max(0, Math.min(index - 1, thumbnails.length - thumbsToShow));
                    moverThumbnails(0);
                }
            }

            function resetInterval() {
                clearInterval(slideInterval);
                if (userActive && isInViewport(document.querySelector('.slider-container'))) {
                    slideInterval = setInterval(() => {
                        const nextSlide = (currentSlide + 1) % slides.length;
                        mostrarSlide(nextSlide);
                    }, 7000);
                }
            }

            function resetUserActivity() {
                userActive = true;
                clearTimeout(userActivityTimer);
                userActivityTimer = setTimeout(() => {
                    userActive = false;
                }, 30000);
                resetInterval();
            }

            function isInViewport(element) {
                const rect = element.getBoundingClientRect();
                return (
                    rect.top <= (window.innerHeight || document.documentElement.clientHeight) &&
                    rect.bottom >= 0
                );
            }

            function handleVisibility() {
                resetInterval();
            }

            // Event listeners
            prevBtn.addEventListener('click', function (e) {
                e.preventDefault();
                moverThumbnails(-1);
                resetUserActivity();
            });

            nextBtn.addEventListener('click', function (e) {
                e.preventDefault();
                moverThumbnails(1);
                resetUserActivity();
            });

            thumbnails.forEach(thumb => {
                thumb.addEventListener('click', function (e) {
                    e.preventDefault();
                    const index = parseInt(this.getAttribute('data-index'));
                    mostrarSlide(index);
                    resetUserActivity();
                });

                thumb.addEventListener('keydown', function (e) {
                    if (e.key === 'Enter' || e.key === ' ') {
                        e.preventDefault();
                        const index = parseInt(this.getAttribute('data-index'));
                        mostrarSlide(index);
                        resetUserActivity();
                    }
                });
            });

            document.addEventListener('keydown', function (e) {
                if (e.key === 'ArrowLeft') {
                    const prevSlide = (currentSlide - 1 + slides.length) % slides.length;
                    mostrarSlide(prevSlide);
                    resetUserActivity();
                } else if (e.key === 'ArrowRight') {
                    const nextSlide = (currentSlide + 1) % slides.length;
                    mostrarSlide(nextSlide);
                    resetUserActivity();
                }
            });

            ['mousemove', 'keydown', 'scroll', 'touchstart'].forEach(event => {
                document.addEventListener(event, resetUserActivity, { passive: true });
            });

            window.addEventListener('scroll', handleVisibility, { passive: true });

            // Inicialización
            moverThumbnails(0);
            resetUserActivity();
            handleVisibility();
        });
    </script>
</asp:Content>